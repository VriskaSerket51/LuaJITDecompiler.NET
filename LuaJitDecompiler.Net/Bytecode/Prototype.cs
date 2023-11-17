namespace LuaJitDecompiler.Net.Bytecode;

public class Flags
{
    public bool HasSubPrototypes { get; set; }
    public bool IsVariadic { get; set; }
    public bool HasFfi { get; set; }
    public bool HasJit { get; set; } = true;
    public bool HasIloop { get; set; }
}

public class Prototype
{
    public Flags Flags { get; } = new Flags();

    public int ArgumentsCount { get; set; }

    public int Framesize { get; set; }

    public int FirstLineNumber { get; set; }
    public int LinesCount { get; set; }

    public List<Instruction> Instructions { get; } = new List<Instruction>();
    public Constants Constants { get; } = new Constants();
    public DebugInformation Debuginfo { get; } = new DebugInformation();
}