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
    public class Students
    {
        public int id { get; set; }
        public string name { get; set; }
        public string section { get; set; }
        public string level { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public string mobile_num { get; set; }
        public string birthdate { get; set; }

    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentList : ContentPage
    {
        private const string retrieve = "http://192.168.100.86/PDC60_api/api_r2.php";
        private const string search = "http://192.168.100.86/PDC60_api/api-search.php";
        private ObservableCollection<Students> _studentsList;
        private HttpClient _Client = new HttpClient();

        public StudentList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {

            var content = await _Client.GetStringAsync(retrieve);
            var student = JsonConvert.DeserializeObject<List<Students>>(content);
            _studentsList = new ObservableCollection<Students>(student);
            studentFilter.ItemsSource = _studentsList;
            base.OnAppearing();
        }

        private async void OnRefresh(object sender, EventArgs e)
        {
            try
            {
                var content = await _Client.GetStringAsync(retrieve);
                var student = JsonConvert.DeserializeObject<List<Students>>(content);
                _studentsList = new ObservableCollection<Students>(student);
                studentFilter.ItemsSource = _studentsList;
                await DisplayAlert("Success", "Data refreshed successfully!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to refresh data. {ex.Message}", "OK");
            }
        }

        private async Task RefreshData()
        {
            try
            {
                var content = await _Client.GetStringAsync(retrieve);
                var student = JsonConvert.DeserializeObject<List<Students>>(content);
                _studentsList = new ObservableCollection<Students>(student);
                studentFilter.ItemsSource = _studentsList;
                await DisplayAlert("Success", "Data refreshed successfully!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to refresh data. {ex.Message}", "OK");
            }

        }

        private async Task DeleteRecord(Students student)
        {
            var confirm = await DisplayAlert("Confirm Deletion", "Are you sure you want to delete this record?", "Yes", "No");

            if (confirm)
            {
                try
                {
                    var urlDelete = "http://192.168.100.86/PDC60_api/api-delete.php";
                    var data = JsonConvert.SerializeObject(new { id = student.id });
                    var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(urlDelete),
                        Content = content
                    };

                    var response = await _Client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Success", "Record deleted successfully!", "OK");
                        await RefreshData();                
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to delete record. Please try again.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Are you sure you want to delete this?", mi.CommandParameter + " delete context action", "OK");
        }

        private async void StudentProfile_Tapped(Object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var selectedStudent = frame?.BindingContext as Students;
            if (selectedStudent != null)
            {
                await Navigation.PushAsync(new StudentProfile(selectedStudent));
            }
        }

        private async void AddStudentPage_Clicked(Object sender, EventArgs e)
        {
           await Navigation.PushAsync(new AddStudentPage());
        }

        private async void UpdateStudentPage_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateStudentPage());
        }
    }
}