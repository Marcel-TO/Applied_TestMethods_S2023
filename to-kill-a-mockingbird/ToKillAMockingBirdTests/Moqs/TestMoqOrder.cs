namespace MockingbirdLibrary.Moqs
{
    using MockingbirdLibrary;
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
            mock.SetupProperty(order => order.Amount);
            mock.SetupProperty(order => order.Amount, amount);

            IOrder order = mock.Object;
            Assert.AreEqual(product, order.Product);
            Assert.AreEqual(amount, order.Amount);
        }

        [DataTestMethod]
        [DataRow("Mock", 7)]
        [DataRow("Mock", 4)]
        public void TestIfIsFilledStartsWithFalse(string product, int amount)
        {
            Order order = new Order(product, amount);
            var mock = new Mock<IWarehouse>();
            mock.Setup(x => x.HasProduct(product)).Returns(false);

            bool isFilled = order.IsFilled();
            Assert.IsFalse(isFilled);

            order.Fill(mock.Object);

            isFilled = order.IsFilled();
            Assert.IsTrue(isFilled);
        }

        [DataTestMethod]
        [DataRow("Mock", 7)]
        [DataRow("Mock", 4)]
        public void TestIfFillThrowsAlreadyFilledException(string product, int amount)
        {
            Order order = new Order(product, amount);
            var mock = new Mock<IWarehouse>();
            mock.Setup(x => x.HasProduct(product)).Returns(true);

            Assert.ThrowsException<OrderAlreadyFilled>(() => order.Fill(mock.Object));
        }

        [DataTestMethod]
        [DataRow("Mock", 7)]
        [DataRow("Mock", 4)]
        public void TestCanFillOrderIsTrue(string product, int amount)
        {
            Order order = new Order(product, amount);
            var mock = new Mock<IWarehouse>();
            mock.Setup(x => x.HasProduct(product)).Returns(false);

            Assert.IsTrue(order.CanFillOrder(mock.Object));
        }

        [DataTestMethod]
        [DataRow("Mock", 7)]
        [DataRow("Mock", 4)]
        public void TestCanFillOrderIsFalse(string product, int amount)
        {
            Order order = new Order(product, amount);
            var mock = new Mock<IWarehouse>();
            mock.Setup(x => x.HasProduct(product)).Returns(true);
            mock.Setup(x => x.CurrentStock(product)).Returns(amount);

            IWarehouse warehouse = mock.Object;
            Assert.IsFalse(order.CanFillOrder(warehouse));
        }
    }
}