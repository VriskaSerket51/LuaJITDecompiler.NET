using System.Text;

namespace LuaJitDecompiler.Net.Utils;

public class BinStream
{
    private FileStream fd;
    private int Size { get; set; } = 0;
    public int Pos { get; set; } = 0;
    public string Name { get; set; }
    public bool IsLittleEndian { get; set; } = BitConverter.IsLittleEndian;

    public void Open(string filename)
    {
        Name = filename;
        fd = File.OpenRead(filename);
        Size = (int)new FileInfo(filename).Length;
    }

    public void Close()
    {
        fd.Close();
        Size = 0;
        Pos = 0;
    }

    public bool Eof()
    {
        return Pos >= Size;
    }

    public bool CheckDataAvailable(int dataSize = 1)
    {
        return Pos + dataSize <= Size;
    }

    public byte[] ReadBytes(int dataSize = 1)
    {
        if (!CheckDataAvailable(dataSize))
        {
            throw new IOException($"Unexpected EOF while trying to read {dataSize} bytes");
        }

        byte[] data = new byte[dataSize];
        fd.Read(data, 0, dataSize);
        Pos += dataSize;

        return data;
    }

    public int ReadByte()
    {
        if (!CheckDataAvailable(1))
        {
            throw new IOException("Unexpected EOF while trying to read 1 byte");
        }

        byte[] data = new byte[1];
        fd.Read(data, 0, 1);
        Pos += 1;

        return data[0];
    }

    public string ReadZString()
    {
        StringBuilder str = new StringBuilder();

        while (!Eof())
        {
            byte byteValue = ReadBytes()[0];

            if (byteValue == '\0')
            {
                return str.ToString();
            }
            else
            {
                str.Append((char)byteValue);
            }
        }

        return str.ToString();
    }

    public uint ReadUleb128()
    {
        uint value = (uint)ReadByte();

        if (value >= 0x80)
        {
            int bitshift = 0;
            value &= 0x7F;

            while (true)
            {
                byte byteValue = (byte)ReadByte();

                bitshift += 7;
                value |= ((uint)byteValue & 0x7F) << bitshift;

                if (byteValue < 0x80)
                {
                    break;
                }
            }
        }

        return value;
    }

    public string ReadUleb128Str(int length = 1)
    {
        StringBuilder str = new StringBuilder();
        int i = 0;

        while (i < length)
        {
            str.Append((char)ReadUleb128());
            i += 1;
        }

        return str.ToString();
    }

    public static string DecodeUleb128(byte[] buff, int buffLen)
    {
        StringBuilder str = new StringBuilder();
        int i = 0;

        while (buffLen > i)
        {
            uint value = buff[i];
            i = i + 1;

            Console.WriteLine(value);
            Console.WriteLine((char)value);
            Console.WriteLine(value >= 0x80);

            if (value >= 0x80 && buffLen > i)
            {
                int bitshift = 0;
                value &= 0x7F;

                Console.WriteLine(value);
                Console.WriteLine((char)value);

                while (buffLen > i)
                {
                    byte byteValue = buff[i];
                    i = i + 1;

                    bitshift += 7;
                    value |= ((uint)byteValue & 0x7F) << bitshift;

                    if (byteValue < 0x80)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine(value);
            Console.WriteLine((char)value);

            if (value == 0 || value == 128)
            {
                str.Append("\\" + value);
            }
            else
            {
                str.Append((char)value);
            }
        }

        return str.ToString();
    }

    public (bool, uint) ReadUleb128From33Bit()
    {
        byte firstByte = (byte)ReadByte();

        bool isNumberBit = (firstByte & 0x1) != 0;
        uint value = (uint)(firstByte >> 1);

        if (value >= 0x40)
        {
            int bitshift = -1;
            value &= 0x3F;

            while (true)
            {
                byte byteValue = (byte)ReadByte();

                bitshift += 7;
                value |= ((uint)byteValue & 0x7F) << bitshift;

                if (byteValue < 0x80)
                {
                    break;
                }
            }
        }

        return (isNumberBit, value);
    }

    public uint ReadUInt(int size = 4)
    {
        byte[] valueBytes = ReadBytes(size);
        if (!IsLittleEndian)
        {
            Array.Reverse(valueBytes);
        }

        if (size == 4)
        {
            return BitConverter.ToUInt32(valueBytes, 0);
        }

        if (this.Size == 2)
        {
            return BitConverter.ToUInt16(valueBytes, 0);
        }

        if (size == 1)
        {
            return valueBytes[0];
        }

        throw new NotSupportedException();
    }
}