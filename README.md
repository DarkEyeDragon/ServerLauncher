# ServerLauncher
Custom server launcher for [Minecraft](https://minecraft.net)

Testing phase. Please try to break it and report how you broke it.


### Current featues:
- Console log tab, supports colors (but only for warnings and errors for now)
- Customize server startup settings like XMX, XMS, Auto startup, Debug mode
- Playerlist with kick/ban/mute features (mute is not supported without plugins however)
- MS paint style statistic graph
- Watchdog (checks if the server is still responding after a while of inactivity) and will restart the server if not responding.

### Planned features:
- Support color codes from plugins like &6 and §6
- Allow custom JVM flags to be set.
- Make the ram graph not look like a kid drew it.
- Add additional information to the ram graph.

### Screenshots:
Please note that these are far from final and are bound to change!
![Console](https://darkeyedragon.me/images/console.png "Console")
![Statistics](https://darkeyedragon.me/images/statistics.png "Statistics")




### Versions:

#### Pre-release 1.3:
- Ditched the graph library
- Optimized some code

#### Pre-release 1.0-1.2:
- No changelog.