using SkateboardNeverDie.Extensions;
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
        private string _fullName;
        private string _nickname;
        private StanceType _naturalStance;
        private DateTime _birthdate;
        private int _pageSize = 10;
        private bool _hasNextPage;

        public SkaterDetailViewModel()
        {
            Title = "Skater Details";
            SkaterTricks = new ObservableCollection<SkaterTrick>();
            LoadSkaterTricksCommand = new Command(async () => await ExecuteLoadSkaterTricksCommand());
        }

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        public StanceType NaturalStance
        {
            get => _naturalStance;
            set => SetProperty(ref _naturalStance, value);
        }

        public DateTime Birthdate
        {
            get => _birthdate;
            set => SetProperty(ref _birthdate, value);
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
                FullName = $"{skater.Data.FirstName} {skater.Data.LastName}";
                Nickname = skater.Data.Nickname;
                NaturalStance = skater.Data.NaturalStance;
                Birthdate = skater.Data.Birthdate;

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
                var skaterTricksHateoasResult = await _skateboardNeverDieApi.GetSkaterTricksAsync(Guid.Parse(SkaterId), pageSize: _pageSize);

                _hasNextPage = skaterTricksHateoasResult.HasLink(SkaterTrick.Rels.Next);
                SkaterTricks.TryAddRange(skaterTricksHateoasResult.Data.Results);
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

        protected override async Task ItemsThresholdReached()
        {
            if (IsBusy || !_hasNextPage)
                return;

            IsBusy = true;

            try
            {
                var nextPage = (SkaterTricks.Count / _pageSize) + 1;
                var skaterTricksHateoasResult = await _skateboardNeverDieApi.GetSkaterTricksAsync(Guid.Parse(SkaterId), nextPage, _pageSize);

                _hasNextPage = skaterTricksHateoasResult.HasLink(SkaterTrick.Rels.Next);
                SkaterTricks.TryAddRange(skaterTricksHateoasResult.Data.Results);
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
