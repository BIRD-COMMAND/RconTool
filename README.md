(https://github.com/jaron780/RconTool)[Original Version] Created by Jaron

# RconTool
This is a tool for Server hosts to monitor and moderate their own servers.

## Build Instructions
- Clone/Download
- (https://github.com/sta/websocket-sharp)[WebSocketSharp] and (https://www.newtonsoft.com/json)[Json.NET] are both required and can be installed with nuget package manager.
- Install-Package WebSocketSharp -Pre | Install-Package Newtonsoft.Json
- Build and run.

# Update Note - BIRD COMMAND
## There are currently a lot of undocumented features, here's a brief list.

- Server selection is now a drop-down menu.
- Toggleable sound effects for when players leave or join the currently displayed server.
- Runtime commands are available in game. Use '!commands' to be PM'd the list.
- '!pm PlayerName Message content' - a private messaging system. Quotation marks are NOT required for player names(even names with spaces) or for messages.
- Additional command triggers have been added. Commands may be run when a player joins, leaves, or when the server player count enters a specified range.
- Added auto-complete for server commands in the console tab.
- Scoreboard rendering now supports multiple fonts and font sizes. File -> Settings -> Set Scoreboard Font Size to select it. Or use the buttons in the bottom-right.
- File -> About window is now 200% cooler.

### Runtime Votefile Manipulation and In-Game Voting + Match Queue
##### (LOCAL ONLY - Only available if Rcon Tool is running on the same computer that is hosting the server)

- Configure the settings from the option in the settings menu.
- You'll be able to use '!list games', '!list maps', and '!voteAddGame gametype mapname' to start an in-game vote for the next match.
- If the vote passes a dynamic votefile will be created and loaded server-side that will ensure the voted match is the only available option for the next game.
- Voted matches are added to an internal match queue, so you may hold as many votes as you want to create a community-driven playlist experience.
- Your regular votefile will be reloaded after all voted matches are complete and the match queue is empty.

#### There's more but I'll get to it later. Peace. - BIRD COMMAND

# Features

- Connect to and control your server via Rcon
- View Live Scoreboard that shows score/kills/deaths etc
- View players name and service-tag
- View and interact with chat via the chat Tab
- View players that join and leave the server
- Timed commands that can be sent every x minutes or at certain hours
- Right click Context menu to Kick or ban players
- Supports multiple servers
- Control tab for easy access to commands you might commonly use
- Info tab to view the name and some basic settings for the server as well as the current map and game type
- Connect to discord Webhook to send messages when a Report command is used

# Instructions 

- Download the latest Release from https://github.com/BIRD-COMMAND/RconTool
- Extract the zip to folder of your choice
- double click the exe
- Click add server and fill in your servers info which can be found in dewrito_prefs.cfg file
- Default ports you need for the tool are server port 11775 and rcon port 11776
- Click save after adding all your servers
- To switch between servers use the dropdown at the bottom-left of the program

(https://github.com/jaron780/RconTool)[Original Version] Created by Jaron
Updated by BIRD COMMAND
