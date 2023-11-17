using System.Text;
using LuaJitDecompiler.Net.Bytecode;

namespace LuaJitDecompiler.Net.RawDump;

public class DebugInformation
{
    private const int VARNAME_END = 0;
    private const int VARNAME_FOR_IDX = 1;
    private const int VARNAME_FOR_STOP = 2;
    private const int VARNAME_FOR_STEP = 3;
    private const int VARNAME_FOR_GEN = 4;
    private const int VARNAME_FOR_STATE = 5;
    private const int VARNAME_FOR_CTL = 6;
    private const int VARNAME__MAX = 7;

    private static readonly List<string> INTERNAL_VARNAMES = new List<string>
    {
        null,
        "<index>",
        "<limit>",
        "<step>",
        "<generator>",
        "<state>",
        "<control>"
    };

    public static bool Read(Prototype parser, int lineOffset, Bytecode.DebugInformation debugInfo)
    {
        bool success = true;

        success = success && ReadLineInfo(parser, lineOffset, debugInfo.AddrToLineMap);
        success = success && ReadUpvalueNames(parser, debugInfo.UpvalueVariableNames);
        success = success && ReadVariableInfos(parser, debugInfo.VariableInfo);

        return success;
    }

    private static bool ReadLineInfo(Prototype parser, int lineOffset, List<int> lineInfo)
    {
        int lineInfoSize;
        if (parser.LinesCount >= 65536)
            lineInfoSize = 4;
        else if (parser.LinesCount >= 256)
            lineInfoSize = 2;
        else
            lineInfoSize = 1;

        lineInfo.Add(0);

        while (lineInfo.Count < parser.InstructionsCount + 1)
        {
            int lineNumber = (int)parser.Stream.ReadUInt(lineInfoSize);
            lineInfo.Add(lineOffset + lineNumber);
        }

        return true;
    }

    private static bool ReadUpvalueNames(Prototype parser, List<string> names)
    {
        while (names.Count < parser.UpvaluesCount)
        {
            string decodedString = parser.Stream.ReadZString();
            names.Add(decodedString);
        }

        return true;
    }

    private static bool ReadVariableInfos(Prototype parser, List<VariableInfo> infos)
    {
        int lastAddr = 0;

        while (true)
        {
            VariableInfo info = new VariableInfo();

            byte internalVarType = (byte)parser.Stream.ReadByte();

            if (internalVarType >= VARNAME__MAX)
            {
                byte[] prefixBytes = new byte[] { internalVarType };
                byte[] suffixBytes = Encoding.UTF8.GetBytes(parser.Stream.ReadZString());
                byte[] fullBytes = new byte[prefixBytes.Length + suffixBytes.Length];
                Array.Copy(prefixBytes, 0, fullBytes, 0, prefixBytes.Length);
                Array.Copy(suffixBytes, 0, fullBytes, prefixBytes.Length, suffixBytes.Length);
                string variableName = System.Text.Encoding.UTF8.GetString(fullBytes);
                info.Name = variableName;
                info.Type = VariableInfo.T_VISIBLE;
            }
            else if (internalVarType == VARNAME_END)
            {
                break;
            }
            else
            {
                int index = internalVarType;
                info.Name = INTERNAL_VARNAMES[index];
                info.Type = VariableInfo.T_INTERNAL;
            }

            int startAddr = lastAddr + (int)parser.Stream.ReadUleb128();
            int endAddr = startAddr + (int)parser.Stream.ReadUleb128();

            info.StartAddr = startAddr;
            info.EndAddr = endAddr;

            lastAddr = startAddr;

            infos.Add(info);
        }

        return true;
    }
}