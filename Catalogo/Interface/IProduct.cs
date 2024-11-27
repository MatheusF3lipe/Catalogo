using Catalogo.Models;

namespace Catalogo.Interface
{
    public interface IProduct
    {
        IEnumerable<Produto> ListAllProducts();
        Produto GetById(int id);
        Produto CreateProduct(Produto produto);
        Produto UpdateProduct(Produto produto);
        Produto DeleteProduct(Produto produto);
    }
}
