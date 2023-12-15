using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FinalProject
{	
	public partial class Dashboard : ContentPage
	{	
		public Dashboard ()
		{
			InitializeComponent ();
		}

        private async void OnBSIT1_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new StudentList());
        }

        private async void OnBSIT2_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new StudentList());
        }

        private async void OnBSIT3_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new StudentList());
        }

        private async void OnBSIT4_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new StudentList());
        }
    }
}

