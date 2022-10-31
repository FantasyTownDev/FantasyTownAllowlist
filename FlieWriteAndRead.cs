using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using LLNET.RemoteCall;

namespace FantasyTownAllowlist
{
    internal class FlieWriteAndRead
    {
        string Path = ".\\plugins\\FantasyTown\\allowlist\\allowlist.json";
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public string Read(string Key)
        {
            using (StreamReader file = File.OpenText(Path))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    var value = o[Key].ToString();
                    return value;
                }
            }
        }
    }
}
