using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HamaraBasketRuleEngine.Domain
{
    public class RuleByItem
    {
        private readonly ILazyLoader _lazyLoader;
        public RuleByItem()
        {
        }
        public RuleByItem(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        [Key]
        public int ReleByItemId { get; set; }
        [Required]
        public int ItemId { get; set; }
        public int QualityChangesOverApproachingSellByDay { get; set; }
        public int QualityChangesAfterSellByDay { get; set; }
        public int MaxQualityValue { get; set; }
        public int MinQualityValue { get; set; }
        public Item Item { get; set; }
        private IList<RuleByNumberOfDaysLeft> _ruleByNumberOfDaysLeft;
        public IList<RuleByNumberOfDaysLeft> RuleByNumberOfDaysLeft
        {
            get => _lazyLoader.Load(this, ref _ruleByNumberOfDaysLeft);
            set => _ruleByNumberOfDaysLeft = value;
        }
    }
}
