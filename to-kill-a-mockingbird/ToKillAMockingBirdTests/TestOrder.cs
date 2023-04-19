namespace ToKillAMockingBirdTests
{
    using MockingbirdLibrary;
    using MockingbirdLibrary.Interfaces;
    using MockingbirdLibrary.Exceptions;
    using Moq;

    [TestClass]
    public class TestMocksOrder
    {
        [DataTestMethod]
        [DataRow("Apple", 7)]
        [DataRow("Banana", 4)]
        public void TestInitializingOrder(string product, int amount)
        {
            Order order = new Order(product, amount);
            Assert.AreEqual(product, order.Product);
            Assert.AreEqual(amount, order.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIfParameterIsNull()
        {
            Order order = new Order(null, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestIfParameterIsEmpty()
        {
            Order order = new Order("", 6);
        }

        [DataTestMethod]
        [DataRow("Mock", 0)]
        [DataRow("Mock", -1)]
        [DataRow("Mock", -2)]
        [DataRow("Mock", -10)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIfAmountIsOutOfRange(string product, int amount)
        {
            Order order = new Order(product, amount);
        }

        [DataTestMethod]
        [DataRow("Mock", 7)]
        [DataRow("Mock", 4)]
        public void TestIfIsFilledStartsWithFalse(string product, int amount)
        {
            Order order = new Order(product, amount);
            IWarehouse warehouse = new MockWarehouse();

            bool isFilled = order.IsFilled();
            Assert.IsFalse(isFilled);

            order.Fill(warehouse);

            isFilled = order.IsFilled();
            Assert.IsTrue(isFilled);
        }

        [DataTestMethod]
        [DataRow("Mock", 7)]
        [DataRow("Mock", 4)]
        [ExpectedException(typeof(OrderAlreadyFilled))]
        public void TestIfFillThrowsAlreadyFilledException(string product, int amount)
        {
            Order order = new Order(product, amount);
            IWarehouse warehouse = new MockWarehouse();

            order.Fill(warehouse);
            order.Fill(warehouse);
        }

        [DataTestMethod]
        [DataRow("Mock", 7)]
        [DataRow("Mock", 4)]
        public void TestCanFillOrderIsTrue(string product, int amount)
        {
            Order order = new Order(product, amount);
            var mock = new Mock<IWarehouse>();
            Assert.IsTrue(order.CanFillOrder(mock.Object));
        }

        [DataTestMethod]
        [DataRow("Mock", 7)]
        [DataRow("Mock", 4)]
        public void TestCanFillOrderIsFalse(string product, int amount)
        {
            Order order = new Order(product, amount);
            IWarehouse warehouse = new MockWarehouse();

            order.Fill(warehouse);
            Assert.IsFalse(order.CanFillOrder(warehouse));
        }
    }
}