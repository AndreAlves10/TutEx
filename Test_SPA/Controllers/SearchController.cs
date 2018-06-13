using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_SPA.DAL;
using Microsoft.EntityFrameworkCore;

namespace Test_SPA.Controllers
{
    [Produces("application/json")]
    [Route("api/Search")]
    public class SearchController : Controller
    {
        private readonly TestSPADBContext _context;

        public SearchController(TestSPADBContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetTeachers()
        {
            return Json(await _context.Teachers.ToListAsync());
            //return View(await _context.Teachers.ToListAsync());
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetTeacherTeachingAreas()
        {
            return Json(await _context.TeacherTeachingAreas.ToListAsync());
        }

        [HttpGet]
        [Route("[action]")]
        public object SearchAreaOfStudy()
        {
            var result = "Hello World";
            return result;
        }
    }
}