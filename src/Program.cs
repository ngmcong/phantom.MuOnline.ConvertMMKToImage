using Microsoft.VisualBasic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            args = Directory.GetFiles("./", "*.MMK");
        }
        try
        {
            foreach (var item in args)
            {
                var buffer = ConvertMMKToImage(item);
                ByteArrayToFile(item.Replace(".MMK", ".jpg"), buffer);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught in process: {0}", ex);
            Console.ReadLine();
        }
    }

    static void ByteArrayToFile(string fileName, byte[] byteArray)
    {
        try
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(byteArray, 0, byteArray.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught in process: {0}", ex);
        }
    }

    static byte[] ConvertMMKToImage(string object_0)
    {
        string str = "MMKTMT";
        string expression = "GIF89a";
        byte[] buffer = File.ReadAllBytes(object_0);
        int num2 = Strings.Len(expression);
        for (int i = 1; i <= num2; i++)
        {
            if (buffer[i - 1] != Strings.Asc(Strings.Mid(str, i, 1)))
            {
                buffer = File.ReadAllBytes("Clean.MMK");
                int num5 = Strings.Len(expression);
                for (int j = 1; j <= num5; j++)
                {
                    buffer[j - 1] = (byte)Strings.Asc(Strings.Mid(expression, j, 1));
                }
                Console.WriteLine("MMK file is not valid.");
                return buffer;
            }
            buffer[i - 1] = (byte)Strings.Asc(Strings.Mid(expression, i, 1));
        }
        return buffer;
    }
}