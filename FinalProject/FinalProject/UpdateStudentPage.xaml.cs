using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FinalProject
{	
	public partial class UpdateStudentPage : ContentPage
	{	
		public UpdateStudentPage ()
		{
			InitializeComponent ();
		}

        private async void CancelUpdateStudent(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SubmitUpdateStudent(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

