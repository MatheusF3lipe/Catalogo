using Catalogo.Context;
using Catalogo.Interface;

namespace Catalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProduct _produtoRepo;

        private ICategoryRepository _categoriaRepo;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProduct ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProductRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoryRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose() { 
            _context?.Dispose();
        }
    }
}
