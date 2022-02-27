using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using Newtonsoft.Json;
using System.Threading;

namespace MLInput
{
    public static class MLIHandler
    {
        private static string MLIDirectory;
        private static Thread InputThread;
        public static List<MLIMod> MLIMods;
        internal static MLIConfig Config;
        internal static void Start()
        {
            Config = new MLIConfig();
            MLIMods = new List<MLIMod>();
            MLIDirectory = MelonHandler.ModsDirectory + "\\MLInput";
            LoadConfig();
            InputThread = new Thread(() =>
            {
                for (; ; )
                {
                    if (Console.ReadKey(true).KeyChar == Config.Hotkey)
                    {
                        Input();
                    }
                    Thread.Sleep(10);
                }
            });
            InputThread.Start();
            MelonLogger.Msg($"Melon Loader Input started. To open, press the hotkey while the MelonLoader console is focused. To change hotkey, go to {MLIDirectory}\\Config.json");
            MelonLogger.Msg($"Hotkey: {Config.Hotkey}");
        }
        public static void AddMod(string ModName, List<MLICommand> Commands)
        {
            int ID = MLIMods.Count;
            MLIMods.Add(new MLIMod(ModName, Commands, ID));
            MelonLogger.Msg($"Added {Commands.Count} commands for: {ModName}. Command is \"{ID}\".");
        }
        private static void Input()
        {
            MelonLogger.Msg("Enter Command.");
            string cmd = Console.ReadLine();
            List<string> args = new List<string>(cmd.Split());
            args.RemoveAt(0);
            try
            {
                ExecuteCommand(cmd.Split(' ')[0], args);
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex);
            }
        }
        private static void ExecuteCommand(string cmd, List<string> args)
        {
            string[] IDs = cmd.Split('.');

            if (IDs.Length > 2)
            {
                MelonLogger.Error("Identified more than 2 IDs. Type 1 ID for a mod to list commands and info of mod. Type another ID beginning with a \'.\' after the 1st ID to execute a command. Example: 2.14 (Arguments)");
                return;
            }
            bool flag1 = int.TryParse(IDs[0], out int ModID);
            if (!flag1)
            {
                MelonLogger.Error("Mod ID has to be a number. Example: \"3\".");
                return;
            }
            if (MLIMods.Count <= ModID)
            {
                MelonLogger.Error("Mod ID does not exist.");
                return;
            }
            if (IDs.Length == 1)
            {
                ModHelp(ModID);
                return;
            }
            bool flag2 = int.TryParse(IDs[1], out int CommandID);
            if (!flag2)
            {
                MelonLogger.Error("Command ID has to be a number. Example: \"1.2\".");
                return;
            }
            if (CommandID < 0 || ModID < 0)
            {
                MelonLogger.Error("IDs can't be below 0.");
                return;
            }

            foreach(MLIMod Mod in MLIMods)
            {
                if (Mod.ID == ModID)
                {
                    foreach (MLICommand Command in Mod.Commands)
                    {
                        if (Command.ID == CommandID)
                        {
                            Command.Method.Invoke(args);
                        }
                    }
                }
            }
        }
        private static void ModHelp(int ModID)
        {
            foreach(MLIMod Mod in MLIMods)
            {
                if (ModID == Mod.ID)
                {
                    string HelpMsg = $"Help for mod \"{Mod.ModName}\":\n";
                    foreach (MLICommand Command in Mod.Commands)
                    {
                        HelpMsg += $"\nID: {Command.ID}\nName: {Command.cmdName}\nDescription: {Command.cmdDescription}\n";
                    }
                    MelonLogger.Msg(HelpMsg);
                }
            }
        }
        private static void LoadConfig()
        {
            string file = GetMLIFile("Config.json", out bool created);
            if (created)
            {
                Config.Hotkey = '`';
                File.WriteAllText(GetMLIFile("Config.json"), JsonConvert.SerializeObject(new MLIConfig() { Hotkey = Config.Hotkey }, Formatting.Indented));
            }
            else
            {
                Config = JsonConvert.DeserializeObject<MLIConfig>(File.ReadAllText(file));
            }
        }
        private static string GetMLIFile(string fileName)
        {
            return MLIDirectory + "\\" + fileName;
        }
        private static string GetMLIFile(string fileName, out bool created)
        {
            string file = MLIDirectory + "\\" + fileName;
            if (File.Exists(file))
            {
                created = false;
                return file;
            }
            else
            {
                Directory.CreateDirectory(MLIDirectory);
                File.Create(file).Dispose();
                created = true;
                return file;
            }
        }
    }
}
