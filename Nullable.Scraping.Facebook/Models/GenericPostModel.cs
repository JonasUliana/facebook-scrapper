using System;
using Nullable.Scraping.Facebook.Enums;

namespace Nullable.Scraping.Facebook.Models
{
    /// <summary>
    /// Indexa informações genéricas de qualquer postagem.
    /// </summary>
    public class GenericPostModel
    {
        /// <summary>
        /// Identificador único da postagem.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Quem consegue ver a postagem.
        /// </summary>
        public WhoVisualizesEnum WhoVisualizes { get; set; }
        
        /// <summary>
        /// Data da publicação.
        /// </summary>
        public DateTime When { get; set; }
        
        /// <summary>
        /// Link absoluto até a publicação.
        /// </summary>
        public string Link { get; set; }

        /*
         * TODO:
         * Implementar modelos de usuários;
         *                        comentários;
         *                        reações.
         */
    }
}