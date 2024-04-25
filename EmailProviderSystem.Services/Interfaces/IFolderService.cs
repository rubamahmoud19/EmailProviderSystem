using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Services.Interfaces
{
    internal interface IFolderService
    {
        public bool createFolder(string path);
        //public bool IsFolderExists(string path);
        //public List<Folder> GetFolders();
    }
}
