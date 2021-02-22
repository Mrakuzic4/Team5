Author of the code review: Harshitha Kommaraju

Date of the code review: 02/22/21

Sprint number: 2

Name of the .cs file being reviewed: FireWallItem.cs

Author of the .cs file being reviewed: Zane Mrakuzic

Specific comments on code quality: 

Low coupling with other classes. 
Few repeating cases in switch constructs. 
State machine class included inside the same file.

A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change

Could modify the existing switch statements to have a case fall to reduce number of repeating cases.
(Could be easily supported since it requires a minor change in structure in the existing file)

Could move the state machine outside the class and have the firewallitem.cs inherit the class.
(Would be slightly harder to execute since it invlives the creation of a new file and subsequent tranefer of code into the file. Access to variables due to inheritence may also 
produce bugs)
