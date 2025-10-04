using System.ComponentModel.DataAnnotations;

namespace ApiTask.Model
{
    public class RequestEntity
    {
        [Key]
        public int Id { get; set; }
        private string inputItems;
        public string InputItems
        {
            get => inputItems;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(inputItems));
                }
                inputItems = value;
            }
        }
        private int combinationLength;
        public int CombinationLength
        {
            get => combinationLength;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(combinationLength));
                }
                combinationLength = value;
            }
        }
        private DateTime createdAt;
        public DateTime CreatedAt
        {
            get => createdAt;
            set
            {
                if (value < DateTime.UtcNow.AddMinutes(-5))
                {
                    throw new ArgumentOutOfRangeException(nameof(createdAt));
                }
                createdAt = value;
            }
        }
        public List<CombinationEntity> Combinations { get; set; }
    }
}
