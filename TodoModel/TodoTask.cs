using System;
using System.Collections.Generic;

namespace TodoModel {

    public class Task {
        public Guid Guid { get; private set; }
        private Task Parent { get; set; }
        private List<Task> ChildTasks { get; set; }
        public string Text { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool Done { get; private set; }
        public byte Percent { get; private set; }




        public Task(Guid guid, Task parent, string text) {
            this.Guid = guid;
            this.Text = text;
            this.Parent = parent;
            this.CreationDate = DateTime.Now;
            this.Done = false;
        }

        public Task(Task parent, string text) : this(Guid.NewGuid(), parent, text) {
        }

        public override string ToString() {
            return string.Format(
                    "{0} (Date: {1}, Status: {2}, guid: {3}, родитель: {4}, потомков: {5}, процент выполнения: {6})",
                    this.Text,
                    this.CreationDate.ToString("dd.MM.yyyy"),
                    this.Done ? $"выполнено в {EndDate.ToString("dd.MM.yyyy")}" : "не выполнено",
                    this.Guid.ToString(),
                    this.Parent == null ? "отсутствует" : "есть",
                    this.ChildTasks.Count == 0 ? "отсутствуют" : this.ChildTasks.Count.ToString(),
                    this.Percent.ToString()
                    );
        }
    }


}
