﻿using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading;
using System.Security.Cryptography;
using System.Xml.Linq;
using Xamarin.Essentials;

namespace FinalProject
{	
	public partial class UpdateStudentPage : ContentPage
	{
        private const string url_update = "http://192.168.100.86/PDC60_api/api-update.php";
        private HttpClient _Client = new HttpClient();
        private ObservableCollection<Students> _posts;

        private async void CancelUpdateStudent(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        as
        private async void SubmitUpdateStudent(Object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

