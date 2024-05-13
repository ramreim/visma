using System;
using System.IO;

namespace vismaUzd
{
    public static class LoggingException
    {
        public static void Log(string msg)
        {
            using (StreamWriter sw = File.AppendText("C:\\Temp\\InfoLog.log"))
            {
                sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}    {msg}");
            }
        }
    }
}



