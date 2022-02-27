using System;
using System.Collections.Generic;

namespace MLInput
{
    public class MLICommand
    {
        public string cmdDescription;
        public string cmdName;
        public int ID;
        public Action<List<String>> Method;

        public MLICommand(string cmdName, string cmdDescription, Action<List<String>> Method)
        {
            this.cmdDescription = cmdDescription;
            this.cmdName = cmdName;
            this.Method = Method;
        }
    }
}
