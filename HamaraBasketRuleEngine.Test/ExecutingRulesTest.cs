using HamaraBasketRuleEngine.Business;
using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using HamaraBasketRuleEngine.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Test
{
    [TestClass]
    public class ExecutingRulesTest
    {
        private Mock<ISellByChange> _mockSellByChange;
        private Mock<IQualityChangeParams> _mockQualityChangeParams;
        private Mock<IQualityChange> _mockQualityChange;
        private IExecutingRules _executingRules;
        [TestInitialize]
        public void Setup()
        {
            _mockSellByChange = new Mock<ISellByChange>();
            _mockQualityChangeParams = new Mock<IQualityChangeParams>();
            _mockQualityChange = new Mock<IQualityChange>();
            _executingRules = new ExecutingRules(_mockSellByChange.Object, _mockQualityChangeParams.Object,
                                                _mockQualityChange.Object);
        }

        [TestMethod]
        public void Execute_Given_AListWithOneItem_Then_AllFlowMethodsExecutes_Once()
        {
            //Given: A list of items
            List<Item> itemsWithRules = new List<Item>();
            itemsWithRules.Add(new Item { 
                ItemId = 1,
                ItemName = "Pencil",
                Quality = 2,
                SellBy = 2,
                RuleByItem = new RuleByItem { 
                    ReleByItemId = 1,
                    ItemId = 1,
                    QualityChangesAfterSellByDay = -2,
                    QualityChangesOverApproachingSellByDay = -1
                }
            });

            _mockSellByChange.Setup(x =>x.ApplyRule(itemsWithRules[0].Quality)).Returns(1);
            QualityParameterModel qualityParams = new QualityParameterModel
            { 
                SellBy = 2,
                MinQualityValue = 0,
                MaxQualityValue = 50,
                CurrentQuality = 2,
                QualityChangesAfterSellByDate = -2,
                QualityChangesOverApproachingSellByDate = 1
            };
            _mockQualityChangeParams.Setup(x => x.GetParamters(itemsWithRules[0])).Returns(qualityParams);
            _mockQualityChange.Setup(x => x.ApplyRule(qualityParams)).Returns(1);
            //When: I execute the method Execute
            _executingRules.Execute(itemsWithRules);

            //Then: Method _sellByChange.ApplyRule, _qualityChangeParams.GetParamters and _qualityChange.ApplyRule executes once
            _mockSellByChange.Verify(x => x.ApplyRule(It.IsAny<int?>()), Times.Once);
            _mockQualityChangeParams.Verify(x => x.GetParamters(It.IsAny<Item>()), Times.Once);
            _mockQualityChange.Verify(x => x.ApplyRule(It.IsAny<QualityParameterModel>()), Times.Once);
        }

        [TestMethod]
        public void Execute_Given_AListWithTwoItems_Then_AllFlowMethodsExecutes_Twice()
        {
            //Given: A list of items
            List<Item> itemsWithRules = new List<Item>();
            Item testItem = new Item
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
                    QualityChangesOverApproachingSellByDay = -1
                }
            };
            itemsWithRules.Add(testItem);
            itemsWithRules.Add(testItem);

            _mockSellByChange.SetupSequence(x => x.ApplyRule(itemsWithRules[0].Quality)).Returns(1).Returns(1);
            QualityParameterModel qualityParams = new QualityParameterModel
            {
                SellBy = 2,
                MinQualityValue = 0,
                MaxQualityValue = 50,
                CurrentQuality = 2,
                QualityChangesAfterSellByDate = -2,
                QualityChangesOverApproachingSellByDate = 1
            };
            _mockQualityChangeParams.SetupSequence(x => x.GetParamters(itemsWithRules[0])).Returns(qualityParams).Returns(qualityParams);
            _mockQualityChange.SetupSequence(x => x.ApplyRule(qualityParams)).Returns(1).Returns(1);
            //When: I execute the method Execute
            _executingRules.Execute(itemsWithRules);

            //Then: Method _sellByChange.ApplyRule, _qualityChangeParams.GetParamters
            //and _qualityChange.ApplyRule executes wtice

            _mockSellByChange.Verify(x => x.ApplyRule(It.IsAny<int?>()), Times.Exactly(2));
            _mockQualityChangeParams.Verify(x => x.GetParamters(It.IsAny<Item>()), Times.Exactly(2));
            _mockQualityChange.Verify(x => x.ApplyRule(It.IsAny<QualityParameterModel>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Execute_Given_AListWithNoItems_Then_Flow_Method_Never_Executes()
        {
            //Given: A list of items
            List<Item> itemsWithRules = new List<Item>();

            //When: I execute the method Execute
            _executingRules.Execute(itemsWithRules);

            //Then: Method _sellByChange.ApplyRule, _qualityChangeParams.GetParamters
            //and _qualityChange.ApplyRule never executes
            _mockSellByChange.Verify(x => x.ApplyRule(It.IsAny<int?>()), Times.Never);
            _mockQualityChangeParams.Verify(x => x.GetParamters(It.IsAny<Item>()), Times.Never);
            _mockQualityChange.Verify(x => x.ApplyRule(It.IsAny<QualityParameterModel>()), Times.Never);
        }

        [TestMethod]
        public void Execute_Given_AListWithOneItemHaveingNoSellby_Then_AllFlowMethods_Never_Executes()
        {
            //Given: A list of items
            List<Item> itemsWithRules = new List<Item>();
            itemsWithRules.Add(new Item
            {
                ItemId = 1,
                ItemName = "Pencil",
                Quality = 1,
                SellBy = null
            });
            //When: I execute the method Execute
            _executingRules.Execute(itemsWithRules);

            //Then: Method _sellByChange.ApplyRule, _qualityChangeParams.GetParamters
            //and _qualityChange.ApplyRule never executes
            _mockSellByChange.Verify(x => x.ApplyRule(It.IsAny<int?>()), Times.Never);
            _mockQualityChangeParams.Verify(x => x.GetParamters(It.IsAny<Item>()), Times.Never);
            _mockQualityChange.Verify(x => x.ApplyRule(It.IsAny<QualityParameterModel>()), Times.Never);
        }

        [TestMethod]
        public void Execute_Given_AListWithOneItemHaveingNoQuality_Then_AllFlowMethods_Never_Executes()
        {
            //Given: A list of items
            List<Item> itemsWithRules = new List<Item>();
            itemsWithRules.Add(new Item
            {
                ItemId = 1,
                ItemName = "Pencil",
                Quality = null,
                SellBy = 1
            });
            //When: I execute the method Execute
            _executingRules.Execute(itemsWithRules);

            //Then: Method _sellByChange.ApplyRule, _qualityChangeParams.GetParamters
            //and _qualityChange.ApplyRule never executes
            _mockSellByChange.Verify(x => x.ApplyRule(It.IsAny<int?>()), Times.Never);
            _mockQualityChangeParams.Verify(x => x.GetParamters(It.IsAny<Item>()), Times.Never);
            _mockQualityChange.Verify(x => x.ApplyRule(It.IsAny<QualityParameterModel>()), Times.Never);
        }

        [TestMethod]
        public void Execute_Given_AListWithOneItemHaveingNoSellbyAndQuality_Then_AllFlowMethods_Never_Executes()
        {
            //Given: A list of items
            List<Item> itemsWithRules = new List<Item>();
            itemsWithRules.Add(new Item
            {
                ItemId = 1,
                ItemName = "Pencil",
                Quality = null,
                SellBy = null
            });
            //When: I execute the method Execute
            _executingRules.Execute(itemsWithRules);

            //Then: Method _sellByChange.ApplyRule, _qualityChangeParams.GetParamters
            //and _qualityChange.ApplyRule never executes
            _mockSellByChange.Verify(x => x.ApplyRule(It.IsAny<int?>()), Times.Never);
            _mockQualityChangeParams.Verify(x => x.GetParamters(It.IsAny<Item>()), Times.Never);
            _mockQualityChange.Verify(x => x.ApplyRule(It.IsAny<QualityParameterModel>()), Times.Never);
        }
    }
}
