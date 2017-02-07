Use woopra API using c# SDK

The purpose of this SDK is to allow our customers who have servers running .NET Applications to track their users from c# code. This SDK will allow you to track your users through the back-end without having to handle http requests manually.


The first step is to setup the tracker SDK. To do so, configure the tracker instance as follows (replace mybusiness.com with your website as registered on Woopra):


``` // Create tracker

var woopra = new WoopraTracker("mybusiness.com");

```

You can also configure the timeout (in milliseconds, defaults to 30000 - equivalent to 30 seconds) after which the event will expire and the visit will be marked as offline:


You can configure secure tracking (over https)


``` // set the protocol
var woopra = new WoopraTracker("mybusiness.com").withSecureTracking(true);
```
To track an event, you should first create an instance of WoopraEvent...

``` // create event object
 	var wevent = new WoopraEvent("play");
	wevent.SetProperty("artist", "Dave Brubeck");
	wevent.SetProperty("song", "Take Five");
	wevent.SetProperty("genre", "Jazz");
	wevent.Timestamp = 1484334934086L;
			
```

``` // WoopraVisitor identified by email:

            var visitor = new WoopraVisitor(WoopraVisitor.Email, "vk@mybusiness.com");

            // In both cases, then add visitor properties:
            visitor.SetProperty("email", i + "johndoe@mybusiness.com");

            //This step is not required if the visitor has been instantiated by email
            visitor.SetProperty("name", "John Doe" + i);
            visitor.SetProperty("company", "My Business" + i);
            visitor.SetIpAddress("192.168.110.18" + i);
            visitor.SetUserAgent("Chrome");
```	    
And you're ready to start tracking events:
```
woopra.Track(visitor, wevent);
```
and you are done
