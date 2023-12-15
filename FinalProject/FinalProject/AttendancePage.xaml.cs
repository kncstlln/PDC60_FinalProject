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
using System.Diagnostics;

namespace FinalProject
{
    public class AttendanceRecord
    {
        public int id { get; set; }
        public int student_id { get; set; }
        public string student_name { get; set; }
        public string attendance_date { get; set; }
        public string status { get; set; }
       
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendancePage : ContentPage
	{
        private Students _selectedStudent;
        private ObservableCollection<AttendanceRecord> attendanceRecord;
        private const string ApiUrl = "http://192.168.100.86/PDC60_api/attendance-read.php";

        public AttendancePage(Students selectedStudent)
        {
            InitializeComponent();
            _selectedStudent = selectedStudent;
            BindingContext = _selectedStudent;
            attendanceRecord = new ObservableCollection<AttendanceRecord>();
            LoadAttendance();
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

        private async void LoadAttendance()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(ApiUrl);
                var attendanceData = JsonConvert.DeserializeObject<List<AttendanceRecord>>(response);
                var filteredAttendanceData = attendanceData.Where(record => record.student_id == _selectedStudent.id);
                foreach (var record in filteredAttendanceData)
                {
                    var frame = new Frame
                    {
                        Padding = new Thickness(20, 10),
                        BackgroundColor = Color.FromHex("#F3F3F3")
                    };

                    var tapGesture = new TapGestureRecognizer();
                    tapGesture.Tapped += async (sender, e) =>
                    {
                        // Navigate to UpdateAttendancePage
                        await Navigation.PushAsync(new UpdateAttendancePage());
                    };
                    frame.GestureRecognizers.Add(tapGesture);
                    var grid = new Grid();
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250) });
                    var dateLabel = new Label
                    {
                        Text = record.attendance_date,
                        FontFamily = "MontserratM",
                        TextColor = Color.Black,
                        FontSize = 14,
                        HorizontalTextAlignment= TextAlignment.Center
                    };

                    var statusLabel = new Label
                    {
                        Text = record.status,
                        FontFamily = "MontserratM",
                        TextColor = Color.Black,
                        FontSize = 14,
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    if (record.status.Equals("Present", StringComparison.OrdinalIgnoreCase))
                    {
                        statusLabel.TextColor = Color.Green;
                    }
                    else if (record.status.Equals("Absent", StringComparison.OrdinalIgnoreCase))
                    {
                        statusLabel.TextColor = Color.Red;
                    }
                    else
                    {
                        
                        statusLabel.TextColor = Color.Black;
                    }
                    grid.Children.Add(dateLabel, 0, 0);
                    grid.Children.Add(statusLabel, 1, 0);
                    frame.Content = grid;
                    attendanceStackLayout.Children.Add(frame);
                }
            }
        }
        private async void AddAttendancePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAttendancePage(_selectedStudent));
        }

        private async void UpdateAttendancePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateAttendancePage());
        }
    }
}

