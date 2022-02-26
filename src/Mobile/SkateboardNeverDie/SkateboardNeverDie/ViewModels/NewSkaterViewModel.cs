using Newtonsoft.Json;
using SkateboardNeverDie.Models;
using SkateboardNeverDie.Views;
using System;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class NewSkaterViewModel : BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _nickName;
        private DateTime _birthdate = new DateTime(1990, 09, 17);
        private string _naturalStance = "Goofy";

        public NewSkaterViewModel()
        {
            Title = "Add New Skater";

            CancelCommand = new Command(OnCancel);
            AddSkaterTricksCommand = new Command(OnAddSkaterTricks, CanExecuteAddSkaterTricks);
            PropertyChanged += (_, __) => AddSkaterTricksCommand.ChangeCanExecute();
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }
        public DateTime Birthdate
        {
            get => _birthdate;
            set => SetProperty(ref _birthdate, value);
        }
        public string NaturalStance
        {
            get => _naturalStance;
            set => SetProperty(ref _naturalStance, value);
        }
        public Command AddSkaterTricksCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel() => await Shell.Current.GoToAsync("..", false);

        private async void OnAddSkaterTricks()
        {
            Enum.TryParse(NaturalStance, true, out StanceType naturalStance);

            var skater = new CreateSkater(
                FirstName,
                LastName,
                NickName,
                Birthdate,
                naturalStance);

            await Shell.Current.GoToAsync($"{nameof(SkaterTricksPage)}?{nameof(SkaterTricksViewModel.SkaterJSON)}={JsonConvert.SerializeObject(skater)}");
        }

        private bool CanExecuteAddSkaterTricks()
        {
            return !string.IsNullOrWhiteSpace(_firstName)
                && !string.IsNullOrWhiteSpace(_lastName);
        }
    }
}
