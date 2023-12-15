using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Xml.Linq;
using Xamarin.Essentials;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace FinalProject
{	
	public partial class AddStudentPage : ContentPage
	{
        public const string url = "http://192.168.100.86/PDC60_api/api_create.php";
        private HttpClient _Client = new HttpClient();
        public AddStudentPage ()
		{
			InitializeComponent ();
		}
        private async void CancelAddStudent(Object sender, EventArgs e)
        {
            bool userConfirmation = await DisplayAlert("Cancel", "Are you sure you want to cancel adding a record?", "Yes", "No");

            if (userConfirmation)
            {
                // Navigate back when the user confirms
                await Navigation.PopAsync();
            }
        }
        private async void SubmitAddStudent(Object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text) ||
                string.IsNullOrWhiteSpace(sectionEntry.Text) ||
                string.IsNullOrWhiteSpace(levelEntry.Text) ||
                string.IsNullOrWhiteSpace(ageEntry.Text) ||
                string.IsNullOrWhiteSpace(mobileNumEntry.Text) ||
                string.IsNullOrWhiteSpace(emailEntry.Text))
            {
                await DisplayAlert("Error", "Please complete each field.", "OK");
                return;
            }

            if (!int.TryParse(levelEntry.Text, out _))
            {
                await DisplayAlert("Error", "Level should be a numeric value 1-4.", "OK");
                return;
            }

            if (!int.TryParse(ageEntry.Text, out _))
            {
                await DisplayAlert("Error", "Age should be a numeric value.", "OK");
                return;
            }

            if (mobileNumEntry.Text.Length != 11 || !mobileNumEntry.Text.All(char.IsDigit))
            {
                await DisplayAlert("Error", "Mobile number should be 11 digits.", "OK");
                return;
            }

            if (!IsValidEmail(emailEntry.Text))
            {
                await DisplayAlert("Error", "Please provide a valid email address.", "OK");
                return;
            }

            bool userConfirmation = await DisplayAlert("Confirm", "Confirm information above is correct?", "Yes", "No");

            if (userConfirmation)
            {
                Students post = new Students
                {
                    name = nameEntry.Text,
                    section = sectionEntry.Text,
                    level = levelEntry.Text,
                    age = int.Parse(ageEntry.Text),
                    mobile_num = mobileNumEntry.Text,
                    email = emailEntry.Text
                };

                var content = JsonConvert.SerializeObject(post);
                var response = await _Client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Record added successfully!", "OK");
                    // Optionally navigate back to the StudentRecordsPage after adding a record
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to add record. Please try again.", "OK");
                }

                await Navigation.PopAsync();
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

