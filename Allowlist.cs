using LLNET.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FantasyTownAllowlist
{
    internal class Allowlist
    {
        string FilePath = ".\\plugins\\FantasyTown\\Allowlist\\allowlist.json";
        string OldFile = ".\\plugins\\FantasyTown\\allowlist\\allowlist.json";
        Logger logger = new("FantasyTownAllowlist");
        public void Start()
        {
            if (File.Exists(OldFile) == false && File.Exists(FilePath) == false)
            {
                logger.Info.WriteLine("检测到白名单文件不存在，正在创建……");
                logger.Info.WriteLine("Allowlist file does not exist, creating...");
                Directory.CreateDirectory(".\\plugins\\FantasyTown\\Allowlist");
                Create();
            }
            else if (File.Exists(OldFile) == true && File.Exists(FilePath) == false)
            {
                logger.Info.WriteLine("暂不支持旧版本白名单文件自动迁移，请手动迁移");
                logger.Info.WriteLine("检测到新版本白名单文件不存在，正在创建……");
                logger.Info.WriteLine("Automatic migration of old allowlist files is not supported, please migrate manually");
                logger.Info.WriteLine("New allowlist file does not exist, creating...");
                Create();
            }
            else if (File.Exists(OldFile) == false && File.Exists(FilePath) == true)
            {
                logger.Info.WriteLine("检测到白名单文件不存在，正在创建……");
                logger.Info.WriteLine("Allowlist file does not exist, creating...");
                Directory.CreateDirectory(".\\plugins\\FantasyTown\\Allowlist");
                Create();
            }
        }
        /// <summary>
        /// 创建白名单文件
        /// </summary>
        /// <param name="File">文件路径</param>
        /// <returns></returns>
        public bool Create()
        {
            try
            {
                List<AllowlistFile> allowlist = new List<AllowlistFile>();
                string json = JsonConvert.SerializeObject(allowlist);
                using (FileStream fs = new(FilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new(fs, Encoding.UTF8))
                    {
                        sw.Write(json);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error.WriteLine("白名单文件创建出错：" + e.Message);
                logger.Error.WriteLine("Error creating allowlist file: " + e.Message);
                return false;
                throw;
            }
            logger.Info.WriteLine("白名单文件创建成功！");
            logger.Info.WriteLine("Allowlist file created!");
            return true;
        }
        /// <summary>
        /// 写白名单
        /// </summary>
        /// <param name="Player">玩家名</param>
        /// <returns></returns>
        public bool Write(string Player)
        {
            return true;
        }
        /// <summary>
        /// 读白名单
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            using (FileStream fs = new(".\\plugins\\FantasyTown\\Allowlist\\allowlist.json", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new(fs, Encoding.UTF8))
                {
                    return reader.ReadToEnd().ToString();
                }
            }
        }
    }
}
