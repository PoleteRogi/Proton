﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proton
{
    static class Program
    {
        public static string htmlPath;
        public static bool isDev;
        public static bool executesJs;
        public static string jsExecuted;
        public static string titleBarHtml;
        public static string iconPath;
        public static bool isOverlay;
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string action = args[0];
            switch (action)
            {
                case ".":
                    string type = args[1];
                    string overlay = args[2];
                    if (type == "dev") isDev = true;
                    if (type == "user") isDev = false;
                    if (type == "execJs")
                    {
                        executesJs = true;
                        jsExecuted = args[2];
                    }
                    if(overlay == "true")
                    {
                        isOverlay = true;
                    }
                    htmlPath = Environment.CurrentDirectory + @"\index.html";
                    titleBarHtml = Environment.CurrentDirectory + @"\titlebar.html";
                    iconPath = Environment.CurrentDirectory + @"\icon.ico";
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Form1 form = new Form1();
                    Application.Run(form);
                    break;
                case "compile":
                    Console.WriteLine("ANTES0");
                    CSharpCodeProvider csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });
                    CompilerParameters parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.dll", "System.Core.dll" }, Environment.CurrentDirectory + @"\CompiledProtonApp.exe", false);
                    parameters.GenerateExecutable = true;
                    string overlayCompile = args[1];
                    string source = "" +
                        "using System;" +
                        "using System.Collections.Generic;" +
                        "using System.Linq;" +
                        "using System.Threading.Tasks;" +
                        "using System.Diagnostics;" +
                        //"using System.Diagnostics.Process;" +
                        "namespace Proton {" +
                        "   static class Program {" +
                        "       static void Main() {" +
                        "           Process process = new System.Diagnostics.Process();" +
                        "           process.StartInfo.FileName = \"proton\";" +
                        "           process.StartInfo.Arguments = \". user " + overlayCompile + "\";" +
                        "           process.StartInfo.WorkingDirectory = @\"" + Environment.CurrentDirectory + "\";" +
                        "           process.Start(); " +
                        "           " +
                        "       } " +
                        "   } " +
                        "}";
                    //Console.WriteLine(source);
                    Console.WriteLine("ANTES1");
                    CompilerResults results = csc.CompileAssemblyFromSource(parameters, source);
                    Console.WriteLine("ANTES2");
                    if (results.Errors.HasErrors)
                    {
                        Console.WriteLine("ANTES3");
                        results.Errors.Cast<CompilerError>().ToList().ForEach(error => Console.WriteLine(error));
                        Console.WriteLine("ANTES4");
                    }
                    else
                    {
                        Console.WriteLine("ANTES5");
                        Console.WriteLine("Compiled succefully");
                    }
                    Console.WriteLine("ANTES6");
                    break;
            }
            Console.WriteLine("ANTES7");
        }

        static void Algo()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "proton";
            process.StartInfo.Arguments = ". user";
            process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            process.Start();
        }
    }
}
