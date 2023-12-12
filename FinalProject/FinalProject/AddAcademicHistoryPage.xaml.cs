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

        public AddAcademicHistoryPage()
        {
            InitializeComponent();
            //_selectedStudent = selectedStudent;
            //BindingContext = _selectedStudent;
        }

        private async void CancelAddAcademicHistory(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SubmitAddAcademicHistory(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //private async void SubmitAddAcademicHistory(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Retrieve data from the form
        //        int student_id = Convert.ToInt32(studentIdEntry.Text);
        //        string name = nameEntry.Text;
        //        string section = sectionEntry.Text;
        //        int year = Convert.ToInt32(yearEntry.Text);
        //        double gpa = Convert.ToDouble(gpaEntry.Text);
        //        string remarks = remarksEntry.Text;

        //        // Create an object to send to the API
        //        var academicHistoryData = new
        //        {
        //            student_id = student_id,
        //            name = name,
        //            section = section,
        //            gpa = gpa,
        //            status = remarks,
        //            year = year
        //        };


        //        string jsonData = JsonConvert.SerializeObject(academicHistoryData);


        //        var httpClient = new HttpClient();
        //        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        //        var response = await httpClient.PostAsync("http://192.168.100.86/academichistory-create.php", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Handle success, maybe display a success message or navigate back
        //            await DisplayAlert("Success", "Academic history added successfully", "OK");

        //            // Navigate back to AcademicHistory page
        //            await Navigation.PopAsync();

        //            // Retrieve and refresh academic history data on AcademicHistory page
        //            var academicHistoryPage = Navigation.NavigationStack.FirstOrDefault(page => page is AcademicHistory) as AcademicHistory;

        //        }
        //        else
        //        {
        //            // Handle failure, display an error message
        //            await DisplayAlert("Error", "Failed to add academic history", "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that may occur
        //        Console.WriteLine($"Exception: {ex.Message}");
        //    }
        //}

    }
}

