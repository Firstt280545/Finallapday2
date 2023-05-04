using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class positions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string positionId { get; set; }
        public string positionName { get; set; }
        public float baseSalary { get; set; }
        public float salaryIncreaseRate { get; set; }

    }
}



