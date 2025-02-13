# GeoPositionViewer.App
A test project for viewing a geographical position (at high interval and at 2 seconds interval) and an average position. 

## Overview
GeoPositionViewer.App is a WPF application designed to display position information. The application shows:


- **Raw Position**: Displays the actual position data at a high interval (100 ms) along with a timestamp.
- **Current Position**: Displays the actual position data every two seconds along with a timestamp.
- **Average Position**: Shows the calculated average position and the count of positions since the application started.
  
![image](https://github.com/user-attachments/assets/63cb36d1-0eaa-4211-bb71-730c1bf1e656)
  
The application follows the MVVM architecture and SOLID principles. It includes a position simulator service that generates a fake position near the Fugro Nootdorp office with a ±5 meter accuracy.



## Getting Started
To run the application, open the GeoPositionViewer.App is the starting point, build the solution in Visual Studio and run the GeoPositionViewer.App.


# Requirment Criteria that was used to do this task

The application is getting frequently a geographic position. You have to calculate the average position and display it in UI. Moreover, the application should also display the current position, not more frequently than every 2 seconds. Please make the UI intuitive and descriptive as much as possible.

Tasks to do:
1. Display average position in UI, update it not more frequently than every 2 seconds.
2. Display current position, not more frequently than every 2 seconds.
3. Possibly, afterwards we have some discussion about the delivered source code.

This exercise should not be done as quick as possible. The highest priority is to write a clean and maintainable code and ensure it works as expected. Stability and quality of our code is of the highest importance.

Supplemental requirements: 
* The implemented logic has to be covered with unit tests. 
* The solution has to be put to a git-based folder, so your progress can be seen with the commits you do during the development. Our recommendation is to use GitHub.com to create a private repository with providing access to us (GitHub usernames to be provided).

Other recommendations: 
* Think "SOLID" 
* Do the "KISS" 
* Write "Clean Code"

