using System.ComponentModel.DataAnnotations;

namespace HamaraBasketRuleEngine.Domain
{
    public class RuleByNumberOfDaysLeft
    {
        [Key]
        public int RuleId { get; set; }
        [Required]
        public int RuleByItemId { get; set; }
        public int DaysLeftToSellByDate { get; set; }
        public int QualityChangesByDaysLeft { get; set; }
        public RuleByItem RuleByItem { get; set; }
    }
}
