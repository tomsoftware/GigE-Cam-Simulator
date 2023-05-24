namespace GigE_Cam_Simulator
{
    using System;
    using System.Text;


    public class RegisterMemory
    {
        private BufferReader data;

        public byte GetByte(RegisterTypes register, int index)
        {
            var address = RegisterTypeHelper.RegisterByType(register).Address + index;
            return data.GetByte(address);
        }

        public void SetByte(RegisterTypes register, int index, byte value)
        {
            var address = RegisterTypeHelper.RegisterByType(register).Address + index;
            data.SetByte(address, value);
        }

        public void WriteBytes(int address, byte[] values)
        {
            data.SetBytes(address, values, values.Length);
        }

        public void WriteIntBE(int address, int value)
        { 
            data.SetIntBE(address, value);
        }

        public uint ReadIntBE(int address)
        {
            return data.GetIntBE(address);
        }

        public uint ReadIntBE(RegisterTypes register)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            return this.ReadIntBE(reg.Address);
        }
        public void SetIntBE(RegisterTypes register, int value)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            this.WriteIntBE(reg.Address, value);
        }

        public byte[] GetBytes(int address, int lenght)
        {
            return this.data.GetBytes(address, lenght);
        }

        public byte[] GetBytes(RegisterTypes register)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            return this.data.GetBytes(reg.Address, reg.Length);
        }


        public void SetBytes(RegisterTypes register, byte[] values)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            var address = reg.Address;

            var l = Math.Min(values.Length, reg.Length);

            // fill in data
            this.data.SetBytes(address, values, l);
            
            // clear buffer
            this.data.SetNull(address + l, l - reg.Length);
           
            address += reg.Length;
        }

        public void WriteBit(int address, int index, bool value)
        {
            this.data.SetBit(address, index, value);
        }

        public void SetBit(RegisterTypes register, int index, bool value)
        {
            var address = RegisterTypeHelper.RegisterByType(register).Address;
            this.data.SetBit(address, index, value);
        }

        public void SetString(RegisterTypes register, string value)
        {
            SetBytes(register, ASCIIEncoding.ASCII.GetBytes(value));
        }

        public string GetString(RegisterTypes register)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            return this.data.GetString(reg.Address, reg.Length);
        }

        public RegisterMemory(int size)
        {
            this.data = new BufferReader(size);
        }
    }
}
