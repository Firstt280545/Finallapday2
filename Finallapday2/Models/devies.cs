using System.ComponentModel.DataAnnotations; //key
using System.ComponentModel.DataAnnotations.Schema; // Auto Increment

namespace WebApplication2.Models
{
    public class devices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Processor { get; set; }
        public float Price { get; set; }
        public int Manufacturer_id { get; set; }

    }
}
