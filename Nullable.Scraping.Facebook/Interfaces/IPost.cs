using System.Threading.Tasks;

namespace Nullable.Scraping.Facebook.Interfaces
{
    /// <summary>
    /// Indexa parâmetros de uma postagem do Facebook.
    /// </summary>
    public interface IPost
    {
        /// <summary>
        /// Pública a postagem.
        /// </summary>
        Task Publish();
    }
}