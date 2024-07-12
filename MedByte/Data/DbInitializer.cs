using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MedByte.Data;
using Medbyte.Models;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Tomografy.Any())
            {
                return; 
            }

            var tomografy = new Tomograf[]
            {
                new Tomograf
                {
                    Nazwa = "Tomograf 1",
                    Opis = "Zaawansowany tomograf komputerowy oferujący wyjątkową jakość obrazów z minimalnym dawkowaniem promieniowania.",
                    Telefon = "123456789",
                    Ulica = "Ulica Zdrowia 1",
                    Miasto = "Warszawa",
                    KodPocztowy = "00-001",
                    ImagePath = "/images/image1.jpg"
                },
                new Tomograf
                {
                    Nazwa = "Tomograf 2",
                    Opis = "Revolution CT to tomograf komputerowy, który łączy szybkość, rozdzielczość i niskie dawki promieniowania.",
                    Telefon = "987654321",
                    Ulica = "Ulica Zdrowia 2",
                    Miasto = "Kraków",
                    KodPocztowy = "30-002",
                    ImagePath = "/images/image2.jpg"
                },
                new Tomograf
                {
                    Nazwa = "Tomograf 3",
                    Opis = "Philips Ingenuity Core oferuje innowacyjne rozwiązania w zakresie obrazowania medycznego z wysoką rozdzielczością.",
                    Telefon = "564738291",
                    Ulica = "Ulica Zdrowia 3",
                    Miasto = "Poznań",
                    KodPocztowy = "60-003",
                    ImagePath = "/images/image3.jpg"
                },
                new Tomograf
                {
                    Nazwa = "Tomograf 4",
                    Opis = "Tomograf Canon Aquilion Prime SP zapewnia szybkie i dokładne obrazowanie medyczne dla różnych zastosowań klinicznych.",
                    Telefon = "102938475",
                    Ulica = "Ulica Zdrowia 4",
                    Miasto = "Gdańsk",
                    KodPocztowy = "80-004",
                    ImagePath = "/images/image4.jpg"
                }
            };

            context.Tomografy.AddRange(tomografy);
            context.SaveChanges();
        }
    }
}
