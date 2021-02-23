# Unity Reflect Dashboard

<!--<span style="color: red;">(UPDATE IN PROGRESS. Breaking this into multiple docs and expanding, adding system tray options, etc.)</span>-->

The Unity Reflect dashboard is a standalone application that lets you manage your projects, local server settings, installed Reflect components, and recent activity log.

<img style="padding: 1em 0" width="650" alt="Unity Reflect dashboard" src="images/DashboardProjects.png">

## Opening the dashboard

To open the standalone Unity Reflect dashboard, click the Unity Reflect icon in your system tray:

<img style="padding: 1em 0" width="300" alt="Unity Reflect dashboard icon" src="images/ReflectSystray.png">

You can also open the Unity Reflect Dashboard from the Start menu.

## Managing your projects

The Unity Reflect Dashboard lets you manage where your projects are stored.

The **Projects** tab displays your projects in a list:

![Projects](images/1.4/DashboardProjects.png)

The list includes icons indicate where each project is currently stored: locally (<img src="images/1.3/Local.png" style="width: 25px;">), on a local network (<img src="images/1.3/Network.png" style="width: 25px;">), or in the cloud (<img src="images/1.3/Cloud.png" style="width: 25px;">).

Click the down arrow <img style="padding: 0" width="25" alt="Unity Reflect dashboard" src="images/ReflectExpandDetails.png"> see all the instances of the project in its linked sources.

<img style="padding: 0; border: 1px solid black;" width="650" alt="Unity Reflect dashboard" src="images/1.4/ExpandProject.png">

Click the name of a source to see additional details.

<img style="padding: 1em 0" width="650" alt="Unity Reflect dashboard" src="images/1.4/Sources.png">

* To delete a project instance from a linked source, click <img style="width: 25px" alt="Trash can" src="images/1.3/DeleteIcon.png">.
* To see a log of recent activity for this project, click **Project Activities** in the left sidebar.

## Managing your server settings

The **Local Settings** tab lets you update the settings of your local sync server.

<img style="padding: 1em 0" width="650" alt="Unity Reflect dashboard" src="images/1.3/ServerSettings.png">

* **Make Public** makes your local server public to users that have access to the projects it contains.
* **Allow Publishing New Projects** allows users to publish new projects to this server. If unchecked, users can only publish to projects already present on this server.

Be sure to click **Save** after making any changes.

## Managing installed components

The **Components** tab shows a list of all Reflect components installed on your computer.

<img style="padding: 1em 0" width="650" alt="Unity Reflect dashboard" src="images/1.3/Components.png">

If you want to uninstall a particular component, click <img style="width: 25px" alt="Trash can" src="images/1.3/DeleteIcon.png"> and then click **Uninstall.**

## Accessing your activity log

The **Activities** tab shows a log of your recent Reflect activity.

<img style="padding: 1em 0" width="650" alt="Unity Reflect dashboard" src="images/1.3/Activities.png">

To resume an activity, click **open**.

To expand any alerts associated with an activity, click <img style="width: 25px" alt="Alert icon" src="images/1.3/AlertIcon.png">.

<img style="padding: 1em 0" width="650" alt="Unity Reflect dashboard" src="images/1.3/ActivitiesExpand.png">

To hide an activity in the log, click <img style="width: 25px" alt="Trash can" src="images/1.3/HideIcon.png">. To hide all activities, click **Clear**.

To reveal hidden activities, click **Show All**.
