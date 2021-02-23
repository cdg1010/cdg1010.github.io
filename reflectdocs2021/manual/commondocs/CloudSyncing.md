# Cloud syncing with Reflect

Reflect now gives you the option to host your projects in the cloud, letting you push data to your mobile devices even when you're on a different network. For more information, see [Managing your storage](ManageStorage.md).

<!--<span style="color: red;">[high-level diagram/schematic here]</span>

**Note:** Cloud syncing is in beta and should not be relied upon for production.
-->

**What data is sent to the cloud when doing an export?**

* If you export to a Local or Network server, there is no model data sent over the internet.

* If you export to the Cloud, then the input file is processed locally, and derivative model data (geometry, materials and so on) is sent to Unity.

* Project names are always sent over the internet to Unity. This happens even when publishing to Local or Network servers.
