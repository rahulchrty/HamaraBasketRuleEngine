using System;
using System.ComponentModel.DataAnnotations;

namespace HamaraBasketRuleEngine.Domain
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        public int? SellBy { get; set; }
        public int? Quality { get; set; }
        public RuleByItem RuleByItem { get; set; }
    }
}
