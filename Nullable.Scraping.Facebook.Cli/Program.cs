using static System.Console;
using System.Threading.Tasks;

namespace Nullable.Scraping.Facebook.Cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var scaper = new FacebookScraper("usuário", "senha");
            WriteLine(await scaper.Login() ? "Autenticado!" : "Usuário ou senha inválidos!");
        }
    }
}