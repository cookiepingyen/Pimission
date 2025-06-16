using IOCServiceCollection;
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
        IPiMissionPresenter pimissionPresenter;

        public MainWindow(PresenterFactory presenterFactory)
        {
            InitializeComponent();
            DataContext = this;

            pimissionPresenter = presenterFactory.Create<IPiMissionPresenter, IPiMissionWindow>(this);
            pimissionPresenter.StartMission();
        }

        private void AddMissionButton_Click(object sender, RoutedEventArgs e)
        {
            this.DebounceTime(() =>
            {
                int sampleSize = int.Parse(sampleSizeText.Text);
                PiModel pimodel = pimissionPresenter.SendMissionRequest(sampleSize);
                if (pimodel != null)
                {
                    viewModel.Add(pimodel);
                }
            }, 400);

        }

        private void CancelMissionButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Content = pimissionPresenter.MissionSwitch() ? "Cancel" : "Start";
        }

        private void PaustButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            CancellationTokenSource cancellationTokenSource = (CancellationTokenSource)button.Tag;
            cancellationTokenSource.Cancel();
        }
    }
}