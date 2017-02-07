using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net.woopra.sdk;

namespace net.woopra.test
{
    class Program
    {
        private static String testProject = "ls1.lycycle.net";
        static void Main(string[] args)
        {
            var woopra = new WoopraTracker("ls1.lycycle.net");

            for(int i=0;i<50;i++)
            {
                var wevent = new WoopraEvent("play");
                wevent.SetProperty("artist", "Dave Brubeck");
                wevent.SetProperty("song", "Take Five");
                wevent.SetProperty("genre", "Jazz");
                wevent.Timestamp = 1484334934086L; //Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
                // WoopraVisitor identified by email:
                var visitor = new WoopraVisitor(WoopraVisitor.Email, "vk@akkadian.com");

                // In both cases, then add visitor properties:
                visitor.SetProperty("email", i+"johndoe@mybusiness.com");

                //This step is not required if the visitor has been instantiated by email
                visitor.SetProperty("name", "John Doe"+i);
                visitor.SetProperty("company", "My Business"+i);
                visitor.SetIpAddress("192.168.110.18"+i);
                visitor.SetUserAgent("Chrome");

               // Task.Run(() => woopra.Track(visitor, wevent));
                woopra.Track(visitor, wevent);

            }
        }
    }
}
