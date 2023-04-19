namespace ToKillAMockingBird
{
    using System;
    using MockingbirdLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            MockWarehouse warehouse = new MockWarehouse();
            warehouse.AddStock("Apple",7);
            warehouse.AddStock("Apple", 4);
            int stock = warehouse.CurrentStock("Apple");
            Console.WriteLine(stock);
        }
    }
}