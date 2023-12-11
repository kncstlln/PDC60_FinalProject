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

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Students
    {
        public int ID { get; set; }
        public string name { get; set; }

    }

    public partial class StudentList : ContentPage
    {
        public const string delete = "http://192.168.100.86/PDC60_api/api-delete.php";
        public const string search = "http://192.168.100.86/PDC60_api/api-search.php";
        public HttpClient _Client = new HttpClient();
        public ObservableCollection<Students> _studentsList;

        public StudentList()
        {
            InitializeComponent();
            LoadStudents();
        }

        private async void LoadStudents()
        {
            try
            {
                // Make a request to your API to get the list of students
                var content = await _Client.GetStringAsync("http://192.168.100.86/PDC60_api/servercon.php");

                // Deserialize the response
                var responseObject = JsonConvert.DeserializeObject<ResponseObject>(content);

                if (responseObject.status)
                {
                    var studentsResult = JsonConvert.DeserializeObject<List<Students>>(responseObject.data.ToString());

                    _studentsList = new ObservableCollection<Students>(studentsResult);
                    studentFilter.ItemsSource = _studentsList;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {responseObject.message}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async void OnMore(object sender, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)sender;
                if (mi.CommandParameter is int ID)
                {
                    bool result = await DisplayAlert("Update Records", $"Are you sure you want to update ID No: {ID}?", "OK", "CANCEL");
                    if (result)
                    {
                        Students studentsList = new Students { ID = ID };
                        await Navigation.PushAsync(new UpdatePage(studentsList));
                    }
                    else
                    {
                        //Handle Cancel Button
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in OnUpdate: {ex.Message}");
            }
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            try
            {
                var mi = (MenuItem)sender;
                if (mi.CommandParameter is int ID)
                {
                    bool result = await DisplayAlert("Delete Records", $"Are you sure you want to delete ID No: {ID}?", "OK", "CANCEL");
                    if (result)
                    {
                        Students post = new Students { ID = ID };
                        var content = JsonConvert.SerializeObject(post);
                        await _Client.PostAsync(delete, new StringContent(content, Encoding.UTF8, "application/json"));
                        await DisplayAlert("Success", $"Student with ID No: {ID} deleted successfully.", "OK");
                    }
                    else
                    {
                        //Cancel actions
                    }
                }
                else
                {
                    //handle null
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in OnDelete: {ex.Message}");
            }
        }

        public class ResponseObject
        {
            public bool status { get; set; }
            public JToken data { get; set; }
            public string message { get; set; }
        }

        private async void StudentProfile_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentProfile());
        }

        private async void OnSearchTextchanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchQuery))
            {

            }
            else
            {
                try
                {

                    var searchUrl = $"{search}?username={searchQuery}";

                    System.Diagnostics.Debug.WriteLine($"Search URL: {searchUrl}");

                    var content = await _Client.GetStringAsync(searchUrl);

                    var responseObject = JsonConvert.DeserializeObject<ResponseObject>(content);

                    if (responseObject.status)
                    {
                        var searchResult = JsonConvert.DeserializeObject<List<Students>>(responseObject.data.ToString());

                        _studentsList = new ObservableCollection<Students>(searchResult);
                        studentFilter.ItemsSource = _studentsList;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Error: {responseObject.message}");
                    }
                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine($"An error occured: {ex.Message}");
                }
            }

        }



    }
}