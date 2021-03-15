Author: Harshitha Kommaraju

Date: Mar 15 2021

Sprint Number:3

Name of the .cs file being reviewed: PlayerCollisionDetector.cs

Author of the .cs file being reviewed: Jimmy Yuan

Number of minutes taken to complete the review: 20

For code's Readability: 
Detailed comments and descriptive variable names throughout the file increases readability significantly. The method contracts for all the methods along 
with the paramter explanation is very descriptive and easy to understand. Function names and variable names a little too similar but readable with the extensive comments. 

For codes' Maintainability: 
Lower cohesion since the class checks for collision against enemies, blocks and walls alomg with player sword with enemy. The methods are all serving collison detection 
but there are also methods like </br></br>
*private CollisionType checkHorizontalCollision(Rectangle collisionBox, Rectangle playerHitbox)*</br></br>
that could benefit from being in another file. Only coupling is with a dorect reference to game1 object which is inevitable therefore desired coupling level.

A hypothetical change: 
A hypothetical change would be to add the seperate collision handler methods into seperate files for better cohesion. This change would be easy to implement as it would only
require additional files to be created with existing code and references to PlayerCollisionDetector changed to call the more specific Collision Detectors. 
