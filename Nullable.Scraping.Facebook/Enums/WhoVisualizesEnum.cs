namespace Nullable.Scraping.Facebook.Enums
{
    /// <summary>
    /// Enumera as possibilidades de visualização de uma postagem.
    /// </summary>
    public enum WhoVisualizesEnum
    {
        /// <summary>
        /// Somente amigos.
        /// </summary>
        Friends,
        
        /// <summary>
        /// Qualquer pessoa.
        /// </summary>
        Public,
        
        /// <summary>
        /// Todos os amigos, exceto.
        /// </summary>
        FriendsExcept,
        
        /// <summary>
        /// Somente eu.
        /// </summary>
        OnlyMe,
        
        /// <summary>
        /// Grupo customizado.
        /// </summary>
        Custom
    }
}