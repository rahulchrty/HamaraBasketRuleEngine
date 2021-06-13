namespace HamaraBasketRuleEngine.Model
{
    public class QualityParameterModel
    {
        public int? CurrentQuality { get; set; }
        public int? SellBy { get; set; }
        public int MaxQualityValue { get; set; }
        public int MinQualityValue { get; set; }
        public int QualityChangesOverApproachingSellByDate { get; set; }
        public int QualityChangesAfterSellByDate { get; set; }
    }
}
