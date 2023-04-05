namespace MockingbirdLibrary.Moqs
{
    using MockingbirdLibrary.Mocks;
    using MockingbirdLibrary.Exceptions;
    using MockingbirdLibrary.Interfaces;
    using Moq;

    [TestClass]
    public class TestMoqWarehouse
    {
        [DataTestMethod]
        [DataRow(null, 3)]
        public void TestForNullParameters(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(order => order.HasProduct(product)).Throws<ArgumentNullException>();
            mock.Setup(order => order.CurrentStock(product)).Throws<ArgumentNullException>();
            mock.Setup(order => order.AddStock(product, amount)).Throws<ArgumentNullException>();
            mock.Setup(order => order.TakeStock(product, amount)).Throws<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow("", 3)]
        public void TestForEmptyParametersf(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(order => order.HasProduct(product)).Throws<ArgumentException>();
            mock.Setup(order => order.CurrentStock(product)).Throws<ArgumentException>();
            mock.Setup(order => order.AddStock(product, amount)).Throws<ArgumentException>();
            mock.Setup(order => order.TakeStock(product, amount)).Throws<ArgumentException>();
        }

        [DataTestMethod]
        [DataRow("Mock")]
        public void TestCurrentStockForNoSuchProductException(string product)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(order => order.CurrentStock(product)).Throws<NoSuchProductException>();
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        public void TestTakeStockForNoSuchProductException(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(order => order.TakeStock(product, amount)).Throws<NoSuchProductException>();
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        public void TestTakeStockForInsufficientStockException(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(order => order.AddStock(product, amount));
            mock.Setup(order => order.TakeStock(product, amount + 2)).Throws<NoSuchProductException>();
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        [DataRow("Test", 2)]
        [DataRow("Apple", 5)]
        [DataRow("Banana", 2)]
        [DataRow("Mock", 8)]
        public void TestIfAddStockWorks(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(warehouse => warehouse.HasProduct(product)).Returns(false);
            mock.Setup(order => order.AddStock(product, amount));
            mock.Setup(warehouse => warehouse.HasProduct(product)).Returns(true);
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        [DataRow("Test", 2)]
        [DataRow("Apple", 5)]
        [DataRow("Banana", 2)]
        [DataRow("Mock", 8)]
        public void TestIfTakeStockWorks(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(order => order.AddStock(product, amount));
            mock.Setup(order => order.TakeStock(product, amount));
        }
    }
}