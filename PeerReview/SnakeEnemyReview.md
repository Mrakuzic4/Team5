# SnakeEnemy.cs Review  

Author of the code review: Amarth Chen(chen.8759)

Date of the code review: 22 Feb 2021

Sprint number: Sprint 2

Name of the .cs file being reviewed: SnakeEnemy.cs

Author of the .cs file being reviewed: Vidhan Bhardwaj (bhardwaj.73)

Number of minutes taken to complete the review: Roughly 30 min taken into account the time for writing this

## Specific comments on what is readable and what is not

* **Good Readability**: All fields are named in a self-explantory way, and they all have comments, so it's pretty easy to understand what their jobs are.  
* **Good Readability**: Grouped similar fields and simlar methods together. 
* **Minor Issue**: The state variable in state mahicne:
  ```C#
  public enum snakeHealth { Idle, Attack, Move, Die, Not }; // the different possible states
  ```
  is slightly confusing at first. The name is `snakeHealth` while it does not seem to have much to do with health. And it is a bit hard to guess what does `Not` do. 
* **Minor Issue**: 1 or 2 unused variables, probably residuals from previous versions.  

## Specific comments on code quality

* **High Quality**: All methods are either within 10 lines of code or split into sections, each does one job and one job only. 
* **High Quality**: Turned out the `Not` in `snakeHealth` is used to decide the visibility, adding this as a state is quite effective.  
* **Minor Issue**: Some if statements have partially same condition, which can be simplified (but since it's just temporarily code, does not really matter). 

## Misc

A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change

* Having `Not` in state could help marking its death and disappearing later when the snake is slain by the player. 
* Does not seem to specifies draw size, which might need modifications if the sprites' images are not in the same resolution. 


