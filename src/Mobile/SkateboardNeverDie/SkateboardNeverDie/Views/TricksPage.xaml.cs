using SkateboardNeverDie.ViewModels;
using Xamarin.Forms;

namespace SkateboardNeverDie.Views
{
    public partial class TricksPage : ContentPage
    {
        readonly TricksViewModel _viewModel;

        public TricksPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TricksViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}