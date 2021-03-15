# Team 5 3902 Game Documentation 

## Keyboard Control (sprint 3)

* `WASD` and arrow keys for moving the player character  
* `Z` and`N` to attack 
* Number key `1`, `2`, and `3` to use the item _(if you have any)_
* `Q` to quit 
* `R` to reset _(It is recommanded that you pick up all the items before reseting)_
* Mouse click `LMB` and `RMB` to switch between maps _(It is, again, recommanded that you pick up all the items before switching to other maps)_
* `U` and `I` key to auto collect/reload the item in the map _(only for now)_

  ### Gamepad Contol (not tested yet)
  * `DPAD` direction buttons for moving the player character
  * `A` button for attacking

  ### Features from previous patches but will likly be removed in the future
  * `O` and `P` to toggle between enemy displayed in the map 

## Running the project
* Project build using MonoGame 3.7.1 
* This project uses NuGet package `Json.net` to parse Json files. 
* Json files were hand-typen, and need to be marked as `Content` and `Always copy` in `Properties` in order to make the parser able to find them. 

## Known issues 

  ### Gaming experience 

  * Player may clip through static blocks if more than one movement key is held down
  * Enemies may suddenly become high on drugs if they find themseleves stuck in between 2 blocks 
  * Link needs to squeeze himself against the block/wall in order to pass through some "bottlenecks"
  * Link got the spirit and will try to use item even if he has none 
  * In newly loaded maps, Link might stuck a bit while using items 

  ### Code-wise 
  
  * Some residues from discarded patches 
  * Magic numbers in some files, but mostly sprint-3-specific


## Devs 

(Soted by last name)
* Vidhan Bhardwaj (bhardwaj.73)
* Amarth Chen (chen.8759)
* Harshitha Kommaraju (kommaraju.3)
* Zane Mrakuzic (mrakuzic.4)
* Daniel Neff (neff.331)
* Jimmy Yuan (yuan.799)
