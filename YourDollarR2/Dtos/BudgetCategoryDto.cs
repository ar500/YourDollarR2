using System;
using System.ComponentModel.DataAnnotations;

namespace YourDollarR2.Dtos
{
    public class BudgetCategoryDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
