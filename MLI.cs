using MelonLoader;
using System.Collections.Generic;

[assembly: MelonInfo(typeof(MLInput.MLI), "MelonLoaderInput", "1.0.1", "$locVar0", "https://github.com/SlocVaro")]
[assembly: MelonGame(null, null)]
[assembly: MelonColor(System.ConsoleColor.Red)]

namespace MLInput
{
    public class MLI : MelonMod
    {
        public override void OnApplicationStart()
        {
            MLIHandler.Start();
            MLIHandler.AddMod("MLInput", new List<MLICommand>() 
            {
                new MLICommand("Mods", "Prints all mods that use MLI with mod's info.", delegate(List<string> args)
                {
                    string info = "All mods that are using MLI:\n";
                    foreach (MLIMod Mod in MLIHandler.MLIMods)
                    {
                        info += $"\nID: {Mod.ID}\nName: {Mod.ModName}\nCommands: {Mod.Commands.Count} commands\n";
                    }
                    MelonLogger.Msg(info);
                })
            });
        }
    }
}
