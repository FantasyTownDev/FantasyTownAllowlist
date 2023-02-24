using LiteLoader.Event;
using LiteLoader.Logger;

namespace FantasyTownAllowlist
{
    internal class Events
    {
        Allowlist allowlistMgr = new();
        string OldFile = ".\\plugins\\FantasyTown\\allowlist\\allowlist.json";
        string FilePath = ".\\plugins\\FantasyTown\\Allowlist\\allowlist.json";
        Logger logger = new("FantasyTownAllowlist");
        public void ServerStrated(Version version)
        {
            //服务器开启后执行的
            ServerStartedEvent.Subscribe(e =>
            {
                logger.Info.WriteLine("FantasyTownAllowlist is loaded!");
                logger.Info.WriteLine("FantasyTownAllowlist已加载！");
                logger.Info.WriteLine("Plugin version: " + version.ToString());
                logger.Info.WriteLine("插件版本：" + version.ToString());
                if (File.Exists(OldFile) == false && File.Exists(FilePath) == false)
                {
                    logger.Info.WriteLine("检测到白名单文件不存在，正在创建……");
                    logger.Info.WriteLine("Allowlist file does not exist, creating...");
                    Directory.CreateDirectory(".\\plugins\\FantasyTown\\Allowlist");
                    allowlistMgr.Create();
                }
                else if (File.Exists(OldFile) == true && File.Exists(FilePath) == false)
                {
                    logger.Info.WriteLine("暂不支持旧版本白名单文件自动迁移，请手动迁移");
                    logger.Info.WriteLine("检测到新版本白名单文件不存在，正在创建……");
                    logger.Info.WriteLine("Automatic migration of old allowlist files is not supported, please migrate manually");
                    logger.Info.WriteLine("New allowlist file does not exist, creating...");
                    allowlistMgr.Create();
                }
                else if (File.Exists(OldFile) == false && File.Exists(FilePath) == true)
                {
                    logger.Info.WriteLine("检测到白名单文件不存在，正在创建……");
                    logger.Info.WriteLine("Allowlist file does not exist, creating...");
                    Directory.CreateDirectory(".\\plugins\\FantasyTown\\Allowlist");
                    allowlistMgr.Create();
                }
                return true;
            });
        }
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
