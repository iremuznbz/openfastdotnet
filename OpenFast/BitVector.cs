using System;

namespace OpenFAST
{
	public class BitVector
	{
		virtual public sbyte[] Bytes
		{
			get
			{
				return bytes;
			}
			
		}
		virtual public sbyte[] TruncatedBytes
		{
			get
			{
				int index = bytes.Length - 1;
				
				for (; (index > 0) && ((bytes[index] & VALUE_BITS_SET) == 0); index--)
					;
				
				if (index == (bytes.Length - 1))
				{
					return bytes;
				}
				
				sbyte[] truncated = new sbyte[index + 1];
				Array.Copy(bytes, 0, truncated, 0, index + 1);
                byte tempStop = STOP_BIT;
                truncated[truncated.Length - 1] |= (sbyte)(tempStop);
				
				return truncated;
			}
			
		}
		virtual public int Size
		{
			get
			{
				return this.size;
			}
			
		}
		virtual public bool Overlong
		{
			get
			{
				return (bytes.Length > 1) && ((bytes[bytes.Length - 1] & VALUE_BITS_SET) == 0);
			}
			
		}
		private const int VALUE_BITS_SET = 0x7F;
		private const int STOP_BIT = 0x80;
		private sbyte[] bytes;
		private int size;
		
		public BitVector(int size):this(new sbyte[((size - 1) / 7) + 1])
		{
		}
		
		public BitVector(sbyte[] bytes)
		{
			this.bytes = bytes;
			this.size = bytes.Length * 7;
            byte tempStop = STOP_BIT;

            bytes[bytes.Length - 1] |= (sbyte)(tempStop);
		}
		
		public virtual void  set_Renamed(int fieldIndex)
		{
			bytes[fieldIndex / 7] |= (sbyte) ((1 << (6 - (fieldIndex % 7))));
		}
		
		public virtual bool IsSet(int fieldIndex)
		{
			if (fieldIndex >= bytes.Length * 7)
				return false;
			return ((bytes[fieldIndex / 7] & (1 << (6 - (fieldIndex % 7)))) > 0);
		}
		
		public  override bool Equals(System.Object obj)
		{
			if ((obj == null) || !(obj is BitVector))
			{
				return false;
			}
			
			return Equals((BitVector) obj);
		}
		
		public bool Equals(BitVector other)
		{
			if (other.size != this.size)
			{
				return false;
			}
			
			for (int i = 0; i < this.bytes.Length; i++)
			{
				if (this.bytes[i] != other.bytes[i])
				{
					return false;
				}
			}
			
			return true;
		}
		
		public override int GetHashCode()
		{
			return bytes.GetHashCode();
		}
		
		public override string ToString()
		{
			return "BitVector [" + ByteUtil.ConvertByteArrayToBitString(bytes) + "]";
		}
		
		public virtual int IndexOfLastSet()
		{
			int index = bytes.Length * 7 - 1;
			while (index >= 0 && !IsSet(index))
				index--;
			return index;
		}
	}
}