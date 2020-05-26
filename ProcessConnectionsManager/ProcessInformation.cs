using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace ProcessConnectionsManager
{
    public struct Port
    {
        public string PortNumber;
        public string Protocol;
        public string ForeignIP;
    }

    public static class ProcessInformation
    {
        public static List<Port> GetPortsByProcessName(string processName, out Process process)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                process = null;
                return new List<Port>();
            }
            return GetPortsByPID(processes[0].Id, out process);
        }

        public static List<Port> GetPortsByPID(int pid, out Process process)
        {
            var ports = new List<Port>();

            using var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    Arguments = "-a -n -o",
                    FileName = "netstat.exe",
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            p.Start();

            StreamReader stdOutput = p.StandardOutput;
            StreamReader stdError = p.StandardError;

            string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
            string exitStatus = p.ExitCode.ToString();

            if (exitStatus != "0")
            {
                // Command Errored. Handle here if need be.
                process = null;
                return ports;
            }

            // Get the rows.
            foreach (string row in Regex.Split(content, "\r\n"))
            {
                string[] tokens = Regex.Split(row, "\\s+");
                if (tokens.Length > 4 && (tokens[1] == "UDP" || tokens[1] == "TCP"))
                {
                    int id = tokens[1] == "UDP" ? int.Parse(tokens[4]) : int.Parse(tokens[5]);
                    if (id != pid)
                    {
                        continue;
                    }

                    string port = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1").Split(':')[1];
                    string foreignAddress = tokens[1] == "UDP" ? "" : Regex.Replace(tokens[3], @"\[(.*?)\]", "1.1.1.1").Split(':')[0];
                    ports.Add(new Port
                    {
                        Protocol = tokens[1],
                        PortNumber = port,
                        ForeignIP = foreignAddress
                    });
                }
            }

            try
            {
                process = Process.GetProcessById(pid);
            }
            catch (ArgumentException)
            {
                process = null;
            }

            return ports;
        }

        public static string GetMainModuleFileName(this Process process)
        {
            const string wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";

            using var searcher = new ManagementObjectSearcher(wmiQueryString);
            using var results = searcher.Get();

            var query = from p in new Process[] { process }
                        join mo in results.Cast<ManagementObject>()
                        on p.Id equals (int)(uint)mo["ProcessId"]
                        select new
                        {
                            Path = (string)mo["ExecutablePath"]
                        };

            return query.FirstOrDefault()?.Path ?? "";
        }
    }
}
