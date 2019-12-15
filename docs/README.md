# MineStat
[![Actions Status](https://github.com/TraceLD/minestat-sharp/workflows/dotnet/badge.svg)](https://github.com/TraceLD/minestat-sharp/actions)
[![Nuget](https://img.shields.io/nuget/v/TraceLd.MineStatSharp)](https://www.nuget.org/packages/TraceLd.MineStatSharp/)

MineStat is a Minecraft server status checker.

You can use this library in a monitoring script to poll multiple Minecraft servers or to let
visitors see the status of your server from their browser.

**Changes compared to original MineStat**:

- C# only.
- Removed obsolete code.
- Comes as a Class Library instead of a single `.cs` file.
- Packaged as a NuGet package.

**Link to original MineStat**:

https://github.com/ldilley/minestat

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
```
