using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model
{
    public class RespAdvancedGameSettings
    {
        public bool CreativeModeEnabled { get; set; }
        public Dictionary<string, string> AdvancedGameSettings { get; set; }
    }
}
