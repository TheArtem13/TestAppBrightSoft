using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TestAppBrightSoft.Controllers
{
    public class HomeController : Controller
    {
        private EfDbContext _context;
        public HomeController(EfDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var existRecords = _context.directoryObjectVersions.FirstOrDefault();
            if(existRecords == null)
            {
                //Если записей в БД нет, то создадим их
                GetFillDb();
            }
            return View();
        }

        private void GetFillDb()
        {
            
            for (int i = 0; i < 5; i++)
            {
                SetNewDirectoryVersionObject("Основная", i);
                SetNewDirectoryVersionObject("Черновик", i);
            }
        }

        private void SetNewDirectoryVersionObject(string version, int i)
        {
            Random rnd = new Random();
            var workVersion = new DirectoryObjectVersion()
            {
                Value = rnd.Next(1, 100),
                Version = new DataLayer.Entities.Version()
                {
                    VersionType = version,
                    Name = "Версия " + i
                },
                DirectoryObject = new DirectoryObject()
                {
                    Name = "Объект " + i,
                    Code = "S-0" + i,
                    Budjet = (float)rnd.Next(500, 1000000)
                }
            };
            _context.directoryObjectVersions.Add(workVersion);
            _context.SaveChanges();
        }
    }
}