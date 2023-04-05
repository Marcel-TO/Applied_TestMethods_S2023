namespace ToKillAMockingBirdTests.Mocks
{
    using MockingbirdLibrary.Mocks;

    [TestClass]
    public class TestMocksOrder
    {
        [DataTestMethod]
        [DataRow("Apple", 7)]
        [DataRow("Banana", 4)]
        public void TestInitializingOrder(string product, int amount)
        {
            MockOrder order = new MockOrder(product, amount);
            Assert.AreEqual(product, order.Product);
            Assert.AreEqual(amount, order.Amount);
        }
    }
}