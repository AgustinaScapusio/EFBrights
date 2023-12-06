using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFConsulent.Domain.Entities;
using EFConsulent.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace EFConsulent.DataAccess
{
    public static class BrightsMethods
    {
        #region CreateMethods
        public static int CreateConsulent(Consulent consulent)
        {
            using BrightsDbContext db = new();
            db.Add(consulent);
            db.SaveChanges();
            return consulent.Id;
        }

        public static int CreateCourse(Course course)
        { 
            using BrightsDbContext db= new();
            db.Add(course);
            db.SaveChanges();
            return course.Id;
        }
        public static int CreateModule(Module module) 
        { 
            using BrightsDbContext db= new();
            db.Add(module);
            db.SaveChanges();
            return module.Id;
        }
        #endregion

        #region DeleteMethods
        public static void DeleteConsulent(Consulent consulent)
        {
            using BrightsDbContext db = new();
            db.Remove(consulent);
            db.SaveChanges();
        }

        public static void DeleteConsulent(int consulentId)
        {
            using BrightsDbContext db = new();
            Consulent consulent= db.Consulent.Where(c=>c.Id==consulentId).Single();
            BrightsMethods.DeleteConsulent(consulent);
        }
        public static void DeleteCourse(Course course)
        {
            using BrightsDbContext db = new();
            db.Remove(course);
            db.SaveChanges();
        }
        public static void DeleteCourse(int courseId)
        {
            using BrightsDbContext db = new();
            Course course=db.Course.Where(c=>c.Id == courseId).Single();
            BrightsMethods.DeleteCourse(course);
        }
        public static void DeleteModule(Module module)
        {
            using BrightsDbContext db = new();
            db.Remove(module);
            db.SaveChanges();
        }
        public static void DeleteModule(int moduleId)
        {
            using BrightsDbContext db = new();
            Module module=db.Module.Where(m=>m.Id==moduleId).Single();
            BrightsMethods.DeleteModule(module);
        }
        #endregion

        #region DatabaseMethods
        public static void RebuildDatabase()
        {
            using BrightsDbContext db = new();

            // Deletes the entire database:
            db.Database.EnsureDeleted();

            // Recreates the DB tables, based on the Migrations folder data.
            //db.Database.Migrate();
            // NOTE: EnsureCreated(), below, also recreates the DB tables. But
            // it doesn't play well with migrations. Only use it if you
            // don't plan to build further on DB with migrations later.
            db.Database.EnsureCreated();
        }
        public static void ClearDatabase()
        {
            using BrightsDbContext db = new();
            db.Course.ExecuteDelete();
            db.Consulent.ExecuteDelete();

            ResetIdentityStartingValue("Consulent");
            ResetIdentityStartingValue("Course");
            ResetIdentityStartingValue("CourseModule");
            ResetIdentityStartingValue("Email");
            ResetIdentityStartingValue("Module");
            

            db.SaveChanges();
        }

        public static void ResetIdentityStartingValue(string tableName, int startingValue = 1)
        {
            using (BrightsDbContext db = new())
            {
                db.Database.ExecuteSqlRaw("IF EXISTS(SELECT * FROM sys.identity_columns " +
                                    "WHERE OBJECT_NAME(OBJECT_ID) = @tableName AND last_value IS NOT NULL) " +
                                    "DBCC CHECKIDENT(@tableName, RESEED, @startingValueMinusOne);",
                                    new SqlParameter("tableName", tableName),
                                    new SqlParameter("startingValueMinusOne", startingValue - 1));
            }
        }

        #endregion

        #region ReadMethods
        public static List<string> ReadConsulentsNames()
        {
            using BrightsDbContext db = new();
            List<string> list = db.Consulent.Select(c => c.Name).ToList();
            return list;
        }
        public static List<string> ReadCourseWithModules(int courseId)
        {
            using BrightsDbContext db = new();
            Course course = db.Course.Include(c => c.CourseModules)!
                                   .ThenInclude(c => c.Module)
                                   .Where(c => c.Id == courseId).Single();

            List<string> modules = new() { course.Name };
            foreach (CourseModule m in course.CourseModules)
            {
                modules.Add(m.Module.Title);
            }
            return modules;

        }
        public static Consulent? ReadConsulent(int id){
            using BrightsDbContext db= new();
            Consulent consulent=db.Consulent.Where(c=>c.Id==id).Single();
            return consulent;
        }
        
        #endregion
    }
}
