namespace FantasyTownAllowlist
{
    public class AllowlistFile
    {
        /// <summary>
        /// 玩家名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 玩家Xuid
        /// </summary>
        public string XUID { get; set; }
        /// <summary>
        /// 玩家最后登录时间
        /// </summary>
        public long LastJoin { get; set; }
    }
    //public class Root
    //{
    //    public ListMain ListMain { get; set; }
    //}
}
