using JobSearch.App.Services;
using JobSearch.App.Views;

namespace JobSearch.App
{
    public partial class App : Application
    {
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost")) return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public App()
        {
            InitializeComponent();

            var possuiLogin = SecureStorage.GetAsync("User").GetAwaiter().GetResult();

            if (!string.IsNullOrEmpty(possuiLogin))
            {
                MainPage = new NavigationPage(new Start());
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }

#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            HttpClient client = new(insecureHandler);
#else
            HttpClient client = new HttpClient();
#endif
        }
    }
}