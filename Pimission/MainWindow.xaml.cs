using Pimission.Models;
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
    public partial class MainWindow : Window
    {
        PiViewModel model = new PiViewModel();
        List<int> sizeLists = new List<int>();


        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = model.collections;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DelayCallback(async () =>
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());

                int sampleSize = int.Parse(sampleSizeText.Text);
                if (!sizeLists.Contains(sampleSize))
                {
                    sizeLists.Add(sampleSize);
                    PiModel piModel = new PiModel(sampleSize);
                    model.Add(piModel);
                    PIMission pIMission = new PIMission(sampleSize);
                    double value = await pIMission.Calculate();
                    piModel.Value = value;
                }
            }, 400);

        }
    }
}