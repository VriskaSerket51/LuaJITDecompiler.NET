namespace LuaJitDecompiler.Net.Utils;

public static class Log
{
    public static void ErrPrint(params object[] args)
    {
        string? fmt = null;
        if (args.Length > 0 && args[0] is string)
        {
            fmt = args[0].ToString();
            args = args[1..];
        }

        if (fmt != null)
        {
            Console.Error.WriteLine(fmt, args);
        }
        else
        {
            string[] strs = Array.ConvertAll(args, x => x.ToString());
            Console.Error.WriteLine(string.Join(" ", strs));
        }
    }
}