using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using OnboardingApp.ViewModels;

namespace OnboardingApp.Services
{
    class FileIOService
    {
        private readonly string PATH;
        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<TodoModel> LoadData()
        {
            if (!File.Exists(PATH) || new FileInfo(PATH).Length == 0)
            {
                var defaultTasks = GetDefaultTasks();
                SaveData(defaultTasks);
                return new BindingList<TodoModel>(defaultTasks);
            }

            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(fileText))
                {
                    var defaultTasks = GetDefaultTasks();
                    SaveData(defaultTasks);
                    return new BindingList<TodoModel>(defaultTasks);
                }

                var list = JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
                return list ?? new BindingList<TodoModel>();
            }
        }

        public void SaveData(IEnumerable<TodoModel> todoDataList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(todoDataList, Formatting.Indented);
                writer.Write(output);
            }
        }

        private List<TodoModel> GetDefaultTasks()
        {
            return new List<TodoModel>
            {
                // Категория 1: Знакомство с компанией
                new TodoModel { Id = 1, Text = "Ознакомиться с внутренним регламентом и политикой безопасности компании", Category = "Знакомство с компанией" },
                new TodoModel { Id = 2, Text = "Заполнить анкету нового сотрудника и отправить в HR", Category = "Знакомство с компанией" },
                new TodoModel { Id = 3, Text = "Посетить вводный онбординг-семинар для новых сотрудников", Category = "Знакомство с компанией" },

                // Категория 2: Знакомство с проектом
                new TodoModel { Id = 4, Text = "Прочитать вики-слайды, технические спецификации и планы проекта", Category = "Знакомство с проектом" },
                new TodoModel { Id = 5, Text = "Установить и запустить проект на локальном окружении", Category = "Знакомство с проектом" },
                new TodoModel { Id = 6, Text = "Изучить инструменты для автоматизации сборки и тестирования", Category = "Знакомство с проектом" },

                // Категория 3: Знакомство с командой
                new TodoModel { Id = 7, Text = "Обсудить ожидания от стажировки и ключевые задачи с наставником", Category = "Знакомство с командой" },
                new TodoModel { Id = 8, Text = "Принять участие в мероприятии по развитию команды", Category = "Знакомство с командой" },
                new TodoModel { Id = 9, Text = "Изучить инструменты для коммуникации (Slack, Trello, Jira)", Category = "Знакомство с командой" },
            };
        }
    }
}
