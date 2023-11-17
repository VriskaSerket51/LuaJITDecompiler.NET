using LuaJitDecompiler.Net.RawDump;

namespace LuaJitDecompiler.Net;

public static class Decompiler
{
    public static void Decompile(string fileName)
    {
        var result = Parser.Parse(fileName, (preHeader) =>
        {
            if (preHeader.Version == 1)
            {
                Code.Init(OpCodes.v20_OPCODES);
            }
            else if (preHeader.Version == 2)
            {
                Code.Init(OpCodes.v21_OPCODES);
            }
            else
            {
                throw new Exception($"Unsupported bytecode version: {preHeader.Version}");
            }
            
            // Init
        });

        if (!result.HasValue)
        {
            return;
        }

        var (header, prototype) = result.Value;
        Console.WriteLine(header.Origin);
    }
}