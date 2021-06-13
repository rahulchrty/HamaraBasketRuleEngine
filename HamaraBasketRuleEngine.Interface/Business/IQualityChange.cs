using HamaraBasketRuleEngine.Model;

namespace HamaraBasketRuleEngine.Interface
{
    public interface IQualityChange
    {
        int ApplyRule(QualityParameterModel qualityParameter);
    }
}
