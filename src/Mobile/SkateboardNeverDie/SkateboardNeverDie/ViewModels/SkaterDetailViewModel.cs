using SkateboardNeverDie.Models;
using SkateboardNeverDie.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    [QueryProperty(nameof(SkaterId), nameof(SkaterId))]
    public class SkaterDetailViewModel : BaseViewModel
    {
        private readonly ISkateboardNeverDieApi _skateboardNeverDieApi = DependencyService.Get<ISkateboardNeverDieApi>();
        private string _skaterId;
        private string _firstName;
        private string _lastName;
        private string _nickname;
        private StanceType _naturalStance;

        public SkaterDetailViewModel()
        {
            Title = "Skater Tricks";
            SkaterTricks = new ObservableCollection<SkaterTrick>();
            LoadSkaterTricksCommand = new Command(async () => await ExecuteLoadSkaterTricksCommand());
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public StanceType NaturalStance
        {
            get => _naturalStance;
            set => SetProperty(ref _naturalStance, value);
        }

        public ObservableCollection<SkaterTrick> SkaterTricks { get; }

        public Command LoadSkaterTricksCommand { get; }

        public string SkaterId
        {
            get
            {
                return _skaterId;
            }
            set
            {
                _skaterId = value;
                LoadSkaterId(Guid.Parse(value));
            }
        }

        private async void LoadSkaterId(Guid skaterId)
        {
            try
            {
                var skater = await _skateboardNeverDieApi.GetSkaterByIdAsync(skaterId);
                FirstName = skater.Data.FirstName;
                LastName = skater.Data.LastName;
                Nickname = skater.Data.Nickname;
                NaturalStance = skater.Data.NaturalStance;

                await ExecuteLoadSkaterTricksCommand();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task ExecuteLoadSkaterTricksCommand()
        {
            IsBusy = true;

            try
            {
                SkaterTricks.Clear();
                var skaterTricks = await _skateboardNeverDieApi.GetSkaterTricksAsync(Guid.Parse(SkaterId));

                foreach (var skaterTrick in skaterTricks.Data.Results)
                    if (!SkaterTricks.Contains(skaterTrick))
                        SkaterTricks.Add(skaterTrick);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
