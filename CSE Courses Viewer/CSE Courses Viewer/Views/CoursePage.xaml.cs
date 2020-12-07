using Xamarin.Forms;

namespace CSE_Courses_Viewer
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public CoursePage()
        {
            InitializeComponent();
            BindingContext = new CoursePageViewModel();
        }
    }
}