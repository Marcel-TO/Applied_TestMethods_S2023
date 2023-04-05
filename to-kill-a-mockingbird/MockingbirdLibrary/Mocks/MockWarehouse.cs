namespace MockingbirdLibrary.Logic
{
    using System.Linq;
    using MockingbirdLibrary.Exceptions;
    using MockingbirdLibrary.Interfaces;

    public class MockWarehouse : IWarehouse
    {
        private List<MockOrder> _warehouse;

        public MockWarehouse()
        {
            this._warehouse = new List<MockOrder>();
        }

        public void AddStock(string product, int amount)
        {
            // checks if the product is null or empty.
            if (product == null)
                {
                    throw new ArgumentNullException($"The {product} must not be null.");
                }
            else if (product == string.Empty)
                {
                    throw new ArgumentException($"The {product} must not be empty.");
                }
            
            // checks if the product doesn't exist yet.
            if (!this.HasProduct(product))
            {
                this._warehouse.Add(new MockOrder(product, amount));
                return;
            }

            // iterates through the warehouse and adds the stock to the current product.
            for (int i = 0; i < this._warehouse.Count; i++)
            {
                if (this._warehouse[i].Product == product)
                {
                    this._warehouse[i].Amount += amount;
                    return;
                }
            }
        }

        public int CurrentStock(string product)
        {
            // checks if the product is null or empty.
            if (product == null)
                {
                    throw new ArgumentNullException($"The {product} must not be null.");
                }
            else if (product == string.Empty)
                {
                    throw new ArgumentException($"The {product} must not be empty.");
                }
            
            // checks if the product doesn't exist yet.
            if (!this.HasProduct(product))
            {
                throw new NoSuchProductException($"There is no such product with the name: {product}");
            }

            return this._warehouse.Find(x => x.Product == product).Amount;
        }

        public bool HasProduct(string product)
        {
            // checks if the product is null or empty.
            if (product == null)
                {
                    throw new ArgumentNullException($"The {product} must not be null.");
                }
            else if (product == string.Empty)
                {
                    throw new ArgumentException($"The {product} must not be empty.");
                }
            
            return this._warehouse.Exists(x => x.Product == product);
        }

        public void TakeStock(string product, int amount)
        {
            // checks if the product is null or empty.
            if (product == null)
                {
                    throw new ArgumentNullException($"The {product} must not be null.");
                }
            else if (product == string.Empty)
                {
                    throw new ArgumentException($"The {product} must not be empty.");
                }
            
            // checks if the product doesn't exist yet.
            if (!this.HasProduct(product))
            {
                throw new NoSuchProductException($"There is no such product with the name: {product}");
            }

            // checks if the amount exceeds the current stock.
            if (this.CurrentStock(product) < amount)
            {
                throw new InsufficientStockException($"The product: {product} has not enough stock in the current warehouse.");
            }

            // iterates through the current warehouse and takes the stock.
            for (int i = 0; i < this._warehouse.Count; i++)
            {
                if (this._warehouse[i].Product == product)
                {
                    this._warehouse[i].Amount -= amount;
                }
            }
        }
    }
}