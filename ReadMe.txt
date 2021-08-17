--- How to connect database to EF

1) Download MySQL on offical site or on the following link: https://dev.mysql.com/downloads/ (for Windows)
2) Open downloaded file and install MySQL
3) When MySQL will ask for the user and password, input following:

	- user: root
	- password: 12345678

4) Clone project from git and open HomeController.cs
5) Write following code in function Index():

	using (var context = new KindOfSportRepository())
            {
                KindOfSport sport = new KindOfSport { Name = "Football" };
                context.AddItem(sport);
            }
	return View();

6) Open MySQL Workbench 8.0 CE
7) Click on "Local instance MySQL80"
8) Hide Workbench and run project in Visual Studio
9) Open Workbench and refresh schemas (right-click on empty space in SCHEMAS -> refresh all)
10) You should see a new shema called "sportschema"

In case if you get errors, contact @Rev1kon or @Ignars3
---  