using Catalogo.Context;
using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Interface
{
    public class CategoryRepository : Repository<Categoria>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public PagedList<Categoria> GetCategoriasPagination(CategoriaParameters categoriasParameters)
        {
            var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();
            var categoriasOrdenadas = PagedList<Categoria>.ToPagedList(categorias, categoriasParameters.PageNumber, categoriasParameters.PageSize);
            return categoriasOrdenadas;
        }
    }
}
