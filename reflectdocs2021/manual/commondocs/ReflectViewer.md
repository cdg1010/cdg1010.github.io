# Using the Reflect Viewer

This page provides an overview of the default Unity Reflect Viewer.

## Opening a project

The Viewer opens to the Projects screen by default.

![Viewer interface - Projects](images/1.3/Viewer0.png)

To refresh the list of projects, click the **Refresh** button (<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//RefreshProjects.png" style="width: 45px; height: 45px; padding: 5px;">) in the top right corner of the screen.

To open a project, click its name in the projects list.

For more information about the options on the Projects screen and how Reflect projects are stored and synced, see [Managing your storage](ManagingStorage.md).

## The Viewer interface

![Viewer interface - Overview](images/1.3/ViewerFull.png)

### 1. Main menu

The main menu is located in the top left corner of the Viewer.

![Viewer interface](images/1.3/Viewer1.png)

#### Logging in or out

To log in or out or to access the BIM 360 Dashboard, click the **Account** icon.

<img src="images/1.3/Account2.png" style="width: 250px; padding: 5px;">

#### Opening the Projects screen

To open the Projects screen, click the **Projects** icon.

<!--<img src="Projects.png" style="width: 150px; padding: 5px;">-->

#### Enabling Help Mode

For assistance with the Viewer tools, click the **Help Mode** icon.

To return to the Viewer, click the **Help Mode** icon again.

#### Turning the sync service on or off

Click the **Sync status** icon to turn the sync service on or off.

<!--<img src=".png" style="width: 150px; padding: 5px;">-->

When the service is **active**, the icon looks like this: <img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//Enabled.png" style="width: 45px; height: 45px; padding: 5px;">

When the service is **inactive**, the icon looks like this: <img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//Disabled.png" style="width: 45px; height: 45px; padding: 5px;">

### 2. Scene settings

![Viewer interface](images/1.3/Viewer2.png)

#### Filtering BIM visibility

Click the **Filters** button (<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3/Filters.png" style="width: 35px; height: 35px; padding: 5px;">) to toggle the visibility of BIM data.

* Tap the eye icon next to each filter to turn it on or off.
* To see a different group of filters, click **Category** and select a category from the drop-down menu.

<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3/FiltersMenu.png" style="width: 300px; border: 1px solid #222;">

#### Toggling texture and light data

Click the **Scene Options** button (<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3/SceneOptions.png" style="width: 35px; height: 35px; padding: 5px;">) to turn texture and light data on or off.

<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//SceneOptionsMenu.png" style="width: 300px; border: 1px solid #222;">

#### Adjusting the position of the sun

The **Sun Study** buttons (<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3/SunStudy.png" style="width: 45px; height: 45px; padding: 5px;">) let you adjust the position of the sun based on a specific time, date, and location.

**Entering specific values**

The Sun Study button on the left-hand side of the screen lets you adjust the position of the sun by entering specific values.

<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3/SunStudyLeft.png" style="width: 300px; border: 1px solid #111;">

You can control any of the following parameters:

| Parameter | Description |
| -- | -- |
| Time of Day | The time of day in 24-hour format. |
| Time of Year | The day and month of the year. |
| UTC Offset | The time difference from UTC in hours and minutes.  |
| Latitude  | Latitude of the desired location. (0 degrees is the equator, 90 degrees is the North Pole, and -90 degrees is the South Pole.) |
| Longitude  | Longitude of the desired location. (0 degrees is Greenwich, with positive values to the east and negative values to the west.) |
| North Angle  | The direction of north in degrees. |

**Using the radial menu**

The Sun Study button on the right-hand side of the screen lets you adjust the position of the sun by rotating the radial menu.

<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//SunStudyRight.png" style="width: 150px; border: 1px solid #111;">
<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//SunStudyRight2.png" style="width: 150px; border: 1px solid #111;">

Click <img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//ClockDial.png" style="width: 35px; height: 35px;"> to switch to the date and time dials. The outer dial sets the date in months and the inner dial sets the time of day in 24-hour format.

Click
<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3/CoordinatesDial.png" style="width: 35px; height: 35px;"> to switch to the coordinate dials. The outer dial sets the solar azimuth and the inner dial sets the solar altitude. These coordinates are translated into the directional light’s rotation.

Click
<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3/ResetDial.png" style="width: 35px; height: 35px;">to reset the dials to their starting position.

### 3. Stats menu

![Viewer interface](images/1.3/Viewer3.png)

#### Showing the Stats Info menu

To display statistics about your scene, click the **Stats Info** button:

<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3/StatsInfo.png" style="width: 150px; border: 1px solid #111;">

### 4. Navigation cube

Click the navigation cube for easy access to left, right, and top views of your model.

![Viewer interface](images/1.3/NavCube.png)

Click the house icon to return to the default view.

### 5. Contextual menu

![Viewer interface](images/1.3/Viewer5.png)

The Reflect Viewer lets you move, orbit, or zoom around your scene. The currently selected navigation control is shown in the right-hand menu.

#### Move (Pan) <img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//Pan.png" style="width: 35px; height: 35px;">

To move the camera from your current position, use the WASD keys to move left/right/forward/backward and the Q and E keys to move up and down.

You can also pan the camera freely by clicking and dragging with the middle mouse button or by holding the Alt key while you click and drag.

#### Orbit <img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//Orbit.png" style="width: 35px; height: 35px;">

To rotate the view around your current position, right-click and drag inside the Viewer.

#### Zoom <img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3//Zoom.png" style="width: 35px; height: 35px;">

Use your scroll wheel to zoom in and out. You can also double-click a point inside the Viewer to jump there quickly.


#### Seeing BIM information about a particular object

Click the **BIM Info** button (<img src="https://docs.unity3d.com/reflect/1.3/manual/images/1.3/BIMInfo.png" style="width: 35px; height: 35px; padding: 5px;">) and click on an object in your scene to highlight it and display its metadata.

<img src="images/1.3/BIMInfo1.png" style="width: 400px; border: 1px solid #222;">

<img src="images/1.3/BIMInfo2.png" style="width: 400px; border: 1px solid #222;">

To switch between metadata categories, click the down arrow:

<img src="images/1.3/BimInfo3.png" style="width: 200px; border: 1px solid #222;">


### 6. Device menu

![Viewer interface](images/1.3/Viewer6.png)

The button in the lower right corner of the Viewer displays the current device mode. Click this button to expand or collapse the device mode menu.

<img style="padding: 1em 0" width="250" alt="Unity Reflect" src="images/1.3/ModeSwitcher2.png">

#### Enabling VR or AR mode

Click one of the device mode buttons to switch to the corresponding device mode. For more information, see:

* [Viewer AR](ViewerAR.md)
* [Viewer VR](ViewerAR.md)
