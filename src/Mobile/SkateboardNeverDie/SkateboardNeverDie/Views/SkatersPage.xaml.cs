using SkateboardNeverDie.ViewModels;
using Xamarin.Forms;

namespace SkateboardNeverDie.Views
{
    public partial class SkatersPage : ContentPage
    {
        readonly SkatersViewModel _viewModel;

        public SkatersPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SkatersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}