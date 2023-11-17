using LuaJitDecompiler.Net.Bytecode;
using LuaJitDecompiler.Net.Utils;

namespace LuaJitDecompiler.Net.RawDump;

public class Code
{
    private static readonly IDef[] _MAP = new IDef[256];

    public static void Init(List<(int, IDef)> opcodes)
    {
        opcodes.Sort((lhs, rhs) => lhs.Item1.CompareTo(rhs.Item1));
        foreach (var (opcode, instruction) in opcodes)
        {
            _MAP[opcode] = instruction;
            instruction.Opcode = opcode;
        }
    }

    public static Instruction Read(Prototype parser)
    {
        int codeword = (int)parser.Stream.ReadUInt(4);
        byte opcode = (byte)(codeword & 0xFF);

        IDef instructionClass = _MAP[opcode];

        if (instructionClass == null)
        {
            Log.ErrPrint("Warning: unknown opcode {0:X2}", opcode);
            instructionClass = Instructions.UNKNW; // Assuming Instructions.UNKNW is defined
        }

        Instruction instruction = instructionClass.Build();
        instruction.Bytecode = codeword;

        if (instructionClass.Opcode != opcode)
        {
            instruction.Opcode = opcode;
        }

        SetInstructionOperands(parser, codeword, instruction);

        return instruction;
    }

    private static void SetInstructionOperands(Prototype parser, int codeword, Instruction instruction)
    {
        int A = (codeword >> 8) & 0xFF;
        int CD = (codeword >> 16) & 0xFFFF;
        int B = (codeword >> 24) & 0xFF;

        if (instruction.AType != -1)
        {
            instruction.A = ProcessOperand(parser, instruction.AType, A);
        }

        if (instruction.BType != -1)
        {
            instruction.B = ProcessOperand(parser, instruction.BType, B);
        }

        if (instruction.CDType != -1)
        {
            instruction.CD = ProcessOperand(parser, instruction.CDType, CD);
        }
    }

    private static int ProcessOperand(Prototype parser, int operandType, int operand)
    {
        if (operandType == Instructions.T_STR ||
            operandType == Instructions.T_TAB ||
            operandType == Instructions.T_FUN ||
            operandType == Instructions.T_CDT)
        {
            return parser.ComplexConstantsCount - operand - 1;
        }
        else if (operandType == Instructions.T_JMP)
        {
            return operand - 0x8000;
        }
        else
        {
            return operand;
        }
    }
}