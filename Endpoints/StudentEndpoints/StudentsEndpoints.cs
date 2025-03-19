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

            //app.MapPost("/students", async (StudentCreateDto newStudent, SchoolDBContext context) =>
            //{
            //    // ✅ Check if the provided ClassId exists in the Classes table
            //    var existingClass = await context.Classes.FindAsync(newStudent.ClassId);
            //    if (existingClass == null)
            //    {
            //        return Results.BadRequest($"Class with ID {newStudent.ClassId} does not exist.");
            //    }

            //    var student = new Student
            //    {
            //        FirstName = newStudent.FirstName,
            //        LastName = newStudent.LastName,
            //        Email = newStudent.EmailAdress,
            //        ClassID_FK = newStudent.ClassId // ✅ Correct mapping
            //    };

            //    context.Students.Add(student);
            //    await context.SaveChangesAsync();

            //    return Results.Ok(student);
            //});


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

            //app.MapPost("/student", async (StudentCreateDto newStudent, SchoolDBContext context) => // Adds a student  in databasen SchoolDBContext
            // {
            //     var validationContext = new ValidationContext(newStudent);
            //     var validationResult = new List<ValidationResult>();
            //     bool isValid = Validator.TryValidateObject(newStudent, validationContext, validationResult, true);

            //     if (!isValid)
            //     {
            //         return Results.BadRequest();
            //     }

            //     var student = new Student
            //     {
            //         FirstName = newStudent.FirstName,
            //         LastName = newStudent.LastName,
            //         Email = newStudent.EmailAdress,
            //         ClassID_FK = newStudent.ClassID

            //     };

            //     context.Students.Add(student);
            //     await context.SaveChangesAsync();
            //     return Results.Ok();
            //     //context.Students.Add(student);
            //     //await context.SaveChangesAsync();


            // });

            app.MapGet("/student/{id}", async (int id, SchoolDBContext context) =>
            {
                var student = await context.Students.FirstOrDefaultAsync(s => s.ID == id);

                if (student == null)
                {
                    return Results.NotFound($"Student with ID {id} not found.");
                }
                return Results.Ok(student);
            });

            //app.MapPut("/student/{id}", (int id, Student updatedStudent, SchoolDBContext context) =>
            //{
            //    var student = context.Students.Find(id);
            //    if (student == null)
            //    {
            //        return Results.NotFound($"Student with ID {id} not found.");
            //    }

            //    student.FirstName = updatedStudent.FirstName;
            //    student.LastName = updatedStudent.LastName;
            //    //student.Age = updatedStudent.Age; // Add property Age in student class otherwise not gonna work
            //    student.Class = updatedStudent.Class; // Adjust fields based on your model

            //    context.SaveChanges();
            //    return Results.Ok(student);
            //});

            //Aldors
            app.MapPut("/students/{id}", (SchoolDBContext context, int id, Student updatedStudent) =>
            {
                var existingStudent = context.Students.FirstOrDefault(s => s.ID == id);

                existingStudent.FirstName = updatedStudent.FirstName;
                existingStudent.LastName = updatedStudent.LastName;
                existingStudent.ClassID_FK = updatedStudent.ClassID_FK;

                context.SaveChanges();

                return Results.Ok(); // mer detaljerad alternativ  return Results.Ok($"Student with ID {id} deleted."); 
            });

            //Remove Student
            //app.MapDelete("/student/{id}", (int id, SchoolDBContext context) =>
            //{
            //    var student = context.Students.Find(id);
            //    if (student == null)
            //    {
            //        return Results.NotFound($"Student with ID {id} not found.");
            //    }

            //    context.Students.Remove(student);
            //    context.SaveChanges();
            //    return Results.Ok($"Student with ID {id} deleted.");
            //});

            //Aldors
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


            //app.MapGet("/Data", () => //Endpoint
            //{
            //    var userList = new List<string>()
            //    {
            //        "Aldor","Aldor Besher","Johan","Tobias","Yummi","Anna"
            //    };


            //    var filterUsers = userList.Where(u => u == "Aldor");
            //    return filterUsers;
            //});

            //app.MapGet("/Data", () => //Endpoint
            //{
            //    var userList = new List<string>()
            //    {
            //        "Aldor","Aldor Besher","Johan","Tobias","Yummi","Anna"
            //    };


            //    var filterUsers = userList.FirstOrDefault(u => u == "Aldor");
            //    return filterUsers;
            //});

            //app.MapGet("/Data", () => //Endpoint
            //{
            //    var userList = new List<string>()
            //    {
            //        "Aldor","Aldor Besher","Johan","Tobias","Yummi","Anna"
            //    };


            //    var filterUsers = userList.Count(u => u == "Aldor");
            //    return filterUsers;
            //});

            //app.MapGet("/Data", () => //Endpoint
            //{
            //    var userList = new List<string>()
            //    {
            //        "Aldor","Aldor Besher","Johan","Tobias","Yummi","Anna"
            //    };


            //    var filterUsers = userList.Any(n => n == "Aldor");
            //    return filterUsers;
            //});


            app.Run();
        }
    }
}
