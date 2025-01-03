using Catalogo.Context;
using Catalogo.Models;
using Catalogo.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Interface
{
    public class CategoryRepository : Repository<Categoria>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) 
        {
            
        }
    }
}
