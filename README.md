This is a horizontal slice of an action platformer that prototypes the traversal mechanics and simple hazards.

Combat mechanics have been simplified (death) or completely omitted (attacking).

Predictably, it is somewhat buggy.

Controls:
 - Quit the game by tapping Q
 - Move side to side with the left and right arrow keys
 - Jump with Spacebar
 - Attack enemies in front of you by tapping Z (Note: no animation, there is an attack cooldown, see Console Log for alerts)
	- direct attacks up by holding the up arrow key
	- direct attacks down during a jump by holding the down arrow key
 - Take on different forms to move through the world:
	- 1: PlainForm, allows you to attack and use abilities
	- 2: FlatForm, floats on thermals well but leap is disabled
	- 3: BallForm, rolls fast and bounces (collision artefacts)
 - Use special abilities to traverse obstacles
	- X: leap higher next time you jump (color changes when active)
	- C: dash in the direction you're moving (there is a cooldown of a few seconds)

Things to Do:
 - jump between platforms, avoiding enemies and spikes.
 - float on the updraft above the blue box on the ground. It lifts you higher when you jump if you are too heavy to float.
	- try reaching the high platform in one jump. Hint: leap + updraft
 - Try moving in each of the different forms, experimenting with traversal
 - Die on enemies and spikes!
