Author: Jimmy Yuan

Date: Apr 22 2021

Sprint Number: 5

Name of the .cs file being reviewed: UpgradesOverlay.cs

Author of the .cs file being reviewed: Zane 

Number of minutes taken to complete the review: 20

For code's Readability: 
Variables and methods are named in a way that are easy to follow. Codes are pretty straight forward and easy to understand. 
Brilliant way to implement the selection menu. Very easy to follow in update method.

For codes' Maintainability: The code is easily maintainable. In the future, if more options are needed at the upgrade overlay page. They can be easily added.

A hypothetical change: 

There are lots of magic numbers in the final. Please make sure to avoid them by defining private constant. 

I don't know why some instance variables are defined inside the constructor and some are not. Maybe it would be better if defining variable in a consistent spot?

Update method is a little too longer. Maybe split it into different helper methods would be better.
