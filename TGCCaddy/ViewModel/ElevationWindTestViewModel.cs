using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using TGCCaddy.Model;

namespace TGCCaddy.ViewModel
{
    public class ShotTypeViewModel
    {
        public ShotTypeViewModel(ShotType shotType)
        {
            this.ShotType = shotType;
        }

        public ShotType ShotType { get; private set; }

        public string Name
        {
            get { return ShotType.ToString(); }
        }
    }

    public class ElevationWindTestViewModel : ViewModelBase
    {
        #region Private fields

        private readonly IElevationWindTester windTester;
        private readonly IClubGenerator clubGenerator;
        private ObservableCollection<IClub> clubLookup;
        private IList<ShotTypeViewModel> shotTypes;
        private ShotTypeViewModel selectedShotType;
        private DelegateCommand calculateCommand;
        private DelegateCommand clearCommand;

        private IElevationWindResult lastResult;
        private int carryDistance;

        #endregion


        public ElevationWindTestViewModel(IElevationWindTester windTester, IClubGenerator clubGenerator)
        {
            this.windTester = windTester;
            this.clubGenerator = clubGenerator;
            CreateClubLookupList();
            CreateShotTypes();
            this.Results = new ObservableCollection<IElevationWindResult>();
            this.lastResult = new ElevationWindResult();
        }

        public ElevationWindTestViewModel()
            : this(new ElevationWindTester(new WindAdjusterFactory(), new ElevationAdjuster()), 
                    new ClubGenerator())
        {

        }

        /// <summary>
        /// Gets a list of shot types
        /// </summary>
        public IEnumerable<ShotTypeViewModel> ShotTypes
        {
            get { return this.shotTypes; }
        }

        /// <summary>
        /// Gets the selected shot types
        /// </summary>
        public ShotTypeViewModel SelectedShotType
        {
            get { return this.selectedShotType; }
            set
            {
                this.selectedShotType = value;
                this.windTester.ShotType = value.ShotType;
                RaisePropertyChanged(()=>SelectedShotType);
            }
        }

        /// <summary>
        /// Gets the available clubs
        /// </summary>
        public ObservableCollection<IClub> ClubLookup
        {
            get { return clubLookup; }
        }

        /// <summary>
        /// Gets the selected club
        /// </summary>
        public IClub SelectedClub
        {
            get { return windTester.Club; }
            set
            {
                this.windTester.Club = value;
                RaisePropertyChanged(()=>SelectedClub);
            }
        }

        public int Step
        {
            get { return this.windTester.Step; }
            set
            {
                this.windTester.Step = value;
                RaisePropertyChanged(()=>Step);
            }
        }

        public int Elevation
        {
            get { return this.windTester.Elevation; }
            set
            {
                this.windTester.Elevation = value;
                RaisePropertyChanged(()=>Elevation);
            }
        }

        public int WindSpeed
        {
            get { return this.windTester.WindSpeed; }
            set { this.windTester.WindSpeed = value; }
        }

        public int WindDirection
        {
            get { return this.windTester.WindDirection; }
            set { this.windTester.WindDirection = value; }
        }

        public int CarryDistance
        {
            get { return carryDistance; }
            set
            {
                carryDistance = value; 
                RaisePropertyChanged(()=>CarryDistance);
            }
        }

        public ObservableCollection<IElevationWindResult> Results { get; private set; }


        public DelegateCommand CalculateCommand
        {
            get { return this.calculateCommand ?? (this.calculateCommand = new DelegateCommand(Calculate)); }
        }

        public DelegateCommand ClearCommand
        {
            get { return this.clearCommand ?? (this.clearCommand = new DelegateCommand(Clear)); }
        }

        private void Clear()
        {
            this.Results.Clear();
        }

        private void Calculate()
        {
            this.lastResult = this.windTester.GetElevationResult(this.CarryDistance);
            this.Results.Add(this.lastResult);
        }

        /// <summary>
        /// Create the club lookup list
        /// </summary>
        private void CreateClubLookupList()
        {
            this.clubLookup = new ObservableCollection<IClub>(this.clubGenerator.GetClubs());
            this.SelectedClub = this.clubLookup.First();
        }

        private void CreateShotTypes()
        {
            var shotTypeEnumList = Enum.GetValues(typeof(ShotType)).Cast<ShotType>();
            this.shotTypes = shotTypeEnumList.Select(x => new ShotTypeViewModel(x)).ToList();
            this.selectedShotType = this.shotTypes.First();
        }
    }
}
