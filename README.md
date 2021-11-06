# What is TaskoMask?


  
<p align="left">
  <img alt="GitHub issues" src="https://img.shields.io/github/issues/hamed-shirbandi/TaskoMask">
<!--- <img src="https://img.shields.io/github/workflow/status/hamed-shirbandi/TaskoMask/.NET%20Core%20Build"> ---> 
  <img src="https://img.shields.io/website?url=http://taskomask.ir">
  <img src="https://img.shields.io/github/license/hamed-shirbandi/TaskoMask">
  <img src="https://img.shields.io/github/contributors/hamed-shirbandi/TaskoMask">

</p>
 

[TaskoMask](http://taskomask.ir/) is a free and open source task management system based on .Net. This project is [online](http://taskomask.ir/) and everyone can use it as a team member or project owner.
But the main goal of this project is to be an effort to show how can we clearly implement software technologies and patterns by .Net so this can be used by developers how look for a real example project with real challenges. Take a look at it's [wiki](https://github.com/hamed-shirbandi/TaskoMask/wiki)!

![taskomask website](https://github.com/hamed-shirbandi/TaskoMask/blob/master/docs/images/taskomask-all-in-one-0.jpg)
# Documentation
We are trying to document all important information so you can use them to get more information about what we did and how we did and why!
There is a list of our documentation:

  - ### [User Guide Documentation](https://github.com/hamed-shirbandi/TaskoMask/wiki/User-Guide-Documentation):
    This can be used by developers who want to know more about website, user panel and admin panel or can be used by end users who want to use the TaskoMask application to manage their project's tasks.
    TaskoMask source contains 3 web project as below:
    
     - [TaskoMask.Web](https://github.com/hamed-shirbandi/TaskoMask/tree/master/Web): This layer implemented by **Blazor** and it contains a website and a user panel for managing user's tasks. This layer uses REST API provided by TaskoMask.Web.Api and make an interactive web client.
     - [TaskoMask.Web.Admin](https://github.com/hamed-shirbandi/TaskoMask/tree/master/Web.Admin): This layer implemented by ASP.NET MVC and it contains a panel to manage whole TaskoMask data by administrators.
     - [TaskoMask.Web.Api](https://github.com/hamed-shirbandi/TaskoMask/tree/master/Web.Api): This layer implemented by ASP.NET Web API and it contains REST API services for TaskoMask clients.
  - ### [Domain Documentation](https://github.com/hamed-shirbandi/TaskoMask/wiki/Domain-Documentation):
    This is for developers to know about the domain model and understand the entities and relations and rules and variants and so on. By reading this doc you can understand the business of this project.
  - ### [Architecture Documentation](https://github.com/hamed-shirbandi/TaskoMask/wiki/Architecture-Documentation):
    This doc is about the architecture, pipelines,technologies, patterns, approaches, decisions and other things we implemented in this project. We talk about our choices and decisions and challenges.
  - ### [Rest Api Documentation](https://github.com/hamed-shirbandi/TaskoMask/wiki/Rest-Api-Documentation):
    This is a live rest api service documentation generated by swagger and it can be used by front-end or mobile developers to make a client app. We use it in [TaskoMask.Web](https://github.com/hamed-shirbandi/TaskoMask/tree/master/Web) layer to make a web client by **Blazor**.

# Architecture And Tools
  * ### Back-end:
      - .Net 5 
      - C#
      - ASP.NET Web API
      - ASP.NET MVC
      -	MongoDB
      -	Redis
      -	SignalR (soon)
      -	MediatR
      -	AutoMapper
      -	FluentValidation
      -	StructureMap
      -	Swagger
      -	MsTest
      -	[MvcPagedList.Core](https://www.nuget.org/packages/MvcPagedList.Core/)
      -	[RedisCache.Core](https://www.nuget.org/packages/RedisCache.Core/)
      -	[DbLogger.Core](https://www.nuget.org/packages/DbLogger.Core/)
  * ### Front-end:
      - Blazor (soon)
      -	.HTML
      -	CSS
      -	Java Script 
      -	JQuery
      -	Bootstrap
      -	Signalr.js
      -	Jquery.noty
      -	Chart.js
  * ### Patterns, Methodologies، Approaches:
      -	Onion Architecture
      -	DDD
      -	CQRS
      -	Event Sourcing
      -	Notification
      -	Repository
      -	CRUD,Anemic Model (for less important subdomains)
      -	Unit Test
  * ### Some technical features:
      -	Caching Behavior
      -	Validation Behavior
      -	Event Storing Behavior
      -	Application Exception Handler
      -	In Memory Bus
      -	Cookie Authentication
      -	JWT Authentication
      -	Role Permission Base User Management
      -	Swagger UI with JWT Support
      -	Combined Validation (Fluent, Data Annotation)
      -	Automatic DB Logger

# Contributing
Contributions, issues and feature requests are welcome. Feel free to check issues page if you want to contribute. Any contributions you make are greatly appreciated.
Please check the [issues](https://github.com/hamed-shirbandi/TaskoMask/issues) and [projecs](https://github.com/hamed-shirbandi/TaskoMask/projects) pages before anything.
  > 1. Give a Star
  > 2. Fork the Project
  > 3. Create your Feature Branch
  > 4. Commit your Changes
  > 5. Open a Pull Request

This project exists thanks to all the people who [contribute](https://github.com/hamed-shirbandi/TaskoMask/graphs/contributors).

![GitHub Contributors Image](https://contrib.rocks/image?repo=hamed-shirbandi/TaskoMask)
     
# Supporting
We work hard to make something useful to you so please give a star ⭐ if this project helped you!
We need your support by giving a star or by contributing or telling about us to your friends.

# Author & License
This project is developed by [Hamed Shirbandi](https://github.com/hamed-shirbandi) under [MIT](https://github.com/hamed-shirbandi/TaskoMask/blob/master/LICENSE) licensed.
Find Hamed around the web and feel free to ask your question.

[![personal blog](http://www.codeblock.ir/Content/site/images/blog/Blog.png)](http://www.codeblock.ir)
[![linkedin](http://www.codeblock.ir/Content/site/images/blog/linkedin_ic.png)](https://www.linkedin.com/in/hamed-shirbandi)
[![nuget](http://www.codeblock.ir/Content/site/images/blog/nuget_ic.png)](https://www.nuget.org/profiles/hamed-shirbandi)
[![email](http://www.codeblock.ir/Content/site/images/blog/Gmail-ic.png)](mailto:hamed.shirbandi@gmail.com)
[![github](http://www.codeblock.ir/Content/site/images/blog/github_ic.jpg?v=2)](https://github.com/hamed-shirbandi)
[![instagram](http://www.codeblock.ir/Content/site/images/blog/instagram.png)](https://www.instagram.com/hamedshirbandi)

# Change Logs

*	### Oct, 2021
    - [x] Start admin panel with ASP.NET MVC
    - [x] Implement administration subdomain by CRUD
*	### Aug, 2021
    - [x] Remove Asp.net Identity from project
    - [x] Add cookie authentication
    - [x] Add jwt authorization
 * ### Jul, 2021
    - [x] Full refactore
 * ### Nov, 2020
    - [x] upgrade from net 3.1 to net 5
*	### Oct, 2020
    - [x] Repository Created
  


