namespace MockingbirdLibrary.Moqs
{
    using MockingbirdLibrary;
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
            mock.Setup(x => x.HasProduct(product)).Throws<ArgumentNullException>();
            mock.Setup(x => x.CurrentStock(product)).Throws<ArgumentNullException>();
            mock.Setup(x => x.AddStock(product, amount)).Throws<ArgumentNullException>();
            mock.Setup(x => x.TakeStock(product, amount)).Throws<ArgumentNullException>();

            IWarehouse warehouse = mock.Object;
            Assert.ThrowsException<ArgumentNullException>(() => warehouse.HasProduct(product));
            Assert.ThrowsException<ArgumentNullException>(() => warehouse.CurrentStock(product));
            Assert.ThrowsException<ArgumentNullException>(() => warehouse.AddStock(product, amount));
            Assert.ThrowsException<ArgumentNullException>(() => warehouse.TakeStock(product, amount));
        }

        [DataTestMethod]
        [DataRow("", 3)]
        public void TestForEmptyParametersf(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(x => x.HasProduct(product)).Throws<ArgumentException>();
            mock.Setup(x => x.CurrentStock(product)).Throws<ArgumentException>();
            mock.Setup(x => x.AddStock(product, amount)).Throws<ArgumentException>();
            mock.Setup(x => x.TakeStock(product, amount)).Throws<ArgumentException>();

            IWarehouse warehouse = mock.Object;
            Assert.ThrowsException<ArgumentException>(() => warehouse.HasProduct(product));
            Assert.ThrowsException<ArgumentException>(() => warehouse.CurrentStock(product));
            Assert.ThrowsException<ArgumentException>(() => warehouse.AddStock(product, amount));
            Assert.ThrowsException<ArgumentException>(() => warehouse.TakeStock(product, amount));
        }

        [DataTestMethod]
        [DataRow("Mock")]
        public void TestCurrentStockForNoSuchProductException(string product)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(x => x.CurrentStock(product)).Throws<NoSuchProductException>();

            IWarehouse warehouse = mock.Object;
            Assert.ThrowsException<NoSuchProductException>(() => warehouse.CurrentStock(product));
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        public void TestTakeStockForNoSuchProductException(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(x => x.HasProduct(product)).Returns(false);
            mock.Setup(x => x.TakeStock(product, amount)).Throws<NoSuchProductException>();

            IWarehouse warehouse = mock.Object;
            Assert.ThrowsException<NoSuchProductException>(() => warehouse.TakeStock(product, amount));
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        public void TestTakeStockForInsufficientStockException(string product, int amount)
        {
            var mock = new Mock<IWarehouse>();
            mock.Setup(x => x.HasProduct(product)).Returns(true);
            mock.Setup(x => x.CurrentStock(product)).Returns(amount);
            mock.Setup(x => x.TakeStock(product, amount + 2)).Throws<InsufficientStockException>();

            IWarehouse warehouse = mock.Object;
            Assert.ThrowsException<InsufficientStockException>(() => warehouse.TakeStock(product, amount + 2));
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
            mock.Setup(x => x.HasProduct(product)).Returns(true);

            IWarehouse warehouse = mock.Object;
            warehouse.AddStock(product, amount);

            mock.Verify(x => x.AddStock(product, amount), Times.Once);
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
            mock.Setup(x => x.HasProduct(product)).Returns(true);
            mock.Setup(x => x.CurrentStock(product)).Returns(amount + 2);

            IWarehouse warehouse = mock.Object;
            warehouse.TakeStock(product, amount);

            mock.Verify(x => x.TakeStock(product, amount), Times.Once);
        }
    }
}