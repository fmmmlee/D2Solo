# D2Solo<a href="https://ci.appveyor.com/project/fmmmlee/d2solo"><img src="https://ci.appveyor.com/api/projects/status/github/fmmmlee/d2solo" alt="UI" align="right"/></a>

Port blocker to disable matchmaking in Destiny 2.

#### Status

**Update 0.2.0 has changed the target ports to block the [ISteamNetworkingSockets](https://github.com/ValveSoftware/GameNetworkingSockets) interface, which Destiny 2 uses as of Season 10.**

UI snapshot, actual size:

![2-18-20-d2soloUI](https://user-images.githubusercontent.com/30479162/74798244-1b91fd80-5282-11ea-91c2-54d8e9ad6632.png)

See the [wiki](https://github.com/fmmmlee/D2Solo/wiki) for info.

### Current Todo:

- Mappable hotkeys
- See if you can port block by simply binding at user-level to all the ports D2 wants to access for matchmaking (to avoid admin requirement)
- temporary firewall rules? that way if process is terminated unexpectedly the rules don't have to manually be removed



##### Disclaimer: This is an unofficial tool and the author is not affiliated with Bungie. All trademarks, registered trademarks, logos, art, etc are the property of their respective owners. You use this tool at your own risk.