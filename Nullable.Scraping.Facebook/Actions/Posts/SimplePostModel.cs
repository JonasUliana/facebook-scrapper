using System.Threading.Tasks;
using Nullable.Scraping.Facebook.Interfaces;
using Nullable.Scraping.Facebook.Models;

namespace Nullable.Scraping.Facebook.Actions.Posts
{
    /// <summary>
    /// Indexa informações de uma postagem simpels.
    /// </summary>
    public class SimplePostModel : GenericPostModel, IPost
    {
        /// <summary>
        /// Conteúdo genérico da postagem.
        /// </summary>
        public string PlainTextContent { get; set; }

        public async Task Publish()
        {
            throw new System.NotImplementedException();
        }
    }
}