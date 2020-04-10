using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReportApp.Models;

namespace ReportApp.Controllers
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

        public IActionResult Index(string json)
        {
            var model = new ShowModel();
            json = "{\'Strings\':{\'Table\':\'directoryObjects\',\'Data\':\'Code, Name\'},\'Columns\':{\'Table\':\'Versions\',\'Data\':\'Name\'}}";
            //json = "{\'Strings\':{\'Table\':\'Versions\',\'Data\':\'Name, VersionType\'},\'Columns\':{\'Table\':\'directoryObjects\',\'Data\':\'Name\'}}";
            TableData jsonData = JsonConvert.DeserializeObject<TableData>(json);
            var versionsOfObjects = _context.directoryObjectVersions.Include("Version").Include("DirectoryObject")
                .ToList();
            var abstractList = new List<object>();
            if(jsonData.Strings.Table == "directoryObjects")
            {
                foreach (var obj in versionsOfObjects.GroupBy(x => x.DirectoryObject))
                {
                    abstractList.Add(obj);
                }
            }
            else //Ну как-то так...
            {
                foreach (var obj in versionsOfObjects.GroupBy(x => x.Version))
                {
                    abstractList.Add(obj);
                }
            }
            //model.Strings = new List<string>();
            //model.Columns = new List<string>();
            //var stringsData = jsonData.Strings.Data.Split(',');
            //foreach(var str in stringsData)
            //{
            //    model.Strings.Add(str.Trim());
            //}
            //var columnData = jsonData.Columns.Data.Split(',');
            //foreach (var col in columnData)
            //{
            //    model.Columns.Add(col.Trim());
            //}
            var htmlTable = "";
            var headerAdded = false;
            foreach (var listItem in abstractList)
            {
                if(jsonData.Strings.Table == "directoryObjects")
                {
                    IGrouping<DirectoryObject, DirectoryObjectVersion> thisListItem = (IGrouping<DirectoryObject, DirectoryObjectVersion>)listItem;
                    if(!headerAdded)
                    {
                        var th = "<tr>";
                        if (jsonData.Strings.Data.ToLower().Contains("name")) { th += "<td></td>"; }
                        if (jsonData.Strings.Data.ToLower().Contains("code")) { th += "<td></td>"; }
                        if (jsonData.Strings.Data.ToLower().Contains("budjet")) { th += "<td></td>"; }
                        foreach (var v in thisListItem)
                        {
                            th += "<td>" + v.Version.Name + " (" + v.Version.VersionType + ")</td>";
                        }
                        htmlTable += th;
                        headerAdded = true;
                    }
                    var tr = "<tr>";
                    if (jsonData.Strings.Data.ToLower().Contains("name")) { tr += "<td>" + thisListItem.Key.Name + "</td>"; }
                    if (jsonData.Strings.Data.ToLower().Contains("code")) { tr += "<td>" + thisListItem.Key.Code + "</td>"; }
                    if (jsonData.Strings.Data.ToLower().Contains("budjet")) { tr += "<td>" + thisListItem.Key.Budjet + "</td>"; }
                    foreach(var v in thisListItem)
                    {
                        tr += "<td>" + v.Value + "</td>";
                    }
                    tr += "</tr>";
                    htmlTable += tr;
                }
                else
                {
                    IGrouping<DataLayer.Entities.Version, DirectoryObjectVersion> thisListItem = (IGrouping<DataLayer.Entities.Version, DirectoryObjectVersion>)listItem;
                    if (!headerAdded)
                    {
                        var th = "<tr>";
                        if (jsonData.Strings.Data.ToLower().Contains("name")) { th += "<td></td>"; }
                        if (jsonData.Strings.Data.ToLower().Contains("version")) { th += "<td></td>"; }
                        foreach (var v in thisListItem)
                        {
                            th += "<td>" + v.DirectoryObject.Name + "</td>";
                        }
                        htmlTable += th;
                        //headerAdded = true;
                    }
                    var tr = "<tr>";
                    if (jsonData.Strings.Data.ToLower().Contains("name")) { tr += "<td>" + thisListItem.Key.Name + "</td>"; }
                    if (jsonData.Strings.Data.ToLower().Contains("version")) { tr += "<td>" + thisListItem.Key.VersionType + "</td>"; }
                    foreach (var v in thisListItem)
                    {
                        tr += "<td>" + v.Value + "</td>";
                    }
                    tr += "</tr>";
                    htmlTable += tr;
                }
            }
            model.HTMLTable = htmlTable;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
