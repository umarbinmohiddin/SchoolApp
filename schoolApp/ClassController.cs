using Microsoft.AspNetCore.Mvc;
using schoolApp.Entities;

namespace schoolApp
{
    public class ClassController : Controller
    {
        public IActionResult classList()
         
        {
            var schooldbcontext = new SchoolDbContext();

            var classes =  schooldbcontext.Classes.ToList();


            return View(classes);
        }
    }
}
