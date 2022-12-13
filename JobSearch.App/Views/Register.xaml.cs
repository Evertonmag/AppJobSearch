using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.App.Utility.Loading;
using JobSearch.Shared.Models;
using Mopups.Services;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace JobSearch.App.Views;

public partial class Register : ContentPage
{
    private UserService _service;

    public Register()
    {
        InitializeComponent();

        _service = new UserService();
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void SaveAction(object sender, EventArgs e)
    {
        await MopupService.Instance.PushAsync(new Loading());

        if (ValidarCampos() is false)
        {
            await MopupService.Instance.PopAsync();
            return;
        }

        string name = txtName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text.Trim();

        User user = new()
        {
            Name = name,
            Email = email,
            Password = password,
        };

        ResponseService<User> response = await _service.AddUser(user);

        if (response.Success is not true)
        {
            if (response.StatusCode == 400)
            {
                StringBuilder sb = new();

                foreach (var dicKey in response.Errors)
                {
                    foreach (var message in dicKey.Value)
                    {
                        sb.AppendLine(message);
                    }
                }

                await DisplayAlert("Erro!", sb.ToString(), "OK");
            }
            else
                await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais tarde.", "Ok");


            await MopupService.Instance.PopAsync();
            return;
        }

        await SecureStorage.SetAsync("User", JsonConvert.SerializeObject(response));

        await MopupService.Instance.PopAsync();

        App.Current.MainPage = new NavigationPage(new Start());
    }

    private bool ValidarCampos()
    {
        txtNameErro.IsVisible = false;
        txtEmailErro.IsVisible = false;
        txtPasswordErro.IsVisible = false;
        txtConfirmPasswordErro.IsVisible = false;

        if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtNameErro.Text = "Este campo é obrigatório";
                txtNameErro.IsVisible = true;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmailErro.Text = "Este campo é obrigatório";
                txtEmailErro.IsVisible = true;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPasswordErro.Text = "Este campo é obrigatório";
                txtPasswordErro.IsVisible = true;
            }
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                txtConfirmPasswordErro.Text = "Este campo é obrigatório";
                txtConfirmPasswordErro.IsVisible = true;
            }
            return false;
        }
        Regex rg = RegexEmail();
        if (txtName.Text.Trim().Length < 10 || txtPassword.Text.Trim().Length < 6 || !rg.IsMatch(txtEmail.Text) || txtPassword.Text != txtConfirmPassword.Text)
        {
            if (txtName.Text.Length < 10)
            {
                txtNameErro.Text = "Este campo deve conter no minimo 10 caracteres";
                txtNameErro.IsVisible = true;
            }
            if (txtPassword.Text.Length < 6)
            {
                txtPasswordErro.Text = "Este campo deve conter no minimo 6 caracteres";
                txtPasswordErro.IsVisible = true;
            }
            if (!rg.IsMatch(txtEmail.Text))
            {
                txtEmailErro.Text = "Este campo deve ser um Email valido";
                txtEmailErro.IsVisible = true;
            }
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                txtConfirmPasswordErro.Text = "As Senhas devem ser iguais";
                txtConfirmPasswordErro.IsVisible = true;
            }
            return false;
        }
        return true;
    }

    [GeneratedRegex("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$")]
    private static partial Regex RegexEmail();
}