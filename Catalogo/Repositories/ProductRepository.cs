using Catalogo.Context;
using Catalogo.Models;
using Catalogo.Pagination;
using Catalogo.Repositories;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Catalogo.Interface
{
    public class ProductRepository : Repository<Produto>,IProduct
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext) 
        {
            
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
        {
            var produtos = await GetAllAsync();
            var produtosOrdenados = produtos.Where(c => c.CategoriaId == id);
            return produtosOrdenados;
        }

        public async Task<IPagedList<Produto>> GetProdutosPaginationAsync(ProdutoParameters produtoParameters)
        {
            var produtos = await GetAllAsync();
            var produtorOrdenados = produtos.OrderBy(p => p.CategoriaId).AsQueryable();
            var resultado = await produtorOrdenados.ToPagedListAsync(produtoParameters.PageNumber, produtoParameters.PageSize);

            return resultado;
        }

        public async Task <IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtoFiltroPreco)
        {
            var produtos = await GetAllAsync();
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
            var produtosFiltrados = await produtos.ToPagedListAsync(produtoFiltroPreco.PageNumber, produtoFiltroPreco.PageSize);

            return produtosFiltrados;
        }
    }
}
