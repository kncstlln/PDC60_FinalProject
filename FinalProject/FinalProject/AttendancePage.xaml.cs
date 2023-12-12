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
    public class AttendanceRecord
    {
        public string Date { get; set; }
        public string Status { get; set; }
    }

    public partial class AttendancePage : ContentPage
	{

        public ObservableCollection<AttendanceRecord> AttendanceRecords { get; set; }

        public AttendancePage ()
		{
			InitializeComponent ();
            AttendanceRecords = new ObservableCollection<AttendanceRecord>
            {
                new AttendanceRecord { Date = "2023-01-01", Status = "Present" },
                new AttendanceRecord { Date = "2023-01-02", Status = "Absent" },
                new AttendanceRecord { Date = "2023-01-03", Status = "Present" },
                new AttendanceRecord { Date = "2023-01-04", Status = "Present" },
                new AttendanceRecord { Date = "2023-01-05", Status = "Absent" },
                new AttendanceRecord { Date = "2023-01-06", Status = "Present" },
                new AttendanceRecord { Date = "2023-01-07", Status = "Absent" },
                new AttendanceRecord { Date = "2023-01-08", Status = "Present" },
                new AttendanceRecord { Date = "2023-01-09", Status = "Present" },
                new AttendanceRecord { Date = "2023-01-10", Status = "Absent" }
            };

            attendanceListView.ItemsSource = AttendanceRecords;

        }

        private async void AddAttendancePage_Clicked(Object sender, EventArgs e)
        {

            await Navigation.PushAsync(new AddAttendancePage());
         
        }

        private async void UpdateAttendancePage_Clicked(Object sender, EventArgs e)
        {

            await Navigation.PushAsync(new UpdateAttendancePage());

        }


    }
}

