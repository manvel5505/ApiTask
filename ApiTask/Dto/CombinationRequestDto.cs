using System.ComponentModel.DataAnnotations;

namespace ApiTask.Dto
{
    public class CombinationRequestDto
    {
        [Required]
        public List<int> Items { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Length { get; set; }
    }
}
