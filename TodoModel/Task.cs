using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TodoModel {

    public class Task : IEnumerable, IEnumerable<Task> {
        // Параметры, которые отвечают за положение в иерархии задач
        public Task Parent { get; private set; }
        public List<Task> ChildTasks { get; private set; } = new List<Task>();
        private Dictionary<string, object> Data = new Dictionary<string, object>();

        // Свойства, отвечающие за идентификацию задачи
        public Guid Guid {
            get {
                return (Guid)this.Data["guid"];
            }
            set {
                this.Data["guid"] = value;
            }
        }

        // Параметры, отвечающие за содержимое задачи
        public string Text {
            get {
                return (string)this.Data["text"];
            }
            set {
                this.Data["text"] = value;
            }
        }

        // Параметры, которые относятся к статусу задачи
        public DateTime CreationDate {
            get {
                return (DateTime)this.Data["creation date"];
            }
            set {
                this.Data["creation date"] = value;
            }
        }

        public DateTime? EndDate { 
            get {
                return (DateTime?)this.Data["end date"];
            }
            set {
                this.Data["end date"] = value;
            }
        }
        
        public bool Done {
            get {
                return (bool)this.Data["done"];
            }
            set {
                this.Data["done"] = value;
            }
        }

        public byte Percent => CalculatePercent();

        public Task() : this(Guid.Empty, null, string.Empty) {}

        public Task(Guid guid, Task parent, string text) {
            this.Guid = guid;
            this.Text = text;
            this.Parent = parent;
            this.CreationDate = DateTime.Now;
            this.EndDate = null;
            this.Done = false;
        }

        public Task(Task parent, string text) : this(Guid.NewGuid(), parent, text) {
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public IEnumerator<Task> GetEnumerator() {
            yield return this;
            foreach (Task task in this.ChildTasks) {
                yield return task;
            }
        }

        private byte CalculatePercent() {
            if (this.ChildTasks.Count == 0) {
                return this.Done ? (byte)100 : (byte)0;
            }
            return (byte)ChildTasks.Average<Task>(t => t.Percent);
        }

        public string WriteToJsonString() {
            Data["parent guid"] = this.Parent == null ? null : Parent.Guid;
            return JsonConvert.SerializeObject(this.Data);
        }

        public void ReadFromJsonString(string jsonString) {
            Data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
        }

        public override string ToString() {
            return string.Format(
                    "{0} (creation date: '{1}', status: '{2}', guid: '{3}', parent: '{4}', children: '{5}', percent: '{6}')",
                    this.Text,
                    this.CreationDate.ToString("dd.MM.yyyy"),
                    this.Done ? $"выполнено в {((DateTime)EndDate).ToString("dd.MM.yyyy")}" : "не выполнено",
                    this.Guid.ToString(),
                    this.Parent == null ? "отсутствует" : "есть",
                    this.ChildTasks.Count == 0 ? "отсутствуют" : this.ChildTasks.Count.ToString(),
                    this.Percent.ToString()
                    );
        }
    }
}
