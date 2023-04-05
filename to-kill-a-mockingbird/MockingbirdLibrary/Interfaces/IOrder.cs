namespace MockingbirdLibrary.Interfaces
{
    public interface IOrder
    {
        string Product {  get; protected set; }

        int Amount { get; set; }
        bool IsFilled();
        bool CanFillOrder(IWarehouse warehouse);
        void Fill(IWarehouse warehouse);
    }
}