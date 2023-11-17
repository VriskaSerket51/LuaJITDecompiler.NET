using System.Text;
using LuaJitDecompiler.Net.Bytecode;

namespace LuaJitDecompiler.Net.RawDump;

public class Constants
{
    private const int BCDUMP_KGC_CHILD = 0;
    private const int BCDUMP_KGC_TAB = 1;
    private const int BCDUMP_KGC_I64 = 2;
    private const int BCDUMP_KGC_U64 = 3;
    private const int BCDUMP_KGC_COMPLEX = 4;
    private const int BCDUMP_KGC_STR = 5;

    private const int BCDUMP_KTAB_NIL = 0;
    private const int BCDUMP_KTAB_FALSE = 1;
    private const int BCDUMP_KTAB_TRUE = 2;
    private const int BCDUMP_KTAB_INT = 3;
    private const int BCDUMP_KTAB_NUM = 4;
    private const int BCDUMP_KTAB_STR = 5;

    public static bool Read(Prototype parser, Bytecode.Constants constants)
    {
        bool r = true;

        r = r && ReadUpvalueReferences(parser, constants.UpvalueReferences);
        r = r && ReadComplexConstants(parser, constants.ComplexConstants);
        r = r && ReadNumericConstants(parser, constants.NumericConstants);

        return r;
    }

    private static bool ReadUpvalueReferences(Prototype parser, List<uint> references)
    {
        int i = 0;

        while (i < parser.UpvaluesCount)
        {
            i += 1;
            uint upvalue = parser.Stream.ReadUInt(2);
            references.Add(upvalue);
        }

        return true;
    }

    private static bool ReadComplexConstants(Prototype parser, List<object> complexConstants)
    {
        int i = 0;

        while (i < parser.ComplexConstantsCount)
        {
            int constantType = (int)parser.Stream.ReadUleb128();

            if (constantType >= BCDUMP_KGC_STR)
            {
                int length = constantType - BCDUMP_KGC_STR;
                byte[] stringBytes = parser.Stream.ReadBytes(length);
                string str = Encoding.UTF8.GetString(stringBytes);
                complexConstants.Add(str);
            }
            else if (constantType == BCDUMP_KGC_TAB)
            {
                Table table = new Table();
                if (!ReadTable(parser, table))
                {
                    return false;
                }

                complexConstants.Add(table);
            }
            else if (constantType != BCDUMP_KGC_CHILD)
            {
                object number = _readNumber(parser);
                if (constantType == BCDUMP_KGC_COMPLEX)
                {
                    object imaginary = _readNumber(parser);
                    complexConstants.Add(new Tuple<object, object>(number, imaginary));
                }
                else
                {
                    complexConstants.Add(number);
                }
            }
            else
            {
                complexConstants.Add(parser.Prototypes[0]);
                parser.Prototypes.RemoveAt(0);
            }

            i += 1;
        }

        return true;
    }

    private static bool ReadNumericConstants(Prototype parser, List<object> numericConstants)
    {
        int i = 0;

        while (i < parser.NumericConstantsCount)
        {
            uint lo;
            (var isNum, lo) = parser.Stream.ReadUleb128From33Bit();

            object number;
            if (isNum)
            {
                uint hi = parser.Stream.ReadUleb128();
                number = AssembleNumber(lo, hi);
            }
            else
            {
                number = ProcessSign(lo);
            }

            numericConstants.Add(number);

            i += 1;
        }

        return true;
    }

    private static double _readNumber(Prototype parser)
    {
        uint lo = parser.Stream.ReadUleb128();
        uint hi = parser.Stream.ReadUleb128();
        return AssembleNumber(lo, hi);
    }

    private static int ReadSignedInt(Prototype parser)
    {
        uint number = parser.Stream.ReadUleb128();
        return ProcessSign(number);
    }

    private static double AssembleNumber(uint lo, uint hi)
    {
        long floatAsInt;
        if (BitConverter.IsLittleEndian)
            floatAsInt = ((long)lo << 32) | hi;
        else
            floatAsInt = ((long)hi << 32) | lo;

        byte[] rawBytes = BitConverter.GetBytes(floatAsInt);
        return BitConverter.ToDouble(rawBytes, 0);
    }

    private static int ProcessSign(uint number)
    {
        return (int)((number & 0x80000000) != 0 ? -0x100000000 + number : number);
    }

    private static bool ReadTable(Prototype parser, Table table)
    {
        int arrayItemsCount = (int)parser.Stream.ReadUleb128();
        int hashItemsCount = (int)parser.Stream.ReadUleb128();

        while (arrayItemsCount > 0)
        {
            object constant = ReadTableItem(parser);
            table.Array.Add(constant);
            arrayItemsCount -= 1;
        }

        while (hashItemsCount > 0)
        {
            object key = ReadTableItem(parser);
            object value = ReadTableItem(parser);
            table.Dictionary.Add(new Tuple<object, object>(key, value));
            hashItemsCount -= 1;
        }

        return true;
    }

    private static object ReadTableItem(Prototype parser)
    {
        int dataType = (int)parser.Stream.ReadUleb128();

        if (dataType >= BCDUMP_KTAB_STR)
        {
            int length = dataType - BCDUMP_KTAB_STR;
            byte[] stringBytes = parser.Stream.ReadBytes(length);
            return Encoding.UTF8.GetString(stringBytes);
        }
        else if (dataType == BCDUMP_KTAB_INT)
        {
            return ReadSignedInt(parser);
        }
        else if (dataType == BCDUMP_KTAB_NUM)
        {
            return _readNumber(parser);
        }
        else if (dataType == BCDUMP_KTAB_TRUE)
        {
            return true;
        }
        else if (dataType == BCDUMP_KTAB_FALSE)
        {
            return false;
        }
        else if (dataType == BCDUMP_KTAB_NIL)
        {
            return null;
        }

        throw new NotSupportedException();
    }
}