Author: Amarth Chen (.8759)

Date: 6 Apr 2021

Sprint Number: 4

Name of the .cs file being reviewed: `FoodItem.cs`

Author of the .cs file being reviewed: Zane Mrakuzic (.4)

Number of minutes taken to complete the review: roughly 10 minutes 

---

That's the 3rd peer review of the project and to be honest, there's not much new stuff to say about readability or maintainability. Members (me included?) are doing pretty good 
on naming or comment or logic etc.. So I'll just be nitpicking things that I think could improve. 

# Readability

The instantiation method has most of the assigment cramped together, it does not affect Readability but might be a bit unfriendly to OCD. 
Spilt them into groups of different usage or type might ease the eye.

# Maintainability

All good. 

# Hypothetical change

Most items seems to be using the same State Machine methods, if sprint 5 is going to add more items, then spilt the state machine into a separated class might decrease workload. 
