using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLInput
{
    public class MLIMod
    {
        public string ModName;
        public int ID;
        public List<MLICommand> Commands;

        public MLIMod(string ModName, List<MLICommand> Commands, int ID)
        {
            this.ModName = ModName;
            this.Commands = Commands;
            this.ID = ID;
            for (int i = 0; i < Commands.Count; i++)
            {
                Commands[i].ID = i;
            }
        }
    }
}
