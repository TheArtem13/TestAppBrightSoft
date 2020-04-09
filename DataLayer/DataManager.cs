using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DataManager
    {
        private EfDbContext _context;
        public DataManager(EfDbContext context)
        {
            _context = context;
        }
        public int GetObjectVersionValue(int objectId, int versionId)
        {
            var result = 0;
            var thisObjectVersion = _context.directoryObjectVersions.Include("Version").Include("DirectoryObject")
                .Where(x => x.DirectoryObject.Id == objectId)
                .Where(x => x.Version.Id == versionId)
                .FirstOrDefault();
            if(thisObjectVersion != null)
            {
                result = thisObjectVersion.Value;
            }
            return result;
        }
    }
}
