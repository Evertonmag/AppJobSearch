using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.App.Utility.Loading;
using JobSearch.Shared.Models;
using Mopups.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;

namespace JobSearch.App.Views;

public partial class Start : ContentPage
{
    private JobService _service;
    private ObservableCollection<Job> _listOfJobs;
    private SearchParams _searchParams;
    private int _listOfJobsFirstRequest;

    public Start()
    {
        InitializeComponent();

        _service = new JobService();

        Task.Run(async () => await GetJobs(string.Empty, string.Empty));

    }

    private async void GoVisualizer(object sender, TappedEventArgs e)
    {
        var eventArgs = (TappedEventArgs)e;
        
        var page = new Visualizer();
        page.BindingContext = eventArgs.Parameter;

        await Navigation.PushAsync(page);
    }

    private async void GoRegisterJob(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RegisterJob());
    }

    private void FocusSearch(object sender, EventArgs e)
    {
        txtSearch.Focus();
    }

    private void FocusCityState(object sender, EventArgs e)
    {
        txtCityState.Focus();
    }

    private async void Logout(object sender, TappedEventArgs e)
    {
        await SecureStorage.SetAsync("User", string.Empty);

        App.Current.MainPage = new NavigationPage(new Login());
    }

    private async void Search(object sender, EventArgs e)
    {
        string word = txtSearch.Text;
        string cityState = txtCityState.Text;

        await GetJobs(word, cityState);
    }

    private async void InfinitySearch(object sender, EventArgs e)
    {
        if (_searchParams != null)
        {
            ListOfJobs.RemainingItemsThreshold = -1;
            _searchParams.NumberOfPage++;

            ResponseService<List<Job>> response = await _service.GetJobs(_searchParams.Word, _searchParams.CityState, _searchParams.NumberOfPage);

            if (response.Success is true)
            {
                var listOfJobsService = response.Data;

                foreach (Job job in listOfJobsService)
                {
                    _listOfJobs.Add(job);
                }
                if (_listOfJobsFirstRequest == listOfJobsService.Count)
                {
                    ListOfJobs.RemainingItemsThreshold = 1;
                }
                else
                {
                    ListOfJobs.RemainingItemsThreshold = -1;
                }
            }
            else
            {
                await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais tarde.", "Ok");
                ListOfJobs.RemainingItemsThreshold = 0;
            }
        }
    }

    private async Task GetJobs(string word, string cityState)
    {
        txtResultsCount.Text = string.Empty;
        Loading.IsVisible = true;
        ListOfJobs.IsVisible = false;

        _searchParams = new()
        {
            Word = word,
            CityState = cityState,
            NumberOfPage = 1
        };

        ResponseService<List<Job>> response = await _service.GetJobs(_searchParams.Word, _searchParams.CityState, _searchParams.NumberOfPage);

        if (response.Success is not true)
        {
            await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais tarde.", "Ok");
            Loading.IsVisible = false;
            ListOfJobs.IsVisible = true;
            return;
        }

        _listOfJobs = new ObservableCollection<Job>(response.Data);
        _listOfJobsFirstRequest = _listOfJobs.Count();
        ListOfJobs.ItemsSource = _listOfJobs;
        ListOfJobs.RemainingItemsThreshold = 1;
        Loading.IsVisible = false;
        ListOfJobs.IsVisible = true;
        txtResultsCount.Text = $"{response.Pagination.TotalItems} resultado(s).";
    }
    
}