using LLNET.Core;
using LLNET.Logger;

namespace FantasyTownAllowlist;
[PluginMain("FantasyTownAllowlist")]
public class FantasyTownAllowList : IPluginInitializer
{
    Logger logger = new("FantasyTownAllowlist");
    Allowlist allowlistMgr = new();
    Events e = new();
    RegCommand reg = new();
    //元信息
    public Dictionary<string, string> MetaData => new()
    {
        {"Auther","Liten_PanJun"}
    };
    //介绍
    public string Introduction => "Allow servers with online-mode=false to enable allowlist.";
    //版本
    public Version Version => new(1, 5, 0);
    //载入
    public void OnInitialize()
    {
        logger.info.WriteLine("Registering Commands...");
        logger.info.WriteLine("正在注册命令……");
        reg.CommandRegisiter();
        Task.Run(() =>
        {
            Thread.Sleep(5500);
            logger.info.WriteLine("FantasyTownAllowlist is loaded!");
            logger.info.WriteLine("FantasyTownAllowlist已加载！");
            logger.info.WriteLine("Plugin version: " + Version.ToString());
            logger.info.WriteLine("插件版本：" + Version.ToString());
            allowlistMgr.Start();
        });
        e.playerJoin();
    }
}
