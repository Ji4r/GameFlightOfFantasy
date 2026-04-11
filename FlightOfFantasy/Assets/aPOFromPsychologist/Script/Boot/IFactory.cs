using System.Threading.Tasks;

namespace DiplomGames
{
    public interface IFactory
    {
        public Task InstantiateAsync();
    }
}