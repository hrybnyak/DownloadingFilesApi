using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.RD6_Task2
{
    public class DownloadingFilesService
    {
        private Stopwatch _stopWatch;
        private static string Url => "http://download.microsoft.com/download/c/6/9/c6938ba7-02cf-49d0-b7bf-88fd88a04024/";
        private static string EndOfUrl => "18msdn_emag.pdf";
        private Stopwatch Stopwatch
        {
            get
            {
                if(_stopWatch == null)
                {
                    _stopWatch = new Stopwatch();
                }
                return _stopWatch;
            }
        }
        public void CreatePathes(int i, out string adress, out string filePath)
        {
            adress = "";
            string journalNumber;
            if (i < 10)
            {
                journalNumber = "0" + i;
            }
            else
            {
                journalNumber = i.ToString();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(Url);
            sb.Append(journalNumber);
            sb.Append(EndOfUrl);
            adress = sb.ToString();
            sb.Clear();
            sb.Append(Environment.CurrentDirectory);
            sb.Append(@"\files\");
            sb.Append(i);
            sb.Append(".pdf");
            filePath = sb.ToString();
        }

        public TimeSpan DownloadFiles()
        {
            Stopwatch.Restart();
            for (int i = 1; i < 13; i++)
            {
                using (WebClient wb = new WebClient())
                {
                    CreatePathes(i, out string adress, out string path);
                    wb.DownloadFile(adress, path);
                }
            }
          
            Stopwatch.Stop();
            return Stopwatch.Elapsed;
        }

        public async Task<TimeSpan> DownloadFilesAsync()
        {
            Stopwatch.Restart();
            for (int i = 1; i < 13; i++)
            {
                using (WebClient _client = new WebClient())
                {
                    CreatePathes(i, out string adress, out string path);
                    await _client.DownloadFileTaskAsync(adress, path);
                }
            }
            Stopwatch.Stop();
            return Stopwatch.Elapsed;
        }

        public TimeSpan DownloadFilesParallel()
        {
            Stopwatch.Restart();
            
                Parallel.For(1, 13, i =>
                {
                    using (WebClient _client = new WebClient())
                    {
                        CreatePathes(i, out string adress, out string path);
                        _client.DownloadFile(adress, path);
                    }
                });
            
            Stopwatch.Stop();
            return Stopwatch.Elapsed;
        }
    }
}
