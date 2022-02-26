using SkateboardNeverDie.Models;
using SkateboardNeverDie.Services;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class NewTrickViewModel : BaseViewModel
    {
        private readonly ISkateboardNeverDieApi _skateboardNeverDieApi = DependencyService.Get<ISkateboardNeverDieApi>();
        private string _name;
        private string _descriptioin;

        public NewTrickViewModel()
        {
            Title = "Add New Trick";
            SaveCommand = new Command(OnSave, CanExecuteSave);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
            CancelCommand = new Command(OnCancel);
        }

        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _descriptioin;
            set => SetProperty(ref _descriptioin, value);
        }

        private async void OnSave()
        {
            var createTrick = new CreateTrick(Name, Description);
            await _skateboardNeverDieApi.PostTricksAsync(createTrick);
            await Shell.Current.GoToAsync("..", false);
        }

        private bool CanExecuteSave()
        {
            return !string.IsNullOrEmpty(Name);
        }

        private async void OnCancel() => await Shell.Current.GoToAsync("..", false);
    }
}