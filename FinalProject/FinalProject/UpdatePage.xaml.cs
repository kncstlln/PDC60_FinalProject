using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using System.Net.Cache;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security.Cryptography;


namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePage : ContentPage
	{
        private const string url_search = "http://192.168.100.86/pdc06/api-searchID.php";
        private const string url_update = "http://192.168.100.86/pdc06/api-update.php";
        private HttpClient _Client = new HttpClient();
        private ObservableCollection<Students> _studentsList = new ObservableCollection<Students>();
        public UpdatePage (Students students)
		{
			InitializeComponent ();
            ID.Text = students.ID.ToString();
        }

        private async Task UpdatePostAsync()
        {
            try
            {
                Students students = new Students
                {
                    ID = int.Parse(ID.Text),
                    name = Name.Text
                };
                var content = JsonConvert.SerializeObject(students);
                var response = await _Client.PostAsync(url_update, new StringContent(content, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Records update successfully", "OK");
                }
                else
                {
                    await DisplayAlert("Error", $"Failed to update", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occured: {ex.Message}", "OK");
            }
        }

        private async void OnUpdate(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Update Confirmation", $"Are you sure you want to update ID no: {ID.Text}", "OK", "CANCEL");
            if (result)
            {
                await UpdatePostAsync();
            }
            else
            {

            }
        }

        private async void OnRetrievedChange(Object sender, TextChangedEventArgs e)
        {
            string searchQuery = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                Name.Text = string.Empty;
            }
            else
            {
                try
                {
                    var searchurl = $"{url_search}?ID={searchQuery}";
                    System.Diagnostics.Debug.WriteLine($"Search URL: {searchurl}");
                    var content = await _Client.GetStringAsync(searchurl);
                    var responseObject = JsonConvert.DeserializeObject<StudentList.ResponseObject>(content);
                    if (responseObject.status)
                    {
                        var searchResults = JsonConvert.DeserializeObject<List<Students>>(responseObject.data.ToString());
                        if (searchResults.Count > 0)
                        {
                            var firstResult = searchResults[0];
                            Name.Text = firstResult.name;
                        }
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

