namespace LuaJitDecompiler.Net.Bytecode;

public static class Helpers
{
    public static int GetJumpDestination(int addr, Instruction instruction)
    {
        return addr + instruction.CD + 1;
    }

    public static void SetJumpDestination(int addr, Instruction instruction, int value)
    {
        instruction.CD = value - addr - 1;
    }
}