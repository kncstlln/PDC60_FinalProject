using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FinalProject
{	
	public partial class AddStudentPage : ContentPage
	{	
		public AddStudentPage ()
		{
			InitializeComponent ();
		}

        private async void CancelAddStudent(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SubmitAddStudent(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

