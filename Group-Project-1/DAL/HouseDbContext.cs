﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Group_Project_1.Models;

namespace Group_Project_1.DAL;
public class HouseDbContext : IdentityDbContext<User>
{
	public HouseDbContext(DbContextOptions<HouseDbContext> options) : base(options)
	{
		// Database.EnsureCreated();
	}

	public DbSet<House> Houses { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		optionsBuilder.UseLazyLoadingProxies();
    }
}
