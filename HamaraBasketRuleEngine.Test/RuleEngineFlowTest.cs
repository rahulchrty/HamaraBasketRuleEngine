using HamaraBasketRuleEngine.Business;
using HamaraBasketRuleEngine.Domain;
using HamaraBasketRuleEngine.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace HamaraBasketRuleEngine.Test
{
    [TestClass]
    public class RuleEngineFlowTest
    {
        private Mock<IItemWithRules> _mockItemWithRules;
        private Mock<IExecutingRules> _mockExecutingRules;
        private Mock<IQualityUpdationRepository> _mockQualityUpdationRepository;
        private IRuleEngineFlow _ruleEngineFlow;

        [TestInitialize]
        public void Setup()
        {
            _mockItemWithRules = new Mock<IItemWithRules>();
            _mockExecutingRules = new Mock<IExecutingRules>();
            _mockQualityUpdationRepository = new Mock<IQualityUpdationRepository>();
            _ruleEngineFlow = new RuleEngineFlow(_mockItemWithRules.Object, _mockExecutingRules.Object,
                                                _mockQualityUpdationRepository.Object);
        }

        [TestMethod]
        public void Execute_VerifyFlowMethodExecution()
        {
            //Given:

            //When: I call Execute Method
            _mockItemWithRules.Setup(x => x.GetItemWithRules()).Returns(new List<Item>());
            _mockExecutingRules.Setup(x => x.Execute(It.IsAny<List<Item>>()));
            _mockQualityUpdationRepository.Setup(x=>x.Update(It.IsAny<List<Item>>()));
            _ruleEngineFlow.Execute();
            //Then: Method GetItemWithRules, Execute and Update are executes.
            _mockItemWithRules.Verify(x => x.GetItemWithRules());
            _mockExecutingRules.Verify(x => x.Execute(It.IsAny<List<Item>>()));
            _mockQualityUpdationRepository.Verify(x => x.Update(It.IsAny<List<Item>>()));
        }

        [TestMethod]
        public void Execute_Given_NoItemList()
        {
            //Given: Item list as Empty

            //When: I call Execute Method
            List<Item> items = null;
            _mockItemWithRules.Setup(x => x.GetItemWithRules()).Returns(items);
            _ruleEngineFlow.Execute();
            //Then: Method Execute and Update never executes.
            _mockExecutingRules.Verify(x => x.Execute(It.IsAny<List<Item>>()), Times.Never);
            _mockQualityUpdationRepository.Verify(x => x.Update(It.IsAny<List<Item>>()), Times.Never);
        }
    }
}
