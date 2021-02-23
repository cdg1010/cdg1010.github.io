# Network configuration

This document provides the configuration information you might need to run the Reflect service.

## Configuring the Sync Service on a specific port

By default, the Reflect service uses a random available port every time it is started.

To host the Reflect service on a static port:

1. Open the file *%ProgramData%\Unity\Reflect\syncServiceConfig.json*. If this file does not exist, create it now.
2. Set the ExternalPort property to the desired value.

```
{
  "ExternalPort": 59876
}
```

The change takes effect the next time the service is restarted.

## Configuring your firewall

The Reflect installation will automatically create an inbound rule in your Windows Firewall. This rule allows other devices to download Reflect models hosted on your PC. If the creation of the rule fails for any reason, or if you want to make it more stringent and allow connections on the port configured in the *syncServiceConfig.json* file, run the following command in an elevated command prompt. (Port 59876 is used as an example in the localport argument.)

```
netsh advfirewall firewall add rule name="Unity Reflect Service" protocol=TCP dir=in action=allow program="%programfiles%\Unity\Reflect\SyncService.exe" localport=59876
```
You may then delete the default rule (also named Unity Reflect Service) using the Windows Firewall interface.

> **Note:** These configurations do not take effect if traffic is blocked at the router level. A network administrator may need to unblock a port in the network-level firewall.

### URLS to whitelist

If your network limits access to unauthorized websites, see [this link](https://docs.google.com/spreadsheets/d/1ovX7dvWRSBADOB1Xw_dH9oO9VATy5eqmlRM5K6McXOs/edit?usp=sharing) for a list of URLs to whitelist.
