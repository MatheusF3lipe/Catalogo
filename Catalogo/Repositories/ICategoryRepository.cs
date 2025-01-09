using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;

namespace Catalogo.Interface
{
    public interface ICategoryRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategoriasPagination(CategoriaParameters CategoriaParameters);
    }
}
