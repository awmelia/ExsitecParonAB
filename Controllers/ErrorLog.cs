using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ExsitecParonAB.Controllers
{
    public class ErrorLog
    {
        // https://stackoverflow.com/questions/21307789/how-to-save-exception-in-txt-file
        //Log all kinds of exceptions 
        public void ErrorLogging(Exception ex)
        {
            string directory = Directory.GetCurrentDirectory();
            string fileName = @"\errorlog.txt";
            string strPath = directory + fileName;

            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);
            }
        }
    }
}