using LiteLoader.NET;
using LiteLoader.Logger;

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
        reg.CommandRegisiter();
        e.playerJoin();
        e.ServerStrated(Version);
    }
}
