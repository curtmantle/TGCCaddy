using DevExpress.Mvvm;

namespace TGCCaddy.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase currentViewModel;

        public MainWindowViewModel()
        {
            SetCurrentToCalculator();
        }

        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                this.currentViewModel = value;
                RaisePropertyChanged(()=>CurrentViewModel);
            }
        }

        private void SetCurrentToCalculator()
        {
            this.CurrentViewModel = new CalculatorViewModel();
        }

        private void SetCurrentToElevationWindTester()
        {
            this.CurrentViewModel = new ElevationWindTestViewModel();
        }
    }
}