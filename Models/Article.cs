using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace razorEntity.Models
{
    public class Article
    {
        [Key]
        public int Id{set;get;}
        [Display(Name ="Tiêu đề")]
        [Required]
        [Column(TypeName ="varchar(255)")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="Độ dài không hợp lí")]
        public string Title{get;set;}
        [DataType(DataType.Date)]
        [Display(Name ="Ngày tạo")]
        [Required]
        public DateTime Created{set;get;}
        [Column(TypeName ="text")]
        [Display(Name ="Nội dung")]
        [Required]
        public string Content{set;get;}
    }
}