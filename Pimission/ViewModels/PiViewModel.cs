using Pimission.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimission.ViewModels
{
    class PiViewModel
    {
        public List<PiModel> datas
        {
            set
            {
                foreach (var item in value)
                {
                    collections.Add(item);
                }
            }
        }

        public ObservableCollection<PiModel> collections { get; set; } = new ObservableCollection<PiModel>();

        public void Add(PiModel item)
        {
            collections.Add(item);
        }
    }
}
