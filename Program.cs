
using CodeFIrstDB.DTOs.StudentDtos;
using CodeFIrstDB.Endpoints.StudentEndpoints;
using CodeFIrstDB.Properties.Data;
using CodeFIrstDB.Properties.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CodeFIrstDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); //Creating the application

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SchoolDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            StudentsEndpoints.RegisterEndpoints(app);

            //Hämtar filterade 
            app.MapGet("/student", async (SchoolDBContext context) => // Async Endpoint, To fetch all student
            {
                var studentList = await context.Students.Select(s => new StudentDto
                {
                    StudentName = s.FirstName,
                    StudentLastName = s.LastName,
                    StudentEmail = s.Email
                }).ToListAsync();

                return studentList;
            });

            

            app.Run();
        }
    }
}
