using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace razorEntity.Models
{
    public class Article
    {
        [Key]
        public int Id{set;get;}
        [Column(TypeName ="varchar(255)")]
        public string Title{get;set;}
        [DataType(DataType.Date)]
        public DateTime Created{set;get;}
        [Column(TypeName ="text")]
        public string Content{set;get;}
    }
}