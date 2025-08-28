//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.ServiceProcess;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyWindowsService
//{
//    public partial class Service1 : ServiceBase
//    {
//        public Service1()
//        {
//            InitializeComponent();
//        }

//        protected override void OnStart(string[] args)
//        {
//            WriteToFile("Service is started__");
//        }

//        protected override void OnStop()
//        {
//            WriteToFile("Service is stoppedd__");
//        }

//        public void WriteToFile(string message)
//        {
//            string path= AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
//            if(!Directory.Exists(path))
//            {
//                Directory.CreateDirectory(path);

//            }
//            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\MyService"+".txt";
//            if (File.Exists(filePath))
//            {

//                using (StreamWriter sw = new StreamWriter(filePath))
//                {
//                    sw.WriteLine(message);
//                }
//            }
//            else
//            {
//                using (StreamWriter sr = File.AppendText(filePath))
//                {
//                    sr.WriteLine(message);
//                }
//            }
//        }
//    }
//}


using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace MyWindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started.");
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped.");
        }

        private void WriteToFile(string message)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string filePath = Path.Combine(path, "MyService.txt");

                // Always append (true = append mode)
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                }
            }
            catch (Exception ex)
            {
                // If logging fails, write to Event Viewer as fallback
                EventLog.WriteEntry("MyWindowsService", $"Logging failed: {ex.Message}", EventLogEntryType.Error);
            }
        }
    }
}
