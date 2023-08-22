using System.ComponentModel.DataAnnotations;

namespace backend.dto
{
    public class UpdateMenuDto
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string content { get; set; }
    }
}
