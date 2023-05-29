namespace GigE_Cam_Simulator.Commads
{
    /// <summary>
    /// https://www.visiononline.org/userAssets/aiaUploads/File/GigE_Vision_Specification_2-0-03.pdf
    /// </summary>
    public class ReadMemAck : GvcpAck
    {
        byte[] resultData;
        uint address;

        private static string ByteToString(byte[] data)
        {
            return System.Text.Encoding.ASCII.GetString(data);
        }

        public ReadMemAck(uint req_id, RegisterMemory registers, BufferReader message) :
            base(req_id, GvcpPacketType.GVCP_PACKET_TYPE_ACK, ArvGvcpCommand.GVCP_COMMAND_READ_MEMORY_ACK)
        {
            var address = (int)message.ReadIntBE();
            var reserved = (int)message.ReadWordBE();
            var count = (int)message.ReadWordBE();

            resultData = registers.ReadBytes(address, count);
            this.address = (uint)address;
            var register = RegisterTypeHelper.RegisterByAddress(address);

            Console.WriteLine("    read: " + address.ToString("x") + " --> " + register.TypeName + " = " + ByteToString(resultData));
        }

        public BufferReader ToBuffer()
        {
            var b = CreateBuffer(4 + resultData.Length);

            b.WriteIntBE(address);
            b.WriteBytes(resultData, resultData.Length);

            return b;

        }
    }
}
