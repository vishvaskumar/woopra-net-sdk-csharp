using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace net.woopra.sdk
{
    public class WoopraTracker
    {
        private static String SDK_ID = "java";
    
    protected static int defaultTimeout = (300 * 1000);
    
    //private static Hashtable instances = new Hashtable();
    protected static Dictionary<string,WoopraTracker> instances = new Dictionary<string, WoopraTracker>();
    
    private String domain;
    
    private int _idleTimeout;
    
    private bool _httpsEnable = false;
    
    private bool _httpAuthEnable = false;
    
    private String _httpAuthUser = null;
    
    private String _httpAuthPassword = null;

        public static WoopraTracker GetInstance(String domain)
        {
            
            if (instances.ContainsKey(domain))
            {
                return instances[domain];
            }

            var instance = new WoopraTracker(domain);
            instances[domain] = instance;
            return instance;


        }

        public WoopraTracker(String domain)
        {
            this.domain = domain;
            _idleTimeout = defaultTimeout;
        }

        //private WoopraTracker()
        //{
        //    throw new NotImplementedException("Not supported yet.");
        //}

        public void Track(WoopraVisitor visitor, WoopraEvent woopraEvent)
        {
            HttpRequest(woopraEvent, visitor);
        }

        public void Identify(WoopraVisitor visitor) {
        if ((visitor != null)) {
            HttpRequest(null, visitor);
        }
        
    }
    
    
    public void SetSecureTracking(bool sslEnable) {
        _httpsEnable = sslEnable;
    }
    
    public WoopraTracker WithSecureTracking(bool sslEnable) {
        _httpsEnable = sslEnable;
        return this;
    }
    
    public void SetIdleTimeout(int idleTime) {
        _idleTimeout = idleTime;
    }
    
    public WoopraTracker WithIdleTimeout(int idleTime) {
        _idleTimeout = idleTime;
        return this;
    }
    
    public void EnableBasicAuth(String user, String password) {
        _httpAuthEnable = true;
        _httpAuthUser = user;
        _httpAuthPassword = password;
    }
    
    public WoopraTracker WithBasicAuth(String user, String password) {
        _httpAuthEnable = true;
        _httpAuthUser = user;
        _httpAuthPassword = password;
        return this;
    }

        private void HttpRequest(WoopraEvent woopraEvent, WoopraVisitor visitor)
        {
            try
            {
                var url = new StringBuilder();
                url.Append(((_httpsEnable) ? "https" : "http") + "://www.woopra.com/track/");
                // TODO: Warning!!!, inline IF is not supported ?
                // Identify or Track
                //Identify or Track

                url.Append(woopraEvent == null ? "identify/" : "ce/");

                // Request Options

                url.Append("?host=").Append(HttpUtility.UrlEncode(domain, Encoding.UTF8));
                url.Append("&app=").Append(SDK_ID);
                url.Append("&cookie=").Append(HttpUtility.UrlEncode(visitor.GetCookieValue(), Encoding.UTF8));
                url.Append("&timeout=").Append(_idleTimeout);
                if (!visitor.GetIpAddress().Equals(""))
                {
                    url.Append("&ip=").Append(HttpUtility.UrlEncode(visitor.GetIpAddress(), Encoding.UTF8));
                }


                if (woopraEvent != null && woopraEvent.Timestamp > 0)
                {
                    url.Append("&timestamp=").Append(woopraEvent.Timestamp);
                }

                // visitor Props
                foreach (var item in visitor.properties)
                {
                    url.Append("&cv_")
                        .Append(HttpUtility.UrlEncode(item.Key, Encoding.UTF8))
                        .Append("=")
                        .Append(HttpUtility.UrlEncode(item.Value.ToString(), Encoding.UTF8));
                }


                // event Props
                if (woopraEvent != null)
                {
                    url.Append("&event=").Append(HttpUtility.UrlEncode(woopraEvent.Name, Encoding.UTF8));

                    foreach (var item in woopraEvent.properties)
                    {
                        url.Append("&ce_")
                            .Append(HttpUtility.UrlEncode(item.Key, Encoding.UTF8))
                            .Append("=")
                            .Append(HttpUtility.UrlEncode(item.Value.ToString(), Encoding.UTF8));
                    }
                }


                //Here is a new thread

                using (var client = new WebClient())
                {
                    if ((visitor.GetUserAgent().Equals("") == false))
                    {
                        client.Headers.Add("User-Agent", visitor.GetUserAgent());
                    }
                    if (_httpAuthEnable)
                    {
                        // create credentials, base64 encode of username:password
                        string credentials =
                            Convert.ToBase64String(Encoding.ASCII.GetBytes(_httpAuthUser + ":" + _httpAuthPassword));

                        // Inject this string as the Authorization header
                        client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);
                    }
                    client.DownloadStringAsync(new Uri(url.ToString()));

                    //client.OpenRead(url.ToString());
                }
            }

                //catch (JSONException e) {
                //    //  TODO Auto-generated catch block
                //    e.printStackTrace();
                //}
            catch (UriFormatException e)
            {
                //  TODO Auto-generated catch block
                Trace.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                //  TODO Auto-generated catch block
                //e.printStackTrace();
                Trace.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                //  TODO Auto-generated catch block
                //e.printStackTrace();
                Trace.WriteLine(e.Message);
            }
        }
    }
}
