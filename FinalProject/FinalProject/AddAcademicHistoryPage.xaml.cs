using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace FinalProject
{
    public partial class AddAcademicHistoryPage : ContentPage
    {
        public const string ApiUrl = "http://192.168.100.86/PDC60_api/academichistory-create.php";
        public AddAcademicHistoryPage()
        {
            InitializeComponent();

        }

        private async void CancelAddAcademicHistory(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SubmitAddAcademicHistory(Object sender, EventArgs e)
        {
            try
            {
                // Retrieve data from entry fields
                string studentId = studentIdEntry.Text;
                string studentName = nameEntry.Text; 
                string gpa = gpaEntry.Text;
                string status = statusEntry.Text;
                string year = yearEntry.Text;
                
                // Validate if required fields are not empty
                if (string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(gpa) || string.IsNullOrWhiteSpace(status) || string.IsNullOrWhiteSpace(year))
                {
                    await DisplayAlert("Error", "All fields are required", "OK");
                    return;
                }

                // Create a data object
                var academicRecord = new
                {
                    student_id = studentId,
                    name = studentName,
                    gpa = gpa,
                    status = status,
                    year = year,
                };
                string jsonData = JsonConvert.SerializeObject(academicRecord);

                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(ApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Success", "Academic record added successfully", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error adding academic record", "OK");
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


  

