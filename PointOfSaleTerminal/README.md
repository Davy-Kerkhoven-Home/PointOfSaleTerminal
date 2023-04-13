# PointOfSaleTerminal

## Author
Davy Kerkhoven (13/04/2023)

## Overview
This project is part of a technical assesment for a job application to XE. The overall goal was to make a library that represented a point of sale terminal that can process the cost of items in a cart (or something similar).

## Requirements
- Project is a library
- UI is not necessary
- Persistence not needed
- Should be able to run automated tests
- Clean, well-factored code

## Testing
A separate project has been made for the unit tests called PointOfSaleTerminal.Tests. To run the tests simply load the project into your favourite IDE (e.g. Visual Studio), and run the test through the IDE menus. For example in Visual Studio: (Menu)Test -> Run All Tests.

## Documentation
[Task Overview](./XE_Programming_Exercise_v3.2.pdf)

## Personal Notes
I came across and interesting situation while unit testing that CalculateTotal() could be called before the pricing had been set. It raised the question: could a cart with contents get away with free stuff if someone forgot to set (or code) the price list? 
Only coincidentally did the ScanProduct() method gaurd against this situation, as it expects to find matching product codes from its field _pricingList, and so will throw earlier on before CalculateTotal() is called with any products. However having this dependency on another method probably isn't ideal.
The idea of setting the pricing through a method call was taken out of the Excercise Question, which was only a suggestion. While simple enough to fix one way or another I opted to leave it as is and document my thought process around it instead.
Possible improvemnts:
* Move the setting of the pricing to the terminal's constructor, forcing it to be provided on creation.
* If CalculateTotal() detects _priceList is null, throw an InvalidOperationException as it could be considered invalid to call this method with the current object's state. This then would have to be caught further up and handle appropriately (so the whole system doesn't crash);
