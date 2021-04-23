Author of code review: Daniel Neff

Date: 4/23/2021

Sprint number: 5

Name of .cs file being reviewed: SoundFactory.cs

Author of .cs file: Vidhan Bhardwaj

Time taken to complete review: 15 minutes

## Readability comments
- Kept code/logic short and optimal for the feature, works well for what it aims to do
- Comments seem slightly rushed; some seem unnecessary and some could use a little more description
- Delegating the actual song selection logic to the SoundFactory that was implemented last sprint keeps the class very organized

## Maintainability comments
- Adding new songs or sound options in the future would be relatively easy code-wise, most the logic would probably go into SoundFactory
- Since the class uses a static image overlay, new songs would require editing and copying in a new overlay file
- High cohesion since the class controls only the things that happen while the sound menu is open
- Only references the Game1 class and the singleton SoundFactory class, therefore low coupling

## Hypothetical change
Adding options in the menu such as muting music or muting sound effects could be a simple process, maybe just switching a boolean in the SoundFactory class
to not play music/FX. That said, would still require some graphics work to redesign the menu overlay image. 
