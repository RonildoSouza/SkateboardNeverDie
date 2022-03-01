using SkateboardNeverDie.ViewModels;
using Xamarin.Forms;

namespace SkateboardNeverDie.Views
{
    public partial class TrickDetailPage : ContentPage
    {
        public TrickDetailPage()
        {
            InitializeComponent();
            BindingContext = new TrickDetailViewModel();
        }
    }
}