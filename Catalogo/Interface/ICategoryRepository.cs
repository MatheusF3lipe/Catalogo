using Catalogo.Models;

namespace Catalogo.Interface
{
    public interface ICategoryRepository 
    {
        IEnumerable<Categoria> GetCategorias();
        Categoria IdCategory(int id);
        Categoria CreateCategory(Categoria categoria);
        Categoria UpdateCategory(Categoria categoria);
        Categoria DeleteCategory(int id);
        List<Categoria> listProductByCategory();
    }
}
