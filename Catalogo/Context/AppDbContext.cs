﻿using Catalogo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Catalogo.Context;

public class AppDbContext : IdentityDbContext<AplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {}
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
}
