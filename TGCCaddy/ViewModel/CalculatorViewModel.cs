using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using TGCCaddy.Model;

namespace TGCCaddy.ViewModel
{

    public class CalculatorViewModel : ViewModelBase
    {
        private IShotCalculator shotCalculator;
        private ObservableCollection<IShotResult> results;


        private DelegateCommand calculateCommand;
        private int _distance;
        private int _elevation;
        private int _roll;
        private int _windSpeed;
        private int _windDirection;
        private int _maximumDistance;

        public CalculatorViewModel()
        {
            CreateCalculator();
        }

        public CalculatorViewModel(IShotCalculator calculator)
        {
            this.shotCalculator = calculator;
        }

        /// <summary>
        /// Gets or sets the distance of the shot
        /// </summary>
        public int Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                RaisePropertyChanged(()=>Distance);
            }
        }

        /// <summary>
        /// Gets or sets the elevation of the target
        /// </summary>
        public int Elevation
        {
            get { return _elevation; }
            set
            {
                _elevation = value; 
                RaisePropertyChanged(()=>Elevation);
            }
        }

        /// <summary>
        /// Gets or sets the expected roll
        /// </summary>
        public int Roll
        {
            get { return _roll; }
            set
            {
                _roll = value;
                RaisePropertyChanged(()=>Roll);
            }
        }

        /// <summary>
        /// gets or sets the wind speed
        /// </summary>
        public int WindSpeed
        {
            get { return _windSpeed; }
            set
            {
                _windSpeed = value; 
                RaisePropertyChanged(()=>WindSpeed);

            }
        }

        /// <summary>
        /// Gets or sets the direction of the wind
        /// </summary>
        public int WindDirection
        {
            get { return _windDirection; }
            set
            {
                _windDirection = value;
                RaisePropertyChanged(()=>WindDirection);
            }
        }

        public ObservableCollection<IShotResult> Results
        {
            get { return this.results ?? (this.results = new ObservableCollection<IShotResult>()); }
            private set
            {
                this.results = value;
                RaisePropertyChanged(()=>Results);
            }
        }

        /// <summary>
        /// Gets or sets the maximum distance from the target to 
        /// gather results
        /// </summary>
        public int MaximumDistance
        {
            get { return this.shotCalculator.MaximumDistance; }
            set
            {
                this.shotCalculator.MaximumDistance = value;
                RaisePropertyChanged(()=>MaximumDistance);
            }
        }

        public DelegateCommand CalculateCommand
        {
            get { return this.calculateCommand ?? (this.calculateCommand = new DelegateCommand(Calculate)); }
        }

        private void CreateCalculator()
        {
            var clubGenerator = new ClubGenerator();
            var clubs = clubGenerator.GetClubs();

            var windAdjusterFactory = new WindAdjusterFactory();

            this.shotCalculator = new ShotCalculator(clubs, windAdjusterFactory);
            shotCalculator.MaximumDistance = 2;
        }

        public void Calculate()
        {
            var elevationAdjuster = new ElevationAdjuster(0.27);

            var targetDistance = GetTargetDistance(elevationAdjuster);

            shotCalculator.Elevation = this.Elevation;
            shotCalculator.MaximumDistance = this.MaximumDistance;
            shotCalculator.WindSpeed = this.WindSpeed;
            shotCalculator.WindDirection = this.WindDirection;
            
            var result = this.shotCalculator.Calculate(targetDistance);

            this.Results = new ObservableCollection<IShotResult>(result);
        }

        private int GetTargetDistance(ElevationAdjuster elevationAdjuster)
        {
            var targetDistanceCalculator = new TargetDistanceCalculator(elevationAdjuster);
            targetDistanceCalculator.Elevation = this.Elevation;
            targetDistanceCalculator.Distance = this.Distance;
            targetDistanceCalculator.Roll = this.Roll;
            var targetDistance = targetDistanceCalculator.GetTargetDistance();
            return targetDistance;
        }
    }
}
