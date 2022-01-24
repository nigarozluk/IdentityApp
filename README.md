# IdentityApp
Authentication and Authorization without .NET Core Identity (if you login, you can see informations about your session  and cookie)
<br/>
<br/>
***I used Entity Framework and MSSQL***
<br/>
<br/>
*This Project has **SeedData**.*
<br/>
<br/>
*When the project runs, 2 users will be created automatically, one in admin role and the other in user.Before that, you should make the database connection in **appsettings.json** .*
<br/>
<br/>
*You can register as user.*
<br/>
<br/>
<img src="ReadMeImages/1.PNG">
<br/>
<br/>
*You can run the project and login with the following user information or you can register as a user (user role). You must use the following admin information to assign roles to users.*
<br/>
<br/>
**The records in the password column hashed(in SeedData),for this reason you can not use them for login, ***You must use unhashed  state of password: They are;*****

<br/>
<br/>
| UserName      | Password |
| ----------- | ----------- |
| admin_example_mail@gmail.com| 123456  |
| user_example_mail@gmail.com | 1234567 |


<br/>
<br/>
<img src="ReadMeImages/2.PNG">
<br/>
<br/>

**if you login, you can see information about your session  and cookie. When You logout, your session and cookie are cleared.**
<br/>
<br/>
<img src="ReadMeImages/3.PNG">
<br/>
<br/>

**if you login as a Admin, you can role to user.(I have defined 2 roles for simplicity; Admin and User. Only users with #admin role# can assign roles to other users.)**
<br/>
<br/>

**You can select users which registered in the database and roles (Admin,User)**
<br/>
<br/>
<img src="ReadMeImages/4.PNG">
