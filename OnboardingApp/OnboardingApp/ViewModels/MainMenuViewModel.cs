using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingApp.ViewModels
{
    public class MainMenuViewModel : Screen
    {
        public EventHandler<EventArgs> GoToOfficeMapCommandEventHandler;

        public void GoToOfficeMapCommand()
        {
            GoToOfficeMapCommandEventHandler.Invoke(this, EventArgs.Empty);
        }
    }
}
