# Team 5 3902 Game Documentation 

## Control (Version 4.05)

* `WASD` and arrow keys for moving the player character.
* `Z` and`N` to attack.
* Number key `1` and `2` to use the item _(if you have any)_.
* `Q` to quit.
* `R` to reset (reset current room only).
* Mouse click `LMB` and `RMB` to switch between maps.
* `Escape` key to pause, the player can also switch items during the pause screen.

  ### Gamepad Contol 
  * `DPAD` direction buttons for moving the player character.
  * `A` button for attacking.

  ### Features from previous patches that has been removed or only avilable in Developer Mode 
  * `O` and `P` to toggle between enemy displayed in the map.
  * `3` to use the third item.
  * `U` and `I` key to auto collect/reload the item in the map.
  
## Running the project
* Project build using MonoGame 3.7.1 
* This project uses NuGet package `Json.net` to parse Json files. 
* Json files were hand-typen, and need to be marked as `Content` and `Always copy` in `Properties` in order to make the parser able to find them. 
* Some debug and local files have been accidentally pushed into the repo during the dev provess, but constitutes no threat for compiling or running the peoject. 
* Consider toggling on or off 
  ```cs
  public const bool DEV_MODE = true;
  ``` 
  in `GlobalSettings.cs` depending on your mood. 

## Known ~~Features~~ Issues 

  ### Gaming experience 

  * Some enemy has telepathy power, and the pain of dying experienced by them may invoke others who have alreday died, appear as force ghosts _"May the bug be with you"_. 
  * Some enemy can perform **mircle** by walking on water. 
  * Level loading logic may cause Link to prematurely step forward and got slapped on the face upon entering a new room (dang you probably won't notice if I don't tell you)
  * Item switching does work, just might be a bit hard to notice where they are. 
  * The boss respawns upon re-entering, providing the player with the experience of stabbing someone in the back.
  * The game teaches the player that sometimes the effort of pushing a block can result in vein, the block moved, but that's it, no nada no zilch no nothing, like life. 
  * _"If you kill, you better prepare to be killed"_. As such, after death, if the player is not mentally prepared enough before choosing to respwan, might die immediately again. 

  ### Code-wise 
  
  * Still some magical numbers scattered around the galaxy. 
  * Some commented out code left untouched for too long, forming their garbadge zones. 
  * Moderate amount of duplication. 

## Devs 

(Soted alphabetically by last name)
* Vidhan Bhardwaj (bhardwaj.73)
* Amarth Chen (chen.8759)
* Harshitha Kommaraju (kommaraju.3)
* Zane Mrakuzic (mrakuzic.4)
* Daniel Neff (neff.331)
* Jimmy Yuan (yuan.799)
