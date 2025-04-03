using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.DTOs
{
    public class NutritionalIntakeDto
    {
        public Guid? Id { get; set; }

        public Guid? FoodItemId { get; set; } // Tham chiếu đến món ăn

        public DateTime? IntakeDate { get; set; } // Ngày trẻ ăn

        public float? ServingSize { get; set; } // Khẩu phần ăn (g/ml)

        public DateTime? RecordDate { get; set; } // Ngày ghi nhận

        public string? Status { get; set; } // Dùng cho đặt lịch khám hoặc xử lý

        // Optional: Nếu bạn muốn hiện tên món ăn kèm
        public string? FoodItemName { get; set; }


    }
}
