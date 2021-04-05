Author: Jimmy Yuan

Date: Apr 5 2021

Sprint Number:4

Name of the .cs file being reviewed: GameWonOverLay.cs

Author of the .cs file being reviewed: Harshitha

Number of minutes taken to complete the review: 20

For code's Readability: Codes are pretty straight forward and easy to understand. However, there are a few fields that are not used and some consle print messages in the draw() method. 
Perhaps do a little clean up would be awesome. Moreover, I would recommend to comment the code to increase the readability.

For codes' Maintainability: Low Coupling and high cohesion. For the overlay implementation, it is very simple to change the overlay page as the page sprite
can be easily changed in the constructor. However, inside the update method, the implementation only works for slection between two options.
It might require extra work and refactor in order to have three or more options in the future.

A hypothetical change: The options in the gamewon page changed from two options to three options. The whole implementation might need to change
in update() method.
