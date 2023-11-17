using System.Diagnostics;
using LuaJitDecompiler.Net.Bytecode;
using LuaJitDecompiler.Net.Utils;

namespace LuaJitDecompiler.Net.RawDump;

public class Prototype
{
    private const byte FLAG_HAS_CHILD = 0b00000001;
    private const byte FLAG_IS_VARIADIC = 0b00000010;
    private const byte FLAG_HAS_FFI = 0b00000100;
    private const byte FLAG_JIT_DISABLED = 0b00001000;
    private const byte FLAG_HAS_ILOOP = 0b00010000;

    public BinStream Stream { get; set; }
    public Flags Flags { get; set; }
    public List<Bytecode.Prototype> Prototypes { get; set; }
    public int LinesCount { get; set; }
    public int UpvaluesCount { get; set; }
    public int ComplexConstantsCount { get; set; }
    public int NumericConstantsCount { get; set; }
    public int InstructionsCount { get; set; }
    public int DebugInfoSize { get; set; }

    public Prototype(Parser parser)
    {
        Stream = parser.Stream;
        Flags = parser.Flags;
        Prototypes = parser.Prototypes;
    }

    public static bool Read(Parser parser, Bytecode.Prototype prototype)
    {
        var wrapper = new Prototype(parser);

        int size = (int)wrapper.Stream.ReadUleb128();

        if (size == 0)
        {
            return false;
        }

        if (!wrapper.Stream.CheckDataAvailable(size))
        {
            Log.ErrPrint("File truncated");
            return false;
        }

        int start = wrapper.Stream.Pos;

        bool success = true;

        success = success && ReadFlags(wrapper, prototype);
        success = success && ReadCountsAndSizes(wrapper, prototype);
        success = success && ReadInstructions(wrapper, prototype);
        success = success && ReadConstants(wrapper, prototype);
        success = success && ReadDebugInfo(wrapper, prototype);

        int end = wrapper.Stream.Pos;

        if (success)
        {
            Debug.Assert(end - start == size,
                $"Incorrectly read: from {start} to {end} ({end - start}) instead of {size}");
        }

        return success;
    }

    private static bool ReadFlags(Prototype wrapper, Bytecode.Prototype prototype)
    {
        int bits = wrapper.Stream.ReadByte();

        prototype.Flags.HasFfi = (bits & FLAG_HAS_FFI) != 0;
        bits &= ~FLAG_HAS_FFI;

        prototype.Flags.HasIloop = (bits & FLAG_HAS_ILOOP) != 0;
        bits &= ~FLAG_HAS_ILOOP;

        prototype.Flags.HasJit = (bits & FLAG_JIT_DISABLED) == 0;
        bits &= ~FLAG_JIT_DISABLED;

        prototype.Flags.HasSubPrototypes = (bits & FLAG_HAS_CHILD) != 0;
        bits &= ~FLAG_HAS_CHILD;

        prototype.Flags.IsVariadic = (bits & FLAG_IS_VARIADIC) != 0;
        bits &= ~FLAG_IS_VARIADIC;

        if (bits != 0)
        {
            Log.ErrPrint("Unknown prototype flags: {0}", Convert.ToString(bits, 2).PadLeft(8, '0'));
            return false;
        }

        return true;
    }

    private static bool ReadCountsAndSizes(Prototype wrapper, Bytecode.Prototype prototype)
    {
        prototype.ArgumentsCount = (byte)wrapper.Stream.ReadByte();
        prototype.Framesize = (byte)wrapper.Stream.ReadByte();

        wrapper.UpvaluesCount = (byte)wrapper.Stream.ReadByte();
        wrapper.ComplexConstantsCount = (int)wrapper.Stream.ReadUleb128();
        wrapper.NumericConstantsCount = (int)wrapper.Stream.ReadUleb128();
        wrapper.InstructionsCount = (int)wrapper.Stream.ReadUleb128();

        if (wrapper.Flags.IsStripped)
        {
            wrapper.DebugInfoSize = 0;
        }
        else
        {
            wrapper.DebugInfoSize = (int)wrapper.Stream.ReadUleb128();
        }

        if (wrapper.DebugInfoSize == 0)
        {
            return true;
        }

        prototype.FirstLineNumber = (int)wrapper.Stream.ReadUleb128();
        prototype.LinesCount = (int)wrapper.Stream.ReadUleb128();

        wrapper.LinesCount = prototype.LinesCount;

        return true;
    }

    private static bool ReadInstructions(Prototype wrapper, Bytecode.Prototype prototype)
    {
        int i = 0;

        Instruction header;
        if (prototype.Flags.IsVariadic)
        {
            header = Instructions.FUNCV.Build();
        }
        else
        {
            header = Instructions.FUNCF.Build();
        }

        header.A = prototype.Framesize;
        prototype.Instructions.Add(header);

        while (i < wrapper.InstructionsCount)
        {
            Instruction instruction = Code.Read(wrapper);

            if (instruction == null)
            {
                return false;
            }

            prototype.Instructions.Add(instruction);

            i++;
        }

        return true;
    }

    private static bool ReadConstants(Prototype wrapper, Bytecode.Prototype prototype)
    {
        return Constants.Read(wrapper, prototype.Constants);
    }

    private static bool ReadDebugInfo(Prototype wrapper, Bytecode.Prototype prototype)
    {
        if (wrapper.DebugInfoSize == 0)
        {
            return true;
        }

        return DebugInformation.Read(wrapper, prototype.FirstLineNumber, prototype.Debuginfo);
    }
}