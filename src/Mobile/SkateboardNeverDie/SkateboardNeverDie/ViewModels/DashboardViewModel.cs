using Microcharts;
using SkateboardNeverDie.Models;
using SkateboardNeverDie.Services;
using SkateboardNeverDie.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private bool _isLogged;
        private int _skatersCount;
        private int _tricksCount;
        private DonutChart _chartSkatersGoofyVsRegular;
        private BarChart _chartSkatersCountPerAge;
        private readonly ISingleSignOnService _singleSignOnService = DependencyService.Get<ISingleSignOnService>();
        private readonly ISecureStorageManager _secureStorageManager = DependencyService.Get<ISecureStorageManager>();
        private readonly ISkateboardNeverDieApi _skateboardNeverDieApi = DependencyService.Get<ISkateboardNeverDieApi>();

        public DashboardViewModel()
        {
            Title = "Dashboard";
            LoginCommand = new Command(OnLoginClicked);
            LogoutCommand = new Command(OnLogoutClicked);
            IsValidTokenAsync().GetAwaiter();
            OnAppearing();
        }

        public Command LoginCommand { get; }
        public Command LogoutCommand { get; }
        public bool IsLogged
        {
            get => _isLogged;
            set => SetProperty(ref _isLogged, value);
        }
        public int SkatersCount
        {
            get => _skatersCount;
            set => SetProperty(ref _skatersCount, value);
        }
        public int TricksCount
        {
            get => _tricksCount;
            set => SetProperty(ref _tricksCount, value);
        }
        public DonutChart ChartSkatersGoofyVsRegular
        {
            get => _chartSkatersGoofyVsRegular;
            set => SetProperty(ref _chartSkatersGoofyVsRegular, value);
        }
        public BarChart ChartSkatersCountPerAge
        {
            get => _chartSkatersCountPerAge;
            set => SetProperty(ref _chartSkatersCountPerAge, value);
        }

        public async void OnAppearing()
        {
            IsBusy = true;

            try
            {
                TricksCount = await _skateboardNeverDieApi.GetTricksCountAsync();
                SkatersCount = await _skateboardNeverDieApi.GetSkatersCountAsync();
                await ChartSkatersGoofyVsRegularBuilder();
                await ChartSkatersCountPerAgeBuilder();
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

        private async Task ChartSkatersCountPerAgeBuilder()
        {
            var entriesChartSkatersCountPerAge = new List<ChartEntry>();
            foreach (var _ in await _skateboardNeverDieApi.GetSkatersCountPerAgeAsync())
                entriesChartSkatersCountPerAge.Add(new ChartEntry(_.Count)
                {
                    Label = $"{_.Age}",
                    ValueLabel = $"{_.Count}",
                    Color = SKColorRandom.GetColor()
                });

            ChartSkatersCountPerAge = new BarChart
            {
                Entries = entriesChartSkatersCountPerAge,
                LabelTextSize = 20
            };
        }

        private async Task ChartSkatersGoofyVsRegularBuilder()
        {
            var entriesChartSkatersGoofyVsRegular = new List<ChartEntry>();
            foreach (var _ in await _skateboardNeverDieApi.GetSkatersGoofyVsRegularAsync())
                entriesChartSkatersGoofyVsRegular.Add(new ChartEntry(_.Value)
                {
                    Label = $"{_.Key}",
                    ValueLabel = $"{_.Value}",
                    Color = SKColorRandom.GetColor()
                });

            ChartSkatersGoofyVsRegular = new DonutChart
            {
                Entries = entriesChartSkatersGoofyVsRegular,
                LabelTextSize = 20
            };
        }

        private async void OnLoginClicked(object obj)
        {
            var tokenResponse = await _singleSignOnService.AuthorizationCodeFlowAsync();

            if (tokenResponse != null)
            {
                await _secureStorageManager.SetAsync(GlobalSetting.TokenResponseKey, tokenResponse);
                IsLogged = true;
            }
        }

        private async void OnLogoutClicked(object obj)
        {
            try
            {
                var tokenResponse = await _secureStorageManager.GetAsync<TokenResponse>(GlobalSetting.TokenResponseKey);

                if (await _singleSignOnService.LogoutAsync(tokenResponse.IdentityToken))
                {
                    _secureStorageManager.Remove(GlobalSetting.TokenResponseKey);
                    IsLogged = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private async Task IsValidTokenAsync()
        {
            var tokenResponse = await _secureStorageManager.GetAsync<TokenResponse>(GlobalSetting.TokenResponseKey);
            IsLogged = tokenResponse != null && !tokenResponse.IsExpired && !string.IsNullOrEmpty(tokenResponse.IdentityToken);
        }
    }
}
