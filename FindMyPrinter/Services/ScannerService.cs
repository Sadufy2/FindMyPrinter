using FindMyPrinter.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPrinter.Services
{
    internal class ScannerService
    {
        ConcurrentBag<Printer> scannedPrinters;
        public ScannerService()
        {
            scannedPrinters = new ConcurrentBag<Printer>();
        }

        public async Task<List<Printer>> StartScanning()
        {
            string sendUrl = "http://192.168.1.{0}:5000";

            scannedPrinters.Clear();
            Task[] tasks = new Task[30];
            for (int i = 0; i < tasks.Length; i++)
            {
                int threadId = i;
                tasks[i] = Task.Run(() => SendMessagesAsync(sendUrl, threadId));
            }
            await Task.WhenAll(tasks);
            return scannedPrinters.ToList();
        }

        async Task SendMessagesAsync(string urlFormat, int threadIdx)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(2);
                string url = string.Format(urlFormat, threadIdx.ToString());
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        if (responseContent.StartsWith("droneName="))
                        {
                            string printerName = responseContent.Substring("printerName=".Length);
                            scannedPrinters.Add(new Printer("192.168.1." + threadIdx.ToString(), printerName));
                        }
                        else
                        {
                            throw new Exception(responseContent);
                        }
                    }
                    else
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
