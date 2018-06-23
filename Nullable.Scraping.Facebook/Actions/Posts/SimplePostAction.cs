using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nullable.Scraping.Facebook.Models;
using Nullable.Scraping.Facebook.Interfaces;
using static Nullable.Scraping.Facebook.FacebookScraper;

namespace Nullable.Scraping.Facebook.Actions.Posts
{
    /// <summary>
    /// Indexa informações de uma postagem simpels.
    /// </summary>
    public class SimplePostAction : IPost<SimplePostAction>
    {
        /// <summary>
        /// Informações da postagem.
        /// </summary>
        public GenericPostModel GenericInformation { get; set; }

        /// <summary>
        /// Conteúdo genérico da postagem.
        /// </summary>
        public string PlainTextContent { get; set; }

        /// <summary>
        /// Pública uma postagem simples em plain text.
        /// </summary>
        /// <returns></returns>
        public async Task Publish()
        {
            var response = await Client.Post($"https://mbasic.facebook.com/composer/mbasic/?av={UserId}&refid=7", new Dictionary<string, string>
            {
                {"xc_message", PlainTextContent ?? throw new ArgumentNullException(nameof(PlainTextContent))},
                {"target", UserId},
                {"c_src", "feed"},
                {"cwevent", "composer_entry"},
                {"referrer", "feed"},
                {"ctype", "inline"},
                {"cver", "amber"},
                {"fb_dtsg", Dtsg},
                {"privacyx", Privacyx},
                {"rst_icv", string.Empty},
                {"view_post", "Publicar"}
            });

            var soup = await response.Content.ReadAsStringAsync();
            Console.WriteLine(soup);

            if (soup.Contains(PlainTextContent))
                Console.WriteLine("WORKS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!SSS");
        }

        /// <summary>
        /// Popula o modelo de dados.
        /// </summary>
        /// <param name="id">Identificador único da postagem.</param>
        /// <returns>Objeto populado.</returns>
        public SimplePostAction Populate(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}