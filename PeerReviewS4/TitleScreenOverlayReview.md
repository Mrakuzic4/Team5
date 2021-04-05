Author: Harshitha Kommaraju

Date: Apr 5 2021

Sprint Number:4

Name of the .cs file being reviewed: TitleScreenOverLay.cs

Author of the .cs file being reviewed: Daniel Neff

Number of minutes taken to complete the review: 20

For code's Readability: The code structure is pretty clean and straightforward. The variable names and method names are pretty descriptive and easy to read and follow. 
Minimal comments explaining methods and variables.To increase readability, there could be more comments explaining the methods and the branching process and describing 
the magic numbers used in the code.  

For codes' Maintainability: The file has high cohesion and comparitively low coupling. There are references to the game class, but it cannot be helped. The class has one purpose 
to draw the title screen and all the methods work towards that purpose. Hence the code has good maintainability. Any changes to the title screen can be done in the same file

A hypothetical change: The implementation of the draw() method can be extracted to a seperate interface, for example, IOverlay(). The gameWonScreenOverlay, gameOverScreenOverlay, 
and TitleScreen could all implement the interface, and use a common draw method to abstract the implementation to different levels.  
