using Microsoft.AspNetCore.Mvc;
using schoolApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace schoolApp
{
    public class SubjectController : Controller
    {

        public IActionResult SubjectsList()
        {
            var schoolDbContext = new SchoolDbContext();

            // LINQ QUERY - Method Syntax
            // EF will generate SQL query 
            // select * from dbo.subjects
            // it will get the result set( table records)
            // EF will convert the table records into List of Subject Objects
            var subjects = schoolDbContext.Subjects.ToList();

            return View(subjects);
        }

        public IActionResult SubjectEditor()
        {
            return View();
        }
    }
}
