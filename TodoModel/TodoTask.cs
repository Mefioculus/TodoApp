using System;
using System.Collections.Generic;

namespace TodoModel {

    public class Task {
        private List<Task> ChildTasks { get; set; }
        public string Text { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime EndDate { get; private set; }


    }

}
