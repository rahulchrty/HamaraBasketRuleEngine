using HamaraBasketRuleEngine.Business;
using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Test
{
    [TestClass]
    public class RuleByDayLeftTest
    {
        private IRuleByDayLeft _ruleByDayLeft;
        [TestInitialize]
        public void Setup()
        {
            _ruleByDayLeft = new RuleByDayLeft();
        }

        [TestMethod]
        public void GetRuleByDayLeft_Given_SellBy_Value_Is_Same_As_RuleByDayLeft()
        {
            //Givem: SellBy as 10
            //And QualityChangesOverApproachingSellByDay as 1
            //And DaysLeftToSellByDate 10
            //And QualityChangesByDaysLeft as 2

            Item item = new Item
            {
                SellBy = 10,
                RuleByItem = new RuleByItem
                {
                    QualityChangesOverApproachingSellByDay = 1,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> { 
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 10,
                            QualityChangesByDaysLeft = 2
                        }
                    }
                }
            };

            //When: I call GetRuleByDayLeft method
            var result = _ruleByDayLeft.GetRuleByDayLeft(item);

            //Then: I get 2
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetRuleByDayLeft_Given_SellBy_Value_Is_Smaller_Than_RuleByDayLeft()
        {
            //Givem: SellBy as 9
            //And QualityChangesOverApproachingSellByDay as 1
            //And DaysLeftToSellByDate 10
            //And QualityChangesByDaysLeft as 2

            Item item = new Item
            {
                SellBy = 9,
                RuleByItem = new RuleByItem
                {
                    QualityChangesOverApproachingSellByDay = 1,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> { 
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 10,
                            QualityChangesByDaysLeft = 2
                        }
                    }
                }
            };

            //When: I call GetRuleByDayLeft method
            var result = _ruleByDayLeft.GetRuleByDayLeft(item);

            //Then: I get 2
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetRuleByDayLeft_Given_Two_Sets_Of_SellByDayRules()
        {
            //Givem: SellBy as 10
            //And QualityChangesOverApproachingSellByDay as 1
            //And the first set of rule as
            //And DaysLeftToSellByDate 10
            //And QualityChangesByDaysLeft as 2
            //And the second set of rule as
            //And DaysLeftToSellByDate 3
            //And QualityChangesByDaysLeft as 3

            Item item = new Item
            {
                SellBy = 10,
                RuleByItem = new RuleByItem
                {
                    QualityChangesOverApproachingSellByDay = 1,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> { 
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 10,
                            QualityChangesByDaysLeft = 2
                        },
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 3,
                            QualityChangesByDaysLeft = 2
                        }
                    }
                }
            };

            //When: I call GetRuleByDayLeft method
            var result = _ruleByDayLeft.GetRuleByDayLeft(item);

            //Then: I get 2
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetRuleByDayLeft_Given_Two_Sets_Of_SellByDayRules_And_SellyBy_Value_as_9()
        {
            //Givem: SellBy as 9
            //And QualityChangesOverApproachingSellByDay as 1
            //And the first set of rule as
            //And DaysLeftToSellByDate 10
            //And QualityChangesByDaysLeft as 2
            //And the second set of rule as
            //And DaysLeftToSellByDate 3
            //And QualityChangesByDaysLeft as 3

            Item item = new Item
            {
                SellBy = 9,
                RuleByItem = new RuleByItem
                {
                    QualityChangesOverApproachingSellByDay = 1,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> {
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 10,
                            QualityChangesByDaysLeft = 2
                        },
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 3,
                            QualityChangesByDaysLeft = 2
                        }
                    }
                }
            };

            //When: I call GetRuleByDayLeft method
            var result = _ruleByDayLeft.GetRuleByDayLeft(item);

            //Then: I get 2
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetRuleByDayLeft_Given_Two_Sets_Of_SellByDayRules_And_SellyBy_Value_as_3()
        {
            //Givem: SellBy as 3
            //And QualityChangesOverApproachingSellByDay as 1
            //And the first set of rule as
            //And DaysLeftToSellByDate 10
            //And QualityChangesByDaysLeft as 2
            //And the second set of rule as
            //And DaysLeftToSellByDate 3
            //And QualityChangesByDaysLeft as 3

            Item item = new Item
            {
                SellBy = 3,
                RuleByItem = new RuleByItem
                {
                    QualityChangesOverApproachingSellByDay = 1,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> {
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 10,
                            QualityChangesByDaysLeft = 2
                        },
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 3,
                            QualityChangesByDaysLeft = 3
                        }
                    }
                }
            };

            //When: I call GetRuleByDayLeft method
            var result = _ruleByDayLeft.GetRuleByDayLeft(item);

            //Then: I get 3
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GetRuleByDayLeft_Given_Two_Sets_Of_SellByDayRules_And_SellyBy_Value_as_2()
        {
            //Givem: SellBy as 2
            //And QualityChangesOverApproachingSellByDay as 1
            //And the first set of rule as
            //And DaysLeftToSellByDate 10
            //And QualityChangesByDaysLeft as 2
            //And the second set of rule as
            //And DaysLeftToSellByDate 3
            //And QualityChangesByDaysLeft as 3

            Item item = new Item
            {
                SellBy = 2,
                RuleByItem = new RuleByItem
                {
                    QualityChangesOverApproachingSellByDay = 1,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> {
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 10,
                            QualityChangesByDaysLeft = 2
                        },
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 3,
                            QualityChangesByDaysLeft = 3
                        }
                    }
                }
            };

            //When: I call GetRuleByDayLeft method
            var result = _ruleByDayLeft.GetRuleByDayLeft(item);

            //Then: I get 3
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GetRuleByDayLeft_Given_SellBy_Value_Is_Greater_Than_RuleByDayLeft()
        {
            //Givem: SellBy as 11
            //And QualityChangesOverApproachingSellByDay as 1
            //And DaysLeftToSellByDate 10
            //And QualityChangesByDaysLeft as 2

            Item item = new Item
            {
                SellBy = 11,
                RuleByItem = new RuleByItem
                {
                    QualityChangesOverApproachingSellByDay = 1,
                    RuleByNumberOfDaysLeft = new List<RuleByNumberOfDaysLeft> {
                        new RuleByNumberOfDaysLeft {
                            DaysLeftToSellByDate = 10,
                            QualityChangesByDaysLeft = 2
                        }
                    }
                }
            };

            //When: I call GetRuleByDayLeft method
            var result = _ruleByDayLeft.GetRuleByDayLeft(item);

            //Then: I get 0
            Assert.AreEqual(0, result);
        }
    }
}
