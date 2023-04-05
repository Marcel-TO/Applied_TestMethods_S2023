# To Kill a Mockingbird

## Assignment
Write and test a class library used for managing item stock and orders in a warehouse.
The IWarehouse interface is defined as such:

```cs
interface IWarehouse
{
  bool HasProduct(string product);
  int CurrentStock(string product);
  void AddStock(string product, int amount);
  void TakeStock(string product, int amount);
}
```
- All methods throw for invalid product names (null or empty).
- CurrentStock throws NoSuchProductException when called with a product for which HasProduct returns false.
- TakeStock throws NoSuchProductException when called with a product for which HasProduct returns false.
- TakeStock throws InsufficientStockException when called with an amount that exceeds what is currently stored.
The Order class is used for filling individual orders from a warehouse.

Its interface is defined as such:
```cs
class Order
{
  Order(string product, int amount);
  bool IsFilled();
  bool CanFillOrder(IWarehouse warehouse);
  void Fill(IWarehouse warehouse);
}
```

- The constructor throws for invalid product names (null or empty) and amounts <1
- IsFilled starts out with false and becomes true once Fill has been called successfully.
- Fill rethrows any exceptions thrown by IWarehouse
- Fill throws OrderAlreadyFilled when Fill is called for an order that has already been filled
- CanFillOrder uses IWarehouse.HasProduct and IWarehouse.CurrentStock to check whether an order can be filled
Implement and test both the Order class and an implementation of IWarehouse.
When testing the Order class, use both your custom implementation of IWarehouse as well as Mock objects for testing the behaviour of Order.

Use <a href="https://github.com/moq/moq4" target="_blank">MOQ</a> or an equivalent Mocking library of your choice.


## Prepairing the Project in VS Code
### Prerequisites
- Visual Studio Code with the C# extension
- The preferred .NET SDK (In this case 7.0)

### Creating the Application
1. Open VS Code
2. Go to the Folder where your project should be created
3. Create the project for the application:
    ```
    dotnet new console -o NAMEOFPROJECT
    ```
4. Create the unit test project for the application:
    ```
    dotnet new mstest -o NAMEOFTESTPROJECT
    ```
5. Create a Solution File:
    ```
    dotnet new sln
    ```
6. Add the projects to the solution:
    ```
    dotnet sln add NAMEOFPROJECT
    dotnet sln add NAMEOFTESTPROJECT
    ```
7. Add a reference so that the test project can use the application:
    ```
    dotnet add NAMEOFTESTPROJECT reference NAMEOFPROJCET
    ```

### How to run and test the application
To run the application, go to the the application project and use the command:
```
dotnet run
```

To test the application, go to the testing project and use the command:
```
dotnet test
```
