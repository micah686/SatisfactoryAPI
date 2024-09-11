using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryAPI.Model
{
    public enum PrivilegeLevel
    {
        NotAuthenticated,
        Client,
        Administrator,
        InitialAdmin,
        APIToken
    }
}
