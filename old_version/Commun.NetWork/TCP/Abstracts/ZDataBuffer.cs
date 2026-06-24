using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commun.NetWork.TCP.Abstracts
{
    public abstract class ZDataBuffer
    {
        protected byte[] _buffer;
        protected int _capacity;
        protected int _length;

        protected ZDataBuffer()
        {
        }

        private void EnsureCapacity(int count)
        {
            if (count > this._capacity)
            {
                if (count < (2 * this._capacity))
                {
                    count = 2 * this._capacity;
                }
                byte[] dst = new byte[count];
                this._capacity = count;
                Buffer.BlockCopy(this._buffer, 0, dst, 0, this._length);
                this._buffer = dst;
            }
        }

        protected void Remove(int length)
        {
            if (this._length <= length)
            {
                this._length = 0;
            }
            else
            {
                byte[] dst = new byte[this._buffer.Length];
                Buffer.BlockCopy(this._buffer, length, dst, 0, this._length - length);
                this._length -= length;
                this._buffer = dst;
            }
        }

        internal abstract ZMessage TryReadMessage();
        public void WriteBytes(byte[] source, int offset, int count)
        {
            if (this._buffer == null)
            {
                this._buffer = new byte[count];
                this._length = 0;
                this._capacity = count;
            }
            this.EnsureCapacity(this._length + count);
            Buffer.BlockCopy(source, offset, this._buffer, this._length, count);
            this._length += count;
        }

        public byte[] UnCompelete
        {
            get
            {
                byte[] dst = new byte[this._length];
                Buffer.BlockCopy(this._buffer, 0, dst, 0, this._length);
                return dst;
            }
        }
    }
}
