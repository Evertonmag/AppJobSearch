namespace JobSearch.App.Services
{
    public abstract class Service
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

        protected HttpClient _client;
#if DEBUG
        //Rodar Api localmente e trocar o '<Ip do Computador>' pelo Ipv4 da sua maquina
        protected string BaseApiUrl = "http://<Ip do Computador>:5297";
#else
        //Api hospedada no azure pelo curso
        protected string BaseApiUrl = "https://xamarinforms2020api.azurewebsites.net/";
#endif
        public Service()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            HttpClient client = new(insecureHandler);
#else
            HttpClient client = new HttpClient();
#endif
            _client = client;
        }
    }
}
