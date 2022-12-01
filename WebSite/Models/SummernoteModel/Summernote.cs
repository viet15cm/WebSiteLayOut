using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.SummernoteModel
{
    public class Summernote
    {
        public Summernote(string IdEditor , bool IsLoadLib)
        {
            this.IsLoadLib = IsLoadLib;
            this.IdEditor = IdEditor;
        }
        public string IdEditor { get; set; }
        public bool IsLoadLib { get; set; }
    }
}
