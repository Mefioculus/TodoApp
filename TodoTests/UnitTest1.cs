using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using TodoModel;


namespace TodoTests
{
    public class UnitTest1 {
        [Fact]
        public void TaskTest() {
            Console.WriteLine($"\n{new string('-', 10)} Тестирование задач {new string('-', 10)}");

            List<Task> tasks = new List<Task>();
            tasks.Add(new Task(null, "Тестовая задача 1"));
            tasks.Add(new Task(null, "Тестовая задача 2"));

            foreach (Task task in tasks) {
                Console.WriteLine(task.ToString());
                Console.WriteLine(task.WriteToJsonString());
            }

        }

        [Fact]
        public void CreatingSettingsManager() {
            Console.WriteLine($"\n{new string('-', 10)} Тестирование настроек {new string('-', 10)}");

            SettingsManager manager = new SettingsManager();
            Console.WriteLine(manager.ToString());

        }

        [Fact]
        public void WorkingWithDataBase() {
            Console.WriteLine($"\n{new string('-', 10)} Тестирование работы с базами данных {new string('-', 10)}");

            // Производим создание новой базы данных
            string pathToTestDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestMefidoDataBase");
            DataBaseManager dbManager = new DataBaseManager(pathToTestDB);

            // Производим начальную очистку для последующих тестов
            dbManager.RemoveAllBases();
            Assert.Equal(0, dbManager.Count);
            
            // Производим создание новых баз данных
            dbManager.CreateBase("testbase1");
            dbManager.CreateBase("testbase2");
            dbManager.CreateBase("testbase3");
            dbManager.CreateBase("testbase4");
            Assert.Equal(4, dbManager.Count);
            Console.WriteLine(dbManager.ToString());

            // Производим очистку баз данных
            dbManager.RemoveAllBases();
            Assert.Equal(0, dbManager.Count);
            Console.WriteLine(dbManager.ToString());

        }
    }
}
