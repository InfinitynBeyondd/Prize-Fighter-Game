PRIZE FIGHTER

CONTROLS (KEYBOARD):
•WASD/Arrow Keys - Movement
•Mouse - Camera Controls (Levels only)
•Spacebar - Jump (Double Jump in midair, Wall Jump against walls)
•Left Click - Attack (Standing Punch on ground, Dive Punch in midair)
•Right Click - Grapple (Shoots grapple beam from arm and pulls towards grappable surfaces on contact)
•E - Interact
•P - Pause Game
•ESC - Quit Game

CONTROLS (XBOX CONTROLLER)
•Left Stick - Movement
•Right Stick - Camera Controls (Levels only)
•A - Jump (Double Jump in midair, Wall Jump against walls)
•X - Attack (Standing Punch on ground)
•B - Dive Punch (Midair only)
•Right Trigger - Grapple (Shoots grapple beam from arm and pulls towards grappable surfaces on contact)
•Y - Interact **CURRENTLY ONLY AVAILABLE ON KEYBOARD**
•Start - Pause Game

HOW TO PLAY:
•Players enter levels from the hub world, talking to the NPC at the end of the hub to begin.
•Players must clear levels by making it to the end, and the tokens they gather will unlock new cosmetics.
•Stickers can be used to unlock features in the gallery mode.

AREAS FOR FEEDBACK:
•Level difficulty
•Frog Jump implementation
•Lighting
•Model updates
•Collision issues

RELEASE NOTES (SPRINT 3 - Alpha Build):
•Major physics changes implemented; no more frog softlocks or inconsistent jumps!
•Pachinko (Level 2) and Claw Machine (Level 3) are now playable.
•New enemy types: Enemy Frog, Patrolling Cone and Claw, adding a practical use for combat mechanics
•Levels can now be influenced by hitting certain buttons with attacks
•Player’s Animation Controller refined

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
•Animation Controller - Player animations for grappling and dive punching are occasionally triggered incorrectly.
•Frogs - The frog enemies turn very abruptly in midair, causing some issues with their positioning.
•Claw Boss - If the claw is already descending when the hologram triggered to distract it is set active, it will not target the hologram and get stuck. It is still able to be attacked in this case, and hitting the boss will trigger the end of the fight.
•Collision Detection - Collision can be inconsistent, as players will sometimes be treated as if they are airborne on a ledge and fall until being considered on solid ground. Players can escape by utilizing the reset gravity of the dive punch and grapple, but it feels tedious and buggy.
•Cones - Cones are not deleted on attack, and instead fall over because the wrong transform is deleted.
•Gallery - Sometimes, it will take the Gallery multiple opens and closes to detect the acquired stickers. The game also stops running when returning to the Title Screen after the final boss, and the only thing the player can do is quit out.
