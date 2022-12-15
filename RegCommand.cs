using MC;
using LLNET.DynamicCommand;
using static LLNET.DynamicCommand.DynamicCommand;
using LLNET.Logger;

namespace FantasyTownAllowlist
{
    internal class RegCommand
    {
        Logger logger = new("FantasyTownAllowlist");
        DynamicCommandInstance cmd = CreateCommand("allowlist", "Allowlist Commands", CommandPermissionLevel.Admin);
        Allowlist allowlistMag = new();
        public void CommandRegisiter()
        {
            var AllowlistOperate = cmd.SetEnum("Operate Allowlist", new() { "add", "remove" }); //增删操作指令枚举
            var Allowlister = cmd.SetEnum("List the player in the Allowlist", new() { "list" }); //列出白名单指令枚举
            //设置指令参数
            cmd.Mandatory("OperateEnum", ParameterType.Enum, AllowlistOperate, CommandParameterOption.EnumAutocompleteExpansion);
            cmd.Mandatory("Lister", ParameterType.Enum, Allowlister, CommandParameterOption.EnumAutocompleteExpansion);
            cmd.Mandatory("PlayerName", ParameterType.String);
            //设置指令重载
            cmd.AddOverload(new List<string>() { AllowlistOperate, "PlayerName" });
            cmd.AddOverload(new List<string>() { "TestOperation2" });
            //设置回调
            cmd.SetCallback((command, origin, output, results) => {
                switch (results["OperateEnum"].AsString())
                {
                    case "add":
                        {
                            if ($"{results["PlayerName"].AsString}" != "" && $"{results["PlayerName"].AsString}" != null)
                            {
                                allowlistMag.Write($"{results["PlayerName"].AsString}");
                                output.Success($"Add - {results["PlayerName"].AsString()}");
                            }
                            else output.Success("Error - PlayerName is null!");
                        }
                        break;
                    case "remove":
                        {
                            //output.Success($"Remove - {results["testString"].AsString()}");
                        }
                        break;
                    default:
                        break;
                }
            });
            logger.info.WriteLine("Registering Commands...");
            logger.info.WriteLine("正在注册命令……");
        }
    }
}
