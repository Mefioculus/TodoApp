using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoModel {

    public class Settings {
        public string SaveDirectory { get; set; }
        public string FileName { get; set; }

        public Settings() {
            this.SaveDirectory = GetDefaultSaveDirectory();
            this.FileName = GetFileName();
        }

        private string GetDefaultSaveDirectory() {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private string GetFileName() {
            return "mefido.txt";
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}
