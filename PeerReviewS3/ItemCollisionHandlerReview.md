Author of code review: Daniel Neff

Date: 3/15/2021

Sprint number: 3

Name of .cs file being reviewed: ItemCollisionHandler.cs

Author of .cs file: Zane Mrakuzic

Time taken to complete review: 15 minutes

## Readability comments
- Comments could be a little more descriptive and longer, but despite this the content of the class is simple enough to understand just by reading the code
- Variables are named well
- Some lines run on long and could use a line break in the middle to simplify reading (could be different for other people, I'm on a smaller-sized laptop screen)

## Maintainability comments
- Relatively low coupling, it only holds a reference to the player and only one or two variables are passed in to the methods
- Both the rectangle array and player fields are private and only accessible from inside the class, which is good
- Methods are simple and short (<10 lines), making any changes that might be necessary much easier

## Hypothetical change
If some item later in development interacts with a specific block/wall/etc., and that specific tile needs to be passed into a different method, this class will need to
return that tile, which will require some changes to all the methods. Furthermore, all the item classes which call upon these methods will need significant changes
in their code to reflect the change in the return variable. 
