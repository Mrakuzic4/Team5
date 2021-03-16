Author of Code Review: Vidhan Bhardwaj

Date: 3/15/2021

Sprint: 3

Name of cs file being reviewed: BlockMovable.cs

Author of cs file being reviewed: Daniel Neff

Time taken to complete review: 20 min

Readability comments:
- The readability of the code is good, clean functions separated well
- Good naming convention, even though there weren't many comments I could easily see what the code was doing due to the names
- I liked the summary above each function, helped clarify the code even further

Maintainability comments:
- Relatively low coupling, since the entire game class isn't being passed in
- Good use of spreading out code through functions, the Update() method isn't long
- Good use of our global settings to keep the code consistent with our other files
- Nice job keeping functions concise

Hypothetical changes:
- Possibly use a different variable than a boolean to determine if the block can move horizontally or vertically (maybe an enum), just for code clarity
- When testing if counter == 64, you can replace 64 with BASE_SCALAR in global settings to avoid have int numbers in the code
