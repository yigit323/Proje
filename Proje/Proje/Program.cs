using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proje
{
    class Program
    {
        private static object weatherForecast;
        private static string foreginAddress;

        public static object WeatherForecast { get => weatherForecast; set => weatherForecast = value; }
        public static string ForeginAddress { get => foreginAddress; set => foreginAddress = value; }

        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            List<Port> ports = new();
            List<Port> Ports = ports;


            using Process p = new();

            ProcessStartInfo ps = new()
            {
                Arguments = "-a -n ",
                FileName = "netstat.exe",
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            p.StartInfo = ps;
            _ = p.Start();

            StreamReader stdOutput = p.StandardOutput;
            StreamReader stdError = p.StandardError;

            string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
            string exitStatus = p.ExitCode.ToString();

            if (exitStatus != "0")
            {
                //Kod Hata Verirse 
            }

            //Satırlar
            string[] rows = Regex.Split(content, "\r\n");
            foreach (string row in rows)
            {
                string[] tokens = Regex.Split(row, "\\s+");
                if (tokens.Length > 4) _ = tokens[1].Equals("TCP");
                    {
                    string foreginAddress = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1");
                    Ports.Add(new Port
                        {
                        protocol = foreginAddress.Contains("1.1.1.1") ? String.Format("{0}v6", tokens[1]) : String.Format("{0}v4", tokens[1]),
                        port_number = foreginAddress.Split(':')[1],
                        process_name = tokens[1] != "TCP" ? LookupProcess(Convert.ToInt16(tokens[5])) : LookupProcess(Convert.ToInt16(tokens[4]))
                    });
                }
                }
            {
                //Json output kodları
                string jsonString = JsonSerializer.Serialize(WeatherForecast);
                jsonString = JsonSerializer.Serialize(WeatherForecast);
                File.WriteAllText(foreginAddress, jsonString);

                var strB = new StringBuilder();

                for (int i = 0; i < 10; i++)
                {
                    strB.Append("(port:443, foreginAddress:'51.103.5.159' ),");
                    strB.Append("(port:27032, foreginAddress:'155.133.226.75' ),");
                    strB.Append("(port:443, foreginAddress:'199.232.58.214' ),");
                    strB.Append("(port:5228, foreginAddress:'74.125.144.188' ),");
                    strB.Append("(port:9354, foreginAddress:'40.84.185.67' ),");

                }
            }



    }

        private static object LookupProcess(short v) => throw new NotImplementedException();


        // ===============================================
        // Liste Hazırlıyoruz 
        // ===============================================
        public class Port
        {
            internal object process_name;
            internal string port_number;
            internal string protocol;

            public string Name
            {
                get
                {
                    return string.Format("{0} ({1} port {2})", this.Process_name, this.Protocol, this.Port_number);
                }
                set { }
            }
            public string Port_number { get; set; }
            public string Process_name { get; set; }
            public string Protocol { get; set; }
        }
    }
}
