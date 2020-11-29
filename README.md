# TwitchModifiers

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
 !wobble |  | Wobbles song speed
 !invisguns |  | Turns guns invisible
 !bettermelees |  | Turns melees into.. better melees.
 !randomoffset |  | Applies a random offset to all targets
 
 **Example usage:** !speed 120
 
 ## Configuration
 You can tweak the following general settings through the [Mod Settings mod written by october](https://github.com/octoberU/ModSettings/releases/latest):
 * Enable/disable the entire mod
 * Enable/disable countdown
 * Enable/disable mod status ingame display
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
