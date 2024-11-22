using System.IO;
using System.IO.Compression;
using System.Net;
using FortniteProjectCreator;

namespace FortniteProjectCreator {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Fortnite Project Creator - Written by @denalisun (on Discord)\n");

            var fnpLAD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FortniteProjectCreator");
            if (!Directory.Exists(fnpLAD))
                Directory.CreateDirectory(fnpLAD);

            Dictionary<string, string> StoredVersions = Utils.CheckForBackups();

            string ProjectVersion = "INVALID";
            while (ProjectVersion == "INVALID")
                ProjectVersion = Utils.GetVersion();
            Console.WriteLine($"[ Selected Version: {ProjectVersion} ]");

            Console.Write("Enter project name\n> ");
            string? ProjectName = Console.ReadLine();

            bool bWasCopied = false;
            if (!StoredVersions.ContainsKey(ProjectVersion)) {
                using (var Client = new WebClient())
                    Client.DownloadFile(new System.Uri($"https://codeload.github.com/FortniteModdingHub/FNGameProj/zip/refs/heads/%2B%2BFortnite%2BRelease-{ProjectVersion}"), Path.Combine(Directory.GetCurrentDirectory(), "UEProject.zip"));
            } else {
                File.Copy(Utils.GetBackedUpProjectFromVersion(ProjectVersion), Path.Combine(Directory.GetCurrentDirectory(), "UEProject.zip"));
                bWasCopied = true;
            }

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "UEProject.zip")))
            {
                ZipFile.ExtractToDirectory(Path.Combine(Directory.GetCurrentDirectory(), "UEProject.zip"), Directory.GetCurrentDirectory());

                var newDir = Path.Combine(Directory.GetCurrentDirectory(), $"FNGameProj--Fortnite-Release-{ProjectVersion}");
                if (ProjectName != null && Directory.Exists(newDir))
                    Directory.Move(newDir, Path.Combine(Directory.GetCurrentDirectory(), ProjectName));

                if (!bWasCopied)
                    File.Move(
                        Path.Combine(Directory.GetCurrentDirectory(), "UEProject.zip"),
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            $"FortniteProjectCreator/FNGameProj--Fortnite-Release-{ProjectVersion}"));
                else
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "UEProject.zip"));
            }
        }
    }
}