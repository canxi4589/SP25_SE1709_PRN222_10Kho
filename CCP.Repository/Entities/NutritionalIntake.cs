using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class NutritionalIntake : BaseEntity
    {
        [ForeignKey("Child")]
        public Guid ChildId { get; set; }

        [ForeignKey("FoodItem")]
        public Guid FoodItemId { get; set; }

        public DateTime? IntakeDate { get; set; }

        public float? ServingSize { get; set; }
        public DateTime? RecordDate {  get; set; }

        public string? Status { get; set; }       //status check if this has been booked for appointment
        // Navigation properties
        public Child Child { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
