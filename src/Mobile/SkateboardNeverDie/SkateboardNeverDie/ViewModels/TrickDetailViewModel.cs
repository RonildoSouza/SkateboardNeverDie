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

        public TrickDetailViewModel()
        {
            Title = "Trick Details";
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

        private async void LoadTrickId(Guid trickId)
        {
            try
            {
                var trickHateoasResult = await _skateboardNeverDieApi.GetTrickByIdAsync(trickId);
                Name = trickHateoasResult.Data.Name;
                Description = trickHateoasResult.Data.Description;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
