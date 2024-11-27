using Catalogo.Context;
using Catalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Interface
{
    public class ProductRepository : IProduct
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public Produto CreateProduct(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public Produto DeleteProduct(Produto produto)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return produto;
        }

        public Produto GetById(int id)
        {
            return _context.Produtos.Find(id);
        }

        public IEnumerable<Produto> ListAllProducts()
        {
            return _context.Produtos.ToList();
        }

        public Produto UpdateProduct(Produto produto)
        {
            _context.Produtos.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return produto;
        }
    }
}
