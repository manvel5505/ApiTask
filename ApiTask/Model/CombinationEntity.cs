using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTask.Model
{
    public class CombinationEntity
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public RequestEntity Request { get; set; }
        private string items;
        public string Items
        {
            get => items;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(items));
                }
                items = value;
            }
        }
    }
}
