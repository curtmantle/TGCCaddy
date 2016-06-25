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
        private IList<IClub> clubs;

        private DelegateCommand calculateCommand;
        private int distance;
        private int roll;
        private IWindAdjusterFactory windAdjusterFactory;
        private double liePercent;
        private int targetDistance;
        private bool useWindElevation;

        public CalculatorViewModel()
        {
            CreateClubs();
            CreateWindFactory();
            CreateCalculator();
            LiePercent = 1;
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
            get { return distance; }
            set
            {
                distance = value;
                RaisePropertyChanged(()=>Distance);
            }
        }

        /// <summary>
        /// Gets or sets the elevation of the target
        /// </summary>
        public int Elevation
        {
            get { return this.windAdjusterFactory.Elevation; }
            set
            {
                this.windAdjusterFactory.Elevation = value; 
                RaisePropertyChanged(()=>Elevation);
            }
        }

        /// <summary>
        /// Gets or sets the expected roll
        /// </summary>
        public int Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                RaisePropertyChanged(()=>Roll);
            }
        }

        /// <summary>
        /// gets or sets the wind speed
        /// </summary>
        public int WindSpeed
        {
            get { return this.windAdjusterFactory.WindSpeed; }
            set
            {
                this.windAdjusterFactory.WindSpeed = value; 
                RaisePropertyChanged(()=>WindSpeed);

            }
        }

        /// <summary>
        /// Gets or sets the direction of the wind
        /// </summary>
        public int WindDirection
        {
            get { return this.windAdjusterFactory.WindDirection; }
            set
            {
                this.windAdjusterFactory.WindDirection = value;
                RaisePropertyChanged(()=>WindDirection);
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

        /// <summary>
        /// Gets or sets the lie percent
        /// </summary>
        public double LiePercent
        {
            get { return this.liePercent; }
            set
            {
                this.liePercent = value;
                RaisePropertyChanged(()=>LiePercent);
            }
        }

        /// <summary>
        /// Gets the calulated target distance
        /// </summary>
        public int TargetDistance
        {
            get { return targetDistance; }
            private set
            {
                targetDistance = value; 
                RaisePropertyChanged(()=>TargetDistance);
            }
        }

        public bool UseWindElevation
        {
            get { return useWindElevation; }
            set
            {
                useWindElevation = value; 
                RaisePropertyChanged(()=>UseWindElevation);
            }
        }

        /// <summary>
        /// Gets the results of the latest calculation
        /// </summary>
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
        /// Command for calculation
        /// </summary>
        public DelegateCommand CalculateCommand
        {
            get { return this.calculateCommand ?? (this.calculateCommand = new DelegateCommand(Calculate)); }
        }

        /// <summary>
        /// Creates the clubs
        /// </summary>
        private void CreateClubs()
        {
            var clubGenerator = new ClubGenerator();
            this.clubs = clubGenerator.GetClubs();
        }

        /// <summary>
        /// Creates the calculator
        /// </summary>
        private void CreateCalculator()
        {

            this.shotCalculator = new ShotCalculator(this.clubs, this.windAdjusterFactory);
            shotCalculator.MaximumDistance = 2;
        }

        /// <summary>
        /// Creates the wind factory
        /// </summary>
        private void CreateWindFactory()
        {
            this.windAdjusterFactory = new WindAdjusterFactory();
        }

        /// <summary>
        /// Calculates
        /// </summary>
        public void Calculate()
        {
            TargetDistance = GetTargetDistance();
            
            var result = this.shotCalculator.Calculate(TargetDistance);

            this.Results = new ObservableCollection<IShotResult>(result);

            this.LiePercent = 1;
        }

        /// <summary>
        /// Gets the target distance
        /// </summary>
        /// <returns></returns>
        private int GetTargetDistance()
        {
            var elevationAdjuster = new ElevationAdjuster(0.27);

            var targetDistanceCalculator = new TargetDistanceCalculator(elevationAdjuster)
            {
                Elevation = this.Elevation,
                Distance = this.Distance,
                Roll = this.Roll,
                LiePercent =  this.LiePercent
            };

            return targetDistanceCalculator.GetTargetDistance();

        }
    }
}
