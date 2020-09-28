# Tabloid MVC
Good news, everyone, our [Tabloid CLI Proof of Concept](https://github.com/nashville-software-school/TabloidCLI) did it's job! We were able to test our business idea after a minimal amount of development time. And we learned people don't want to keep a list of other people's blog content. What they really want is to make their own content.

This application is a multi-user web application built utilizing ASP.NET Core MVC; it allows users to write posts and share them with other users, comment on these posts in order to discuss them with fellow users.

Tabloid MVC has two types of users:

Authors can create Posts, manage their own Posts, and read and comment on other authors' posts.

Admins can do all the things authors can do, but are also in charge of managing all the data in the system.

## Installation
1. In the terminal, git clone git@github.com:nss-day-cohort-41/tabloidmvc-the-lobster-rolls.git
2. open Visual Studio and select to open the pertinent file
3. Open SQL Server Object Explorer
4. Go to Solution Explorer
5. Click Folder icon and change it to 'Folder View'
6. From the SQL Folder click 01_TabloidMVC_Create_DB.sql
7. Click Run
8. Click 02_TabloidMVC_Seed_Data.sql to populate the database
9. Click Run
10. Click Run TabloidMVC
11. You will have to copy the url https://localhost:5001 into the browser
12. Click the 'Register' button
13. If registration is successful, the application will take you to the homepage

## Usage
**Post View**
* This page is a list of ALL user's posts; users will be able to view the title, author, category of the post, and publish date of the post
1. User can add a post by clicking ```Add Post```
2. View post details by clicking the 'eye' icon
3. If the user is the author of the post, they can edit and delete posts by clicking the 'pencil' icon and/or 'trash can' icon, respectively

**Post Details**
* This page is a detailed view of the respective post the user selects; users will be able to view everything aforementioned as well as an estimated read time and the contents of the post. Users will also be able to view the tags that are attached to the post.
1. Add a comment to the selected post by clicking ```Add Comment```
2. View a list of comments attached to this post by clicking ```View Comments```
3. Add tags to the post by clicking ```Add Tag```
4. Delete tags from the post by clicking ```Delete Tag```
5. If the user is the author of the post, they will also be able to edit and delete the post by clicking on the 'pencil' icon and/or 'trash can' icon, respectively

**Add Tags**
To add more than one tag, hold down CTRL and select the tags you wish to associate with the post and click ```Add Tags```

**Delete Tags**
To delete more than one tag, hold down CTRL and select the tags you wish to delete from the post and click ```Delete Tags```

**Comments List**
* This page will list all the comments associated with the user's selected post; users will be able to see the author of the post, subject, comment, and date written for the comment
1. The user can edit and delete comments by clicking the 'pencil' icon and 'trash can' icon, respectively 

**My Posts View**
* This page is a list of all the posts written by the user that is logged in; you can add a post by clicking ```Add Post```
1. View post details by clicking the 'eye' icon
2. Edit and delete posts by clicking the 'pencil' icon and 'trash can' icon, respectively 

## Technologies Used
1. C# with ASP.NET Core MVC
2. Microsoft SQL Server Express

## Authors and Acknowledgment
Our group consisted of Kelley Crittenden, Tyler Hilliard, Brett Stoudt, and Sisi Freeley. Thank you to everyone in the group for working, communicating, and troubleshooting so well together! You guys are all awesome!

### ERD
![Tabloid ERD](./Tabloid.png)
