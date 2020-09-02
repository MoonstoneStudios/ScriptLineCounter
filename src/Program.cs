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
            "<add key=\"defaultExtention\" value=\".dsc\"/>\n" +
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
            Console.Title = "Script Line Counter Version: 0.5";
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
            }//if (String.IsNullOrEmpty(fileExtention) == true)

            /*if (searchSubDirectories == true)
            {
                allFiles = Directory.GetFiles(path, "*" + fileExtention, SearchOption.AllDirectories);
                getFilesForCounter();
            }
            else if (searchSubDirectories == false){
            */
            allFiles = Directory.GetFiles(path, "*" + fileExtention, SearchOption.TopDirectoryOnly);
            getFilesForCounter();
           /* } 
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The App.config file contains errors in the searchSubDirectories key. Make sure it is set to \"true\" or \"false\"");
                Console.ReadLine();
            }//if (searchSubDirectories == true)
            */

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
                }//If (Array.IndexOf(ignore, fileName) != -1)

            }//Foreach (string file in allFiles)

            Console.WriteLine(totalLines + " ---- total lines ----");
            Console.WriteLine(allFiles.Length - ignoreSize + " ---- total files ----");
            Console.ReadLine();

        }// getFilesForCounter()

    }//Program

}//ScriptLineCounter
