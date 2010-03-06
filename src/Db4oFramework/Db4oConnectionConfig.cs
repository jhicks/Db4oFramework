using System.Configuration;

namespace NServiceBus.Db4o
{
    public class Db4oConnectionConfig : ConfigurationSection
    {
        [ConfigurationProperty("Host", IsRequired = false, DefaultValue = "localhost")]
        public string Host
        {
            get { return (string)this["Host"]; }
            set { this["Host"] = value; }
        }

        [ConfigurationProperty("Port", IsRequired = false, DefaultValue = 2099)]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }

        [ConfigurationProperty("Username", IsRequired = false, DefaultValue = "db4o")]
        public string Username
        {
            get { return (string)this["Username"]; }
            set { this["Username"] = value; }
        }

        [ConfigurationProperty("Password", IsRequired = false, DefaultValue = "db4o")]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }
    }
}