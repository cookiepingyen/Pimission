using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pimission.Models
{
    public class PiModel : INotifyPropertyChanged
    {
        public long SampleSize { get; set; }
        public DateTime Time { get; set; }

        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }



        public PiModel(long samplesize, double value)
        {
            this.SampleSize = samplesize;
            this.Time = DateTime.Now;
            this.Value = value;
        }

        public PiModel(long samplesize)
        {
            this.SampleSize = samplesize;
            this.Time = DateTime.Now;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
