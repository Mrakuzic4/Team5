Author of code review: Vidhan Bhardwaj

Date: 4/5/2021

Sprint: 4

Name of .cs file being reviewed: DrawPlayerHealth

Author of .cs file being reviewed: Jimmy Yuan

Readability Comments:
- Great use of comments to clarify what each bit of code is doing
- I especially liked the commented method headers
- Clear variable names, helps identify what they're used for
- Good spacing of code

Maintainability Comments:
- Easy to change code for hearts if we need to so in the future
- Good coupling, I see the need to bring in game1 in the constructor

Hypothetical Changes:
- Move constants to GlobalSettings to try and avoid magic numbers
- Potentially use the new DrawRectangle method we recently added to avoid having to repeatedly draw the rectangles for the differenct heart icons
