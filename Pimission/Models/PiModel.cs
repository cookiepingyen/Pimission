using Pimission.Enums;
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

        private MissionState _state;
        public MissionState State
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _value;

        public CancellationTokenSource CancellationTokenSource { get; set; }
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
            CancellationTokenSource = new CancellationTokenSource();
        }

        public PiModel(long samplesize)
        {
            this.SampleSize = samplesize;
            this.Time = DateTime.Now;
            CancellationTokenSource = new CancellationTokenSource();

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
