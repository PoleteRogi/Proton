using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;

namespace Proton
{
    static class Program
    {
        public static string htmlPath;
        public static bool isDev;
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string action = args[0];
            switch(action)
            {
                case ".":
                    string type = args[1];
                    if (type == "dev") isDev = true;
                    if (type == "user") isDev = false;
                    htmlPath = Environment.CurrentDirectory + @"\index.html";
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Form1 form = new Form1();
                    Application.Run(form);
                    break;
                case "compile":
                    CSharpCodeProvider csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });
                    CompilerParameters parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, Environment.CurrentDirectory + @"\CompiledProtonApp.exe", true);
                    parameters.GenerateExecutable = true;
                    string source = string.Format(File.ReadAllText(Application.StartupPath + @"\compile\.compile"), Environment.CurrentDirectory);
                    CompilerResults results = csc.CompileAssemblyFromSource(parameters, source);
                    break;
            }
        }
    }
}
