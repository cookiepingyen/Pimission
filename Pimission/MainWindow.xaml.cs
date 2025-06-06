using Pimission.Contracts;
using Pimission.Models;
using Pimission.Presenters;
using Pimission.Service;
using Pimission.Utility;
using Pimission.ViewModels;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer = System.Threading.Timer;

namespace Pimission
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPiMissionWindow
    {
        public PiViewModel viewModel { get; set; } = new PiViewModel();
        PimissionPresenter pimissionPresenter;
        public Timer timer;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            this.pimissionPresenter = new PimissionPresenter(this);
            pimissionPresenter.StartMission();

            timer = new Timer(IntervalCallback, null, 0, 1000);
        }

        private void AddMissionButton_Click(object sender, RoutedEventArgs e)
        {
            this.DebounceTime(() =>
            {
                int sampleSize = int.Parse(sampleSizeText.Text);
                pimissionPresenter.SendMissionRequest(sampleSize);
            }, 400);

        }

        private void IntervalCallback(object state)
        {
            pimissionPresenter.FetchMissionDatas();
        }

        public void RenderDatas(List<PiModel> piModels)
        {
            this.Dispatcher.Invoke(() =>
            {
                viewModel.datas = piModels;
            });
        }
    }
}