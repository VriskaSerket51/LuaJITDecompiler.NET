using LuaJitDecompiler.Net.Bytecode;
using LuaJitDecompiler.Net.Utils;

namespace LuaJitDecompiler.Net.RawDump;

public class Parser
{
    public BinStream Stream { get; set; } = new BinStream();
    public Flags Flags { get; set; } = new Flags();
    public List<Bytecode.Prototype> Prototypes { get; set; } = new List<Bytecode.Prototype>();


    public static (Header, Bytecode.Prototype)? Parse(string filename, Action<Header> onParseHeader = null)
    {
        var parser = new Parser();
        parser.Stream.Open(filename);

        var header = new Header();
        bool success = true;

        try
        {
            success = success && ReadHeader(parser, header);

            if (success && onParseHeader != null)
            {
                onParseHeader(header);
            }

            success = success && ReadPrototypes(parser, parser.Prototypes);
        }
        catch (IOException e)
        {
            Log.ErrPrint("I/O error while reading dump: {0}", e.Message);
            success = false;
        }

        if (success && !parser.Stream.Eof())
        {
            Log.ErrPrint("Stopped before the whole file was read, something wrong");
            success = false;
        }

        parser.Stream.Close();

        return success ? (header, parser.Prototypes[0]) : null;
    }

    private static bool ReadHeader(Parser parser, Header header)
    {
        if (!Header.Read(parser, header))
        {
            Log.ErrPrint("Failed to read raw-dump header");
            return false;
        }

        parser.Stream.IsLittleEndian = !header.Flags.IsBigEndian;

        return true;
    }

    private static bool ReadPrototypes(Parser parser, List<Bytecode.Prototype> prototypes)
    {
        while (!parser.Stream.Eof())
        {
            var prototype = new Bytecode.Prototype();

            if (!Prototype.Read(parser, prototype))
            {
                if (parser.Stream.Eof())
                {
                    break;
                }
                else
                {
                    Log.ErrPrint("Failed to read prototype");
                    return false;
                }
            }

            prototypes.Add(prototype);
        }

        return true;
    }
}