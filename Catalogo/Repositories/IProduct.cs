using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;
using X.PagedList;

namespace Catalogo.Interface
{
    public interface IProduct : IRepository<Produto>
    {
        Task <IEnumerable<Produto>>GetProdutosPorCategoriaAsync(int id);
        Task <IPagedList<Produto>> GetProdutosPaginationAsync(ProdutoParameters produtoParameters);
        Task <IPagedList<Produto>>   GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtoFiltroPreco);
    }
}
