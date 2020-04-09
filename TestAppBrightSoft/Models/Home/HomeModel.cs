using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppBrightSoft.Models.Home
{
    public class HomeModel
    {
        public List<DirectoryObject> directoryObjects { get; set; }
        public List<DataLayer.Entities.Version> Versions { get; set; }
    }
}
