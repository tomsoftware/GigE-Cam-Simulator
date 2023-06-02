namespace GigE_Cam_Simulator.Streams
{
    using System.Net;
    using System.Net.Sockets;

    internal class StreamClient
    {
        private UdpClient imageSendClient = new UdpClient();
        private int imageSendClientBlockId = 0;


        public void Send(ImageData data, IPAddress ip, uint port, int packetSize)
        {
            this.imageSendClientBlockId++;

            var blockId = (uint)this.imageSendClientBlockId;

            var endpoint = new IPEndPoint(ip, (int)port);

            Console.WriteLine("--- send: Lead to " + endpoint);

            // send Lead
            var lead = new DataLeader_ImageData(blockId, PixelFormat.GVSP_PIX_MONO8, (uint)data.Width, (uint)data.Height);
            var leadPackage = lead.ToBuffer();
            this.imageSendClient.Send(leadPackage.Buffer, leadPackage.Buffer.Length, endpoint);

            // send payload
            var offset = 0;
            var packetId = 1;
            /*
             Image data formatted as specified in the Data Leader pixel_format field. For Data Payload
            packets, the IP Header + UDP Header + GVSP Header + data must be equal to the packet
            size specified in the Stream Channel Packet Size register of the GVSP transmitter. The
            only exception is the last Data Payload packet which may be smaller than the specified
            packet size. However, it is also possible to pad the last data packet so that all data payload
            packets are exactly the same size.
             */
            var chunkSize = (packetSize & 0xFFFFFFF) - 36;

            while (offset < data.Data.Length)
            {
                var payload = new DataPayload_ImageData(blockId, (uint)packetId);
                var payloadLength = Math.Min(data.Data.Length - offset, chunkSize);
                if (payloadLength < chunkSize)
                {
                    Console.WriteLine("?? " + payloadLength);
                }
                var payloadPackage = payload.ToBuffer(data.Data, offset, payloadLength);
                this.imageSendClient.Send(payloadPackage.Buffer, payloadPackage.Buffer.Length, endpoint);

                offset += payloadLength;
                packetId++;
            }

            Console.WriteLine("--- send: trailer");

            // send trailer
            var trailer = new DataTrailer_ImageData(blockId, (uint)packetId, (uint)data.Height);
            var trailerPackage = trailer.ToBuffer();
            this.imageSendClient.Send(trailerPackage.Buffer, trailerPackage.Buffer.Length, endpoint);
        }

    }
}
