Author: Jimmy Yuan

Date: Mar 15 2021

Sprint Number:3

Name of the .cs file being reviewed: Level.cs

Author of the .cs file being reviewed: Amarth Chen

Number of minutes taken to complete the review: 20

For code's Readability: There aren't lots of comments in Level.cs class, only single-purpose comments for each method is present. However,
most of the methods and variables are named in a way that can be easily understood by others. Moreover, with in-line comments inside methods,
the overall code's readability is good although I would suggest adding a little more comments.

For codes' Maintainability: Low coupling as it has few depencies. Most of the instance field variables are private and only visable inside
the class. Methods of the class serve the single-purpose to create map layouts for different levels, so high cohesion. Therefore, the code's 
overall maintainability is good.

A hypothetical change: if the map needs to have hidden breaking doors, the code can easily handles that by calling update() method.
Also, changes in different levels can be easily made by manipulating the .JSON file. 
