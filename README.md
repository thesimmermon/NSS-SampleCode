# SampleCode - Sample Application
## A simple application written in .NET Core 3.1 to access a RESTful API

## Table of Contents
* [General Info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Future Development](#future-development)

## General Info
This application was the result of a coding exersize that I completed for a job interview.  Basically, the requirements were to create an application to connect to a backend RESTful API, gather data based on specified criteria, write the data to a log file or to the console.  

Specifically, the exercise required a call to [Cat Facts API](https://alexwohlbruck.github.io/cat-facts/docs/endpoints/facts.html) to get data about cats and other animals, either randomly or by specifying the animal type.  It should also be able to get a specific number of facts from the API.

The data needs to be written to the console and log in a similar way - {ISO 8601 date}{tab}{AnimalType}{tab}{Fact}.  
When writing the data to the console, data about a cat should be written in RED, dog info in BLUE, and for Horse data in GREEN.  

## Technologies
The code is written in C#, .NET Core 3.1.  For IOC, I used AutoFac and for the testing framework I have used XUnit.  For logging, I am using Serilog, although only in a very simplified way in this code base.    

## Setup
After compiling the source, if you simply run the AnimalFactConsole.exe, it will start polling for data using the default settings. Configuration can be setup in the AnimalFactConsole.runtimeconfig.json file. There are settings to manage where the log file is stored and what polling threads are setup for gathering the data.  

## Future Development
There are a few things that I would like to change on this application.  The original specification called for two console applications.  One to write to the console, the other to the log.  I would like to combine this in to one application and use an option in the config to allow it to write to the log, console or both.  

I think there is a better way to call the HttpClient and I would like to take a closer look at that, as well.  Since this was a sample application, I tried to stick within the time limit of a few hours and not conduct a lot of refactoring.
