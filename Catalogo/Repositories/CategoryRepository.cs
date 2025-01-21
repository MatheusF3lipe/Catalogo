using Catalogo.Context;
using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;
using X.PagedList;

namespace Catalogo.Interface
{
    public class CategoryRepository : Repository<Categoria>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IPagedList<Categoria>> GetCategoriasFiltroNome(CategoriaFiltroNome categoriaParams)
        {
            var categorias = await GetAllAsync();
            if(!string.IsNullOrEmpty(categoriaParams.Nome))
            {
                categorias = categorias.Where(c => c.Nome.Contains(categoriaParams.Nome));
            }

            //var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias.AsQueryable(), categoriaParams.PageNumber, categoriaParams.PageSize);
            var categoriasFiltradas = await categorias.ToPagedListAsync(categoriaParams.PageNumber, categoriaParams.PageSize);

            return categoriasFiltradas;      
            
        }

        public async Task<IPagedList<Categoria>> GetCategoriasPagination(CategoriaParameters categoriasParameters)
        {
            var categorias = await GetAllAsync();
            var categoriasOrdenadas = categorias.OrderBy(c => c.CategoriaId).AsQueryable();
            var resultado = await categoriasOrdenadas.ToPagedListAsync(categoriasParameters.PageNumber, categoriasParameters.PageSize);
            return resultado;
        }
    }
}
