using SkateboardNeverDie.ViewModels;
using Xamarin.Forms;

namespace SkateboardNeverDie.Views
{
    public partial class NewTrickPage : ContentPage
    {
        public NewTrickPage()
        {
            InitializeComponent();
            BindingContext = new NewTrickViewModel();
        }
    }
}