using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nullable.Scraping.Facebook.Requests
{
    /// <summary>
    /// Gerencia requisições para o Facebook.
    /// </summary>
    internal class FacebookWebClient
    {
        #region Objetos locais

        private readonly HttpClient _client;
        private CookieContainer _cookies;

        #endregion

        #region Propriedades
    
        /// <summary>
        /// Cookies utilizados na sessão.
        /// </summary>
        public CookieContainer Cookies => _cookies;
        
        /// <summary>
        /// Cabeçalho fixado para cada requisição.
        /// </summary>
        public Dictionary<string, string> FixedHeaders { get; set; } = new Dictionary<string, string>();

        #endregion

        /// <summary>
        /// Construtor padrão, cria uma nova instância de <see cref="FacebookWebClient"/>.
        /// </summary>
        public FacebookWebClient()
        {
            _cookies = new CookieContainer();
            _client = new HttpClient(new HttpClientHandler
            {
                CookieContainer = _cookies,
                UseCookies = true,
                AllowAutoRedirect = true
            });
        }

        /// <summary>
        /// Realiza uma requisição genérica do tipo <see cref="HttpMethod.Get"/> em um alvo.
        /// </summary>
        /// <param name="target">Destino da requisição.</param>
        /// <returns>Resposta do servidor.</returns>
        /// <exception cref="ArgumentException">Caso <see cref="target"/>
        /// nulo ou vazio, ou caso sem presença de agente de usuário.
        /// </exception>
        public async Task<HttpResponseMessage> Get(string target)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, target ??
                                                                 throw new ArgumentNullException(nameof(target)));
            request = _mix(FixedHeaders, request);
            if (request.Headers.All(h => h.Key != "User-Agent"))
                throw new ArgumentException("Nenhum agente de usuário definido!");

            return await _client.SendAsync(request);
        }
        
        /// <summary>
        /// Realiza uma requisição genérica do tipo <see cref="HttpMethod.Post"/> em um alvo.
        /// </summary>
        /// <param name="target">Destino da requisição.</param>
        /// <returns>Resposta do servidor.</returns>
        /// <exception cref="ArgumentException">Caso <see cref="target"/>
        /// nulo ou vazio, ou caso sem presença de agente de usuário.
        /// </exception>
        public async Task<HttpResponseMessage> Post(string target)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, target ??
                                                                 throw new ArgumentNullException(nameof(target)));
            request = _mix(FixedHeaders, request);
            if (request.Headers.All(h => h.Key != "User-Agent"))
                throw new ArgumentException("Nenhum agente de usuário definido!");

            return await _client.SendAsync(request);
        }
        
        /// <summary>
        /// Realiza uma requisição genérica do tipo <see cref="HttpMethod.Post"/> em um alvo.
        /// </summary>
        /// <param name="target">Destino da requisição.</param>
        /// <param name="content">Conteúdo do POST</param>
        /// <returns>Resposta do servidor.</returns>
        /// <exception cref="ArgumentException">Caso <see cref="target"/>
        /// nulo ou vazio, caso <see cref="content"/> nulo ou vazio
        /// ou caso sem presença de agente de usuário.
        /// </exception>
        public async Task<HttpResponseMessage> Post(string target, Dictionary<string, string> content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, target ??
                                                                  throw new ArgumentNullException(nameof(target)));
            request = _mix(FixedHeaders, request);
            request.Content = new FormUrlEncodedContent(content ??
                                                        throw new ArgumentNullException(nameof(content)));
            if (request.Headers.All(h => h.Key != "User-Agent"))
                throw new ArgumentException("Nenhum agente de usuário definido!");

            return await _client.SendAsync(request);
        }

        #region Helpers

        private readonly Func<Dictionary<string, string>, HttpRequestMessage, HttpRequestMessage> _mix = (headers, origin) =>
        {
            headers
                .ToList()
                .ForEach(h =>
                    origin.Headers.TryAddWithoutValidation(h.Key, h.Value));
            return origin;
        };

        #endregion
    }
}