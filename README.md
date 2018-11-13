# Fleet-4D
Using time-geography to visualize COTA trip data

![alt text](https://github.com/SCODEMeetup/Fleet-4D/Images/COTA-4D.png)

With inspiration from the transit [visualization](http://mbtaviz.github.io/) tool [Jesse](MathematicsCLtd) found for the Boston MBTA, 
I decided to make one in Unity in 4D. There's a dropdown to select a route, although that part doesn't work yet (it's taking too long to parse the file after making the selection). 
This is using COTA's [GTFS Static Feed](https://www.cota.com/COTA/media/COTAContent/OpenGTFSData.zip) zipped package from the [COTA Data](https://www.cota.com/data/) site. 
I'm using this link because while some of this data is in [SCOS](https://www.smartcolumbusos.com/), this zip pulls in data from the current day (separate from the live streaming data).

