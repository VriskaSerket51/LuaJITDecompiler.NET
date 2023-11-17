using System.Text;
using LuaJitDecompiler.Net.Utils;

namespace LuaJitDecompiler.Net.RawDump;

public class Flags
{
    public bool IsBigEndian { get; set; }
    public bool IsStripped { get; set; }
    public bool HasFfi { get; set; }
    public bool Fr2 { get; set; }
}

public class Header
{
    private static readonly byte[] Magic = { 0x1b, (byte)'L', (byte)'J' };
    private const int MAX_VERSION = 0x80;

    private const byte FLAG_IS_BIG_ENDIAN = 0b00000001;
    private const byte FLAG_IS_STRIPPED = 0b00000010;
    private const byte FLAG_HAS_FFI = 0b00000100;
    private const byte FLAG_FR2 = 0b00001000;

    public int Version { get; set; }
    public Flags Flags { get; set; } = new();
    public string Origin { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public static bool Read(Parser state, Header header)
    {
        bool r = true;

        header.Origin = state.Stream.Name;

        r = r && CheckMagic(state);
        r = r && ReadVersion(state, header);
        r = r && ReadFlags(state, header);
        r = r && ReadName(state, header);

        return r;
    }

    private static bool CheckMagic(Parser parser)
    {
        byte[] magic = parser.Stream.ReadBytes(3);
        if (!ByteArrayCompare(magic, Magic))
        {
            Log.ErrPrint("Invalid magic, not a LuaJIT format");
            return false;
        }

        return true;
    }

    private static bool ReadVersion(Parser parser, Header header)
    {
        header.Version = parser.Stream.ReadByte();

        if (header.Version > MAX_VERSION)
        {
            Log.ErrPrint("Version {0}: proprietary modifications", header.Version);
            return false;
        }

        return true;
    }

    private static bool ReadFlags(Parser parser, Header header)
    {
        int bits = (int)parser.Stream.ReadUleb128();

        header.Flags.IsBigEndian = (bits & FLAG_IS_BIG_ENDIAN) != 0;
        bits &= ~FLAG_IS_BIG_ENDIAN;

        header.Flags.IsStripped = (bits & FLAG_IS_STRIPPED) != 0;
        bits &= ~FLAG_IS_STRIPPED;

        header.Flags.HasFfi = (bits & FLAG_HAS_FFI) != 0;
        bits &= ~FLAG_HAS_FFI;

        header.Flags.Fr2 = (bits & FLAG_FR2) != 0;
        bits &= ~FLAG_FR2;

        parser.Flags.IsStripped = header.Flags.IsStripped;
        if (bits != 0)
        {
            Log.ErrPrint("Unknown flags set: {0:08b}", Convert.ToString(bits, 2).PadLeft(8, '0'));
            return false;
        }

        return true;
    }

    private static bool ReadName(Parser parser, Header header)
    {
        if (header.Flags.IsStripped)
        {
            header.Name = parser.Stream.Name;
        }
        else
        {
            int length = (int)parser.Stream.ReadUleb128();
            header.Name = Encoding.UTF8.GetString(parser.Stream.ReadBytes(length));
        }

        return true;
    }

    private static bool ByteArrayCompare(byte[] a1, byte[] a2)
    {
        if (a1.Length != a2.Length)
            return false;

        for (int i = 0; i < a1.Length; i++)
        {
            if (a1[i] != a2[i])
                return false;
        }

        return true;
    }
}