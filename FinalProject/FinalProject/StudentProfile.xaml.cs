using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FinalProject
{	
	public partial class StudentProfile : ContentPage
	{	
		public StudentProfile ()
		{
			InitializeComponent ();
		}

        private async void AcademicHistory_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AcademicHistory());
        }
    }
}

