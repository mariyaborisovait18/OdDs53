using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnboardingApp.ViewModels
{
    public class MainMenuViewModel : Screen
    {
        //переключение на карту офиса
        public EventHandler<EventArgs> GoToOfficeMapCommandEventHandler;

        public void GoToOfficeMapCommand()
        {
            GoToOfficeMapCommandEventHandler.Invoke(this, EventArgs.Empty);
        }

        //переключение на сотрудников
        public EventHandler<EventArgs> GoToEmployersCommandEventHandler;

        public void GoToEmployersCommand()
        {
            GoToEmployersCommandEventHandler.Invoke(this, EventArgs.Empty);
        }

        //переключение на задачи
        public EventHandler<EventArgs> GoToTasksCommandEventHandler;

        public void GoToTasksCommand()
        {
            GoToTasksCommandEventHandler.Invoke(this, EventArgs.Empty);
        }

        //переключение на базу знаний
        public EventHandler<EventArgs> GoToKnowlegeBaseCommandEventHandler;

        public void GoToKnowlegeBaseCommand()
        {
            GoToKnowlegeBaseCommandEventHandler.Invoke(this, EventArgs.Empty);
        }

        //переключение с базы знаний обратно в меню        
    public void OnGoToMainMenu(object sender, EventArgs e)
        {
            // Логика для отображения главного меню
            Console.WriteLine("Вернуться в Главное Меню");
            // Здесь добавьте код, чтобы показать главное меню в UI (например, переключить вид на главный интерфейс)
        }
    }
}
