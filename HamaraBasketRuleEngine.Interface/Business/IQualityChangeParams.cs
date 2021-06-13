using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Model;

namespace HamaraBasketRuleEngine.Interface
{
    public interface IQualityChangeParams
    {
        QualityParameterModel GetParamters(Item item);
    }
}
