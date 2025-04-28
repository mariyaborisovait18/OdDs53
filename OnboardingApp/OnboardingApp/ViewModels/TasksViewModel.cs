using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingApp.ViewModels
{
    public class TasksViewModel : Screen
    {
        //переключение на главное меню
        public EventHandler<EventArgs> GoToMainMenuEventHandler;

        public void GoToMainMenuCommand()
        {
            GoToMainMenuEventHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
