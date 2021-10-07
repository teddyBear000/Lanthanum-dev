# Lanthanum

## How to run the app locally
1. Clone project
2. Install MySQL Server from [official website](https://dev.mysql.com/downloads/mysql/ "MySQL")
3. Set up user secrets manager and replace "password" and "user" with correct credentials:
```dosini
> dotnet user-secrets init --project Lanthanum.Web
> dotnet dotnet user-secrets set "DataBase:Password" "password" --project Lantanum.Web
> dotnet user-secrets set "DataBase:User" "user" --project Lanthanum.Web
```
## How to setup footer and its configuration
Run commented code from _Layout.cshtml once to add all required entities to database.
