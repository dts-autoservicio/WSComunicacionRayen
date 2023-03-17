using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Security.Permissions;
using System.Security;
using System.Configuration;

namespace ComunicacionRayen
{
    public class Log
    {
        public Log(){}

        public void Escribe(string text)
        {
            try
            {
                string RequiereLog = ConfigurationManager.AppSettings["RequiereLog"];

                if (RequiereLog == "true")
                {
                    string path = ConfigurationManager.AppSettings["PathLog"];
                    string fecha = DateTime.Today.ToString("ddMMyyyy");
                    string FechaFull = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                    string curFile = path + fecha + ".txt";

                    if (File.Exists(curFile))
                    {
                        //Pass the filepath and filename to the StreamWriter Constructor
                        StreamWriter sw = new StreamWriter(curFile, true);

                        //Write a line of text
                        sw.WriteLine(FechaFull + " | " + text);
                        sw.Close();
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter(curFile);

                        //Write a line of text
                        sw.WriteLine(FechaFull + " | " + text);
                        sw.Close();
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}