using System.Threading.Tasks;
using Nullable.Scraping.Facebook.Models;

namespace Nullable.Scraping.Facebook.Interfaces
{
    /// <summary>
    /// Indexa parâmetros de uma postagem do Facebook.
    /// </summary>
    /// <typeparam name="T">Tipo da saída.</typeparam>
    public interface IPost<out T>
    {
        /// <summary>
        /// Pública a postagem.
        /// </summary>
        Task Publish();

        /// <summary>
        /// Popula um modelo de <see cref="IPost{T}"/> com as suas propriedades únicas.
        /// </summary>
        /// <param name="id">Identificador único da postagem.</param>
        /// <returns>Objeto populado.</returns>
        T Populate(string id);

        /// <summary>
        /// Informações genéricas da postagem.
        /// </summary>
        GenericPostModel GenericInformation { get; set; }
    }
}