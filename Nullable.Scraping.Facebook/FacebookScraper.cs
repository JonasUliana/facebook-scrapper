using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Nullable.Scraping.Facebook.Requests;

namespace Nullable.Scraping.Facebook
{
    /// <summary>
    /// Gerencia todas as interações com a biblitoeca.
    /// </summary>
    public class FacebookScraper
    {
        #region Objetos locais

        private readonly string _user;
        private readonly string _pass;
        private readonly FacebookWebClient _client;

        #endregion

        #region Propriedades

        /// <summary>
        /// Cookies utilizados na sessão.
        /// </summary>
        public CookieContainer Cookies => _client.Cookies;

        /// <summary>
        /// Cabeçalho fixado para cada requisição.
        /// </summary>
        public Dictionary<string, string> FixedHeaders
        {
            get => _client.FixedHeaders;
            set => _client.FixedHeaders = value;
        }        

        #endregion

        #region Constantes

        private const string Root = "https://www.facebook.com/";
        private const string Authentication = "https://www.facebook.com/login.php?login_attempt=1";

        #endregion
        
        /// <summary>
        /// Construtor padrão, cira uma nova instância de <see cref="FacebookScraper"/>.
        /// </summary>
        /// <param name="user">Nome de usuário, e-mail ou telefone.</param>
        /// <param name="pass">Senha do usuário.</param>
        public FacebookScraper(string user, string pass)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _pass = pass ?? throw new ArgumentNullException(nameof(pass));
            _client = new FacebookWebClient();
            FixedHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.117 Safari/537.36");
        }

        /// <summary>
        /// Efetua o login com as credênciais parametrizadas.
        /// </summary>
        /// <returns>Caso nenhum erro, verdadeiro.</returns>
        public async Task<bool> Login()
        {
            await _client.Get(Root);
            await _client.Post(Authentication, new Dictionary<string, string>
            {
                {"email", _user},
                {"pass", _pass}
            });

            foreach (Cookie cookie in Cookies.GetCookies(new Uri(Root)))
            {
                if (cookie.Name == "c_user")
                    return true;
            }

            return false;
        }
    }
}