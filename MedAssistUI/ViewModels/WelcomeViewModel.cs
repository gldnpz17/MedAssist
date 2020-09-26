using MedAssistUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAssistBusinessLogic;

namespace MedAssistUI.ViewModels
{
    public class WelcomeViewModel : ViewModelBase, IWelcomeViewModel
    {
        private MedAssistBLLibrary _medAssistLib;
        public WelcomeViewModel(MedAssistBLLibrary medAssistLib)
        {
            _medAssistLib = medAssistLib;

            WelcomeMessage = $"Welcome to MedAssist, {medAssistLib.Authorization.GetUserInfo().Username}";
        }

        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            private set
            {
                _welcomeMessage = value;
                OnPropertyChanged();
            }
        }
    }
}
