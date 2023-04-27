# Applied_TestMethods_S2023
This repository contains the test exercises from the lecture "Angewandte Testverfahren" from the forth FH semester.

## Overview of the Examples
- [Who watches the watchman? (Testing functionality of a web application)](#who-watches-the-watchman)
- [To kill a Mockingbird (Using Mocks to test a library)](#to-kill-a-mockingbird)

# Who watches the watchman?
<p align="center">
<img src="./to-kill-a-mockingbird/banner.gif" width="800" >
</p>

## Assignment
Prepare a comprehensive test that covers the functionality of the <a href="https://files.perry.fyi/hero/" target="_blank">Tour of Heroes Web Application</a>.
Evaluate the functionality of the application and implement a test using <a href="https://www.selenium.dev/" target="_blank">Selenium</a> for browser automation.

The assignment will be graded based on
- Test Coverage
- Code Quality
- Documentation
- Followed Test Practices

## Execution
(Python) To be continued.......

<div style="text-align: right"><a href="#applied_testmethods_s2023">scroll to top</a></div>

# To kill a Mockingbird
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

## Execution
(C#) To be continued......

<div style="text-align: right"><a href="#applied_testmethods_s2023">scroll to top</a></div>