using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;

namespace Catalogo.Interface
{
    public interface IProduct : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorCategoria(int id);
        PagedList<Produto> GetProdutosPagination(ProdutoParameters produtoParameters);
        PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtoFiltroPreco);
    }
}
