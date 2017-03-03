using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoCdConsoleSummary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                var info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

                Console.WriteLine($@"{info.InternalName}
Version {info.ProductVersion}
{info.LegalCopyright}

{info.Comments}

Usage:
    {info.InternalName} [output-file]
Examples:
    {info.InternalName} Console-Summary.html");
                Environment.Exit(1);
            }
        }
    }
}
