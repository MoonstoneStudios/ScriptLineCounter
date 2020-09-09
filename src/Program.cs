using System;
using System.IO;
using System.Configuration;
using System.Linq;

namespace ScriptLineCounter
{
    class Program
    {
        // config:
        public static string[] ignore;

        //public static bool searchSubDirectories;

        public static string defaultExtention;

        //files:
        private static string path = Directory.GetCurrentDirectory().ToString();
        public static string fileExtention;
        public static string[] allFiles;

        public static string configFormat = 
            "<?xml version=\"1.0\" encoding=\"utf-8\">\n" +
            "<configuration>\n" +
            "<appSettings>\n" +
            "<add key=\"defaultExtention\" value=\".cs\"/>\n" +
            "<add key=\"filesToIgnore\" value=\"App.config,Script Line Counter.exe\"/>\n" +
            "</appSettings>\n" +
            "</configuration>";

        public static string configPath = "App.config";

        //total:
        public static int totalLines;

        public static int ignoreSize;

        static void Main(string[] args)
        {
            if (!File.Exists(configPath)){
                File.WriteAllText(configPath, configFormat);
            }
            defaultExtention = ConfigurationManager.AppSettings["defaultExtention"];
            //searchSubDirectories = Convert.ToBoolean(ConfigurationManager.AppSettings["searchSubDirectories"]);
            ignore = ConfigurationManager.AppSettings["filesToIgnore"].Split(',');
            startUp();
        }//Main


        static void startUp()
        {
            Console.Title = "Script Line Counter Version: 5.0.1";
            Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("                                                 Script Line Counter                                                   ");
            Console.WriteLine("                                                     by Moonstone                                                 " + "\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Type File extention (Default: " + defaultExtention + ")");
            Console.ResetColor();
            fileExtention = Console.ReadLine();
            if (String.IsNullOrEmpty(fileExtention) == true)
            {
                fileExtention = defaultExtention;
            }//if

            allFiles = Directory.GetFiles(path, "*" + fileExtention, SearchOption.AllDirectories);
            getFilesForCounter();

        }//Startup

        static void getFilesForCounter()
        {
            foreach (string file in allFiles)
            {

                var fileName = Path.GetFileName(file);
                if (Array.IndexOf(ignore, fileName) != -1)
                {
                    ignoreSize++;
                }
                else 
                {
                    var fileLines = File.ReadAllLines(file).Length;
                    var name = file;
                    totalLines += fileLines;
                    Console.WriteLine("Lines in " + name + ": " + fileLines);
                }//If

            }//Foreach

            Console.WriteLine(totalLines + " ---- total lines ----");
            Console.WriteLine(allFiles.Length - ignoreSize + " ---- total files ----");
            Console.ReadLine();

        }// getFilesForCounter()

    }//Program

}//ScriptLineCounter
