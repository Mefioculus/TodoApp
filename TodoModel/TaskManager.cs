using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TodoModel {

    public class Manager : IEnumerable, IEnumerable<Task> {
        private Dictionary<Guid, Task> TasksDict { get; set; }
        private List<Task> RootTasks { get; set; }

        // Работа с тастроками
        private SettingsManager SettingsManager { get; set; }
        public Settings Settings => this.SettingsManager.Settings;

        // Работа с базой данных
        private DataBaseManager DataBaseManager { get; set; }

        public DataBase ChoosedBase => this.DataBaseManager[this.SettingsManager.Settings.ChoosedBase];
        public Dictionary<string, FileInfo> AvailableBases { get; private set; }
        

        public Manager() {

            this.SettingsManager = new SettingsManager(); // Производим загрузку настроек
            this.DataBaseManager = new DataBaseManager(this.Settings.DataBaseDirectory); // Производим загрузку базы данных

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

        public Task this[Guid key] {
            get {
                return this.TasksDict[key];
            }
            set {
                this.TasksDict[key] = value;
            }
        }

        public void ReadFromBase() {
            ReadFromBase(this.ChoosedBase);
        }

        public void ReadFromBase(DataBase db) {
            Task currentTask;
            foreach (string jsonString in db.Read()) {
                currentTask = new Task();
                currentTask.ReadFromJsonString(jsonString);

            }
        }

        public void SaveToBase() {
            SaveToBase(this.ChoosedBase);
        }

        public void SaveToBase(DataBase db) {
            db.Write(this.Select(task => task.WriteToJsonString()));
        }

        public override string ToString() {
            return string.Empty;
        }

    }


}
