using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MaxTest.Models
{
    public static class DataInit
    {
        public static void InitializeData(LiteContext context)
        {
            context.Database.EnsureCreated();
            if (context.Children.Any())
            {
                // DB has been seeded, no need to continue
                return;
            }
            SeedChildren(context.Children);
            context.SaveChanges();
        }
        // public static void InitializeData(PostgreContext context)
        // {
        //     context.Database.EnsureCreated();
        //     if (context.Children.Any())
        //     {
        //         // DB has been seeded, no need to continue
        //         return;
        //     }
        //     SeedChildren(context.Children);
        //     SeedParents(context.Parents);
        //     context.SaveChanges();
        // }

        private static void SeedParents(DbSet<Parent> parents)
        {
            for (int i = 0; i < 4; i++)
            {
                parents.Add(new Parent
                {
                    Name = $"Parent{i}"
                });
            }
        }
        private static void SeedChildren(DbSet<Child> children)
        {
            for (int i = 0; i < 4; i++)
            {
                var parent = new Parent
                {
                    Name = $"Parent{i}"
                };
                for (int j = 0; j < 4; j++)
                {
                    children.Add(new Child
                    {
                        Name = $"Child{(i + 1) * (j + 1)}",
                        Group = Math.Max(0, j - i),
                        Parent = parent
                    });
                }
            }
        }
    }
}