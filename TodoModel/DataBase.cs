using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TodoModel {

    public class DataBase {
        public FileInfo DBFile { get; private set; }

        public bool Exists => this.DBFile.Exists;
        public string FullName => this.DBFile.FullName;
        public string Name => this.DBFile.Name;

        public DataBase(string path) : this(new FileInfo(path)) {}

        public DataBase(FileInfo fileInfo) {
            this.DBFile = fileInfo;

            // При отсутствии файла создаем его
            if (!this.Exists)
                this.DBFile.Create();
        }

        public string[] Read() {
            return File.ReadAllLines(this.FullName);
        }

        public void Write(string text) {
            // Создаем файл, если он до этого отсутствовал
            File.WriteAllText(this.FullName, text);
        }

        public void Write(IEnumerable<string> lines) {
            this.Write(string.Join(Environment.NewLine, lines));
        }

        public void Delete() {
            File.Delete(this.DBFile.FullName);
        }

        public override string ToString() {
            return string.Format(
                    "Директория: {0}, Имя: {1}, Размер: {2} байт",
                    this.DBFile.Directory.FullName,
                    this.DBFile.Name,
                    this.DBFile.Length
                    );
        }
    }


}
