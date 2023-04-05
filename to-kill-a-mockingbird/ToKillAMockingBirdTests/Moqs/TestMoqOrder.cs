namespace MockingbirdLibrary.Moqs
{
    using MockingbirdLibrary.Mocks;
    using MockingbirdLibrary.Exceptions;
    using MockingbirdLibrary.Interfaces;
    using Moq;

    [TestClass]
    public class TestMoqOrder
    {
        [DataTestMethod]
        [DataRow("Apple", 7)]
        [DataRow("Banana", 4)]
        public void TestInitializingOrder(string product, int amount)
        {
            var mock = new Mock<IOrder>();
            mock.SetupProperty(order => order.Product);
            mock.SetupProperty(order => order.Product, product);

            IOrder order = mock.Object;
            Assert.AreEqual(product, order.Product);
        }

        [TestMethod]
        public void TestIfParameterIsNull()
        {
            var mock = new Mock<IOrder>();
            mock.SetupProperty(order => order.Product);
            mock.SetupProperty(order => order.Product, null);
        }

        [TestMethod]
        public void TestIfParameterIsEmpty()
        {
            var mock = new Mock<IOrder>();
            mock.SetupProperty(order => order.Product);
            mock.SetupProperty(order => order.Product, "");
        }
    }
}