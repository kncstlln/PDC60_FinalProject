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
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }

    public partial class AttendancePage : ContentPage
	{
        private const string ApiUrl = "http://192.168.100.86/PDC60_api/attendance_read.php";
        public ObservableCollection<AttendanceRecord> AttendanceRecords { get; set; }

        public AttendancePage ()
		{
			InitializeComponent ();
            LoadAttendanceData();
        }
        private async void LoadAttendanceData()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(ApiUrl);
                    List<AttendanceRecord> attendanceRecords = JsonConvert.DeserializeObject<List<AttendanceRecord>>(response, new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat // Adjust based on your actual date format
                    });

                    attendanceListView.ItemsSource = attendanceRecords;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await DisplayAlert("Error", $"Error: {ex.Message}", "OK");
            }
        }

        private async void AddAttendancePage_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddAttendancePage());
        }

        private async void UpdateAttendancePage_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateAttendancePage());
        }
        private async Task<bool> DeleteAttendance(int attendanceId)
        {
            string apiUrl = "http://192.168.100.86/pdc6/attendance-delete.php";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", attendanceId.ToString())
                };
                    var response = await client.PostAsync(apiUrl, new FormUrlEncodedContent(postData));
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}

