using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace net.woopra.sdk
{
    public class WoopraEvent
    {
        public String Name;

        //public JsonObject properties = new JSONObject();
        public Dictionary<string, Object> properties = new Dictionary<string, object>();

        public long Timestamp = -1;

        public WoopraEvent(String eventName)
        {
            Name = eventName;
        }

        //public WoopraEvent(String eventName, Object[,] properties)
        //{
        //    this.name = eventName;
        //    foreach (Object[] keyValue in this.properties)
        //    {
        //        this.properties.put(((String)(keyValue[0])), keyValue[1]);
        //    }

        //}

        public void SetProperty(String key, Object value)
        {
            properties[key] = value;
        }

        public WoopraEvent WithProperty(String key, Object value)
        {
            properties[key] =  value;
            return this;
        }

        public WoopraEvent WithTimestamp(long timestamp)
        {
            Timestamp = timestamp;
            return this;
        }
        //[Override()]
        //public String toString()
        //{
        //    return this.name.concat(": ").concat(this.properties.toString());
        //}
 
    }
}
