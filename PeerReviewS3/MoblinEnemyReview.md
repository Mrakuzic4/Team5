Author: Zane Mrakuzic

Date: Mar 16 2021

Sprint Number: 3

Name of the .cs file being reviewed: MoblinEnemy.cs

Author of the .cs file being reviewed: Harshitha Kommaraju

Number of minutes taken to complete the review: 20

For code's Readability: 
Comments help identify variables and function purposes. Some comments have been left as possible changes to the enemy. 
Some comments like in state change methods are unnecessary and repetitive, impacting overall readability.

For codes' Maintainability: 
Some magic numbers used to count cycles can be difficult to change in the future. 
Leaving in commented out functionalities for other direction changes / movement as included in other enemies 
allows for easy implementation in the future.


A hypothetical change: 
Adding in constant variables that are initialized at the top of the code can help maitainability. 
If, for instance, developers wish to make the enemy turn / move faster, then they can simply change the constants.