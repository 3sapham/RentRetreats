using Microsoft.AspNetCore.Identity;
using Group_Project_1.Models;

namespace Group_Project_1.DAL;
public class DBInit
{
    public static async void AddRoles(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var roleManager =
            serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] { "Admin", "Host", "Tenant" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    public static async void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager =
            serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        HouseDbContext context = serviceScope.ServiceProvider.GetRequiredService<HouseDbContext>();
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if(!await roleManager.RoleExistsAsync("Host") || !await roleManager.RoleExistsAsync("Admin") || !await roleManager.RoleExistsAsync("Tenant"))
        {
            AddRoles(app);
        }

        if (!context.Users.Any())
        {

            string email = "admin@admin.com";
            string password = "Admin123,";

            if (await userManager.FindByEmailAsync(email.ToUpper()) == null)
            {
                var user = new User
                {
                    Email = email,
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = email,
                    NormalizedEmail = email.ToUpper(),
                    NormalizedUserName = email.ToUpper()
                };

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, "Admin");
            }

            var email1 = "john.h@gmail.com";
            var user1 = new User
            {
                FirstName = "John",
                LastName = "Hopkins",
                Email = email1,
                PhoneNumber = "12345678",
                NormalizedEmail = email1.ToUpper(),
                NormalizedUserName = email1.ToUpper(),
                UserName = email1
            };

            await userManager.CreateAsync(user1, "JohnH2000,");
            await userManager.AddToRoleAsync(user1, "Host");

            var email2 = "sarahtucker12@gmail.com";
            var user2 = new User
            {
                FirstName = "Sarah",
                LastName = "Tucker",
                Email = email2,
                NormalizedUserName = email2.ToUpper(),
                UserName = email2,
                NormalizedEmail = email2.ToUpper(),
                PhoneNumber = "98765432"
            };

            await userManager.CreateAsync(user2, "SarahT2000,");
            await userManager.AddToRoleAsync(user2, "Host");

            var email3 = "maria.hansen@email.com";
            var user3 = new User
            {
                FirstName = "Maria",
                LastName = "Hansen",
                Email = email3,
                NormalizedUserName = email3.ToUpper(),
                UserName = email3,
                NormalizedEmail = email3.ToUpper(),
                PhoneNumber = "23456789"
            };

            await userManager.CreateAsync(user3, "MariaH2000,");
            await userManager.AddToRoleAsync(user3, "Tenant");

            var email4 = "anders.olsen@email.com";
            var user4 = new User
            {
                FirstName = "Anders",
                LastName = "Olsen",
                Email = email4,
                NormalizedUserName = email4.ToUpper(),
                UserName = email4,
                NormalizedEmail = email4.ToUpper(),
                PhoneNumber = "87654321"
            };

            await userManager.CreateAsync(user4, "AndersO2000,");
            await userManager.AddToRoleAsync(user4, "Host");

            var email5 = "erik.johansen@email.com";
            var user5 = new User
            {
                FirstName = "Erik",
                LastName = "Johansen",
                Email = email5,
                NormalizedUserName = email5.ToUpper(),
                UserName = email5,
                NormalizedEmail = email5.ToUpper(),
                PhoneNumber = "99988877"
            };

            await userManager.CreateAsync(user5, "ErikJ2000,");
            await userManager.AddToRoleAsync(user5, "Tenant");
        }

        if (!context.Houses.Any())
        {
            var user1 = userManager.Users.FirstOrDefault(u => u.FirstName == "John");
            var user2 = userManager.Users.FirstOrDefault(u => u.FirstName == "Sarah");
            var user3 = userManager.Users.FirstOrDefault(u => u.FirstName == "Maria");
            var user4 = userManager.Users.FirstOrDefault(u => u.FirstName == "Anders");
            var user5 = userManager.Users.FirstOrDefault(u => u.FirstName == "Erik");
            if (user1 != null && user2 != null && user4 != null)
            {
                var houses = new List<House>()
            {
                new House
                {
                    HouseId = 1,
                    Title = "Charming cottage on Åsen",
                    Description = "Small charming cottage with charm on Øståsen in Vikersund.",
                    HouseImageUrl = "/images/aasen_cottage.jpg",
                    BedroomImageUrl = "/images/aasen_bed.jpg",
                    BathroomImageUrl = "/images/aasen_bath.jpg",
                    Location = "Modum, Viken, Norway",
                    PricePerNight = 990,
                    Bedrooms = 3,
                    Bathrooms = 2,
                    UserId = user1.Id
                },
                new House
                {
                    HouseId = 2,
                    Title = "Big villa in Holmenkollen",
                    Description = "Modern big villa with swimming pool and nice view.",
                    HouseImageUrl = "/images/villa.jpg",
                    BedroomImageUrl = "/images/villa_bed.jpg",
                    BathroomImageUrl = "/images/villa_bath.jpg",
                    Location = "Oslo, Norway",
                    PricePerNight = 1190,
                    Bedrooms = 4,
                    Bathrooms = 3,
                    UserId = user2.Id
                },
                new House
                {
                    HouseId = 3,
                    Title = "Typical Norwegian cottage",
                    Description = "Typical Norwegian cottage, very cosy near water",
                    HouseImageUrl = "/images/small_cottage.jpg",
                    BedroomImageUrl = "/images/cottage_bed.jpg",
                    BathroomImageUrl = "/images/cottage_bath.jpg",
                    Location = "Sandefjord, Norway",
                    PricePerNight = 690,
                    Bedrooms = 1,
                    Bathrooms = 1,
                    UserId = user4.Id
                },
                new House
                {
                    HouseId = 4,
                    Title = "Northern Lights Retreat",
                    Description = "Experience the magic of the Northern Lights from this cozy log cabin in the Arctic Circle of Norway.",
                    HouseImageUrl = "/images/northernlights.jpg",
                    BedroomImageUrl = "/images/northernlights_bed.jpg",
                    BathroomImageUrl = "/images/northernlights_bath.jpg",
                    Location = "Tromsø, Norway",
                    PricePerNight = 790,
                    Bedrooms = 2,
                    Bathrooms = 1,
                    UserId = user2.Id
                },
                new House
                {
                    HouseId = 5,
                    Title = "Bergen City Apartment",
                    Description = "Stay in the heart of Bergen in this stylish and modern city apartment.",
                    HouseImageUrl = "/images/bergen.jpg",
                    BedroomImageUrl = "/images/bergen_bed.jpg",
                    BathroomImageUrl = "/images/bergen_bath.jpg",
                    Location = "Bergen, Norway",
                    PricePerNight = 490,
                    Bedrooms = 2,
                    Bathrooms = 1,
                    UserId = user2.Id
                }
            };
                context.AddRange(houses);
                context.SaveChanges();
            }
        }

        if (!context.Reservations.Any())
        {
            var user1 = userManager.Users.FirstOrDefault(u => u.FirstName == "John");
            var user2 = userManager.Users.FirstOrDefault(u => u.FirstName == "Sarah");
            var user3 = userManager.Users.FirstOrDefault(u => u.FirstName == "Maria");
            var user4 = userManager.Users.FirstOrDefault(u => u.FirstName == "Anders");
            var user5 = userManager.Users.FirstOrDefault(u => u.FirstName == "Erik");
            if (user3 != null && user5 != null)
            {
                var reservations = new List<Reservation>
                {
                    new Reservation
                    {
                        CheckInDate = new DateTime(2023, 09, 28),
                        CheckOutDate = new DateTime(2023, 10, 15),
                        UserId = user3.Id,
                        User = user1,
                        HouseId = 1,
                        DateCreated = DateTime.Today.AddDays(-25)
                    },
                    new Reservation
                    {
                        CheckInDate = new DateTime(2023, 10, 20),
                        CheckOutDate = new DateTime(2023, 10, 27),
                        UserId = user5.Id,
                        HouseId = 2,
                        User = user2,
                        DateCreated = DateTime.Today.AddDays(-10)
                    },
                };

                foreach (var reservation in reservations)
                {
                    var house = context.Houses.Find(reservation.HouseId);
                    if (house != null)
                    {
                        TimeSpan duration = reservation.CheckOutDate - reservation.CheckInDate;
                        reservation.BookingDuration = duration.Days;
                        reservation.TotalPrice = reservation.BookingDuration * house.PricePerNight;
                    }
                }
                context.AddRange(reservations);
                context.SaveChanges();
            }
        }
    }
}
