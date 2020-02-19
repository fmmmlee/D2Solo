using System.ComponentModel;
using System.Windows;

namespace D2Solo.UI
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private static MainPageViewModel onlyInstance;

        private bool soloEnabled;
        private bool canToggle;
        private string statusLabel;
        private string solobtnimg;
        private string lockBtnBg;
        private string minBtnBg;
        private Visibility uIVisibility;

        public static MainPageViewModel getInstance()
        {
            if (onlyInstance == null)
                onlyInstance = new MainPageViewModel();

            return onlyInstance;
        }

        private MainPageViewModel()
        {
            SoloEnabled = false;
            CanToggle = true;
        }

        public bool SoloEnabled
        {
            get { return soloEnabled; }
            set { 
                soloEnabled = value;
                StatusLabel = soloEnabled ? "Solo Mode" : "Matchmaking";
                MinBtnBg = soloEnabled ? "Yellow" : "LightGreen";
                SoloBtnImg = soloEnabled ? "/Resources/ironwolflogo.jpg" : "/Resources/ironwolveslogo.jpg";
                propertyChanged($"{nameof(SoloEnabled)}");
            }
        }

        public Visibility UIVisibility
        {
            get { return uIVisibility; }
            set
            {
                uIVisibility = value;
                propertyChanged($"{nameof(UIVisibility)}");
            }
        }

        public string SoloBtnImg
        {
            get { return solobtnimg; }
            set { solobtnimg = value; propertyChanged("SoloBtnImg"); }
        }

        public string MinBtnBg
        {
            get { return minBtnBg; }
            set { minBtnBg= value; propertyChanged("MinBtnBg"); }
        }

        public string LockBtnBg
        {
            get { return lockBtnBg; }
            set { lockBtnBg = value; propertyChanged("LockBtnBg"); }
        }

        public string StatusLabel
        {
            get { return statusLabel; }
            set { statusLabel = value; propertyChanged("StatusLabel"); }
        }

        public bool CanToggle
        {
            get { return canToggle; }
            set 
            { 
                canToggle = value;
                LockBtnBg = canToggle ? "Black" : "DarkRed";
                propertyChanged("CanToggle");
            }
        }

        //Add:

        //minimize button, resizes window to tiny blip that when clicked expands to regular size
        //close button (should do "are you sure"), removes rules if applied and exits
        //lock button

        /*** Notify UI of Changed Binding Items ***/
        /// <summary>
        /// An event used to indicate that a property's value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// A method used in notifying that a property's value has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed value.</param>
        public void propertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
