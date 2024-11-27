using Catalogo.Context;
using Catalogo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Interface
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext contex)
        {
            _context = contex;
        }

        public Categoria CreateCategory(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public Categoria DeleteCategory(int id)
        {
            Categoria categoria = _context.Categorias.Find(id);
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            return _context.Categorias;
        }

        public Categoria IdCategory(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId.Equals(id));
            return categoria;
        }

        public Categoria UpdateCategory(Categoria categoria)
        {
            _context.Categorias.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return categoria;
        }
        public List<Categoria> listProductByCategory()
        {
            return _context.Categorias.Include(c => c.Produtos).ToList();
        }
    }
}
