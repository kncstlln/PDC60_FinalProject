using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace FinalProject
{	
	public partial class StudentList : ContentPage
       
    {
    
        // Your student list
        private List<string> studentList = new List<string>
        {
            "Kane Erryl G. Castillano",
            "Karylle L. Lopez",
            "Aaron Echon",
            // Add more students as needed
        };

        public StudentList()
        {
            InitializeComponent();
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;

            // Filter the student list based on the search text
            List<string> filteredStudents = studentList
                .Where(student => student.ToLower().Contains(searchText.ToLower()))
                .ToList();

            // Update the UI based on the filtered student list
            UpdateStudentUI(filteredStudents);
        }

        private void UpdateStudentUI(List<string> students)
        {
            // Remove existing frames from the UI
            stackLayout.Children.Clear();

            // Add frames for the filtered students
            foreach (var student in students)
            {
                Frame frame = new Frame
                {
                    HasShadow = false,
                    CornerRadius = 10,
                    HeightRequest = 30,
                    Padding = 10,
                    Margin = new Thickness(3, 0, 3, 1),
                    BorderColor = Color.Gray
                };

                Label label = new Label
                {
                    Text = student,
                    FontSize = 20,
                    FontFamily = "Montserrat",
                    TextColor = Color.Black
                };

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += StudentProfile_Tapped;
                frame.GestureRecognizers.Add(tapGestureRecognizer);

                frame.Content = label;
                stackLayout.Children.Add(frame);
            }
        }





        private async void StudentProfile_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new StudentProfile());
        }
    }
}

