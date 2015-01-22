# flockingbirds
Academic project for simulating flocking birds.

# Usage
## Command line
You can run program from command line. Available commands:
```
FlockingBirds - Help
Parameters descriptions for FlockingBirds program: 
	fullscreen:
		Expecting value: No
		Description: Determines wheter application should run in fullscreen mode.

	size:
		Expecting value: Yes
		Description: Determines the size of application. Format: widthxheight (like 320x240).

	runAtStart:
		Expecting value: No
		Description: Determines wheter application should run simulation immeditely.

	birdsCount:
		Expecting value: Yes
		Description: The quantity of birds in group. Less = better performance.

	visibilityDistance:
		Expecting value: Yes
		Description: Determines the range of bird's visibility (a circle, in px).

	groups:
		Expecting value: Yes
		Description: The quantity of groups.

	maxBirdSpeed:
		Expecting value: Yes
		Description: Birds maximum speed. Expressed in pixels per frame.

	birdsSeparation:
		Expecting value: Yes
		Description: Modifier for birds separation.

	noMouseInteraction:
		Expecting value: No
		Description: When set, birds wouldn't interact with mouse.

	maxSteer:
		Expecting value: Yes
		Description: Max bird's steer vector length.

	birdsCohesion:
		Expecting value: Yes
		Description: Modifier for birds cohesion.

	birdsAlignment:
		Expecting value: Yes
		Description: Modifier for birds alignment.

	wind:
		Expecting value: Yes
		Description: Wind vector. Usage: x;y.

```

## Configurator
If you are not familiar with command lines, you can use GUI layer that generates proper command line arguments and run programs. With GUI you can also save/load own configurations.
