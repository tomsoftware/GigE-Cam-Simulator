namespace GigE_Cam_Simulator
{
    using System.Collections;
    using System.Text;

    public class BufferReader
    {
        byte[] buffer;
        int bufferPos;

        public byte[] Buffer => this.buffer;

        public bool Eof => (bufferPos < 0 || bufferPos >= this.buffer.Length);

        public int Length => this.buffer.Length;

        public BufferReader(int size)
        {
            this.buffer = new byte[size];
            this.bufferPos = 0;
        }
        public BufferReader(byte[] buffer)
        {
            this.buffer = buffer;
        }

        public void WriteWordBE(int value)
        {
            this.buffer[this.bufferPos] = (byte)((value >> 8) & 0xFF);
            this.bufferPos++;
            this.buffer[this.bufferPos] = (byte)(value & 0xFF); 
            this.bufferPos++;
        }

        public uint ReadWordBE()
        {
            uint b1 = this.buffer[this.bufferPos];
            this.bufferPos++;
            uint b2 = this.buffer[this.bufferPos];
            this.bufferPos++;

            return (b1 << 8 | b2);
        }

        public void SetIntBE(int offset, int value)
        {
            this.buffer[offset] = (byte)((value >> 24) & 0xFF);
            offset++;
            this.buffer[offset] = (byte)((value >> 16) & 0xFF);
            offset++;
            this.buffer[offset] = (byte)((value >> 8) & 0xFF);
            offset++;
            this.buffer[offset] = (byte)((value >> 0) & 0xFF);
        }
        public void WriteIntBE(uint value)
        {
            this.buffer[this.bufferPos] = (byte)((value >> 24) & 0xFF);
            this.bufferPos++;
            this.buffer[this.bufferPos] = (byte)((value >> 16) & 0xFF);
            this.bufferPos++;
            this.buffer[this.bufferPos] = (byte)((value >> 8) & 0xFF);
            this.bufferPos++;
            this.buffer[this.bufferPos] = (byte)((value >> 0) & 0xFF);
            this.bufferPos++;
        }
        public uint ReadIntBE()
        {
            uint b1 = this.buffer[this.bufferPos];
            this.bufferPos++;
            uint b2 = this.buffer[this.bufferPos];
            this.bufferPos++;
            uint b3 = this.buffer[this.bufferPos];
            this.bufferPos++;
            uint b4 = this.buffer[this.bufferPos];
            this.bufferPos++;

            return (b1 << 24 | b2 << 16 | b3 << 8 | b4);
        }

        public uint GetIntBE(int address)
        {
            uint b1 = this.buffer[address];
            address++;
            uint b2 = this.buffer[address];
            address++;
            uint b3 = this.buffer[address];
            address++;
            uint b4 = this.buffer[address];

            return (b1 << 24 | b2 << 16 | b3 << 8 | b4);
        }

        public void WriteBytes(byte[] data, int length)
        {
            this.SetBytes(this.bufferPos, data, length);
            this.bufferPos += length;
        }

        public byte[] ReadBytes(int length)
        {
            var b = this.GetBytes(this.bufferPos, length);
            this.bufferPos += length;
            return b;
        }


        public void SetBytes(int offset, byte[] data, int length)
        {
            if (data == null)
            {
                Array.Fill(this.buffer, (byte)0, offset, length);
                return;
            }

            var l = Math.Min(length, data.Length);
            Array.Copy(data, 0, this.buffer, offset, l);

            var left = length - l;
            if (left > 0)
            {
                Array.Fill(this.buffer, (byte)0, offset + l, left);
            }
        }

        public byte[] GetBytes(int address, int length)
        {
            var result = new byte[length];
            Array.Copy(this.buffer, address, result, 0, length);

            return result;
        }

        public void WriteString(string value, int length)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            WriteBytes(bytes, length);
            WriteNull(Math.Max(0, length - bytes.Length));
        }

        public string GetString(int offset, int length)
        {
            return System.Text.Encoding.UTF8.GetString(this.buffer, offset, length);
        }

        public byte GetByte(int offset)
        {
            return this.buffer[offset];
        }

        public byte ReadByte()
        {
            var b = this.buffer[this.bufferPos];
            this.bufferPos++;
            return b;
        }


        public void SetByte(int offset, byte value)
        {
            this.buffer[offset] = value;
        }

        public void WriteNull(int length)
        {
            this.SetNull(this.bufferPos, length);
            this.bufferPos += length;
        }

        public void SetNull(int offset, int length)
        {
            for (int i = 0; i < length; i++)
            {
                this.buffer[offset] = 0;
                offset++;
            }
        }

        public void SetBit(int offset, int index, bool value)
        {
            int byteIndex = index >> 3;
            int bitIndex = 7 - (index & 0x7);

            int result;
            if (value)
            {
                result = this.buffer[offset + byteIndex] | (1 << bitIndex);
            }
            else
            {
                result = this.buffer[offset + byteIndex] & (~(1 << bitIndex));
            }

            this.buffer[offset + byteIndex] = (byte)result;
        }

        public void GetBit(int offset, int index, bool value)
        {
            int byteIndex = index >> 3;
            int bitIndex = index & 0x7;

            int result;
            if (value)
            {
                result = this.buffer[offset + byteIndex] | (1 << bitIndex);
            }
            else
            {
                result = this.buffer[offset + byteIndex] & (~(1 << bitIndex));
            }

            this.buffer[offset + byteIndex] = (byte)result;
        }


    }
}
