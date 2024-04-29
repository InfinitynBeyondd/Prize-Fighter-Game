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

RELEASE NOTES (SPRINT 4 - Release-To-Manufacturing Build:)
•The frog softlock no longer occurs
•The Claw Machine now has 2 new phases added to the boss fight
•Level layout updates across all stages
•Update to Ground Checker to reduce instances of players getting stuck on terrain
•Functional dialogue for NPCs, including iDog, Black Frog and the Claw
•Shop and Gallery fixes implemented; game no longer breaks after a finished run
•Camera sensitivity fixes
•All SFX and final OST versions implemented

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
•Animation Controller - Dive Punching while grappling does not trigger the animation properly.
•Collision Detection - If a player lands on the edge of a platform, they may occasionally get stuck and will need to reset their gravity scale using the Grapple mechanic to get unstuck.
•Gallery - The gallery does not function as intended in the Hub World, but works fine within levels.
•Shop - The shop does not detract coins when purchasing skins, and going back to the shop when a skin is bought after completing a level will make the game think the skin has not been purchased. Changing back to the Default Skin does not work as intended.
•Respawn Transition - Depending on the level, the respawn transition may be offset and may not always animate properly.
