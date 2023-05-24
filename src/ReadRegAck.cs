using System;
using System.Security.Cryptography;

namespace GigE_Cam_Simulator
{

    /// <summary>
    /// https://aravisproject.github.io/docs/aravis-0.4/aravis-gvcp.html
    /// </summary>
    public class ReadRegAck : GvcpAck
    {
        BufferReader resultData;

        public ReadRegAck(uint req_id, RegisterMemory registers, BufferReader message) :
            base(req_id, ArvGvcpPacketType.ARV_GVCP_PACKET_TYPE_ACK, ArvGvcpCommand.ARV_GVCP_COMMAND_READ_REGISTER_ACK)
        {
            var resultData = new BufferReader(message.Length);
            while (!message.Eof)
            {
                var registerAddress = (int)message.ReadIntBE();
                Console.WriteLine("    read: " + registerAddress.ToString("x") + " --> " + registers.FindRegisterTypeByAddress(registerAddress) + " = " + registers.ReadIntBE(registerAddress));
                
                var data = registers.GetBytes(registerAddress, 4);
                resultData.WriteBytes(data, 4);
            }

            this.resultData = resultData;
        }

        public BufferReader ToBuffer()
        {
            var b = base.CreateBuffer(resultData.Length);

            b.WriteBytes(resultData.Buffer, resultData.Length);
            
            return b;

        }
    }
}
