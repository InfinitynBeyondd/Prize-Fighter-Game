PRIZE FIGHTER

CONTROLS (KEYBOARD):
•WASD/Arrow Keys - Movement
•Mouse - Camera Controls (Levels only)
•Spacebar - Jump (Double Jump in midair, Wall Jump against walls)
•Left Shift - Attack (Standing Punch on ground, Dive Punch in midair)
•E - Grapple (Shoots grapple beam from arm and pulls towards grappable surfaces on contact)
•Q - Change Scene (Hub World Only)
•P - Pause Game
•ESC - Quit Game

CONTROLS (XBOX CONTROLLER)
•Left Stick - Movement
•Right Stick - Camera Controls (Levels only)
•A - Jump (Double Jump in midair, Wall Jump against walls)
•X - Attack (Standing Punch on ground)
•B - Dive Punch (Midair only)
•Right Trigger - Grapple (Shoots grapple beam from arm and pulls towards grappable surfaces on contact)
•Y - Change Scene (Hub World Only)
•Start - Pause Game

HOW TO PLAY:
•Players enter levels from the hub world, pressing Q inside the portal to begin.
•Levels contain tokens used to progress to new levels, as well as stickers to unlock things in the Gallery Mode.
•Players must clear levels by making it to the end, and the tokens they gather will unlock new levels.

AREAS FOR FEEDBACK:
•Level difficulty
•Frog Jump implementation
•Lighting 
•Drop Shadow Spot Light
•Model updates

RELEASE NOTES (SPRINT 2 - Vertical Slice): 
•All assets implemented; no more proxy assets!
•Player size increased to accommodate the new model
•Basic UI Design and Pop-Ups
•Platforms updated and camera sensitivity adjusted
•Lighting and material adjustments
•Players now cast a spot light downward in midair; functions like a drop shadow to better position jumps

RELEASE NOTES (Pre-Vertical Slice): 
•All assets implemented (only proxy left is the player)
•Basic UI Design
•Double Jump, Wall Jump and Grapple refined for build based on prototype feedback
•Sloped surfaces and camera sensitivity adjusted

RELEASE NOTES (SPRINT 1 - Prototype):
•Whitebox level implemented
•Double Jump, Wall Jump and Grapple functional
•Sloped surfaces must have Slope physics material applied

KNOWN BUGS:
•Frogs - Jumping force is a set value, but presumably due to collision errors, the jump heights are inconsistent.
•Animation Controller - Player animations are triggered incorrectly. Due to time constraints, not all work as intended.
•Physics - Physics applications in-engine vary from physics applications in-build. This creates inconsistencies with frogs and the wall jump.
