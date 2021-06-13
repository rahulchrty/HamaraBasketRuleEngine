using HamaraBasketRuleEngine.Business;
using HamaraBasketRuleEngine.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HamaraBasketRuleEngine.Test
{
    [TestClass]
    public class SellByChangeTest
    {
        private ISellByChange _sellByChange;
        [TestInitialize]
        public void Setup()
        {
            _sellByChange = new SellByChange();
        }

        [TestMethod]
        public void ApplyRule_Given_Input_as_1_Then_I_Get_0()
        {
            //Given: currentSellByValue value as 1
            int? currentSellByValue = 1;
            //When: I call ApplyRule method
            var result = _sellByChange.ApplyRule(currentSellByValue);
            //Then: I get 0
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ApplyRule_Given_Input_as_20_Then_I_Get_19()
        {
            //Given: currentSellByValue value as 20
            int? currentSellByValue = 20;
            //When: I call ApplyRule method
            var result = _sellByChange.ApplyRule(currentSellByValue);
            //Then: I get 19
            Assert.AreEqual(19, result);
        }

        [TestMethod]
        public void ApplyRule_Given_Input_as_Null_Then_I_Get_NUll()
        {
            //Given: currentSellByValue value as null
            int? currentSellByValue = null;
            //When: I call ApplyRule method
            var result = _sellByChange.ApplyRule(currentSellByValue);
            //Then: I get null
            Assert.AreEqual(null, result);
        }
    }
}
