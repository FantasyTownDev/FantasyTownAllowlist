using LLNET.Event;
using LLNET.Logger;
using Newtonsoft.Json;
using System.Text;

namespace FantasyTownAllowlist
{
    internal class Events
    {
        Allowlist allowlistMgr = new();
        /// <summary>
        /// 判断玩家是否在白名单中
        /// </summary>
        /// <param name="Name">玩家名</param>
        /// <returns></returns>
        private bool PlayerInAllowlist(string Name)
        {
            List<AllowlistFile>? list = JsonConvert.DeserializeObject<List<AllowlistFile>>(allowlistMgr.Read());
            if (list == null || list.Count == 0)
                return false;
            else
            {
                foreach (var item in list)
                {
                    if (item.Name == Name)
                        return true;
                }
                return false;
            }
        }
        public void playerJoin()
        {
            //预进入，检查白名单
            PlayerPreJoinEvent.Subscribe(e =>
            {
                string Name = e.Player.Name;
                if (PlayerInAllowlist(Name) == false)
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
                if (PlayerInAllowlist(Name) == false)
                {
                    e.Player.Kick("你还没有服务器白名单，请联系你所在的服务器的管理员！");
                    return false;
                }
                return true;
            });
        }
    }
}
