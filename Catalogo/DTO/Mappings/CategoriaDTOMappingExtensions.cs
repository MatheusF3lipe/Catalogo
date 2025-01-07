using Catalogo.DTO;
using Catalogo.Models;

namespace Catalogo.DTO.Mappings
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoriaDTO? ToCategoriaDto(this Categoria categoria)
        {
            if (categoria is null) return null;

            return new CategoriaDTO()
            {
                CategoriaId = categoria.CategoriaId,
                ImagemUrl = categoria.ImagemUrl,
                Nome = categoria.Nome,
            };
        }
        public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO is null) return null;

            return new Categoria()
            {
                Nome = categoriaDTO.Nome,
                ImagemUrl = categoriaDTO?.ImagemUrl,
                CategoriaId = categoriaDTO.CategoriaId
            };

        }

        public static IEnumerable<CategoriaDTO> ToCategoriaDtos(this IEnumerable<Categoria> categorias)
        {
            if (!categorias.Any() || categorias is null)
            {
                return new List<CategoriaDTO>();
            }

            return categorias.Select(c => new CategoriaDTO()
            {
                Nome = c.Nome,
                ImagemUrl = c.ImagemUrl,
                CategoriaId = c.CategoriaId
            }).ToList();

        }


    }
}
