using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAppBrightSoft.Models.Home;

namespace TestAppBrightSoft.Controllers
{
    public class HomeController : Controller
    {
        private EfDbContext _context;
        private DataManager _dataManager;
        public HomeController(EfDbContext context, DataManager dataManager)
        {
            _context = context;
            _dataManager = dataManager;
        }
        public IActionResult Index()
        {
            //int r = _dataManager.GetObjectVersionValue(1,1); //Пример вызова функции задания п.6
            var existRecords = _context.directoryObjectVersions.FirstOrDefault();
            if(existRecords == null)
            {
                //Если записей в БД нет, то создадим их
                GetFillDb();
            }
            var model = new HomeModel();
            model.directoryObjects = new List<DirectoryObject>();
            model.Versions = new List<DataLayer.Entities.Version>();
            foreach (var vrsn in _context.directoryObjectVersions.Include("Version").Include("DirectoryObject"))
            {
                if(model.directoryObjects.FirstOrDefault(x=>x.Id == vrsn.DirectoryObject.Id) == null)
                {
                    model.directoryObjects.Add(vrsn.DirectoryObject);
                }
                model.Versions.Add(vrsn.Version);
            }
            return View(model);
        }
        public IActionResult MetaData()
        {
            var model = new MetadetaModel();
            model.tableMetadatas = new List<TableMetadata>();
            foreach (var entity in _context.Model.GetEntityTypes())
            {
                var tableData = new TableMetadata();
                tableData.TableName = entity.GetTableName();
                tableData.ColumnDatas = new List<ColumnData>();
                foreach (var propertyType in entity.GetProperties())
                {
                    var colData = new ColumnData();
                    colData.Name = propertyType.GetColumnName();
                    colData.DataType = propertyType.GetColumnType();
                    tableData.ColumnDatas.Add(colData);
                }
                model.tableMetadatas.Add(tableData);
            }
            return View(model);
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
            var directoryObjectToUse = new DirectoryObject();
            var existItem = _context.directoryObjects.FirstOrDefault(x => x.Code == "S-0" + i);
            if(existItem == null)
            {
                directoryObjectToUse = new DirectoryObject()
                {
                    Name = "Объект " + i,
                    Code = "S-0" + i,
                    Budjet = (float)rnd.Next(500, 1000000)
                };
            }
            else
            {
                directoryObjectToUse = existItem;
            }
            var workVersion = new DirectoryObjectVersion()
            {
                Value = rnd.Next(1, 100),
                Version = new DataLayer.Entities.Version()
                {
                    VersionType = version,
                    Name = "Версия " + i
                },
                //DirectoryObject = new DirectoryObject()
                //{
                //    Name = "Объект " + i,
                //    Code = "S-0" + i,
                //    Budjet = (float)rnd.Next(500, 1000000)
                //}
                DirectoryObject = directoryObjectToUse
            };
            _context.directoryObjectVersions.Add(workVersion);
            _context.SaveChanges();
        }
    }
}