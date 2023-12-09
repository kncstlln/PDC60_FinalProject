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

        private async void OnFrameTapped(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CollegesPage());

        }
    }
}

