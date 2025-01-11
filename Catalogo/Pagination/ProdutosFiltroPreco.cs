namespace Catalogo.Pagination
{
    public class ProdutosFiltroPreco : ParametersAbstract
    {
        public decimal? Preco { get; set; }
        public string? PrecoCriterio {  get; set; } // "Maior","Menor","Igual"

    }
}
