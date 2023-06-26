using System;

namespace GigE_Cam_Simulator.Commads
{

    /// <summary>
    /// https://www.visiononline.org/userAssets/aiaUploads/File/GigE_Vision_Specification_2-0-03.pdf
    /// 16.4.2 WRITEREG_ACK
    /// </summary>
    public class WriteRegAck : GvcpAck
    {
        readonly uint index;

        public WriteRegAck(uint req_id, RegisterMemory registers, BufferReader message) :
            base(req_id, GvcpPacketType.GVCP_PACKET_TYPE_ACK, ArvGvcpCommand.GVCP_COMMAND_WRITE_REGISTER_ACK)
        {
            this.index = 0;

            while (!message.Eof)
            {
                var address = (int)message.ReadIntBE();
                var data = message.ReadBytes(4);
                var register = RegisterTypeHelper.RegisterByAddress(address);

                Console.WriteLine("    write: " + address.ToString("x") + " --> " + register.TypeName + " = " + string.Join(", ", data));

                registers.WriteBytes(address, data);

                this.index++;
            }
        }

        public BufferReader ToBuffer()
        {
            var b = CreateBuffer(4);

            b.WriteWordBE(0); // reserved
            b.WriteWordBE(this.index);

            return b;

        }
    }
}
