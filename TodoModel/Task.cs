using System;
using System.Collections;
using System.Collections.Generic;

namespace TodoModel {

    public class Task : IEnumerable, IEnumerable<Task> {
        // Параметры, которые отвечают за положение в иерархии задач
        private Task Parent { get; set; }
        private List<Task> ChildTasks { get; set; } = new List<Task>();

        // Идентификационные параметры задач
        public Guid Guid { get; private set; }

        // Параметры, отвечающие за содержимое задачи
        public string Text { get; private set; }

        // Параметры, которые относятся к статусу задачи
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

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public IEnumerator<Task> GetEnumerator() {
            yield return this;
            foreach (Task task in this.ChildTasks) {
                yield return task;
            }
        }

        public override string ToString() {
            return string.Format(
                    "{0} (creation date: '{1}', status: '{2}', guid: '{3}', parent: '{4}', children: '{5}', percent: '{6}')",
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
