using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TodoModel {

    public class DataBaseManager : IEnumerable, IEnumerable<KeyValuePair<string, DataBase>> {
        public DirectoryInfo WorkingDirectory { get; private set; }
        private Dictionary<string, DataBase> BasesDict { get; set; }
        public int Count => this.BasesDict.Count();

        public DataBaseManager(string path) : this(new DirectoryInfo(path)) {
        }

        public DataBaseManager(DirectoryInfo directory) {
            ChangeDirectory(directory);
        }

        public void ChangeDirectory(string path) {
            ChangeDirectory(new DirectoryInfo(path));
        }
        
        public void ChangeDirectory(DirectoryInfo directory) {
            this.WorkingDirectory = directory;

            // Если директория не существует, создаем ее
            if (!this.WorkingDirectory.Exists)
                WorkingDirectory.Create();

            this.BasesDict = GetBases();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string, DataBase>> GetEnumerator() {
            foreach (KeyValuePair<string, DataBase> kvp in this.BasesDict) {
                yield return kvp;
            }
        }

        public DataBase this[string key] {
            get {
                return this.BasesDict[key];
            }
            set {
                if (!this.BasesDict.ContainsKey(key))
                    this.BasesDict.Add(key, value);
            }
        }

        public bool Contains(string key) {
            return this.BasesDict.ContainsKey(key);
        }

        public void CreateBase(string name) {
            if (this.Contains(name))
                throw new Exception($"Попытка создать базу с уже существующим именем '{name}'");

            DataBase newBase = new DataBase(Path.Combine(this.WorkingDirectory.FullName, name));
            this.BasesDict.Add(newBase.Name, newBase);
        }

        public void DeleteBase(string name) {
            if (!this.Contains(name))
                throw new Exception($"Попытка удалить базу ({name}), отсутствующую среди доступных");

            this.BasesDict[name].Delete();
            this.BasesDict.Remove(name);
        }

        public void RemoveAllBases() {
            foreach (string name in this.BasesDict.Keys) {
                DeleteBase(name);
            }
        }

        private Dictionary<string, DataBase>  GetBases() {
            Dictionary<string, DataBase> result = new Dictionary<string, DataBase>();

            DataBase currentBase;
            foreach (FileInfo file in this.WorkingDirectory.GetFiles()) {
                currentBase = new DataBase(file);
                result.Add(currentBase.Name, currentBase);
            }

            return result;
        }

        public override string ToString() {
            return string.Format(
                    "Расположение: '{0}' (кол: {1}){2}",
                    this.WorkingDirectory.FullName,
                    this.BasesDict.Count,
                    this.BasesDict.Count == 0 ? string.Empty : $":\n{string.Join(Environment.NewLine, this.BasesDict.Keys.Select(k => "- " + k))}"
                    );
        }

    }

}

