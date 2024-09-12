using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model.DataPayloads
{
    public class DataPasswordlessLogin
    {
        private PrivilegeLevel _minimumPrivilegeLevel;

        public string MinimumPrivilegeLevel
        {
            get => _minimumPrivilegeLevel.ToString();
            set
            {
                if (Enum.TryParse(value, out PrivilegeLevel parsedValue))
                {
                    _minimumPrivilegeLevel = parsedValue;
                }
                else
                {
                    throw new ArgumentException("Invalid PrivilegeLevel value", nameof(value));
                }
            }
        }
    }
}
