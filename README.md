# 4FUN
This is a Windows Forms application for renting cars. It stores information about users who have rented cars, as well as information about the cars themselves, including their prices and availability. The application is intended for use by an administrator, as users should not have access to sensitive data. On the home page, the number of clients and all cars in the system, including those not currently available, can be viewed. Additionally, the availability of cars can also be viewed.
#
I used SQLite in this project, because I think that it is the simpliest form of a database, I didn't need to use Entity or Mdf databases. I even used MDF database, but then a lot of errors occured when I wanted to launch my application on other computers. I couldn't launch my application, because: There was a wrong version between computer, and computer on which I was working on, the next error was about hardcoding the path to the database, I couldn't use relative path because it wasn't working, so I had to use SQLite instead.

View of the application:

![Renting](https://user-images.githubusercontent.com/83167847/212550415-8798aeef-5eae-40e1-b981-0b793f01720c.png)
![Home](https://user-images.githubusercontent.com/83167847/212550418-a0b4baf5-7e71-4f2c-8416-5cf604aa0e2f.png)
![Car](https://user-images.githubusercontent.com/83167847/212550419-5434e59e-99a6-4485-b3cb-ebd601c1dbe3.png)
![Clients](https://user-images.githubusercontent.com/83167847/212550420-a1717e2d-1a23-4cd5-8741-d4e3e1d05337.png)

#
Launch: 
1)Install zip package
2) Make sure that your antivirus isn't blocking files ( it happens commonly )
3) Go into "WindowsFormsApp4\bin\Debug"
4) Launch WindowsFormsApp4.exe
