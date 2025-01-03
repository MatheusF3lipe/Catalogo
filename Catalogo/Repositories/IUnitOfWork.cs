using Catalogo.Interface;

namespace Catalogo.Repositories
{
    public interface IUnitOfWork
    {
        IProduct ProdutoRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        void Commit();
    }
}
