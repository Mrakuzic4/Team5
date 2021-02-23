SPRINT 2 REFLECTION

- Sprint Task Assignments
  * Decided on 02/08/21
  * Sprint split into following tasks: player, enemy, items, blocks and commands 
 
- Overall team performance: 
  * Each individual member contributed to the project and worked on their tasks  
  
- Changes made during this sprint
  * Enemy classes, item classes and block classes implemented
  * Command pattern and keyboard controller implemented
  * Player movement,attack and damage implemented
  * Enemy movement, animation and damage implemented
  * Overall concept changed from original game to recreating Zelda
  
- Things aimed to be done differently next time
  * More joint review sessions to make sure everyone is comfortable with their tasks
  * Start on the tasks early and formulate a planned approach

Jimmy's Relfection on Player Implementation

Figuring out the design pattern was a big part of my work. At first I did not follow the design pattern professor talked about in the class. I neglected player class because I don’t know the reason of implemented an extra player class and went straight to PlayerStateMachine. It worked well for player movement. 

However, problems came up when I tried to implement player damaged state. The damaged state was just a mere decorator and it wasn’t necessarily a “state”, and therefore the player flashes when damaged for only one direction, and the damaged state/decorator went away if I walked in other directions. That is a really bad bug there and I wanted to fix it. 

I then realized the importance of the player class and tried to added the player class into my design pattern. I thought it would be a fast change, but it really took me a long time to figure out the correct design pattern. When I changed something in one file, inconsistency in other files causes errors. I guess now I realize how much time does low coupling save me during the maintenance. 

I had a lot of errors that I came up of different solutions to get around of. For example, the given SpriteFacotry code for every method in it was to “return new PlayerSprite”. I couldn’t use it because it because I have to use IPlayer interface instead of ISprite interface. I only use one object to draw and update in Game1.cs. I found another way to be more effective in my design pattern. I created a DrawPlayer class for the purpose of draw and update player. I then used the singleton design for that class. I changed “return new PlayerSprite” to DrawPlayer.Instance.SetSprite. This is the change in design pattern that really helped me. 

I now realize how important the planning stage is for every project. I did not spend much time in planning stage, and I hit a tons of walling during the implementation stage. I had trouble figuring out the right layout of everything, and I did not discuss with my teammates. That’s another important thing: to work with teammates. We as teammates are supposed to help each other out for difficulties, and not just do our own part of project.










