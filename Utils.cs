using System;
using System.IO;
using System.Net;

namespace FortniteProjectCreator {
    static class Utils {
        public static string GetVersion() {
            Console.Write("Enter Fortnite Version (Valid Versions: 7.40, 8.51, 9.10, 14.30)\n> ");
            string? version = Console.ReadLine();

            string[] ValidVersions = ["7.40", "8.51", "9.10", "14.30"]; // Doesn't include 23.00 because it's deprecated
            if (!ValidVersions.Contains(version))
            {
                Console.WriteLine("Invalid version provided!");
                return "INVALID";
            }

            if (version != null)
                return version;
            else
                return "INVALID";
        }

        public static string GetBackedUpProjectFromVersion(string Version) {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"FortniteProjectCreator/FNGameProj--Fortnite-Release-{Version}");
        }

        public static Dictionary<string, string> CheckForBackups() {
            Dictionary<string, string> StoredVersions = new Dictionary<string, string>();

            string[] ValidVersions = ["7.40", "8.51", "9.10", "14.30"];
            foreach (string Version in ValidVersions) {
                var Pth = GetBackedUpProjectFromVersion(Version);
                if (File.Exists(Pth)) {
                    StoredVersions.Add(Version, Pth);
                }
            }

            return StoredVersions;
        }
    }
}