using SkateboardNeverDie.Models;
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
        private StanceType _naturalStance = StanceType.Goofy;

        public NewSkaterViewModel()
        {
            Title = "Add New Skater";
            SaveCommand = new Command(OnSave, CanExecuteSave);
            CancelCommand = new Command(OnCancel);
            AddSkaterTricksCommand = new Command(OnAddSkaterTricks);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
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

        public StanceType NaturalStance
        {
            get => _naturalStance;
            set => SetProperty(ref _naturalStance, value);
        }

        public Command SaveCommand { get; }

        public Command AddSkaterTricksCommand { get; }

        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            //var skater = new Skater()
            //{
            //    FirstName = FirstName,
            //    LastName = LastName,
            //    Nickname = NickName,
            //    Birthdate = Birthdate,
            //    NaturalStance = NaturalStance
            //};

            //await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnAddSkaterTricks()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool CanExecuteSave()
        {
            return !string.IsNullOrWhiteSpace(_firstName)
                && !string.IsNullOrWhiteSpace(_lastName)
                //&& _skaterTricks.Any()
                ;
        }
    }
}
