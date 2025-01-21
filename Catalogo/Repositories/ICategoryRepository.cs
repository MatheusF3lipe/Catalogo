using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;
using X.PagedList;

namespace Catalogo.Interface
{
    public interface ICategoryRepository : IRepository<Categoria>
    {
        Task<IPagedList<Categoria>> GetCategoriasPagination(CategoriaParameters CategoriaParameters);
        Task<IPagedList <Categoria>> GetCategoriasFiltroNome(CategoriaFiltroNome categoriaParams);
    }
}
