using static System.Console;
using System.Threading.Tasks;
using Nullable.Scraping.Facebook.Actions.Posts;

namespace Nullable.Scraping.Facebook.Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var scraper = new FacebookScraper(args[0], args[1]);
            WriteLine(await scraper.Login() ? "Autenticado!" : "Usuário ou senha inválidos!");
            WriteLine($"Seu identificador único é: \"{scraper.Id}\"");
            WriteLine($"Seu identificador único de sessão é: \"{scraper.FacebookDtsg}\"");
            WriteLine($"Seu \"coiso\" de sessão é: \"{scraper.FacebookPrivacyx}\"");
            var post = await scraper.Publish(new SimplePostAction
            {
                PlainTextContent = "ISSO AQUI SIM, EVOLUÇÃO"
            });
        }
    }
}