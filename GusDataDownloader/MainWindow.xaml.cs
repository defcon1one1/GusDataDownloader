using GusDataDownloader.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace GusDataDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Area> AllAreas { get; set; }
        public List<Area> DisplayedAreas { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AllAreas = await DownloadAreas();
            DataContext = this;

            int defaultPageNumber = 1;
            int defaultRecordsPerPage = 10;
            UpdateDisplayedAreas(defaultPageNumber, defaultRecordsPerPage);
        }


        private async Task<List<Area>> DownloadAreas()
        {
            string url = "https://api-dbw.stat.gov.pl/api/1.1.0/area/area-area";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Area>>(json).OrderBy(a => a.Id).ToList(); // sort by id
            }
        }

        private void UpdateDisplayedAreas(int pageNumber, int recordsPerPage)
        {
            int startIndex = (pageNumber - 1) * recordsPerPage;
            DisplayedAreas = AllAreas.GetRange(startIndex, recordsPerPage);

            OnPropertyChanged(nameof(DisplayedAreas));
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int pageNumber = Int32.Parse(pageNumberTextBox.Text);
                int records = Int32.Parse(recordsPerPageComboBox.Text);
                UpdateDisplayedAreas(pageNumber, records);
            }
            catch (FormatException formatException)
            {
                MessageBox.Show("Wrong page number format");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}