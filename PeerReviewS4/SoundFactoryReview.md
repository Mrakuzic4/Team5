Author of code review: Daniel Neff

Date: 4/5/2021

Sprint number: 4

Name of .cs file being reviewed: SoundFactory.cs

Author of .cs file: Vidhan Bhardwaj

Time taken to complete review: 15 minutes

## Readability comments
- Minimal and efficient code, very easy to read
- The few comments simply help to structure the code into related chunks
- Variables are named well
- Having all the sound effect loading in its own LoadFactory() method inside the sound factory helps to keep the LoadContent() method in Game1.cs shorter and cleaner

## Maintainability comments
- Using the singleton pattern is great to maintain only one instance of SoundFactory across the project which avoids song/sound conflicts
- High cohesion, all sounds are managed through this class and each sound has it's own method
- Very low coupling, only holds references to itself and the sounds, and all fields are private
- Adding new sounds would be a simple process

## Hypothetical change
If we added a volume slider for the music, it would be easy enough to add a method for that but would require some slight changes in the SongByte class since
the volume level is currently hardcoded for each song. 
