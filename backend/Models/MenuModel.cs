using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    [Table("menu")]
    public class MenuModel
    {

        [Key]
        [Required]
        public int ID { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string content { set; get; }

        [Column(TypeName = "int")]
        [Required]
        public int lft { set; get; }

        [Column(TypeName = "int")]
        [Required]
        public int rgt { set; get; }

        public ICollection<MenuModel> childrens { get; set; }

        public int? parentID { get; set; }

        public MenuModel? parent { get; set; }
    }
}
