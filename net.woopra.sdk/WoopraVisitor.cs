using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.woopra.sdk
{
    public class WoopraVisitor
    {

        public static String Email = "email";

        public static String UniqueId = "uniqueId";

        public Dictionary<string,Object> properties = new Dictionary<string, object>();

        private String _ipAddress = "";

        private readonly String _cookieValue = "";

        private String _userAgent = "";

        public WoopraVisitor(String identifier, String value)
        {
            if (identifier.Equals(Email))
            {
                properties[Email] = value;
                _cookieValue = Math.Abs(value.GetHashCode()).ToString();
            }
            else if (identifier.Equals(UniqueId))
            {
                _cookieValue = value;
            }

        }

        public void SetIpAddress(String ip)
        {
            _ipAddress = ip;
        }

        public WoopraVisitor WithIpAddress(String ip)
        {
            _ipAddress = ip;
            return this;
        }

        public void SetUserAgent(String agent)
        {
            _userAgent = agent;
        }

        public WoopraVisitor WithUserAgent(String agent)
        {
            _userAgent = agent;
            return this;
        }

        public void SetProperty(String key, Object value)
        {
            properties[key] = value;
        }

        public WoopraVisitor WithProperty(String key, Object value)
        {
            properties[key] = value;
            return this;
        }

       
        public String toString()
        {
            return string.Join(",", properties.Select(kv => kv.Key + "=" + kv.Value).ToArray());
        }

        public String GetCookieValue()
        {
            return _cookieValue;
        }

        public String GetIpAddress()
        {
            return _ipAddress;
        }

        public String GetUserAgent()
        {
            return _userAgent;
        }
    }
}
