using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nullable.Scraping.Facebook.Interfaces;
using Nullable.Scraping.Facebook.Models;
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
        
        internal static readonly FacebookWebClient Client = new FacebookWebClient();
        
        internal static string UserId { get; private set; }
        internal static string Dtsg { get; private set; }
        internal static string Privacyx { get; private set; }
        
        #endregion

        #region Propriedades

        /// <summary>
        /// Cookies utilizados na sessão.
        /// </summary>
        public CookieContainer Cookies => Client.Cookies;

        /// <summary>
        /// Cabeçalho fixado para cada requisição.
        /// </summary>
        public Dictionary<string, string> FixedHeaders
        {
            get => Client.FixedHeaders;
            set => Client.FixedHeaders = value;
        }

        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public string Id => UserId;
        
        /// <summary>
        /// Identificador de sessão.
        /// </summary>
        public string FacebookDtsg => Dtsg;

        /// <summary>
        /// TODO: O que é isso velho? '-'
        /// </summary>
        public string FacebookPrivacyx => Privacyx;

        
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
            FixedHeaders.Add("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.117 Safari/537.36");
        }

        /// <summary>
        /// Efetua o login com as credênciais parametrizadas.
        /// </summary>
        /// <returns>Caso nenhum erro, verdadeiro.</returns>
        public async Task<bool> Login()
        {
            await Client.Get(Root);
            await Client.Post(Authentication, new Dictionary<string, string>
            {
                {"email", _user},
                {"pass", _pass}
            });

            foreach (Cookie cookie in Cookies.GetCookies(new Uri(Root)))
            {
                if (cookie.Name != "c_user") continue;
                UserId = cookie.Value;
                var request = await Client.Get(Root);
                var soup = await request.Content.ReadAsStringAsync();
                
                Dtsg = Regex.Match(soup, "<input[^>]*name=\"fb_dtsg\"[^>]*value=\"([^\"]*)\"")
                    .Groups[0]
                    .Value
                    .Split('"')[5]
                    .Replace("\"", string.Empty);
                
                Privacyx = Regex.Match(soup, "<input[^>]*name=\"privacyx\"[^>]*value=\"([^\"]*)\"")
                    .Groups[0]
                    .Value
                    .Split('"')[7]
                    .Replace("\"", string.Empty);
                
                return true;
            }

            return false;
        }

        /// <summary>
        /// Publica uma postagem no Facebook.
        /// </summary>
        /// <param name="post">Tipo da postagem.</param>
        /// <returns>Informações genéricas da postagem.</returns>
        public async Task<GenericPostModel> Publish(IPost<dynamic> post)
        {
            await post.Publish();
            return post.GenericInformation;
        }
    }
}