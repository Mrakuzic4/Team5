Author of code review: Daniel Neff

Date of code review: 2/22/21

Sprint number: 2

Name of .cs file: BugEnemy.cs

Author of .cs file: Harshitha Kommaraju

Minutes taken to complete review: ~15 minutes

 - Comments on readability
 
    * Great use of comments throughout to explain what the code is doing
    
    * Methods are generally pretty short, making it easier to understand
    
    * Some variables could have better names e.g "temp" is a counter for updating the bug's state, enum "bugHealth" seems to be more of a indicator of the bug's state
    
    * Lots of individual if statements under the Update method (not a huge deal, this is just nitpicking)
    
 - Comments on maintainability
 
    * The bugStateMachine class is also in this file, which should be in its own file for best practice
    
    * Coupling seems relatively low for most methods/statements
    
    * Update method could use a switch statement instead, however since that code is mostly temporary the if statements work fine
    
 - Hypothetical change(s)
 
    * The LoadContent method can be removed, as it isn't called anywhere else (easy fix)
    
    * BugEnemy class doesn't need to hold a reference to the GraphicsDevice, can be removed (easy fix)
    
    * The functions to switch states in the BugEnemy class seem somewhat redundant, since they only call the respective function in the bugStateMachine.
    This might be able to be refactored, however the way it's set up now might be best for maintaining low coupling.
