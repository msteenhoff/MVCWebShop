using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace WebShopPage.DAL
{
    public sealed class ExceptionWriter
    {
        private ExceptionWriter()
        { }

        public static void LoggFeil(Exception exc, string source)
        {
            string loggFil = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/ErrorLog.txt";

            StreamWriter sw = new StreamWriter(loggFil, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (exc.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(exc.InnerException.GetType().ToString());
                sw.Write("Inner Exception: ");
                sw.WriteLine(exc.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exc.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(exc.GetType().ToString());
            sw.WriteLine("Exception: " + exc.Message);
            sw.WriteLine("Source: " + source);
            sw.WriteLine("Stack Trace: ");
            if (exc.StackTrace != null)
            {
                sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
            }
            sw.Close();
        }
    }
}
