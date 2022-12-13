using JobSearch.Shared.Models;

namespace JobSearch.App.Views;

public partial class Visualizer : ContentPage
{
    public Visualizer()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Job job = ((Job)BindingContext);

        if (string.IsNullOrWhiteSpace(job.CompanyDescription))
        {
            HeaderCompanyDescription.IsVisible = false;
            textCompanyDescription.IsVisible = false;
        }
        if (string.IsNullOrEmpty(job.Benefits))
        {
            HeaderBenefits.IsVisible = false;
            TextBenefits.IsVisible = false;
        }
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {

    }
}