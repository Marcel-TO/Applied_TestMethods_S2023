namespace MockingbirdLibrary.Logic
{
    using MockingbirdLibrary.Exceptions;
    using MockingbirdLibrary.Interfaces;

    public class MockOrder : IOrder
    {
        private string _product;

        private int _amount;

        private bool _isFilled;

        #pragma warning disable CS8618
        public MockOrder(string product, int amount)
        {
            this.Product = product;
            this.Amount = amount;
            this._isFilled = false;
        }

        public string Product
        {
            get
            {
                return this._product;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException($"The {nameof(this.Product)} must not be null.");
                }
                else if (value == string.Empty)
                {
                    throw new ArgumentException($"The {nameof(this.Product)} must not be empty.");
                }

                this._product = value;
            }
        }

        public int Amount
        {
            get
            {
                return this._amount;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException($"The {nameof(this.Amount)} must not be less than zero.");
                }

                this._amount = value;
            }
        }

        public bool CanFillOrder(IWarehouse warehouse)
        {
            // checks if the current warehouse already contains the exact order.
            if (warehouse.HasProduct(this.Product) && warehouse.CurrentStock(this.Product) == this.Amount)
            {
                return false;
            }

            return true;
        }

        public void Fill(IWarehouse warehouse)
        {
            // Checks if the prodcut already got filled.
            bool alreadyFilled = warehouse.HasProduct(this.Product);
            if (alreadyFilled)
            {
                throw new OrderAlreadyFilled($"The product {this.Product} is already existing in the warehouse.");
            }

            // tries to add the stock to the current warehouse, otherwise rethrows the exception.
            try
            {
                warehouse.AddStock(this.Product, this.Amount);
            }
            catch(Exception e)
            {
                throw e;
            }

            // Updates the is filled boolean.
            this._isFilled = true;
        }

        public bool IsFilled()
        {
            return _isFilled;
        }
    }
}