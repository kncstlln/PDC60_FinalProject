using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

using System.Net.Http;

using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace FinalProject
{
    public class AcademicRecord
    {
        public int id { get; set; }
        public int student_id { get; set; }
        public decimal gpa { get; set; }
        public string status { get; set; }
        public int year { get; set; }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcademicHistory : ContentPage
	{
        private Students _selectedStudent;
        private ObservableCollection<AcademicRecord> academicHistoryList;
        private const string academicHistory_read = "http://192.168.100.86/PDC60_api/academichistory-read.php";


        public AcademicHistory (Students selectedStudent)
		{
			InitializeComponent ();
            _selectedStudent = selectedStudent;
            BindingContext = _selectedStudent;
            academicHistoryList = new ObservableCollection<AcademicRecord>();
            LoadAcademicHistory();


        }

        public Students SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                if (_selectedStudent != value)
                {
                    _selectedStudent = value;
                    OnPropertyChanged(nameof(SelectedStudent));

                
                    BindingContext = this;
                }
            }
        }

        private async void LoadAcademicHistory()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(academicHistory_read);
                var academicData = JsonConvert.DeserializeObject<List<AcademicRecord>>(response);

                
                var filteredAcademicHistory = academicData.Where(record => record.student_id == _selectedStudent.id);

                foreach (var record in filteredAcademicHistory)
                {
                    var frame = new Frame
                    {
                        Padding = new Thickness(20, 10),
                        BackgroundColor = Color.FromHex("#F3F3F3")
                    };

                    var grid = new Grid();
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60) });

                    var yearLabel = new Label
                    {
                        Text = record.year.ToString(),
                        FontFamily = "MontserratM",
                        TextColor = Color.Black,
                        FontSize = 14
                    };

                    var gpaLabel = new Label
                    {
                        Text = record.gpa.ToString(),
                        FontFamily = "MontserratM",
                        TextColor = Color.Black,
                        FontSize = 14
                    };

                    var remarksLabel = new Label
                    {
                        Text = record.status,
                        FontFamily = "MontserratM",
                        TextColor = Color.Black,
                        FontSize = 14
                    };

                    grid.Children.Add(yearLabel, 0, 0);
                    grid.Children.Add(gpaLabel, 1, 0);
                    grid.Children.Add(remarksLabel, 2, 0);

                    frame.Content = grid;

                    academicHistoryStackLayout.Children.Add(frame);
                }
            }
        }

        private async void OnAddAcademicHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAcademicHistoryPage(_selectedStudent));
        }

        private async void OnUpdateAcademicHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateAcademicHistoryPage());
        }




    }
}

