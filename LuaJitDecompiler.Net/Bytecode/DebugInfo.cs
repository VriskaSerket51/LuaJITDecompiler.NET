namespace LuaJitDecompiler.Net.Bytecode;

public class VariableInfo
{
    public const int T_VISIBLE = 0;
    public const int T_INTERNAL = 1;

    public int StartAddr { get; set; }
    public int EndAddr { get; set; }
    public int Type { get; set; } = -1;
    public string Name { get; set; } = string.Empty;
}

public class DebugInformation
{
    public List<int> AddrToLineMap { get; set; } = new List<int>();
    public List<string> UpvalueVariableNames { get; set; } = new List<string>();
    public List<VariableInfo> VariableInfo { get; set; } = new List<VariableInfo>();

    public int LookupLineNumber(int addr)
    {
        try
        {
            return AddrToLineMap[addr];
        }
        catch (IndexOutOfRangeException)
        {
            return 0;
        }
    }

    public VariableInfo LookupLocalName(int addr, int slot, bool altMode = false)
    {
        foreach (var info in VariableInfo)
        {
            if (info.StartAddr > addr)
                break;

            if (info.EndAddr <= addr)
            {
                if (altMode && info.EndAddr == addr)
                {
                    if (slot == 0)
                        return info;
                    else
                        slot -= 1;
                }

                continue;
            }
            else if (slot == 0)
            {
                return info;
            }
            else
            {
                slot -= 1;
            }
        }

        return null;
    }

    public string LookupUpvalueName(int slot)
    {
        try
        {
            return UpvalueVariableNames[slot];
        }
        catch (IndexOutOfRangeException)
        {
            return null;
        }
    }
}