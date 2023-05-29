namespace GigE_Cam_Simulator
{
    using System;
    using System.Text;

    public class RegisterMemory
    {
        private BufferReader data;
        Dictionary<int, Action<RegisterMemory>> writeRegisterHock = new Dictionary<int, Action<RegisterMemory>>();

        public byte ReadByte(RegisterTypes register, int index)
        {
            var address = RegisterTypeHelper.RegisterByType(register).Address + index;
            return data.GetByte(address);
        }

        public void ReadByte(RegisterTypes register, int index, byte value)
        {
            var address = RegisterTypeHelper.RegisterByType(register).Address + index;
            data.SetByte(address, value);
            this.TriggerWriteHock(address);
        }

        public void WriteBytes(int address, byte[] values)
        {
            data.SetBytes(address, values, values.Length);
            this.TriggerWriteHock(address);
        }

        public void WriteIntBE(int address, int value)
        { 
            data.SetIntBE(address, value);
            this.TriggerWriteHock(address);
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
        public void WriteIntBE(RegisterTypes register, int value)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            this.WriteIntBE(reg.Address, value);
        }

        public byte[] ReadBytes(int address, int lenght)
        {
            return this.data.GetBytes(address, lenght);
        }

        public byte[] ReadBytes(RegisterTypes register)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            return this.data.GetBytes(reg.Address, reg.Length);
        }


        public void WriteBytes(RegisterTypes register, byte[] values)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            var address = reg.Address;

            var l = Math.Min(values.Length, reg.Length);

            // fill in data
            this.data.SetBytes(address, values, l);
            
            // clear buffer
            this.data.SetNull(address + l, l - reg.Length);

            this.TriggerWriteHock(address);
        }

        public void WriteBit(int address, int index, bool value)
        {
            this.data.SetBit(address, index, value);

            this.TriggerWriteHock(address);
        }

        public void WriteBit(RegisterTypes register, int index, bool value)
        {
            var address = RegisterTypeHelper.RegisterByType(register).Address;
            this.data.SetBit(address, index, value);

            this.TriggerWriteHock(address);
        }

        public void WriteString(RegisterTypes register, string value)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            var charData = ASCIIEncoding.ASCII.GetBytes(value);
            if (charData.Length >= reg.Length)
            {
                // force NULL termination
                charData[reg.Length - 1] = 0;
            }
            WriteBytes(register, charData);
        }

        public string ReadString(RegisterTypes register)
        {
            var reg = RegisterTypeHelper.RegisterByType(register);
            return this.data.GetString(reg.Address, reg.Length);
        }

        /// <summary>
        /// Register a callback that is triggered when data is written to a given address
        /// </summary>
        public void AddWriteRegisterHock(int address, Action<RegisterMemory> callback)
        {
            this.writeRegisterHock.Add(address, callback);
        }

        private void TriggerWriteHock(int address)
        {
            if (this.writeRegisterHock.TryGetValue(address, out var callback))
            {
                callback(this);
            }
        }

        public RegisterMemory(int size)
        {
            this.data = new BufferReader(size);
        }
    }
}
