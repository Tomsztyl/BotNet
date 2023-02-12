namespace BotAssistant_Net.Code.Core
{
    internal class BotProperties
    {
        public string TokenBot { get; set; } = "NzIxMDMxNTMyNjI5NzIxMTMw.GPv3p6.xilVv9CvwIFBQcsviEnAG2hMkKVAl1_ol00bQs";
        public string PrefixBot { get; set; } = string.Empty;
        public MysqlProperties MysqlProperties { get; set; } = new MysqlProperties();
    }

    internal class MysqlProperties
    {
        public string Server { get; set; } = "127.0.0.1;";
        public string Database { get; set; } = "testdb;";
        public string Uid { get; set; } = "root;";
        public string Password { get; set; } = ";";
    }
}
