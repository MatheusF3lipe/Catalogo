using Catalogo.Models;
using Catalogo.Repositories;

namespace Catalogo.Interface
{
    public interface IProduct : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorCategoria(int id);
    }
}
