namespace GigE_Cam_Simulator
{

    /// <summary>
    /// https://www.visiononline.org/userAssets/aiaUploads/File/GigE_Vision_Specification_2-0-03.pdf
    /// 16.4.2 WRITEREG_ACK
    /// </summary>
    public class WriteRegAck : GvcpAck
    {
        int index;

        public WriteRegAck(uint req_id, RegisterMemory registers, BufferReader message) :
            base(req_id, ArvGvcpPacketType.ARV_GVCP_PACKET_TYPE_ACK, ArvGvcpCommand.ARV_GVCP_COMMAND_WRITE_REGISTER_ACK)
        {
            this.index = 0;

            while (!message.Eof)
            {
                var address = (int)message.ReadIntBE();
                var data = message.ReadBytes(4);
                var register = registers.FindRegisterTypeByAddress(address);

                Console.WriteLine("    write: " + address.ToString("x") + " --> " + register + " = " + data);

                registers.WriteBytes(address, data);

                registers.TriggerWriteCallback(register);

                this.index++;
            }
        }

        public BufferReader ToBuffer()
        {
            var b = base.CreateBuffer(4);

            b.WriteWordBE(0); // reserved
            b.WriteWordBE(this.index);

            return b;

        }
    }
}
