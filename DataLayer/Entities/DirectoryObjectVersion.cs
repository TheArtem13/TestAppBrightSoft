using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class DirectoryObjectVersion
    {
        public int Id { get; set; }
        public DirectoryObject DirectoryObject { get; set; }
        public Version Version { get; set; }
        public int Value { get; set; }
    }
}
