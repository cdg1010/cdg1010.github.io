# SunStudy

## Description

The SunStudy package for the Untiy Editor helps position the Sun in a Unity scene according to real-life parameters such as latitude/longitude or a specific address on Earth.

## Instructions

   1. Extract the contents of the .zip file into your Unity project.
   2. Add the SunStudy component to a directional light.

The component automatically rotates the directional light to match the real-world Sun position.

## Settings

### True North

The True North vector indicates the direction of “North” in your scene in world space. This does not have to be a unit vector. You can use the slider to rotate the vector and adjust the sun position accordingly.

![#](images/#.png)

### Static Mode

In Static mode, you can manually enter the azimuth and altitude coordinates of the sun. These coordinates are translated into the directional light’s rotation.

### Geographical Mode

In Geographical mode, you can enter geographical parameters to position the sun. You can choose from the following parameters:

### Latitude/Longitude

This parameter lets you specify the desired latitude and longitude manually.
* Latitude: 0 degrees is the equator, 90 degrees is the North Pole, and -90 degrees is the South Pole.
* Longitude: 0 degrees is Greenwich with positive values to the east and negative values to the west.

### Degrees, Minutes, Seconds
This parameter lets you specify hemisphere (North/South) and direction (East/West). It also translates minutes and seconds into decimals for the latitude/longitude values.

### Address
This parameter lets you enter or search for an address and set the latitude/longitude values accordingly. Note that you will need a valid Google Geocoding API key to use this feature (see SunStudy.cs, line 103).

### Time

This setting positions the Sun to correspond to a specific date and time.
The day slider lets you select a value from 1 to 365 and translates that value into a date. You can also enter a date directly and the slider will adjust accordingly.
The time slider lets you select a value from 0 to 1439 (the number of minutes in a day) and translates that value into a time. You can also enter a time directly and the slider will adjust accordingly.

### Intensity

This setting lets you adjust the intensity of the directional light associated with the SunStudy script. By default, this will be the directional light to which the script is added.
* The Intensity value is a multiplier of the light intensity.
* The Apply Dimming option dims the intensity based on the time of day.

   **Note:** This option might not reflect the correct values for a given location or date but you can use it for artistic effect.
