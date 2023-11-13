using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TodoModel {

    public class Manager : IEnumerable, IEnumerable<Task> {
        private Dictionary<Guid, Task> TasksDict { get; set; } = new Dictionary<Guid, Task>();
        private List<Task> RootTasks { get; set; } = new List<Task>();
        
        public Settings Settings { get; private set; }

        public Manager() {
            //this.Settings = ReadSettings();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public IEnumerator<Task> GetEnumerator() {
            foreach (Task rootTask in this.RootTasks) {
                foreach (Task task in rootTask) {
                    yield return task;
                }
            }

        }

        public void ReadFromFile() {
        }

        public void ReadFromFile(FileInfo file) {
            // TODO: Реализовать чтение данных из файла
        }

        public void SaveToFile() {
        }

        public void SaveToFile(FileInfo file) {
            // TODO: Реализовать сохранение данных в файл
        }

        public override string ToString() {
            return string.Empty;
        }

    }


}
