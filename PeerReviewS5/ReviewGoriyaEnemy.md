Author of Code Review: Vidhan Bhardwaj

Date: 4/23/2021

Sprint: 5

Name of .cs files being reviewed: goriyaEnemy.cs and goriyaItem.cs

Author of .cs files: Harshitha Kommaraju

Time taken to complete review: 20 min

Readability Comments:
- Update() in both files are somewhat long and can get complex
- Good comments in goriyaEnemy to clarify the code, can add a couple more to goriyaItem
- Good use of recycling similar code in the past and changing up only what's necessary

Maintainability Comments:
- It'd be easy to modify small details of the goriya and/or item
- Good use of state machine in goriya enemy and item
- Relatively low coupling in both classes, only reference what is necessary
- Could be higher cohesion if enemy was split into multiple classes

Hypothetical Changes:
- Adding an IEnemyItem interface to help standardize enemy itmes across the board as more get added (so it would no longer implement IItem)
- Spliting up the enemy class since there is a lot of code in it, could increase the cohesion of the file
