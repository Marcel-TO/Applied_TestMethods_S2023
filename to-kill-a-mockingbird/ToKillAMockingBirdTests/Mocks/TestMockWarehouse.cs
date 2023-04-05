namespace ToKillAMockingBirdTests.Mocks
{
    using MockingbirdLibrary.Mocks;
    using MockingbirdLibrary.Interfaces;
    using MockingbirdLibrary.Exceptions;

    [TestClass]
    public class TestMockWarehouse
    {
        [DataTestMethod]
        [DataRow(null, 3)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestForNullParameters(string product, int amount)
        {
            IWarehouse warehouse = new MockWarehouse();

            warehouse.HasProduct(product);
            warehouse.CurrentStock(product);
            warehouse.AddStock(product, amount);
            warehouse.TakeStock(product, amount);
        }

        [DataTestMethod]
        [DataRow("", 3)]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForEmptyParametersf(string product, int amount)
        {
            IWarehouse warehouse = new MockWarehouse();

            warehouse.HasProduct(product);
            warehouse.CurrentStock(product);
            warehouse.AddStock(product, amount);
            warehouse.TakeStock(product, amount);
        }

        [DataTestMethod]
        [DataRow("Mock")]
        [ExpectedException(typeof(NoSuchProductException))]
        public void TestCurrentStockForNoSuchProductException(string product)
        {
            IWarehouse warehouse = new MockWarehouse();

            warehouse.CurrentStock(product);
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        [ExpectedException(typeof(NoSuchProductException))]
        public void TestTakeStockForNoSuchProductException(string product, int amount)
        {
            IWarehouse warehouse = new MockWarehouse();

            warehouse.TakeStock(product, amount);
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        [ExpectedException(typeof(InsufficientStockException))]
        public void TestTakeStockForInsufficientStockException(string product, int amount)
        {
            IWarehouse warehouse = new MockWarehouse();

            warehouse.AddStock(product, amount);
            warehouse.TakeStock(product, amount + 2);
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        [DataRow("Test", 2)]
        [DataRow("Apple", 5)]
        [DataRow("Banana", 2)]
        [DataRow("Mock", 8)]
        public void TestIfAddStockWorks(string product, int amount)
        {
            IWarehouse warehouse = new MockWarehouse();
            Assert.IsFalse(warehouse.HasProduct(product));
            warehouse.AddStock(product, amount);
            bool isAdded = warehouse.HasProduct(product);

            Assert.IsTrue(isAdded);
        }

        [DataTestMethod]
        [DataRow("Mock", 3)]
        [DataRow("Test", 2)]
        [DataRow("Apple", 5)]
        [DataRow("Banana", 2)]
        [DataRow("Mock", 8)]
        public void TestIfTakeStockWorks(string product, int amount)
        {
            IWarehouse warehouse = new MockWarehouse();
            warehouse.AddStock(product, amount + 1);
            warehouse.TakeStock(product, amount);

            Assert.AreEqual(warehouse.CurrentStock(product), 1);
        }
    } 
}