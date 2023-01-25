using MC;
using LiteLoader.DynamicCommand;
using static LiteLoader.DynamicCommand.DynamicCommand;
using LiteLoader.Logger;
using Newtonsoft.Json;

namespace FantasyTownAllowlist
{
    internal class RegCommand
    {
        Logger logger = new("FantasyTownAllowlist");
        DynamicCommandInstance cmd = CreateCommand("ftallowlist", "Allowlist Commands", CommandPermissionLevel.GameMasters);
        Allowlist allowlistMgr = new();
        public void CommandRegisiter()
        {
            logger.info.WriteLine("Registering Commands...");
            logger.info.WriteLine("正在注册命令……");
            var AllowlistOperate = cmd.SetEnum("Operate Allowlist", new() { "add", "remove" }); //增删操作指令枚举
            var Allowlister = cmd.SetEnum("List the player in the Allowlist", new() { "list" }); //列出白名单指令枚举
            //设置指令参数
            cmd.Mandatory("OperateEnum", ParameterType.Enum, AllowlistOperate, CommandParameterOption.EnumAutocompleteExpansion);
            cmd.Mandatory("Lister", ParameterType.Enum, Allowlister, CommandParameterOption.EnumAutocompleteExpansion);
            cmd.Mandatory("PlayerName", ParameterType.String);
            //设置指令重载
            cmd.AddOverload(new List<string>() { AllowlistOperate, "PlayerName" });
            cmd.AddOverload(new List<string>() { Allowlister });
            //设置回调
            cmd.SetCallback((command, origin, output, results) => {
                if (results != null && results.Count != 0)
                {
                    try
                    {
                        switch (results["OperateEnum"].Get())
                        {
                            case "add":
                                {
                                    if ($"{results["PlayerName"].AsString()}" != "" && $"{results["PlayerName"].AsString()}" != null)
                                    {
                                        if(allowlistMgr.PlayerInAllowlist($"{results["PlayerName"].AsString()}"))
                                        {
                                            output.Error($"{results["PlayerName"].AsString()} already in allowlist");
                                        }
                                        else
                                        {
                                            allowlistMgr.Write($"{results["PlayerName"].AsString()}");
                                            output.Success($"{results["PlayerName"].AsString()} was added in allowlist!");
                                        }
                                    }
                                    else
                                    {
                                        output.Error("Error - PlayerName is null!");
                                    }
                                }
                                break;
                            case "remove":
                                {
                                    if (allowlistMgr.PlayerInAllowlist($"{results["PlayerName"].AsString()}"))
                                    {
                                        allowlistMgr.Delete($"{results["PlayerName"].AsString()}");
                                        output.Success($"{results["PlayerName"].AsString()} was removed from allowlist!");
                                    }
                                    else
                                    {
                                        output.Error($"{results["PlayerName"].AsString()} not in allowlist!")
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        switch (results["Lister"].Get())
                        {
                            case "list":
                                {
                                    List<AllowlistFile>? list = JsonConvert.DeserializeObject<List<AllowlistFile>>(allowlistMgr.Read());
                                    string pln = "";
                                    if (list == null || list.Count == 0)
                                        break;
                                    else
                                    {
                                        foreach (var item in list)
                                        {
                                            if (pln == "")
                                                pln = item.Name;
                                            else pln += $", {item.Name}";
                                        }
                                    }
                                    logger.info.WriteLine("There are players in allowlist:");
                                    logger.info.WriteLine(pln);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.error.WriteLine(e.Message);
                        logger.error.WriteLine(e.Source);
                        logger.error.WriteLine(e.StackTrace);
                        throw;
                    }
                }
                else logger.error.WriteLine("results is null or empty!");
            });
            Setup(cmd);
            logger.info.WriteLine("Commands Registered!");
            logger.info.WriteLine("命令注册成功！");
        }
    }
}
