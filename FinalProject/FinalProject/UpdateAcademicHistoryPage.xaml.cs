using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FinalProject
{	
	public partial class UpdateAcademicHistoryPage : ContentPage
	{	
		public UpdateAcademicHistoryPage ()
		{
			InitializeComponent ();
		}

        private async void SubmitUpdateAcademicHistory(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void CancelUpdateAcademicHistory(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

