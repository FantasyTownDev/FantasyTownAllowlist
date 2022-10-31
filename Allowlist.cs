using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyTownAllowlist
{
    internal class Allowlist
    {
        FlieWriteAndRead far = new();
        /// <summary>
        /// 判断玩家是否在白名单中
        /// </summary>
        /// <param name="Name">玩家名</param>
        /// <returns></returns>
        public bool PlayerInAllowlist(string Name)
        {
            string v = far.Read(Name);
            if (v != "" && v != null)
                return true;
            else return false;
        }
    }
}
