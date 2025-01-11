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

        public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtoFiltroPreco)
        {
            var produtos = GetAll().AsQueryable();
            if (produtoFiltroPreco.Preco.HasValue && !string.IsNullOrEmpty(produtoFiltroPreco.PrecoCriterio))
            {
                if (produtoFiltroPreco.PrecoCriterio.Equals("Maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtoFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtoFiltroPreco.PrecoCriterio.Equals("Menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtoFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
                }
                else
                {
                    produtos = produtos.Where(p => p.Preco == produtoFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
                }
            }
            var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtoFiltroPreco.PageNumber, produtoFiltroPreco.PageSize);

            return produtosFiltrados;
        }
    }
}
