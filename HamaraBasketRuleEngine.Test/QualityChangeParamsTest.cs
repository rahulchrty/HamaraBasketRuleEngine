using HamaraBasketRuleEngine.Business;
using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Test
{
    [TestClass]
    public class QualityChangeParamsTest
    {
        private Mock<IRuleByDayLeft> _mockRuleByDayLeft;
        private IQualityChangeParams _qualityChangeParams;
        [TestInitialize]
        public void Setup()
        {
            _mockRuleByDayLeft = new Mock<IRuleByDayLeft>();
            _qualityChangeParams = new QualityChangeParams(_mockRuleByDayLeft.Object);
        }

        [TestMethod]
        public void GetParamters_Given_An_Item_With_RuleByNumberOfDaysLeft_As_0_Rules_And_QualityChangesOverApproachingSellByDay_As_MInus_1()
        {
            //Given: An Item
            //And RuleByNumberOfDaysLeft with no rules
            //And QualityChangesOverApproachingSellByDay as -1

            Item item = new Item
            {
                ItemId = 1,
                ItemName = "Pencil",
                Quality = 2,
                SellBy = 2,
                RuleByItem = new RuleByItem
                {
                    ReleByItemId = 1,
                    ItemId = 1,
                    QualityChangesAfterSellByDay = -2,
                    QualityChangesOverApproachingSellByDay = -1,
                    MaxQualityValue = 50,
                    MinQualityValue = 0,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft>()
                }
            };

            //When: I call GetParamters method
            var result = _qualityChangeParams.GetParamters(item);

            //Then: I get QualityChangesOverApproachingSellByDate as -1
            Assert.AreEqual(-1, result.QualityChangesOverApproachingSellByDate);
            //And also gets other required values as it is
            Assert.AreEqual(2, result.SellBy);
            Assert.AreEqual(2, result.CurrentQuality);
            Assert.AreEqual(50, result.MaxQualityValue);
            Assert.AreEqual(0, result.MinQualityValue);
            Assert.AreEqual(-2, result.QualityChangesAfterSellByDate);
        }

        [TestMethod]
        public void GetParamters_Given_SellBy_Value_Is_Greater_Than_SellByDaysRule()
        {
            //Given: An Item
            //And RuleByNumberOfDaysLeft with list of rules
            //And having rule to increase quality by 2 when 10 days left to sell
            //And Current SellBy as 20
            //And QualityChangesOverApproachingSellByDay as 1

            Item item = new Item
            {
                ItemId = 1,
                ItemName = "Pencil",
                Quality = 50,
                SellBy = 20,
                RuleByItem = new RuleByItem
                {
                    ReleByItemId = 1,
                    ItemId = 1,
                    QualityChangesAfterSellByDay = 0,
                    QualityChangesOverApproachingSellByDay = 1,
                    MaxQualityValue = 50,
                    MinQualityValue = 0,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> { new RuleByNumberOfDaysLeft {
                                                                                DaysLeftToSellByDate = 10,
                                                                                QualityChangesByDaysLeft = 2
                                                                            } 
                    }
                }
            };

            //When: I call GetParamters method
            var result = _qualityChangeParams.GetParamters(item);

            _mockRuleByDayLeft.Setup(x => x.GetRuleByDayLeft(item)).Returns(0);

            //Then: I get QualityChangesOverApproachingSellByDate as 1
            Assert.AreEqual(1, result.QualityChangesOverApproachingSellByDate);
            //And also gets other required values as it is
            Assert.AreEqual(20, result.SellBy);
            Assert.AreEqual(50, result.CurrentQuality);
            Assert.AreEqual(50, result.MaxQualityValue);
            Assert.AreEqual(0, result.MinQualityValue);
            Assert.AreEqual(0, result.QualityChangesAfterSellByDate);
        }

        [TestMethod]
        public void GetParamters_Given_SellBy_Value_Is_Same_As_SellByDaysRule()
        {
            //Given: An Item
            //And RuleByNumberOfDaysLeft with list of rules
            //And having rule to increase quality by 2 when 10 days left to sell
            //And Current SellBy as 10
            //And QualityChangesOverApproachingSellByDay as 1

            Item item = new Item
            {
                ItemId = 1,
                ItemName = "Pencil",
                Quality = 50,
                SellBy = 10,
                RuleByItem = new RuleByItem
                {
                    ReleByItemId = 1,
                    ItemId = 1,
                    QualityChangesAfterSellByDay = 0,
                    QualityChangesOverApproachingSellByDay = 1,
                    MaxQualityValue = 50,
                    MinQualityValue = 0,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> { new RuleByNumberOfDaysLeft {
                                                                                DaysLeftToSellByDate = 10,
                                                                                QualityChangesByDaysLeft = 2
                                                                            }
                    }
                }
            };

            //When: I call GetParamters method
            _mockRuleByDayLeft.Setup(x => x.GetRuleByDayLeft(item)).Returns(2);
            var result = _qualityChangeParams.GetParamters(item);

            //Then: I get QualityChangesOverApproachingSellByDate as 2
            Assert.AreEqual(2, result.QualityChangesOverApproachingSellByDate);
            //And also gets other required values as it is
            Assert.AreEqual(10, result.SellBy);
            Assert.AreEqual(50, result.CurrentQuality);
            Assert.AreEqual(50, result.MaxQualityValue);
            Assert.AreEqual(0, result.MinQualityValue);
            Assert.AreEqual(0, result.QualityChangesAfterSellByDate);
        }

        [TestMethod]
        public void GetParamters_Given_SellBy_Value_Is_Same_As_SellByDaysRule_Then_Method_GetRuleByDayLeft_Executes_Once()
        {
            //Given: An Item
            //And RuleByNumberOfDaysLeft with list of rules
            //And having rule to increase quality by 2 when 10 days left to sell
            //And Current SellBy as 10
            //And QualityChangesOverApproachingSellByDay as 1

            Item item = new Item
            {
                ItemId = 1,
                ItemName = "Pencil",
                Quality = 50,
                SellBy = 20,
                RuleByItem = new RuleByItem
                {
                    ReleByItemId = 1,
                    ItemId = 1,
                    QualityChangesAfterSellByDay = 0,
                    QualityChangesOverApproachingSellByDay = 1,
                    MaxQualityValue = 50,
                    MinQualityValue = 0,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> { new RuleByNumberOfDaysLeft {
                                                                                DaysLeftToSellByDate = 10,
                                                                                QualityChangesByDaysLeft = 2
                                                                            }
                    }
                }
            };

            //When: I call GetParamters method
            var result = _qualityChangeParams.GetParamters(item);

            _mockRuleByDayLeft.Setup(x => x.GetRuleByDayLeft(item)).Returns(2);

            //Then: Method GetRuleByDayLeft executes once
            _mockRuleByDayLeft.Verify(x => x.GetRuleByDayLeft(item), Times.Once);
        }

        [TestMethod]
        public void GetParamters_Given_An_Item_With_RuleByNumberOfDaysLeft_As_NUll_Then_Method_GetRuleByDayLeft_Never_Executes()
        {
            //Given: An Item
            //And RuleByNumberOfDaysLeft as null
            //And QualityChangesOverApproachingSellByDay as -1

            Item item = new Item
            {
                ItemId = 1,
                ItemName = "Pencil",
                Quality = 2,
                SellBy = 2,
                RuleByItem = new RuleByItem
                {
                    ReleByItemId = 1,
                    ItemId = 1,
                    QualityChangesAfterSellByDay = -2,
                    QualityChangesOverApproachingSellByDay = -1,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft>()
                }
            };

            //When: I call GetParamters method
            var result = _qualityChangeParams.GetParamters(item);

            //Then: Method GetRuleByDayLeft never exevites
            _mockRuleByDayLeft.Verify(x => x.GetRuleByDayLeft(It.IsAny<Item>()), Times.Never);
        }
    }
}
