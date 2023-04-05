namespace MockingbirdLibrary.Interfaces
{
    public interface IOrder
    {
        bool IsFilled();
        bool CanFillOrder(IWarehouse warehouse);
        void Fill(IWarehouse warehouse);
    }
}