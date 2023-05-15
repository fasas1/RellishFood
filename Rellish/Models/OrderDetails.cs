using System.ComponentModel.DataAnnotations.Schema;

namespace Rellish.Models
{
    public class OrderDetails
    {
        public int OrderDetailId { get; set; }
        public int OrderHeaderId { get; set; }
        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }
    }
}
