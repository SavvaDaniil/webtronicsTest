# Test Task for Webtronics C#/WPF candidate
### Description
Create a file manager that presents the file system with the MVVM pattern. 

## Functional requirements:
●	Consists of a single area ("working area"), which displays the contents of the current directory (or folder)  
●	Upon double clicking on an element:  
  -	If it is a file, then the application tries to open using windows;  
  -	If it is a folder, then the working area is filled with the contents of this folder  

●	Upon a single click on an element, on the right side of the working area, a panel should appear that displays additional information:  
  -	If it is a file, then its metadata is displayed(size, date created, etc..)  
  -	If it is a folder, then its size and amount of file it contains is displayed  

●	Upon opening a file, write into the database. The history entity contains:  
  -	Id  
  -	Filename  
  -	Date visited  

  
## Bonus section (not required):
●	Implement a path in the folder hierarchy at the top of the work area. Clicking on a folder in the path hierarchy, jumps to the given folder  
●	Implement a search function by name. An area (or a window at your discretion) will be added, in which, when entering text, a list of relevant files in the given folder appears.  
When you click on a file, the application tries to open it in Windows.  
●	If you add tests based on any testing framework (pytest, unittest, etc.)  
●	Demonstrate your use of explicit and implicit styles  
Double bonus section (not assessed, but demonstrates your knowledge in specific fields):  
●	Create a simple implementation of an FTP service, where the second application only accepts files, whilst the first retains all of the above functionalities.  
●	Add the ability to localize the application (you don’t have to translate the application, but add a file for someone to be able to)  
  
## Technology Requirements
Tasks should be completed:  
●	On C#/.NET + WPF  
●	With any DBMS (Sqlite, PostgreSQL, MySQL)  
●	Uploaded to the GitHub  
●	Bonus: Using English to write comments and descriptions of classes, fields, etc  
  
## Requirements
When implementing your solution, please make sure that the code is:  
●	Well-structured  
●	Contains instructions (best to be put into readme.md) about how to deploy and test it  
●	Clean  
  
The program you implement must be a complete program product, i.e. should be easy to install, provide for the handling of non-standard situations, be resistant to incorrect user actions, etc.  
  
--------------------------------------------------------------------------  
  
# Instructions for deploy:
1) Make sure you have Visual Studio 2022 IDE versions installed,
2) Clone this repository to your local machine,
3) In the open project of Visual Studio 2022, the necessary settings for generating a single exe file have already been registered.
To generate an exe file on your computer, run the "Publish" command (right-click on the project in "Solution Explorer" -> "Publish")
Generate a single exe file according to the official instructions
https://docs.microsoft.com/en-us/dotnet/core/deploying/single-file/overview?tabs=vs#tabpanel_1_vs ,
4) When the program is launched for the first time, in the folder where the exe of this generated project is located, a Sqlite database file with the name "webtronicsSqlite.db" will appear, in which all file visits by the user will be recorded. You can use any DBMS to read it, in this case I recommend https://sqlitestudio.pl (use VPN if you are in the territory of the Russian Federation or the Republic of Belarus).
