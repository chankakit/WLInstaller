using System.ComponentModel;

namespace WLInstall.ViewModels
{
    public class ButtonIsEnable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool nxtBtnIsEnabled;
        public bool NxtBtnIsEnabled
        {
            get { return nxtBtnIsEnabled; }
            set
            {
                nxtBtnIsEnabled = value;
                OnPropertyChanged("NxtBtnIsEnabled");
            }
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
