using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using TGCCaddy.Model;

namespace TGCCaddy.ViewModel
{
    public class CalculatorViewModel : ViewModelBase
    {
        private IShotCalculator shotCalculator;

        public CalculatorViewModel(IShotCalculator calculator)
        {
            this.shotCalculator = calculator;
        }

        /// <summary>
        /// Gets or sets the distance of the shot
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// Gets or sets the elevation of the target
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// Gets or sets the expected roll
        /// </summary>
        public int Roll { get; set; }

        /// <summary>
        /// gets or sets the wind speed
        /// </summary>
        public int WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the direction of the wind
        /// </summary>
        public int WindDirection { get; set; }

        /// <summary>
        /// Gets or sets the maximum distance from the target to 
        /// gather results
        /// </summary>
        public int MaximumDistance { get; set; }

        private void Calculate()
        {
            var result = this.shotCalculator.Calculate();

            //create list view model
        }
    }
}
