using LiteLoader.Event;
using LiteLoader.Logger;
using Newtonsoft.Json;
using System.Text;

namespace FantasyTownAllowlist
{
    internal class Events
    {
        Allowlist allowlistMgr = new();
        public void playerJoin()
        {
            //预进入，检查白名单
            PlayerPreJoinEvent.Subscribe(e =>
            {
                string Name = e.Player.Name;
                if (allowlistMgr.PlayerInAllowlist(Name) == false)
                {
                    e.Player.Kick("你还没有服务器白名单，请联系你所在的服务器的管理员！");
                    return false;
                }
                return true;
            });
            //已进入，检查白名单（适配WeaterDog）
            PlayerJoinEvent.Subscribe(e =>
            {
                string Name = e.Player.Name;
                if (allowlistMgr.PlayerInAllowlist(Name) == false)
                {
                    e.Player.Kick("你还没有服务器白名单，请联系你所在的服务器的管理员！");
                    return false;
                }
                return true;
            });
        }
    }
}
