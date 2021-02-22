Author of the code review: Harshitha Kommaraju

Date of the code review: 02/22/21

Sprint number: 2

Name of the .cs file being reviewed: FireWallItem.cs

Author of the .cs file being reviewed: Zane Mrakuzic

Specific comments on code quality: 

- Code Readability
  * Good variable names through the class.
  * Functions update, draw and useItem have comments but could explain the different switch classes better.
  * Use of switch statements make the larger functions easier ton read and maintain.
  * Small functions for state machine make it easier to read.

- Code Maintainability
  * Low coupling with other classes. 
  * Few repeating cases in switch constructs that could be simplified. 
  * State machine class included inside the same file.
  * Nested switch statement structures could be cumbersome.

- Hypothetical Changes:

  * Could modify the existing switch statements to have falling cases to reduce number of repeating cases.
    (Could be easily supported since it requires a minor change in structure in the existing file)

  * Could move the state machine outside the class and have the firewallitem.cs inherit the class.
    (Would be slightly harder to execute since it invlives the creation of a new file and subsequent tranefer of code into the file. Access to variables due to inheritence may        also produce bugs)
    
  * Could remove the nested switch to have a seperate if branches to reduce complexity. 
    (Would be a little difficult but would not change the cohesion or coupling)
