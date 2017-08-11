# asp_net_mvc #
## ASP.NET MVC Application. ##
This is an .NET MVC5 application made with Visual Studio.


## How to install this project on a brand new machine ##

These are the steps I used to install this project for Visual
Studio on a new laptop (Lenovo G50) with Windows 10.

Install Visual Studio with .NET MVC5

Open cmd as admin. Install chocolatey like this:
Use web browser to navigate to
https://chocolatey.org/install#install-with-cmdexe

You should see "Install with cmd.exe"

Just below this is a LONG command string that starts with
@"%SystemRoot%\System32\WindowsPowerShell.....

Copy this whole thing and paste into cmd. Execute this command
to install chocolatey.

Chocolatey is like apt-get on Linux, it allows you to quickly
install huge dependency chains with a single command. Yay!

Next we will use chocolatey to install git.
choco install git -params '"/GitAndUnixToolsOnPath"'

Next we will clone the git repo down onto our local hard drive.
Navigate to C:/Users/G50/mvc_projects/

Run the following command to clone the github repo to your machine.
git clone https://github.com/gellmr/asp_net_mvc.git

You now have C:\Users\G50\mvc_projects\asp_net_mvc

Which contains these files..
Gruntfile.js  bower.json  gellmvc      package.json
README.md     deploy.sh   gellmvc.sln  site_images

We are ready to start installing third party dependencies.

This project uses NPM for third party libraries which run
on the server, and uses Bower for third party libraries which
the website user will download as he visits the site (eg jquery)
The reason for this is NPM just gets everything in your dependency
list so you don't have to resolve conflicting dependencies, you
can just install everything, even have 3 versions of bootstrap-sass
if you want to. But bower has a flat dependency graph so it only lets
you have one version of jquery. This is good because it minimizes the
amount of download for the user. NPM is good for all the loads of libraries
that will sit on the server because it doesnt matter if we have 3 versions
of ruby installed there.

Reopen cmd as admin. Install bower like this:
choco install bower

Install Node.js and npm, using the official msi installer at:
https://nodejs.org/en/download/
I couldn't get the choco installer to work but this way works fine.
Navigate there with your web browser and download it, then run it.
I downloaded "node-v6.11.2-x64"

This will install Node.js and npm under Program Files like this.
C:\Program Files\nodejs\

Reopen cmd as admin and try "node -v" and "npm -v" to see they are installed.

C:\Windows\system32>node -v
v6.11.2

C:\Windows\system32>npm -v
3.10.10

Success!

Now we are ready to install all the npm components.
This project uses npm to collect all the dependencies which run on the server.
Navigate to C:\Users\G50\mvc_projects\asp_net_mvc

npm install

This will take a few minutes to install lots of things under 
C:\Users\G50\mvc_projects\asp_net_mvc\node_modules

Now we will install all the bower components.

This project uses bower to collect all the dependencies which are served
to users of the web application. The reason for this is bower has a flat
dependency graph, so the user only downloads one version of jquery, etc.

Navigate to C:\Users\G50\mvc_projects\asp_net_mvc

bower install

This takes a minute to install some third party libraries under
C:\Users\G50\mvc_projects\asp_net_mvc\bower_components

NPM has already installed some grunt items for us but we need
to install "grunt-cli" which lets us run grunt tasks.

npm install -g grunt-cli

This will install grunt-cli globally.
Once it is installed, ensure you are at
C:\Users\G50\mvc_projects\asp_net_mvc

And then run the following command:
grunt

This will run the grunt tasks 'clean', 'copy', and 'sass'

Which will delete everything in gellmvc\dist

And it will copy all the important deployable assets to
gellmvc\dist
(all the stuff that eventually gets deployed to azure)

And it will run the sass conversion task, turning our sass
files into one compiled css file "gellmvc\dist\site.css"

Now we are ready to deploy these files to azure.

## (TODO - write steps for MSBuild, Kudusync and Azure deployment)##

[The site can be viewed here] (http://mike-gell.azurewebsites.net)

It is currently hosted on Azure.