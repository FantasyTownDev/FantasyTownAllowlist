using LLNET.Core;
using LLNET.Logger;

namespace FantasyTownAllowlist;
[PluginMain("FantasyTownAllowlist")]
public class FantasyTownAllowList : IPluginInitializer
{
    Logger logger = new();
    //元信息
    public Dictionary<string, string> MetaData => new()
    {
        {"Auther","Liten_PanJun"},
        {"作者","小潘菌"}
    };
    //介绍
    public string Introduction => "Allow servers with online-mode=false (such as WeaterDog downstream servers) to enable white list plugin.";
    //版本
    public Version Version => new(1, 5, 0);
    //载入
    public void OnInitialize()
    {
        string Path = ".\\plugins\\FantasyTown\\allowlist\\allowlist.json";
        Task.Run(() =>
        {
            Thread.Sleep(5500);
            logger.info.WriteLine("FantasyTownAllowlist is loaded!");
            logger.info.WriteLine("FantasyTownAllowlist已加载！");
            logger.info.WriteLine("Plugin version: " + Version.ToString());
            logger.info.WriteLine("插件版本：" + Version.ToString());
            if (File.Exists(Path) == false)
            {
                logger.Info.WriteLine("检测到白名单文件不存在，正在创建……");
                logger.Info.WriteLine("Allowlist file does not exist, creating...");
                Directory.CreateDirectory(".\\plugins\\FantasyTown\\allowlist");
                File.Create(Path);
            }
        });
    }
}
