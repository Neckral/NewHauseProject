using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;

namespace HouseProjectWpfApi.ViewModel
{
    public partial class MainViewModel
    {
        private IEnumerable<ForCompareDTO>? forCompares;
        public IEnumerable<ForCompareDTO> ForCompares
        {
            get=> forCompares;
            set
            {
                forCompares = value;
                NotifyPropertyChanged(nameof(ForCompares));
            }
        }
    }
}
