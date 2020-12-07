using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CSE_Courses_Viewer
{
    public class CoursePageViewModel : ViewModelBase
    {
        // public ICommand GoToSyllabus => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public ICSECourseList CourseList { get { return CSECourseData.CourseList; } }

        // Displayed if the user entered invalid input
        string errorMessage; 
        public string ErrorMessage {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged();
            }
        }

        // Button action to get CSECourse from the number entered
        public ICommand FindCourse => new Command<string>((string query) =>
        {
            if (int.TryParse(query, out int number))
            {
                if (CourseList.ContainsNumber(number))
                {
                    SelectedCourse = CourseList.GetCourseByNumber(number);
                    ErrorMessage = "";
                } else
                {
                    ErrorMessage = "Error: Course not found.";
                }
            } else
            {
                ErrorMessage = "Error: Invalid format entered.";
            }
        });

        // The selected course to display info for
        ICSECourse selectedCourse;
        public ICSECourse SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                selectedCourse = value;
                OnPropertyChanged();
            }
        }
    }
}
