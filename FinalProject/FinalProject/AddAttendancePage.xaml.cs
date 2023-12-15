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
    public partial class AddAttendancePage : ContentPage
    {
        private Students _selectedStudent;
        public const string ApiUrl = "http://192.168.100.86/PDC60_api/attendance-create.php";

        public AddAttendancePage(Students selectedStudent)
        {
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
                string studentId = studentIdEntry.Text;
                string studentName = nameEntry.Text;
                string date = dateEntry.Text;

                if (!int.TryParse(studentId, out int studentIdValue))
                {
                    await DisplayAlert("Error", "Invalid student ID", "OK");
                    return;
                }

                var attendanceRecord = new
                {
                    student_id = int.Parse(studentId),
                    student_name = studentName,
                    attendance_date = DateTime.Parse(date),
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