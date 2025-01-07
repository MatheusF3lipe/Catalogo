using AutoMapper;
using Catalogo.Models;

namespace Catalogo.DTO.Mappings
{
    public class ProdutoDTOMappingProfile : Profile
    {
        public ProdutoDTOMappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap(); 
            CreateMap<Produto,ProdutoDtoUpdateRequest>().ReverseMap();
            CreateMap<Produto,ProdutoDtoUpdateResponse>().ReverseMap();
            CreateMap<Categoria,CategoriaDTO>().ReverseMap();  
        }
    }
}
