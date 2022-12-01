﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class DataImage
    {
        [Key]
        public string Id { get; set; }

        public string Url { get; set; }

       
        public Guid? PostId { get; set; }

        //[ForeignKey("PostId")]
        //public virtual Post Post { get; set; }


    }
}
