using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourDollarR2.Core
{
    public class Budget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CycleStartDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CycleEndDate { get; set; } = DateTime.Now;

        [DataType(DataType.Currency)]
        public decimal Funds { get; set; } = 0;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string OwnerEmail { get; set; } = "removethis@soonest.opp";
    }
}
