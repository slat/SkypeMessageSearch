using System;
using System.IO;
using System.Linq;

namespace SkypeMessageSearch
{
    public class ProfileSelectViewModel
    {
        private const string DatabaseName = "main.db";
        private string[] _profileDirectories;
        public string Profile { get; set; }

        public string ProfileDatabasePath
        {
            get
            {
                if (Profile == null)
                    return null;

                return Path.Combine(SkypePath, Profile, DatabaseName);
            }
        }

        /// <summary>
        /// Get Skype profile directory names from Windows AppData directory.
        /// Assumed directories within AppData\Roaming\Skype that contain a main.db SQLite database file are Skype profile directories.
        /// </summary>
        public string[] ProfileDirectories
        {
            get
            {
                if (_profileDirectories != null)
                    return _profileDirectories;

                var possibleDirectories = Directory.GetDirectories(SkypePath);
                return _profileDirectories = possibleDirectories
                    .Where(x => File.Exists(Path.Combine(x, DatabaseName)))
                    .Select(x => Path.GetFileName(Path.GetDirectoryName(Path.Combine(x, DatabaseName))))
                    .ToArray();
            }
        }

        private static string SkypePath
        {
            get
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(appDataPath, "Skype");
            }
        }
    }
}
