using System;
using System.IO;
using Newtonsoft.Json;

namespace TodoModel {

    public class SettingsManager {
        // Параметры сохранения настроек
        private string HomeDirectoryName = "mefido";
        private string RootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string SettingsFileName = "settings.json";
        public FileInfo SettingsFile => new FileInfo(Path.Combine(RootDirectory, HomeDirectoryName, SettingsFileName));

        // Текущие настройки
        public Settings Settings { get; private set; }

        public SettingsManager() {
            if (SettingsFile.Exists)
                ReadSettings();
            else {
                this.Settings = new Settings();
                WriteSettings();
            }
                
        }

        public void ReadSettings() {
            string settingsString = File.ReadAllText(SettingsFile.FullName);
            this.Settings = JsonConvert.DeserializeObject<Settings>(settingsString);
        }

        public void WriteSettings() {

            string settingsString = this.Settings.ToString();
            if (!SettingsFile.Directory.Exists)
                SettingsFile.Directory.Create();

            File.WriteAllText(SettingsFile.FullName, settingsString);

        }

        public override string ToString() {
            return this.Settings.ToString();
        }

    }

}

