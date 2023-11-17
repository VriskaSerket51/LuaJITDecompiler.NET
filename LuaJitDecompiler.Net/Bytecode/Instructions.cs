namespace LuaJitDecompiler.Net.Bytecode;

public static class Instructions
{
    public const int T_VAR = 0; // variable slot number
    public const int T_DST = 1; // variable slot number, used as a destination

    public const int T_BS = 2; // base slot number, read-write
    public const int T_RBS = 3; // base slot number, read-only

    public const int T_UV = 4; // upvalue number (slot number, but specific to upvalues)

    public const int T_LIT = 5; // literal
    public const int T_SLIT = 6; // signed literal

    public const int T_PRI = 7; // primitive type (0 = nil, 1 = false, 2 = true)
    public const int T_NUM = 8; // numeric constant, index into constant table
    public const int T_STR = 9; // string constant, negated index into constant table

    public const int T_TAB = 10; // template table, negated index into constant table
    public const int T_FUN = 11; // function prototype, negated index into constant table
    public const int T_CDT = 12; // cdata constant, negated index into constant table
    public const int T_JMP = 13; // branch target, relative to next instruction, biased with 0x8000

    public const int SLOT_FALSE = 30000; // placeholder slot value for logical false
    public const int SLOT_TRUE = 30001; // placeholder slot value for logical true

    public static readonly IDef ISLT = new IDef("ISLT", T_VAR, -1, T_VAR, "if {A} < {D}");
    public static readonly IDef ISGE = new IDef("ISGE", T_VAR, -1, T_VAR, "if {A} >= {D}");
    public static readonly IDef ISLE = new IDef("ISLE", T_VAR, -1, T_VAR, "if {A} <= {D}");
    public static readonly IDef ISGT = new IDef("ISGT", T_VAR, -1, T_VAR, "if {A} > {D}");

    public static readonly IDef ISEQV = new IDef("ISEQV", T_VAR, -1, T_VAR, "if {A} == {D}");
    public static readonly IDef ISNEV = new IDef("ISNEV", T_VAR, -1, T_VAR, "if {A} ~= {D}");

    public static readonly IDef ISEQS = new IDef("ISEQS", T_VAR, -1, T_STR, "if {A} == {D}");
    public static readonly IDef ISNES = new IDef("ISNES", T_VAR, -1, T_STR, "if {A} ~= {D}");

    public static readonly IDef ISEQN = new IDef("ISEQN", T_VAR, -1, T_NUM, "if {A} == {D}");
    public static readonly IDef ISNEN = new IDef("ISNEN", T_VAR, -1, T_NUM, "if {A} ~= {D}");

    public static readonly IDef ISEQP = new IDef("ISEQP", T_VAR, -1, T_PRI, "if {A} == {D}");
    public static readonly IDef ISNEP = new IDef("ISNEP", T_VAR, -1, T_PRI, "if {A} ~= {D}");

    // Unary test and copy ops

    public static readonly IDef ISTC = new IDef("ISTC", T_DST, -1, T_VAR, "{A} = {D}; if {D}");
    public static readonly IDef ISFC = new IDef("ISFC", T_DST, -1, T_VAR, "{A} = {D}; if not {D}");

    public static readonly IDef IST = new IDef("IST", -1, -1, T_VAR, "if {D}");
    public static readonly IDef ISF = new IDef("ISF", -1, -1, T_VAR, "if not {D}");

    // Added in bytecode version 2
    public static readonly IDef ISTYPE = new IDef("ISTYPE", T_VAR, -1, T_LIT, "see lj vm source");
    public static readonly IDef ISNUM = new IDef("ISNUM", T_VAR, -1, T_LIT, "see lj vm source");

    // Unary ops

    public static readonly IDef MOV = new IDef("MOV", T_DST, -1, T_VAR, "{A} = {D}");
    public static readonly IDef NOT = new IDef("NOT", T_DST, -1, T_VAR, "{A} = not {D}");
    public static readonly IDef UNM = new IDef("UNM", T_DST, -1, T_VAR, "{A} = -{D}");
    public static readonly IDef LEN = new IDef("LEN", T_DST, -1, T_VAR, "{A} = //{D}");

    // Binary ops

    public static readonly IDef ADDVN = new IDef("ADDVN", T_DST, T_VAR, T_NUM, "{A} = {B} + {C}");
    public static readonly IDef SUBVN = new IDef("SUBVN", T_DST, T_VAR, T_NUM, "{A} = {B} - {C}");
    public static readonly IDef MULVN = new IDef("MULVN", T_DST, T_VAR, T_NUM, "{A} = {B} * {C}");
    public static readonly IDef DIVVN = new IDef("DIVVN", T_DST, T_VAR, T_NUM, "{A} = {B} / {C}");
    public static readonly IDef MODVN = new IDef("MODVN", T_DST, T_VAR, T_NUM, "{A} = {B} % {C}");

    public static readonly IDef ADDNV = new IDef("ADDNV", T_DST, T_VAR, T_NUM, "{A} = {C} + {B}");
    public static readonly IDef SUBNV = new IDef("SUBNV", T_DST, T_VAR, T_NUM, "{A} = {C} - {B}");
    public static readonly IDef MULNV = new IDef("MULNV", T_DST, T_VAR, T_NUM, "{A} = {C} * {B}");
    public static readonly IDef DIVNV = new IDef("DIVNV", T_DST, T_VAR, T_NUM, "{A} = {C} / {B}");
    public static readonly IDef MODNV = new IDef("MODNV", T_DST, T_VAR, T_NUM, "{A} = {C} % {B}");

    public static readonly IDef ADDVV = new IDef("ADDVV", T_DST, T_VAR, T_VAR, "{A} = {B} + {C}");
    public static readonly IDef SUBVV = new IDef("SUBVV", T_DST, T_VAR, T_VAR, "{A} = {B} - {C}");
    public static readonly IDef MULVV = new IDef("MULVV", T_DST, T_VAR, T_VAR, "{A} = {B} * {C}");
    public static readonly IDef DIVVV = new IDef("DIVVV", T_DST, T_VAR, T_VAR, "{A} = {B} / {C}");
    public static readonly IDef MODVV = new IDef("MODVV", T_DST, T_VAR, T_VAR, "{A} = {B} % {C}");

    public static readonly IDef POW = new IDef("POW", T_DST, T_VAR, T_VAR, "{A} = {B} ^ {C} (pow)");

    public static readonly IDef CAT = new IDef("CAT", T_DST, T_RBS, T_RBS,
        "{A} = {concat_from_B_to_C}");

    // Constant ops.

    public static readonly IDef KSTR = new IDef("KSTR", T_DST, -1, T_STR, "{A} = {D}");
    public static readonly IDef KCDATA = new IDef("KCDATA", T_DST, -1, T_CDT, "{A} = {D}");
    public static readonly IDef KSHORT = new IDef("KSHORT", T_DST, -1, T_SLIT, "{A} = {D}");
    public static readonly IDef KNUM = new IDef("KNUM", T_DST, -1, T_NUM, "{A} = {D}");
    public static readonly IDef KPRI = new IDef("KPRI", T_DST, -1, T_PRI, "{A} = {D}");

    public static readonly IDef KNIL = new IDef("KNIL", T_BS, -1, T_BS, "{from_A_to_D} = nil");

    // Upvalue and function ops.

    public static readonly IDef UGET = new IDef("UGET", T_DST, -1, T_UV, "{A} = {D}");

    public static readonly IDef USETV = new IDef("USETV", T_UV, -1, T_VAR, "{A} = {D}");
    public static readonly IDef USETS = new IDef("USETS", T_UV, -1, T_STR, "{A} = {D}");
    public static readonly IDef USETN = new IDef("USETN", T_UV, -1, T_NUM, "{A} = {D}");
    public static readonly IDef USETP = new IDef("USETP", T_UV, -1, T_PRI, "{A} = {D}");

    public static readonly IDef UCLO = new IDef("UCLO", T_RBS, -1, T_JMP,
        "nil uvs >= {A}; goto {D}");

    public static readonly IDef FNEW = new IDef("FNEW", T_DST, -1, T_FUN, "{A} = function {D}");

    // Table ops.

    public static readonly IDef TNEW = new IDef("TNEW", T_DST, -1, T_LIT, "{A} = new table(array: {D_array}, dict: {D_dict})");

    public static readonly IDef TDUP = new IDef("TDUP", T_DST, -1, T_TAB, "{A} = copy {D}");

    public static readonly IDef GGET = new IDef("GGET", T_DST, -1, T_STR, "{A} = _env[{D}]");
    public static readonly IDef GSET = new IDef("GSET", T_VAR, -1, T_STR, "_env[{D}] = {A}");

    public static readonly IDef TGETV = new IDef("TGETV", T_DST, T_VAR, T_VAR, "{A} = {B}[{C}]");
    public static readonly IDef TGETS = new IDef("TGETS", T_DST, T_VAR, T_STR, "{A} = {B}.{C}");
    public static readonly IDef TGETB = new IDef("TGETB", T_DST, T_VAR, T_LIT, "{A} = {B}[{C}]");

    // Added in bytecode version 2
    public static readonly IDef TGETR = new IDef("TGETR", T_DST, T_VAR, T_VAR, "{A} = {B}[{C}]");

    public static readonly IDef TSETV = new IDef("TSETV", T_VAR, T_VAR, T_VAR, "{B}[{C}] = {A}");
    public static readonly IDef TSETS = new IDef("TSETS", T_VAR, T_VAR, T_STR, "{B}.{C} = {A}");
    public static readonly IDef TSETB = new IDef("TSETB", T_VAR, T_VAR, T_LIT, "{B}[{C}] = {A}");

    public static readonly IDef TSETM = new IDef("TSETM", T_BS, -1, T_NUM,
        "for i = 0, MULTRES, 1 do {A_minus_one}[{D_low} + i] = slot({A} + i)");

    // Added in bytecode version 2
    public static readonly IDef TSETR = new IDef("TSETR", T_VAR, T_VAR, T_VAR, "{B}[{C}] = {A}");

    // Calls and vararg handling. T = tail call.

    public static readonly IDef CALLM = new IDef("CALLM", T_BS, T_LIT, T_LIT,
        "{from_A_x_B_minus_two} = {A}({from_A_plus_one_x_C}, ...MULTRES)");

    public static readonly IDef CALL = new IDef("CALL", T_BS, T_LIT, T_LIT,
        "{from_A_x_B_minus_two} = {A}({from_A_plus_one_x_C_minus_one})");

    public static readonly IDef CALLMT = new IDef("CALLMT", T_BS, -1, T_LIT,
        "return {A}({from_A_plus_one_x_D}, ...MULTRES)");

    public static readonly IDef CALLT = new IDef("CALLT", T_BS, -1, T_LIT,
        "return {A}({from_A_plus_one_x_D_minus_one})");

    public static readonly IDef ITERC = new IDef("ITERC", T_BS, T_LIT, T_LIT,
        "{A}, {A_plus_one}, {A_plus_two} = {A_minus_three}, {A_minus_two}, {A_minus_one}; {from_A_x_B_minus_two} = {A_minus_three}({A_minus_two}, {A_minus_one})");

    public static readonly IDef ITERN = new IDef("ITERN", T_BS, T_LIT, T_LIT,
        "{A}, {A_plus_one}, {A_plus_two} = {A_minus_three}, {A_minus_two}, {A_minus_one}; {from_A_x_B_minus_two} = {A_minus_three}({A_minus_two}, {A_minus_one})");

    public static readonly IDef VARG = new IDef("VARG", T_BS, T_LIT, T_LIT,
        "{from_A_x_B_minus_two} = ...");

    public static readonly IDef ISNEXT = new IDef("ISNEXT", T_BS, -1, T_JMP,
        "Verify ITERN at {D}; goto {D}");

    // Returns.

    public static readonly IDef RETM = new IDef("RETM", T_BS, -1, T_LIT,
        "return {from_A_x_D_minus_one}, ...MULTRES");

    public static readonly IDef RET = new IDef("RET", T_RBS, -1, T_LIT,
        "return {from_A_x_D_minus_two}");

    public static readonly IDef RET0 = new IDef("RET0", T_RBS, -1, T_LIT, "return");
    public static readonly IDef RET1 = new IDef("RET1", T_RBS, -1, T_LIT, "return {A}");

    // Loops and branches. I/J = interp/JIT, I/C/L = init/call/loop.

    public static readonly IDef FORI = new IDef("FORI", T_BS, -1, T_JMP,
        "for {A_plus_three} = {A},{A_plus_one},{A_plus_two} else goto {D}");

    public static readonly IDef JFORI = new IDef("JFORI", T_BS, -1, T_JMP,
        "for {A_plus_three} = {A},{A_plus_one},{A_plus_two} else goto {D}");

    public static readonly IDef FORL = new IDef("FORL", T_BS, -1, T_JMP,
        "{A} = {A} + {A_plus_two}; if cmp({A}, sign {A_plus_two},  {A_plus_one}) goto {D}");

    public static readonly IDef IFORL = new IDef("IFORL", T_BS, -1, T_JMP,
        "{A} = {A} + {A_plus_two}; if cmp({A}, sign {A_plus_two}, {A_plus_one}) goto {D}");

    public static readonly IDef JFORL = new IDef("JFORL", T_BS, -1, T_JMP,
        "{A} = {A} + {A_plus_two}; if cmp({A}, sign {A_plus_two}, {A_plus_one}) goto {D}");

    public static readonly IDef ITERL = new IDef("ITERL", T_BS, -1, T_JMP,
        "{A_minus_one} = {A}; if {A} != nil goto {D}");

    public static readonly IDef IITERL = new IDef("IITERL", T_BS, -1, T_JMP,
        "{A_minus_one} = {A}; if {A} != nil goto {D}");

    public static readonly IDef JITERL = new IDef("JITERL", T_BS, -1, T_LIT,
        "{A_minus_one} = {A}; if {A} != nil goto {D}");

    public static readonly IDef LOOP = new IDef("LOOP", T_RBS, -1, T_JMP, "Loop start, exit goto {D}");
    public static readonly IDef ILOOP = new IDef("ILOOP", T_RBS, -1, T_JMP, "Noop");
    public static readonly IDef JLOOP = new IDef("JLOOP", T_RBS, -1, T_LIT, "Noop");

    public static readonly IDef JMP = new IDef("JMP", T_RBS, -1, T_JMP, "	goto {D}");

    // Function headers. I/J = interp/JIT, F/V/C = fixarg/vararg/C func.
    // Shouldn't be ever seen - they are not stored in raw dump?

    public static readonly IDef FUNCF = new IDef("FUNCF", T_RBS, -1, -1,
        "Fixed-arg function with frame size {A}");

    public static readonly IDef IFUNCF = new IDef("IFUNCF", T_RBS, -1, -1,
        "Interpreted fixed-arg function with frame size {A}");

    public static readonly IDef JFUNCF = new IDef("JFUNCF", T_RBS, -1, T_LIT,
        "JIT compiled fixed-arg function with frame size {A}");

    public static readonly IDef FUNCV = new IDef("FUNCV", T_RBS, -1, -1,
        "Var-arg function with frame size {A}");

    public static readonly IDef IFUNCV = new IDef("IFUNCV", T_RBS, -1, -1,
        "Interpreted var-arg function with frame size {A}");

    public static readonly IDef JFUNCV = new IDef("JFUNCV", T_RBS, -1, T_LIT,
        "JIT compiled var-arg function with frame size {A}");

    public static readonly IDef FUNCC = new IDef("FUNCC", T_RBS, -1, -1,
        "C function with frame size {A}");

    public static readonly IDef FUNCCW = new IDef("FUNCCW", T_RBS, -1, -1,
        "Wrapped C function with frame size {A}");

    public static readonly IDef UNKNW = new IDef("UNKNW", T_LIT, T_LIT, T_LIT, "Unknown instruction");
}

public class Instruction
{
    public string Name { get; }
    public int Opcode { get; set; }
    public int AType { get; }
    public int BType { get; }
    public int CDType { get; }
    public string Description { get; }
    public int ArgsCount { get; }
    public int Bytecode { get; set; }
    public int A { get; set; }
    public int B { get; set; }
    public int CD { get; set; }

    public Instruction(IDef definition)
    {
        Name = definition.Name;
        Opcode = definition.Opcode;
        AType = definition.AType;
        BType = definition.BType;
        CDType = definition.CDType;
        Description = definition.Description;
        ArgsCount = definition.ArgsCount;
        Bytecode = 0;
        if (AType != -1)
        {
            A = 0;
        }

        if (BType != -1)
        {
            B = 0;
        }

        if (CDType != -1)
        {
            CD = 0;
        }
    }
}

public class IDef
{
    public string Name { get; }
    public int Opcode { get; set; }
    public int AType { get; }
    public int BType { get; }
    public int CDType { get; }
    public string Description { get; }
    public int ArgsCount { get; }

    public IDef(string name, int aType, int bType, int cdType, string description)
    {
        Name = name;
        Opcode = -1;
        AType = aType;
        BType = bType;
        CDType = cdType;
        Description = description;

        ArgsCount = (AType != -1 ? 1 : 0) + (BType != -1 ? 1 : 0) + (CDType != -1 ? 1 : 0);
    }

    public Instruction Build() => new Instruction(this);
}