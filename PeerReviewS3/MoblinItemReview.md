
Author of code review: Amarth Chen(.8759)

Date: 3/15/2021

Sprint number: 3

Name of .cs file being reviewed: `MoblinItem.cs`

Author of .cs file: Vidhan Bhardwaj

Time taken to complete review: 20 minutes count in the time to write this 


# Readability comments
* Most control flows are quite clean. 
* Most variables are named in a descriptive way. 
* The `Update()` method is pretty long, but since it's mostly same structure, it's not hard to understand.
* Some declaration feels a bit redundant, such as assiging `spriteWidth` and `spriteHeight` with a lobal variable without changing much.
* It is not really clear why `collidableTiles` needs to be an array, but since it's `public`, probably some outside code confined the
way how this cariable can be declared.

# Maintainability comments
* Moblin item deals with enemy, item, and collision all together, so it is inevitable to have a relatively high degree of coupling, 
considering the varity of things it needs to check and perform, possibily high cohesion as well. 
* The way this class is structured has minimized the coupling to a pretty low degree. Thus, despite the high coupling (compare to 
other less-integrated classes), maintenance-wise it is still accptable even for people who is not the author.

# Hypothetical change
* The `switch` cases in `Update()` are consititued of mostly the same content, it would probably be better to write a method with 
some input parameter to reduce possible duplicate.
* Add a scale factor in `Draw()` to make it look bigger. 
