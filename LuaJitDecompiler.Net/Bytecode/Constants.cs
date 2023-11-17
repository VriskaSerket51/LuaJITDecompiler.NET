namespace LuaJitDecompiler.Net.Bytecode;

public class Table
{
    public List<object> Array { get; set; } = new List<object>();
    public List<object> Dictionary { get; set; } = new List<object>();
}

public class Constants
{
    public const int T_NIL = 0;
    public const int T_FALSE = 1;
    public const int T_TRUE = 2;
    
    public List<uint> UpvalueReferences { get; set; } = new List<uint>();
    public List<object> NumericConstants { get; set; } = new List<object>();
    public List<object> ComplexConstants { get; set; } = new List<object>();
}