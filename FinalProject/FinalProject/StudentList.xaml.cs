using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

using System.Net.Http;

using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace FinalProject

{

    public partial class StudentList : ContentPage
    {
        public const string student_update = "http://192.168.100.86/PDC60_api/api-update.php";
        public const string delete = "http://192.168.100.86/PDC60_api/api-delete.php";
        public const string search = "http://192.168.100.86/PDC60_api/api-searchID.php";
       
        private async void StudentProfile_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentProfile());
        }

       
       
    }
}