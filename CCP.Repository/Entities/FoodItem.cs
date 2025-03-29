using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class FoodItem : BaseEntity
    {
        [Required, StringLength(50)]
        public string FoodGroup { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string ImagePath { get; set; }

        // Navigation property
        public ICollection<NutritionalIntake> NutritionalIntakes { get; set; }
    }
}
