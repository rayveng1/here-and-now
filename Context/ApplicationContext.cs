using HereAndNow.Models;
using Microsoft.EntityFrameworkCore;

namespace HereAndNow.Context;

public class ApplicationContext : DbContext
{
    public DbSet<Place> Places {get; set;}
    public DbSet<User> Users {get; set;}
    public DbSet<DisplayName> DisplayNames {get; set;}
    public DbSet<Association> Associations {get; set;}
    public ApplicationContext(DbContextOptions options): base(options){ }
}