using LiteLoader.Logger;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FantasyTownAllowlist
{
    internal class Allowlist
    {
        string FilePath = ".\\plugins\\FantasyTown\\Allowlist\\allowlist.json";
        Logger logger = new("FantasyTownAllowlist");
        /// <summary>
        /// 判断玩家是否在白名单中
        /// </summary>
        /// <param name="Name">玩家名</param>
        /// <returns></returns>
        public bool PlayerInAllowlist(string Name)
        {
            List<AllowlistFile>? list = JsonConvert.DeserializeObject<List<AllowlistFile>>(Read());
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
        /// <param name="Xuid">Xuid</param>
        /// <param name="UUID">UUID</param>
        /// <param name="LastJoin">最后加入时间</param>
        /// <returns></returns>
        public bool Write(string Player, string Xuid = "", string UUID = "", long LastJoin = 0)
        {
            try
            {
                AllowlistFile allowlist = new()
                {
                    Name = Player,
                    XUID = Xuid,
                    UUID = UUID,
                    LastJoin = LastJoin
                };
                List<AllowlistFile>? list = JsonConvert.DeserializeObject<List<AllowlistFile>>(Read());
                if (PlayerInAllowlist(Player))
                {
                    List<AllowlistFile>? _allowlist = new() { allowlist };
                    int i = 0;
                    foreach (var item in list)
                    {
                        if (item.Name == Player)
                            break;
                        i++;
                    }
                    list.RemoveRange(i, i + 1);
                    list.InsertRange(i, _allowlist);
                }
                else
                {
                    list.Add(allowlist);
                }
                string json = JsonConvert.SerializeObject(list);
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
                logger.Error.WriteLine("修改白名单时出错：" + e.Message);
                logger.Error.WriteLine("Error editing allowlist file: " + e.Message);
                return false;
                throw;
            }
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
        /// <summary>
        /// 删白名单
        /// </summary>
        /// <param name="Player">玩家名</param>
        /// <returns></returns>
        public bool Delete(string Player)
        {
            try
            {
                List<AllowlistFile>? list = JsonConvert.DeserializeObject<List<AllowlistFile>>(Read());
                int n = 0;
                if (list == null || list.Count == 0)
                    logger.Error.WriteLine("白名单为空，无法删除！");
                else
                {
                    foreach (var item in list)
                    {
                        if (item.Name == Player)
                        {
                            break;
                        }
                        else n++;
                    }
                    list.RemoveAt(n);
                    string json = JsonConvert.SerializeObject(list);
                    using (FileStream fs = new(FilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (StreamWriter sw = new(fs, Encoding.UTF8))
                        {
                            sw.Write(json);
                            sw.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error.WriteLine("修改白名单时出错：" + e.Message);
                logger.Error.WriteLine("Error editing allowlist file: " + e.Message);
                return false;
                throw;
            }
            return true;
        }
    }
}
