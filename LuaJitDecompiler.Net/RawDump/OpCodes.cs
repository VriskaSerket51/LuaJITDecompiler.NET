using LuaJitDecompiler.Net.Bytecode;

namespace LuaJitDecompiler.Net.RawDump;

public static class OpCodes
{
    public static readonly List<(int, IDef)> v20_OPCODES = new()
    {
        // Comparison ops
        (0x00, Instructions.ISLT), // @UndefinedVariable
        (0x01, Instructions.ISGE), // @UndefinedVariable
        (0x02, Instructions.ISLE), // @UndefinedVariable
        (0x03, Instructions.ISGT), // @UndefinedVariable

        (0x04, Instructions.ISEQV), // @UndefinedVariable
        (0x05, Instructions.ISNEV), // @UndefinedVariable

        (0x06, Instructions.ISEQS), // @UndefinedVariable
        (0x07, Instructions.ISNES), // @UndefinedVariable

        (0x08, Instructions.ISEQN), // @UndefinedVariable
        (0x09, Instructions.ISNEN), // @UndefinedVariable

        (0x0A, Instructions.ISEQP), // @UndefinedVariable
        (0x0B, Instructions.ISNEP), // @UndefinedVariable

        // Unary test and copy ops

        (0x0C, Instructions.ISTC), // @UndefinedVariable
        (0x0D, Instructions.ISFC), // @UndefinedVariable

        (0x0E, Instructions.IST), // @UndefinedVariable
        (0x0F, Instructions.ISF), // @UndefinedVariable

        // Unary ops

        (0x10, Instructions.MOV), // @UndefinedVariable
        (0x11, Instructions.NOT), // @UndefinedVariable
        (0x12, Instructions.UNM), // @UndefinedVariable
        (0x13, Instructions.LEN), // @UndefinedVariable

        // Binary ops

        (0x14, Instructions.ADDVN), // @UndefinedVariable
        (0x15, Instructions.SUBVN), // @UndefinedVariable
        (0x16, Instructions.MULVN), // @UndefinedVariable
        (0x17, Instructions.DIVVN), // @UndefinedVariable
        (0x18, Instructions.MODVN), // @UndefinedVariable

        (0x19, Instructions.ADDNV), // @UndefinedVariable
        (0x1A, Instructions.SUBNV), // @UndefinedVariable
        (0x1B, Instructions.MULNV), // @UndefinedVariable
        (0x1C, Instructions.DIVNV), // @UndefinedVariable
        (0x1D, Instructions.MODNV), // @UndefinedVariable

        (0x1E, Instructions.ADDVV), // @UndefinedVariable
        (0x1F, Instructions.SUBVV), // @UndefinedVariable
        (0x20, Instructions.MULVV), // @UndefinedVariable
        (0x21, Instructions.DIVVV), // @UndefinedVariable
        (0x22, Instructions.MODVV), // @UndefinedVariable

        (0x23, Instructions.POW), // @UndefinedVariable
        (0x24, Instructions.CAT), // @UndefinedVariable

        // Constant ops

        (0x25, Instructions.KSTR), // @UndefinedVariable
        (0x26, Instructions.KCDATA), // @UndefinedVariable
        (0x27, Instructions.KSHORT), // @UndefinedVariable
        (0x28, Instructions.KNUM), // @UndefinedVariable
        (0x29, Instructions.KPRI), // @UndefinedVariable

        (0x2A, Instructions.KNIL), // @UndefinedVariable

        // Upvalue and function ops

        (0x2B, Instructions.UGET), // @UndefinedVariable

        (0x2C, Instructions.USETV), // @UndefinedVariable
        (0x2D, Instructions.USETS), // @UndefinedVariable
        (0x2E, Instructions.USETN), // @UndefinedVariable
        (0x2F, Instructions.USETP), // @UndefinedVariable

        (0x30, Instructions.UCLO), // @UndefinedVariable

        (0x31, Instructions.FNEW), // @UndefinedVariable

        // Table ops

        (0x32, Instructions.TNEW), // @UndefinedVariable

        (0x33, Instructions.TDUP), // @UndefinedVariable

        (0x34, Instructions.GGET), // @UndefinedVariable
        (0x35, Instructions.GSET), // @UndefinedVariable

        (0x36, Instructions.TGETV), // @UndefinedVariable
        (0x37, Instructions.TGETS), // @UndefinedVariable
        (0x38, Instructions.TGETB), // @UndefinedVariable

        (0x39, Instructions.TSETV), // @UndefinedVariable
        (0x3A, Instructions.TSETS), // @UndefinedVariable
        (0x3B, Instructions.TSETB), // @UndefinedVariable

        (0x3C, Instructions.TSETM), // @UndefinedVariable

        // Calls and vararg handling

        (0x3D, Instructions.CALLM), // @UndefinedVariable
        (0x3E, Instructions.CALL), // @UndefinedVariable
        (0x3F, Instructions.CALLMT), // @UndefinedVariable
        (0x40, Instructions.CALLT), // @UndefinedVariable

        (0x41, Instructions.ITERC), // @UndefinedVariable
        (0x42, Instructions.ITERN), // @UndefinedVariable

        (0x43, Instructions.VARG), // @UndefinedVariable

        (0x44, Instructions.ISNEXT), // @UndefinedVariable

        // Returns

        (0x45, Instructions.RETM), // @UndefinedVariable
        (0x46, Instructions.RET), // @UndefinedVariable
        (0x47, Instructions.RET0), // @UndefinedVariable
        (0x48, Instructions.RET1), // @UndefinedVariable

        // Loops and branches

        (0x49, Instructions.FORI), // @UndefinedVariable
        (0x4A, Instructions.JFORI), // @UndefinedVariable

        (0x4B, Instructions.FORL), // @UndefinedVariable
        (0x4C, Instructions.IFORL), // @UndefinedVariable
        (0x4D, Instructions.JFORL), // @UndefinedVariable

        (0x4E, Instructions.ITERL), // @UndefinedVariable
        (0x4F, Instructions.IITERL), // @UndefinedVariable
        (0x50, Instructions.JITERL), // @UndefinedVariable

        (0x51, Instructions.LOOP), // @UndefinedVariable
        (0x52, Instructions.ILOOP), // @UndefinedVariable
        (0x53, Instructions.JLOOP), // @UndefinedVariable

        (0x54, Instructions.JMP), // @UndefinedVariable

        // Function headers

        (0x55, Instructions.FUNCF), // @UndefinedVariable
        (0x56, Instructions.IFUNCF), // @UndefinedVariable
        (0x57, Instructions.JFUNCF), // @UndefinedVariable

        (0x58, Instructions.FUNCV), // @UndefinedVariable
        (0x59, Instructions.IFUNCV), // @UndefinedVariable
        (0x5A, Instructions.JFUNCV), // @UndefinedVariable

        (0x5B, Instructions.FUNCC), // @UndefinedVariable
        (0x5C, Instructions.FUNCCW) // @UndefinedVariable
    };

    public static readonly List<(int, IDef)> v21_OPCODES = new()
    {
// Comparison ops

        (0x00, Instructions.ISLT), // @UndefinedVariable
        (0x01, Instructions.ISGE), // @UndefinedVariable
        (0x02, Instructions.ISLE), // @UndefinedVariable
        (0x03, Instructions.ISGT), // @UndefinedVariable

        (0x04, Instructions.ISEQV), // @UndefinedVariable
        (0x05, Instructions.ISNEV), // @UndefinedVariable

        (0x06, Instructions.ISEQS), // @UndefinedVariable
        (0x07, Instructions.ISNES), // @UndefinedVariable

        (0x08, Instructions.ISEQN), // @UndefinedVariable
        (0x09, Instructions.ISNEN), // @UndefinedVariable

        (0x0A, Instructions.ISEQP), // @UndefinedVariable
        (0x0B, Instructions.ISNEP), // @UndefinedVariable

// Unary test and copy ops

        (0x0C, Instructions.ISTC), // @UndefinedVariable
        (0x0D, Instructions.ISFC), // @UndefinedVariable

        (0x0E, Instructions.IST), // @UndefinedVariable
        (0x0F, Instructions.ISF), // @UndefinedVariable
        (0x10, Instructions.ISTYPE), // @UndefinedVariable
        (0x11, Instructions.ISNUM), // @UndefinedVariable

// Unary ops

        (0x12, Instructions.MOV), // @UndefinedVariable
        (0x13, Instructions.NOT), // @UndefinedVariable
        (0x14, Instructions.UNM), // @UndefinedVariable
        (0x15, Instructions.LEN), // @UndefinedVariable

// Binary ops

        (0x16, Instructions.ADDVN), // @UndefinedVariable
        (0x17, Instructions.SUBVN), // @UndefinedVariable
        (0x18, Instructions.MULVN), // @UndefinedVariable
        (0x19, Instructions.DIVVN), // @UndefinedVariable
        (0x1A, Instructions.MODVN), // @UndefinedVariable

        (0x1B, Instructions.ADDNV), // @UndefinedVariable
        (0x1C, Instructions.SUBNV), // @UndefinedVariable
        (0x1D, Instructions.MULNV), // @UndefinedVariable
        (0x1E, Instructions.DIVNV), // @UndefinedVariable
        (0x1F, Instructions.MODNV), // @UndefinedVariable

        (0x20, Instructions.ADDVV), // @UndefinedVariable
        (0x21, Instructions.SUBVV), // @UndefinedVariable
        (0x22, Instructions.MULVV), // @UndefinedVariable
        (0x23, Instructions.DIVVV), // @UndefinedVariable
        (0x24, Instructions.MODVV), // @UndefinedVariable

        (0x25, Instructions.POW), // @UndefinedVariable
        (0x26, Instructions.CAT), // @UndefinedVariable

// Constant ops

        (0x27, Instructions.KSTR), // @UndefinedVariable
        (0x28, Instructions.KCDATA), // @UndefinedVariable
        (0x29, Instructions.KSHORT), // @UndefinedVariable
        (0x2A, Instructions.KNUM), // @UndefinedVariable
        (0x2B, Instructions.KPRI), // @UndefinedVariable

        (0x2C, Instructions.KNIL), // @UndefinedVariable

// Upvalue and function ops

        (0x2D, Instructions.UGET), // @UndefinedVariable

        (0x2E, Instructions.USETV), // @UndefinedVariable
        (0x2F, Instructions.USETS), // @UndefinedVariable
        (0x30, Instructions.USETN), // @UndefinedVariable
        (0x31, Instructions.USETP), // @UndefinedVariable

        (0x32, Instructions.UCLO), // @UndefinedVariable

        (0x33, Instructions.FNEW), // @UndefinedVariable

// Table ops

        (0x34, Instructions.TNEW), // @UndefinedVariable

        (0x35, Instructions.TDUP), // @UndefinedVariable

        (0x36, Instructions.GGET), // @UndefinedVariable
        (0x37, Instructions.GSET), // @UndefinedVariable

        (0x38, Instructions.TGETV), // @UndefinedVariable
        (0x39, Instructions.TGETS), // @UndefinedVariable
        (0x3A, Instructions.TGETB), // @UndefinedVariable
        (0x3B, Instructions.TGETR), // @UndefinedVariable

        (0x3C, Instructions.TSETV), // @UndefinedVariable
        (0x3D, Instructions.TSETS), // @UndefinedVariable
        (0x3E, Instructions.TSETB), // @UndefinedVariable

        (0x3F, Instructions.TSETM), // @UndefinedVariable
        (0x40, Instructions.TSETR), // @UndefinedVariable

// Calls and vararg handling

        (0x41, Instructions.CALLM), // @UndefinedVariable
        (0x42, Instructions.CALL), // @UndefinedVariable
        (0x43, Instructions.CALLMT), // @UndefinedVariable
        (0x44, Instructions.CALLT), // @UndefinedVariable

        (0x45, Instructions.ITERC), // @UndefinedVariable
        (0x46, Instructions.ITERN), // @UndefinedVariable

        (0x47, Instructions.VARG), // @UndefinedVariable

        (0x48, Instructions.ISNEXT), // @UndefinedVariable

// Returns

        (0x49, Instructions.RETM), // @UndefinedVariable
        (0x4A, Instructions.RET), // @UndefinedVariable
        (0x4B, Instructions.RET0), // @UndefinedVariable
        (0x4C, Instructions.RET1), // @UndefinedVariable

// Loops and branches

        (0x4D, Instructions.FORI), // @UndefinedVariable
        (0x4E, Instructions.JFORI), // @UndefinedVariable

        (0x4F, Instructions.FORL), // @UndefinedVariable
        (0x50, Instructions.IFORL), // @UndefinedVariable
        (0x51, Instructions.JFORL), // @UndefinedVariable

        (0x52, Instructions.ITERL), // @UndefinedVariable
        (0x53, Instructions.IITERL), // @UndefinedVariable
        (0x54, Instructions.JITERL), // @UndefinedVariable

        (0x55, Instructions.LOOP), // @UndefinedVariable
        (0x56, Instructions.ILOOP), // @UndefinedVariable
        (0x57, Instructions.JLOOP), // @UndefinedVariable

        (0x58, Instructions.JMP), // @UndefinedVariable

// Function headers

        (0x59, Instructions.FUNCF), // @UndefinedVariable
        (0x5A, Instructions.IFUNCF), // @UndefinedVariable
        (0x5B, Instructions.JFUNCF), // @UndefinedVariable

        (0x5C, Instructions.FUNCV), // @UndefinedVariable
        (0x5D, Instructions.IFUNCV), // @UndefinedVariable
        (0x5E, Instructions.JFUNCV), // @UndefinedVariable

        (0x5F, Instructions.FUNCC), // @UndefinedVariable
        (0x60, Instructions.FUNCCW) // @UndefinedVariable
    };
}