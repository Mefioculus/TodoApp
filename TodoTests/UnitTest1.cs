using System;
using Xunit;
using TodoModel;


namespace TodoTests
{
    public class UnitTest1 {
        [Fact]
        public void TaskTest() {
            Task task = new Task(Guid.NewGuid(), null, "Тестовая задача");
            Console.WriteLine(task.ToString());
        }

        [Fact]
        public void CreatingSettingsManager() {

            SettingsManager manager = new SettingsManager();
            Console.WriteLine(manager.ToString());

        }
    }
}
