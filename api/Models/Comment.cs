using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;

          //Had fichier kaymthl table dyal Comment f database

namespace api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        public Stock? Stock { get; set; }
        public string AppUserId { get; set; } 
        public AppUser AppUser { get; set; } 
    }
}

