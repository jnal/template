using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Crossover.LBS.API.Contracts.DTO;
using Crossover.LBS.Client.API;
using Crossover.LBS.Client.DTO;
using SimpleImpersonation;

namespace Crossover.LBS.Client
{
    public static class BackupHelper
    {

        public static void DoBackup(BackupDto backupConfig)
        {
            var diSource = new DirectoryInfo(backupConfig.SourcePath);
            var diTarget = new DirectoryInfo(backupConfig.DestinationPath);


            using (Impersonation.LogonUser(backupConfig.SourceDomain, backupConfig.SourceUsername, backupConfig.SourcePassword, LogonType.Network))
            {
                using (new NetworkConnection(backupConfig.DestinationPath, new NetworkCredential(backupConfig.DestinationUsername, backupConfig.DestinationPassword, backupConfig.DestinationDomain)))
                {
                    DoBackup(diSource, diTarget, backupConfig.Id);
                }
            }



        }

        public static void DoBackup(DirectoryInfo source, DirectoryInfo target, int backupConfigId)
        {

                Directory.CreateDirectory(target.FullName);

                // Copy each file into the new directory.
                foreach (FileInfo fi in source.GetFiles())
                {
                    LBSLogApi.Log(backupConfigId, $"Copied {target.FullName}\\{fi.Name}");

                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                }

                // Copy each subdirectory using recursion.
                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    var nextTargetSubDir =
                        target.CreateSubdirectory(diSourceSubDir.Name);
                    DoBackup(diSourceSubDir, nextTargetSubDir, backupConfigId);
                }
            }
        

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

       
    }
}
