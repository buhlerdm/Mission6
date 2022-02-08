using Microsoft.EntityFrameworkCore;
using Mission6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Mission6.Models
{
    public class TasksContext : DbContext
    {
        //Constructor
        public TasksContext (DbContextOptions<TasksContext> options) : base(options)
        {
            // Leave Blank
        }

        public DbSet<TaskResponse> responses { get; set; }
        public DbSet<Category> Categories { get; set; }


        //Seed data 
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Home" },
                new Category { CategoryId = 2, CategoryName = "School" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Church" }
                );

            mb.Entity<TaskResponse>().HasData(

                new TaskResponse
                {
                    TaskId = 1,
                    TaskName = "Vaccuum the living room",
                    DueDate = "2/10/21",
                    Quadrant = "4",
                    CategoryId = 1,
                    Completed = false

                },

                new TaskResponse
                {
                    TaskId = 2,
                    TaskName = "Shop for Mom's birthday",
                    DueDate = "2/9/21",
                    Quadrant = "2",
                    CategoryId = 1,
                    Completed = false

                }

                );

            

        }
    }
}
