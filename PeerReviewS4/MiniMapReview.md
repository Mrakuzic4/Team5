Author: Zane Mrakuzic

Date: Apr 6 2021

Sprint Number: 4

Name of the .cs file being reviewed: Minimap.cs

Author of the .cs file being reviewed: Amarth Chen

Number of minutes taken to complete the review: 20

For code's Readability: Code is split up into highly cohesive functions. Each one is commented with purpose and proper use. 
Variable names are easy to understand and locate. Some are commented with purpose, especially colors.

For codes' Maintainability: Code makes great use of constant values to slightly change the appearance of the minimap if needed. 
Names are descriptive or well commented. Color options are also given for future implementations to appear differently since it
does not rely on static png files in content folder like most other drawn entities.

A hypothetical change: Creating vector position calulations outside of spriteBatch.Draw methods could improve readability and understanding by reducing line length.