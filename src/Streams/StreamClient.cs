namespace GigE_Cam_Simulator.Streams
{
    using System.Net;
    using System.Net.Sockets;

    internal class StreamClient
    {
        private UdpClient imageSendClient = new UdpClient();
        private int imageSendClientBlockId = 0;


        public void Send(ImageData data, long ip, uint port, int packetSize)
        {
            this.imageSendClientBlockId++;

            var blockId = (uint)this.imageSendClientBlockId;

            var endpoint = new IPEndPoint(ip, (int)port);

            // send Lead
            var lead = new DataLeader_ImageData(blockId, PixelFormat.GVSP_PIX_MONO8, (uint)data.Width, (uint)data.Height);
            var leadPackage = lead.ToBuffer();
            this.imageSendClient.Send(leadPackage.Buffer, leadPackage.Buffer.Length, endpoint);

            var offset = 0;
            var packetId = 1;
            // send payload
            while (offset < data.Data.Length)
            {
                var payload = new DataPayload_ImageData(blockId, (uint)packetId);
                var payloadLength = Math.Min(data.Data.Length - offset, packetSize);
                var payloadPackage = payload.ToBuffer(data.Data, offset, payloadLength);
                this.imageSendClient.Send(payloadPackage.Buffer, payloadPackage.Buffer.Length, endpoint);

                offset += payloadLength;
                packetId++;
            }

            // send trailer
            var trailer = new DataTrailer_ImageData(blockId, (uint)packetId, (uint)data.Height);
            var trailerPackage = trailer.ToBuffer();
            this.imageSendClient.Send(trailerPackage.Buffer, trailerPackage.Buffer.Length, endpoint);
        }

    }
}
