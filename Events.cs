using LLNET.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyTownAllowlist
{
    internal class Events
    {
        Allowlist allowlist = new();
        public void playerJoin()
        {
            PlayerPreJoinEvent.Subscribe(e =>
            {
                string Name = e.Player.Name;
                if (allowlist.PlayerInAllowlist(Name) == true)
                    return true;
                else return false;
            });
            PlayerJoinEvent.Subscribe(e =>
            {
                string Name = e.Player.Name;
                if (allowlist.PlayerInAllowlist(Name) == true)
                    return true;
                else return false;
            });
        }
    }
}
