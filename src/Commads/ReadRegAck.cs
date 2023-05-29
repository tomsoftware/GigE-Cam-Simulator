namespace GigE_Cam_Simulator.Commads
{

    /// <summary>
    /// https://aravisproject.github.io/docs/aravis-0.4/aravis-gvcp.html
    /// </summary>
    public class ReadRegAck : GvcpAck
    {
        BufferReader resultData;

        public ReadRegAck(uint req_id, RegisterMemory registers, BufferReader message) :
            base(req_id, GvcpPacketType.GVCP_PACKET_TYPE_ACK, ArvGvcpCommand.GVCP_COMMAND_READ_REGISTER_ACK)
        {
            var resultData = new BufferReader(message.Length);
            while (!message.Eof)
            {
                var address = (int)message.ReadIntBE();
                var register = RegisterTypeHelper.RegisterByAddress(address);
                Console.WriteLine("    read: " + address.ToString("x") + " --> " + register.TypeName + " = " + registers.ReadIntBE(address));

                var data = registers.ReadBytes(address, 4);
                resultData.WriteBytes(data, 4);
            }

            this.resultData = resultData;
        }

        public BufferReader ToBuffer()
        {
            var b = CreateBuffer(resultData.Length);

            b.WriteBytes(resultData.Buffer, resultData.Length);

            return b;

        }
    }
}
