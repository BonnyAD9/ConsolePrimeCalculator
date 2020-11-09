using Microsoft.CSharp.RuntimeBinder;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml.Xsl;

namespace Primes
{
    class Program
    {
        private static string separator = "\n";
        private static bool file = false;
        private static string path;
        private static string[] Args { get; set; }

        static void Main(string[] args)
        {
            Args = args;
            if (Args.Length == 0)
            {
                ByOne();
                return;
            }
            int count;
            if (!int.TryParse(Args[0], out count))
            {
                Console.WriteLine($"Invalid first argument '{Args[0]}', first argument must be integer");
                return;
            }
            if (count <= 0)
            {
                Console.WriteLine($"Invalid first argument '{Args[0]}', first argument must be positive integer");
                return;
            }
            int h;
            for (int i = 1; i < Args.Length; i++)
            {
                switch (Args[i].ToLower())
                {
                    case "/f":
                        if ((h = ArgF(i)) < 0)
                            return;
                        i += h;
                        break;
                    case "/s":
                        if ((h = ArgS(i)) < 0)
                            return;
                        i += h;
                        break;
                    default:
                        Console.WriteLine($"Invalid argument '{Args[i]}'");
                        return;
                }
            }
            if (file)
                ToFile(count);
            else ToConsole(count);
        }

        private static int ArgF(int pos)
        {
            file = true;
            if (Args.Length <= ++pos)
            {
                Console.WriteLine("No file specified after the '/f' argument");
                return -1;
            }
            path = Args[pos];
            if (File.Exists(path))
            {
                Console.WriteLine($"File '{path}' will be overwriten");
                Console.Write("Continue? [Y/n]: ");
                return Console.ReadLine().ToLower() == "y" ? 1 : -1;
            }    
            return 1;
        }

        private static int ArgS(int pos)
        {
            if (Args.Length <= ++pos)
            {
                Console.WriteLine("No separator after argument '/s' found (use '\"\"' if you want no separator");
                return -1;
            }
            separator = Args[pos];
            return 1;
        }

        private static void ToFile(int count)
        {
            DateTime dt = DateTime.Now;
            Console.Write("Calculating... ");
            Step s = new Step();
            for (int i = 0; i < count; i++)
            {
                s.Next();
            }
            Console.Write($"Done!\nWriting... ");
            StringBuilder sb = new StringBuilder();
            foreach (int i in s.PrimeNumbers)
            {
                sb.Append(i);
                sb.Append(separator);
            }
            try
            {
                File.WriteAllText(path, sb.ToString().Remove(sb.Length - separator.Length));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            Console.WriteLine("Done!");
            Console.WriteLine($"Calculated {count} prime numbers in {(DateTime.Now - dt).TotalSeconds} s, and stored them in '{path}'");
        }

        public static void ToConsole(int count)
        {
            Console.Write("Calculating... ");
            Step s = new Step();
            for (int i = 0; i < count; i++)
            {
                s.Next();
            }
            Console.WriteLine($"Done!");
            StringBuilder sb = new StringBuilder();
            foreach (long l in s.PrimeNumbers)
            {
                sb.Append(l);
                sb.Append(separator);
            }
            Console.WriteLine(sb.ToString().Remove(sb.Length - separator.Length));
        }

        private static void ByOne()
        {
            Console.WriteLine("Press 'q' to exit or any other key to show the next prime number");
            Step s = new Step();
            Console.WriteLine(s.PrimeNumbers[0]);
            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.CursorLeft = 0;
                Console.WriteLine(s.Next());
            }
        }
    }
}
