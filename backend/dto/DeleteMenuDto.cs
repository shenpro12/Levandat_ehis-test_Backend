using System.ComponentModel.DataAnnotations;

namespace backend.dto
{
    public class DeleteMenuDto
    {
        [Required(ErrorMessage = "ID is Required!")]
        public int ID { get; set; }
    }
}
