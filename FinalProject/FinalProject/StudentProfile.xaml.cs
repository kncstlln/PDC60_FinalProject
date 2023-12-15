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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentProfile : ContentPage
    {

        private Students _selectedStudent;

        public StudentProfile (Students selectedStudent)
		{
			InitializeComponent ();
            _selectedStudent = selectedStudent;
            BindingContext = _selectedStudent;
        }

        private async void AcademicHistory_Tapped(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AcademicHistory(_selectedStudent));
        }

        private async void AttendancePage_Tapped(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AttendancePage(_selectedStudent));
        }
    }
}

