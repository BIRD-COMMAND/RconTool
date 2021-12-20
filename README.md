# RconTool
This is a tool for ElDewrito Server hosts to monitor and moderate their own servers.

[Current Version Released Here](https://github.com/BIRD-COMMAND/RconTool/releases/) Updated by BIRD COMMAND

[Original Version](https://github.com/jaron780/RconTool) Created by Jaron


## Build Instructions
- Clone/Download
- [WebSocketSharp](https://github.com/sta/websocket-sharp) and [Json.NET](https://www.newtonsoft.com/json) are both required and can be installed with nuget package manager.
- Install-Package WebSocketSharp -Pre | Install-Package Newtonsoft.Json
- Build and run.

<!--![Ready for action.](https://i.imgur.com/W9Qo0WY.png)-->

## Brief overview of some of the features

- Server selection drop-down menu in the bottom-left.
- Toggleable sound effects for when players leave or join the currently displayed server.
- Runtime commands are available in game. Use '!commands' to be PM'd the list.
- '!pm Player Name *message*' - a private messaging system. Quotation marks should NOT be used for player names (even names with spaces) or for messages.
- '!r *message*' - will respond to the last player who sent you a '!pm'. Do not use quotation marks.
- Additional command triggers have been added. Commands may be run when a player joins or leaves, when the server player count enters a specified range, and more.
- Added auto-complete for server commands in the console tab. You can use the TAB key to cycle through all commands containing your currently entered text.
- Scoreboard rendering supports multiple fonts and font sizes. File -> Settings -> Set Scoreboard Font Size to select it. Or use the buttons in the bottom-right.

## ServerHook - Enables manual player-team assignment and !balanceTeams command
##### (LOCAL ONLY - Only available if Rcon Tool is running on the same computer that is hosting the server)

When enabled (and the lobby has a team gametype loaded), you can right-click a player on the scoreboard and assign them to another team. Send-To-Team commands are disregarded in lobbies and gametypes with a FFA gametype loaded. If the loaded gametype has only 2 valid teams, Send-To-Team will only work if you try to send a player to one of those 2 teams. Unlike me, the server doesn't care that *gold team rules*.

There is now a !balanceTeams command that initiates a custom vote to balance the teams, and there's also a !forceBalanceTeams admin command that immediately executes the action. It's a simple algorithm that attempts to construct balanced team lists based on current player Kill/Death ratios and Kill/Death ratios saved from the previous match (when available). It's not perfect, but it can be helpful.

If you do want to use this feature, it's good to have at least a basic understanding of how it works:

- Works by finding the server process, and temporarily tweaking the bytes for the !shuffleTeams command function.
  - Sets a bit within the server process memory indicating which team player(s) should be on.
  - Edits !shuffleTeams function to skip random team assignment and use a custom message instead of 'Teams have been shuffled'.
  - The remainder of the function propagates the altered player team indices out to all players.
- After a brief delay the !shuffleTeams function bytes are reverted back to their original values.
- *The !shuffleTeams command will not work correctly during this ~3-second window.*
- > But that's a sacrifice I'm willing to make.

**Warning**: In the absolute worst-case scenario, if the Rcon Tool crashes in the middle of a send-to-team operation, your server's !shuffleTeams function could be left in a non-functional state. ***Restarting your server will fix this issue***, and otherwise your server should still run the game just fine aside from not being able to automatically randomize or balance teams.

## Auto-Translate
If you have a Google Translate API key set up (with billing configured and all that), you can plug it into the app and get automatic translation services in your server chat. You can set your server's default language, and messages detected in other languages will have an automatic translation posted in chat. You can also configure a list of words and phrases that will be ignored by the auto-translate ('lol', 'haha', etc.).

Additionally, you can manually translate with the '!t' and '!t-lc' commands.
* '!t *message*': translate the message to the server language.
* '!t-*lc*' *message*: translate the message to the language denoted by the ISO 639-1 language code *lc*: en, es, fr, de, etc.
* Excample: you type in chat '!t-es Hello, my friends'.
  * App detects command, auto-detects message language - gets translation to 'es'pañol (spanish).
  * App sends message to server chat - 'PlayerName: !t-es Hello, my friends'.
  * App sends message to server chat - 'PlayerName: Hola, mis amigos'.
  * This format makes the translation process transparent and more easily understandable for players.

## In-Game Custom Voting + Match Queue

- Use '!voteAddGame gametype mapname' to start an in-game vote to add that match to the 'match queue'.
- Add as many games to the queue as you want. The tool will load the next match after the current one ends.
- When the queue is empty, your regular votefile and voting will be re-enabled.

# Features

- Connect to and control your server via Rcon - Supports multiple servers
- View Live Scoreboard that shows score/kills/deaths etc
- Right-click context menu on scoreboard for player management
- Info panel with map and gametype icons
- Dropdowns for loading built-in maps and gametypes
- Will also include all your custom maps and gametypes in local mode
- View and interact with chat via the chat Tab
- View players that join and leave the server or switch teams
- Connect to discord Webhook to send messages when a Report command is used

# Instructions 

- Download the latest Release from https://github.com/BIRD-COMMAND/RconTool/releases/
- Extract the contents of the zip to the folder of your choice
- Run the .exe (as administrator if you want to use ServerHook)
- Click add server and fill in your server's info which can be found in dewrito_prefs.cfg file
- Default ports you need for the tool are (usually) server port 11775 and rcon port 11776
- Click save after adding all your servers
- To switch between servers use the dropdown at the bottom-left of the program

[Original Version](https://github.com/jaron780/RconTool) Created by Jaron
Updated by BIRD COMMAND
