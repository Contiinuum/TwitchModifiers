# TwitchModifiers

# Important
* For easier value tweaking get [Mod Settings mod written by october](https://github.com/octoberU/ModSettings/releases/latest)
* This Mod works with Score Overlay 2.0.3 and newer - [grab the Score Overlay mod written by october here](https://github.com/octoberU/ScoreOverlay/releases//latest)
* This Mod has a modifier (!rtxon) that relies on Ahriana's Timing Attack mod. [Download it here](https://github.com/Ahriana/AUDICA-Timing-Attack/releases/latest)
* Some mods won't allow you to submit scores to the leaderboard. If you want to prevent that, either disable those modifiers or disable "Allow Score Disabling Mods". <br /> Those modifiers include: 

   * speed below 100  
   * stream mode

# Channel Points
You can choose to require channel points for modifiers. you need to create a custom channel points reward. Name it whatever you want, just make sure the reward requires text. In that text, the user is required to type a command listed below.


## Command List and Default Values

 Command  | Default Values | Description 
 --- | --- | --- | 
 !speed | 50-150 | Changes song speed 
 !aa | 0-100 | Changes aim assist 
 !psy | 50-1000 | Enables psychedelia to speed after command
 !particles | 0-1000 | Sets particle amount
 !zoffset | -50-300 | Changes zOffset 
 !scale | 50-300 | Scales target distance
 !mines |  | Spawns mines at random positions 
 !womble | 50-150 | Wobbles song speed depending on value 
 !wobble |  | Wobbles song speed 
 !wooble |  | More retarded wobble 
 !wrobl |  | Wobble, but full retard 
 !invisguns |  | Turns guns invisible 
 !bettermelees |  | Turns melees into.. better melees.
 !randomoffset |  | Applies a random offset to all targets 
 !randomcolors |  | Picks random colors
 !colorswap |  | Swaps the player's colors around
 !streammode | | Turns chains into alternating standard targets
 !hiddenteles | | Hides telegraphs
 !unifycolors | | Sets left and right hand color to be the same
 !rtxon | | Disables clouds and darts ([Requires Timing Attack, written by Ahriana](https://github.com/Ahriana/AUDICA-Timing-Attack/releases/latest))
 !dropnuke | | Enables a buttload of modifiers at once
 !bopmode | | Flashes the lights to the beat of the song
 !stutterchains |1-10 | Rotates the arena on every chain node
 
 **Example usage:** !speed 120
 
 ## Configuration
 You can tweak the following general settings through the [Mod Settings mod written by october](https://github.com/octoberU/ModSettings/releases/latest):
 * Enable/disable the entire mod
 * Enable/disable countdown
 * Enable/disable mod status ingame display
 * Enable/disable mods that prevent you from posting scores to the leaderboard
 * Enable/disable channel point requirement
 * Change the cooldown before a new modifier can be activated
 * Change the amount of modifiers that can be active at once
 
 You can tweak every modifier individually. The following values can be tweaked:
 * Enable/disable a modifier
 * Change modifier duration
 * Change modifier cooldown
 * Set minimum and maximum values that can be used with a modifier
 
 ## Information
 * Per default, a modifier can only be triggered once every 20 seconds
 * Per default, a modifier will last 20 seconds
 * If you have particle killer installed and enabled, the !particles command will not do anything
