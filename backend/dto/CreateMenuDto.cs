using System.ComponentModel.DataAnnotations;

namespace backend.dto
{
    public class CreateMenuDto
    {
        [Required(ErrorMessage = "conntent is Required!")]
        [MaxLength(50)]
        public string content { get; set; }
        public int? subRight { get; set; } = null;
        public int? parent_ID { get; set; } = null;
    }
}
