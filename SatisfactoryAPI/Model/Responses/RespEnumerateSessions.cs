using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model.Responses
{
    public class RespEnumerateSessions
    {
        public SessionData[] Sessions { get; set; }
        public int CurrentSessionIndex { get; set; }
    }
    public class SessionData
    {
        public string SessionName { get; set; }
        public SaveHeader[] SaveHeaders { get; set; }
    }
    public class SaveHeader
    {
        public int SaveVersion { get; set; }
        public int BuildVersion { get; set; }
        public string SaveName { get; set; }
        public string SaveLocationInfo { get; set; }
        public string MapName { get; set; }
        public string MapOptions { get; set; }
        public string SessionName { get; set; }
        public int PlayDurationSeconds { get; set; }
        public string SaveDateTime { get; set; }
        public bool IsModdedSave { get; set; }
        public bool IsEditedSave { get; set; }
        public bool IsCreativeModeEnabled { get; set; }
    }

}
