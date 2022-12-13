using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.App.Utility.Loading;
using JobSearch.Shared.Models;
using Mopups.Services;
using Newtonsoft.Json;
using System.Net;

namespace JobSearch.App.Views;

public partial class Login : ContentPage
{
    private UserService _service;

    public Login()
    {
        InitializeComponent();

        _service = new UserService();
    }

    private async void GoRegister(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register());
    }

    private async void GoStart(object sender, EventArgs e)
    {
        await MopupService.Instance.PushAsync(new Loading());

        string email = txtEmail.Text;
        string password = txtPassword.Text;

        ResponseService<User> response = await _service.GetUser(email, password);

        if (response.Success is not true)
        {
            if (response.StatusCode == 404)
                await DisplayAlert("Erro!", "Nenhum usuario encontrado!", "Ok");
            else
                await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais tarde.", "Ok");
            await MopupService.Instance.PopAsync();
            return;
        }

        await SecureStorage.SetAsync("User", JsonConvert.SerializeObject(response));

        await MopupService.Instance.PopAsync();

        App.Current.MainPage = new NavigationPage(new Start());
    }
}