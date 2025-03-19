using CodeFIrstDB.DTOs.StudentDtos;
using CodeFIrstDB.Properties.Data;
using CodeFIrstDB.Properties.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace CodeFIrstDB.Endpoints.StudentEndpoints
{
    public class StudentsEndpoints
    {
        public async static void RegisterEndpoints (WebApplication app)
        {

            app.MapGet("/student", async (SchoolDBContext context) => // Async Endpoint, To fetch all student
            {
                var studentList = await context.Students.Select(s => new StudentDto
                {
                    StudentName = s.FirstName,
                    StudentEmail = s.Email
                }).ToListAsync();

                return studentList;

            });

    


            app.MapPost("/students", async (StudentCreateDto newStudent, SchoolDBContext context) =>
            {
                var student = new Student
                {
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    Email = newStudent.EmailAdress,
                    ClassID_FK = newStudent.ClassId
                };
                context.Students.Add(student);
                await context.SaveChangesAsync();

                return student;


            });

      
            app.MapGet("/student/{id}", async (int id, SchoolDBContext context) =>
            {
                var student = await context.Students.FirstOrDefaultAsync(s => s.ID == id);

                if (student == null)
                {
                    return Results.NotFound($"Student with ID {id} not found.");
                }
                return Results.Ok(student);
            });

          
            app.MapPut("/students/{id}", (SchoolDBContext context, int id, Student updatedStudent) =>
            {
                var existingStudent = context.Students.FirstOrDefault(s => s.ID == id);

                existingStudent.FirstName = updatedStudent.FirstName;
                existingStudent.LastName = updatedStudent.LastName;
                existingStudent.ClassID_FK = updatedStudent.ClassID_FK;

                context.SaveChanges();

                return Results.Ok(); // mer detaljerad alternativ  return Results.Ok($"Student with ID {id} deleted."); 
            });

           

            app.MapDelete("/student/{id}", (int id, SchoolDBContext context) =>
            {
                var student = context.Students.FirstOrDefault(s => s.ID == id);
                if (student == null)
                {
                    return Results.NotFound($"Student with {id}.Not found.");
                }
                context.Students.Remove(student);
                context.SaveChanges();
                return Results.Ok($"Student with ID {id} deleted.");

            });

            app.Run();
        }
    }
}
