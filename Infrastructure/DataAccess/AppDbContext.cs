namespace  Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Application.Extension.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Entities;


public class AppDbContext(DbContextOptions<AppDbContext> options) : 
    IdentityDbContext<ApplicationUser>(options)

{
    public DbSet<Tracker> ActivityTracker {get; set;}
    public DbSet<Product> Products {get; set;}
    public DbSet<Category> Categories {get; set;}
    public DbSet<Location> Locations {get; set;}
    public DbSet<Order> Orders {get; set;}
}