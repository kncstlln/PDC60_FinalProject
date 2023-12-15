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
	public partial class UpdateAttendancePage : ContentPage
	{
        private Students _selectedStudent;
        public const string ApiUrl = "http://192.168.100.86/PDC60_api/attendance-update.php";

        public UpdateAttendancePage (Students selectedStudent)
		{
			InitializeComponent ();
            InitializeComponent();
            _selectedStudent = selectedStudent;
            BindingContext = _selectedStudent;
            SetCurrentDate();
        }

        private void SetCurrentDate()
        {
            dateEntry.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private async void OnPresentClicked(object sender, EventArgs e)
        {
            await RecordAttendance("Present");
        }

        private async void OnAbsentClicked(object sender, EventArgs e)
        {
            await RecordAttendance("Absent");
        }

        private async Task RecordAttendance(string status)
        {
            try
            {
                var attendanceRecord = new
                {
                    status = status
                };

                string jsonData = JsonConvert.SerializeObject(attendanceRecord);

                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(ApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Success", $"Attendance recorded successfully as {status}", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error recording attendance", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await DisplayAlert("Error", $"Error: {ex.Message}", "OK");
            }
        }
    }
}

