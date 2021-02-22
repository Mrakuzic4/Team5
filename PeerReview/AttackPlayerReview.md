Author of the code review: Zane Mrakuzic

Date of the code review: 02/22/21

Sprint number: 2

Name of the .cs file being reviewed: AttackPlayer.cs

Author of the .cs file being reviewed: Jimmy Yuan

Readability notes:

Code is well organized.
Drcorator class helps keep attack specific code seperate.
Could use more comments on why certain frames are set at certain times, unclear from client perspective.

Specific comments on code quality: 

One example of many player states. 
Many player states show high cohesion and low coupling as each state has its own purpose.

A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change

Class constants could be set so that the important frame numbers and timer values are named and can be changed.
Avoiding magic numbers using constants. (Could be easily implemented by renaming and adding a const int field)


