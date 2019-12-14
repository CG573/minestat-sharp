MineStat
========

MineStat is a Minecraft server status checker.

You can use this library in a monitoring script to poll multiple Minecraft servers or to let
visitors see the status of your server from their browser.

### Usage example
```cs
    MineStat ms = new MineStat("minecraft.dilley.me", 25565);
    Console.WriteLine("Minecraft server status of {0} on port {1}:", ms.Address, ms.Port);
    
    if(ms.ServerUp)
    {
      Console.WriteLine("Server is online running version {0} with {1} out of {2} players.", ms.Version, ms.CurrentPlayers, ms.MaximumPlayers);
      Console.WriteLine("Message of the day: {0}", ms.Motd);
      Console.WriteLine("Latency: {0}ms", ms.Latency);
    }
    else
    {
      Console.WriteLine("Server is offline!");
    }
  }
}
```
