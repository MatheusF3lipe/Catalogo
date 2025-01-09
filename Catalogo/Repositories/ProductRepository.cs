using Catalogo.Context;
using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Interface
{
    public class ProductRepository : Repository<Produto>,IProduct
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext) 
        {
            
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        }

        public PagedList<Produto> GetProdutosPagination(ProdutoParameters produtoParameters)
        {
            var produtos = GetAll().OrderBy(p =>  p.CategoriaId).AsQueryable();
            var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtoParameters.PageNumber, produtoParameters.PageSize);

            return produtosOrdenados;
        }

    }
}
