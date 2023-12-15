using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
		}

        private async void Submit_Button_Clicked(object sender, EventArgs e)
        {
            string username = EntryUsername.Text;
            string password = EntryPassword.Text;

            if (username == "admin" && password == "admin123")
            {
                await Navigation.PushAsync(new Dashboard());

            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid username or password", "OK");
            }
        }
    }
}
