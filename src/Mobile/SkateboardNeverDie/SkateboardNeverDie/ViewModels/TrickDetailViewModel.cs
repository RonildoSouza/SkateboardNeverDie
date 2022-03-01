using SkateboardNeverDie.Models;
using SkateboardNeverDie.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    [QueryProperty(nameof(TrickId), nameof(TrickId))]
    public class TrickDetailViewModel : BaseViewModel
    {
        private readonly ISkateboardNeverDieApi _skateboardNeverDieApi = DependencyService.Get<ISkateboardNeverDieApi>();
        private string _trickId;
        private string _name;
        private string _description;
        private bool _canDeleteTrick;

        public TrickDetailViewModel()
        {
            Title = "Trick Details";
            DeleteCommand = new Command(OnDelete);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string TrickId
        {
            get
            {
                return _trickId;
            }
            set
            {
                _trickId = value;
                LoadTrickId(Guid.Parse(value));
            }
        }
        public bool CanDeleteTrick
        {
            get => _canDeleteTrick;
            set => SetProperty(ref _canDeleteTrick, value);
        }

        public Command DeleteCommand { get; }

        private async void LoadTrickId(Guid trickId)
        {
            try
            {
                var trickHateoasResult = await _skateboardNeverDieApi.GetTrickByIdAsync(trickId);
                Name = trickHateoasResult.Data.Name;
                Description = trickHateoasResult.Data.Description;

                CanDeleteTrick = trickHateoasResult.HasLink(Trick.Rels.Delete);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void OnDelete()
        {
            if (!Guid.TryParse(TrickId, out Guid trickId))
                return;

            if (await Application.Current.MainPage.DisplayAlert("Alert!", $"Would you like to delete trick ({Name})?", "Yes", "No"))
            {
                await _skateboardNeverDieApi.DeleteTrickAsync(trickId);
                await Shell.Current.GoToAsync("..", false);
            }
        }
    }
}
