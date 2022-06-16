using SkateboardNeverDie.ViewModels;
using Xamarin.Forms;

namespace SkateboardNeverDie.Views
{
    public partial class SkaterDetailPage : ContentPage
    {
        public SkaterDetailPage()
        {
            InitializeComponent();
            BindingContext = new SkaterDetailViewModel();
        }
    }
}