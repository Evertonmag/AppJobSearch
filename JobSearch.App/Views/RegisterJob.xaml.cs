using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.App.Utility.Loading;
using JobSearch.Shared.Models;
using Mopups.Services;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace JobSearch.App.Views;

public partial class RegisterJob : ContentPage
{
    private JobService _service;
    private double vlrInicial = 0;
    private double vlrFinal = 0;

    public RegisterJob()
    {
        InitializeComponent();

        _service = new JobService();
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Save(object sender, EventArgs e)
    {
        await MopupService.Instance.PushAsync(new Loading());

        if (await ValidarCampos() is false)
        {
            await MopupService.Instance.PopAsync();
            return;
        }

        var cacheUser = SecureStorage.GetAsync("User").GetAwaiter().GetResult();
        var user = JsonConvert.DeserializeObject<ResponseService<User>>(cacheUser);

        Job job = new()
        {
            Company = txtCompany.Text.Trim(),
            JobTitle = txtJobTitle.Text.Trim(),
            CityState = txtCityState.Text.Trim(),
            InitialSalary = vlrInicial,
            FinalSalary = vlrFinal,
            ContractType = (rbCLT.IsChecked) ? "CLT" : "PJ",
            TecnologyTools = txtTecnologyTools.Text.Trim(),
            CompanyDescription = txtCompanyDescription.Text.Trim() ?? string.Empty,
            JobDescription = txtJobDescription.Text.Trim(),
            Benefits = txtBenefits.Text.Trim() ?? string.Empty,
            InterestedSendEmailTo = txtInteresedSendToEmailTo.Text.Trim(),
            PublicationDate = DateTime.Now,
            UserId = user.Data.Id
        };

        ResponseService<Job> response = await _service.AddJob(job);

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
                //TODO - Trocar DisplayAlert por um popup personalizado
                await DisplayAlert("Erro!", sb.ToString(), "OK");
            }
            else
            {
                //TODO - Trocar DisplayAlert por um popup personalizado
                await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado! Tente novamente mais tarde.", "Ok");
            }

            await MopupService.Instance.PopAsync();
            return;
        }

        await MopupService.Instance.PopAsync();

        await DisplayAlert("Vaga", "Vaga foi cadatrada com sucesso!", "OK");
        await Navigation.PopAsync();
    }

    private async Task<bool> ValidarCampos()
    {
        #region Limpar mensagens de erro

        txtBenefitsError.IsVisible = false;
        txtCityStateError.IsVisible = false;
        txtCompanyError.IsVisible = false;
        txtCompanyDescriptionError.IsVisible = false;
        txtFinalSalaryError.IsVisible = false;
        txtInitialSalaryError.IsVisible = false;
        txtInteresedSendToEmailToError.IsVisible = false;
        txtJobDescriptionError.IsVisible = false;
        txtJobTitleError.IsVisible = false;
        txtTecnologyToolsError.IsVisible = false;
        #endregion

        #region Validação de campos nulos ou vazios
        if (string.IsNullOrWhiteSpace(txtCityState.Text) || string.IsNullOrWhiteSpace(txtCompany.Text) || string.IsNullOrWhiteSpace(txtInitialSalary.Text) || string.IsNullOrWhiteSpace(txtFinalSalary.Text) || string.IsNullOrWhiteSpace(txtInteresedSendToEmailTo.Text) || string.IsNullOrWhiteSpace(txtJobDescription.Text) || string.IsNullOrWhiteSpace(txtJobTitle.Text) || string.IsNullOrWhiteSpace(txtTecnologyTools.Text))
        {
            if (string.IsNullOrWhiteSpace(txtCityState.Text))
            {
                txtCityStateError.IsVisible = true;
                txtCityStateError.Text = "Este Campo deve ser preenchido";
            }
            if (string.IsNullOrWhiteSpace(txtCompany.Text))
            {
                txtCompanyError.IsVisible = true;
                txtCompanyError.Text = "Este Campo deve ser preenchido";
            }
            if (string.IsNullOrWhiteSpace(txtInitialSalary.Text))
            {
                txtInitialSalaryError.IsVisible = true;
                txtInitialSalaryError.Text = "Este Campo deve ser preenchido";
            }
            if (string.IsNullOrWhiteSpace(txtFinalSalary.Text))
            {
                txtFinalSalaryError.IsVisible = true;
                txtFinalSalaryError.Text = "Este Campo deve ser preenchido";
            }
            if (string.IsNullOrWhiteSpace(txtInteresedSendToEmailTo.Text))
            {
                txtInteresedSendToEmailToError.IsVisible = true;
                txtInteresedSendToEmailToError.Text = "Este Campo deve ser preenchido";
            }
            if (string.IsNullOrWhiteSpace(txtJobDescription.Text))
            {
                txtJobDescriptionError.IsVisible = true;
                txtJobDescriptionError.Text = "Este Campo deve ser preenchido";
            }
            if (string.IsNullOrWhiteSpace(txtJobTitle.Text))
            {
                txtJobTitleError.IsVisible = true;
                txtJobTitleError.Text = "Este Campo deve ser preenchido";
            }
            if (string.IsNullOrWhiteSpace(txtTecnologyTools.Text))
            {
                txtTecnologyToolsError.IsVisible = true;
                txtTecnologyToolsError.Text = "Este Campo deve ser preenchido";
            }
            return false;
        }

        #endregion

        if (!double.TryParse(txtInitialSalary.Text.Trim(), out double vlrOutInicial) ||
            !double.TryParse(txtFinalSalary.Text.Trim(), out double vlrOutFinal))
        {
            return false;
        }

        vlrInicial = vlrOutInicial;
        vlrFinal = vlrOutFinal;

        if (vlrFinal < vlrInicial)
        {
            //TODO - Trocar DisplayAlert por um popup personalizado
            await DisplayAlert("Aviso!", "O valor final deve ser maior ou igual ao valor inicial", "OK");
            return false;
        }

        return true;
    }

}