# SmashGame
Dan and Steve


Early stages of smash bros-type 2D multiplayer arena brawler.
Still working on the basic mechanics of hitboxes and movement.

Added death particle system. Needs to be tweaked. Only works in one direction (up), will need to factor in side boundaries and top boundaries. Currently only works correctly if player hits bottom boundary. Need to code in destory function so particle object does not remain

Spawning is now fully working.

	To do (spawning)-

  	Add spawn platforms so players do not fall right away

  	Invicible for time after spawning

  	Code multiple spawn points so players do not spawn on top of each other if they die at similar times

Down smash is working, but only in one direction. Trying to combine the direction of the attacker, with the distance between the two players, and the position of the target to "bounce" target back and forth until the final attack stroke.
