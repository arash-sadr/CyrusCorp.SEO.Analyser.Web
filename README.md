# CyrusCorp.SEO.Analyser.Web
Simplified SEP Analyzer


SEO Analyser
SEO Analyser is a web application that performs a simple SEO analysis of the text.
User submits a text in English or URL,
-Filters out stop-words (e.g. ‘or’, ‘and’, ‘a’, ‘the’ etc),
-	Calculates number of occurrences of each word,
-	Number of occurrences on the page of each word listed in meta tags,
-	Number of external links in the text


This is a rough idea of the process of this system. 
Technologies:-

Main :
   o	API : .NET MVC 5

Tools Used:-
•	Visual Studio 2015

Installation Instruction


This software is an ASP.Net Framework 4.5.2 MVC applications that needs the related Microsoft framework installed on your machine. By having a free version of Visual Studio 2015 e.g. Community, you have all related tools to start downloading the app and playing with code. I appreciate if you report the issue and bugs to me for enhancement purpose.

- After clicking on Clone or Download, you have the option to download the zip file which contain the solution folder for you.
- Unzip the file and open the solution with your Visual Studio 2015 Community. Make sure it has the latest NuGet package manager tool installed.
- Right click on the solution in Solution explorer view and click on the build, the VS NuGet manager start to download the packages and then the build will be successful
- By click on Run at the menu bar or press F5 on keyboard the application start to build again and setup the IIS Express to load the application as a localhost web application on browser of your OS default, e.g. Chrome.

The application is just having one start form which as a text box few options and submit button. You can select URL or a text that you like to analyze it’s content and paste it into the text area provided. Then if your specimen is a URL, tick the check box “I am analyzing a URL” otherwise just paste your text in the text area and click on the submit button – “Analyze”.
There are three options for the analysis which are by default active:

- Show Word Occurrence
- Show Meta Tag Word Occurrence
- Show The External Links
 
The result will be shown in a separate page and you need to click on back button in order to return back to your main page.


