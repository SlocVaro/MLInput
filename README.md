<!---
Markdown README file for a repository.

Must contain the following:

Name
Description
How to use
Developer examples
-->


# MLInput

## Description

MLInput, short for MelonLoader Input, is a library i made to allow MelonLoader mods to be used in console using input.

## How to use

Once ran, a child directory "MLInput" is made in the MelonLoader "Mods" folder. In there you will find "Config.json".

Config.json contains the data for what the hotkey is. This Hotkey is only used when you press it while the MelonLoader console is focused.

The hotkey is <span style="color:orange">`</span> by default

Uppon clicking the hotkey, will ask to enter a command. Previous MelonLoader console logs will show mods loaded by MLInput and show the command for the mod. If you are unsure, type "0" as a command. It should help.

MLInput mods have commands. Typing 1 number will show help of a mod.

Input:
```
0
```
Output:
```
[MelonLoaderInput] Help for mod "MLInput":

ID: 0
Name: Mods
Description: Prints all mods that use MLI with mod's info.
```

To access a command of the mod, you have to follow the number with a period, then the ID number of that command.

Input:
```
0.0
```
Output:
```
[MelonLoaderInput] All mods that are using MLI:

ID: 0
Name: MLInput
Commands: 1 commands

ID: 1
Name: VRCTelekinesis
Commands: 5 commands
```
VRCTelekinesis is a mod i made for VRChat, don't mind that. MLInput does not come with it.

Commands also come with arguments that you can send to the command.

Input:
```
1.4 8
```
Output:
```
[VRCTelekinesis] Ball size set to 8
```
## Developer examples

```csharp
using MelonLoader;
using MLInput;

namespace TestMod
{
    public class Mod : MelonMod
    {
        public override void OnApplicationStart()
        {
            MLIHandler.AddMod("Mod Name", new List<MLICommand>()
            {
                new MLICommand("Command Name", "Command Description", delegate(List<string> args)
                {
                    MelonLogger.Msg($"There are {args.Count} arguments.");
                });
            }
        }
    }
}
```

## Installation

Put in MelonLoader mods folder. The mods folder will be in the game directory where MelonLoader is installed.

### Requirements

https://github.com/LavaGang/MelonLoader