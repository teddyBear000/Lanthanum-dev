# Lanthanum

## How to run the app locally
1. Clone project
2. Install MySQL Server from [official website](https://dev.mysql.com/downloads/mysql/ "MySQL")
3. Set up user secrets manager and replace "password" and "user" with correct credentials:
```dosini
> dotnet user-secrets init --project Lanthanum.Web
> dotnet dotnet user-secrets set "DataBase:Password" "password" --project Lantanum.Web
> dotnet user-secrets set "DataBase:User" "user" --project Lanthanum.Web

> dotnet user-secrets set "Authentication:Google:ClientId" "750623719907-nbrpj519a0037fdbq97n6hmve53qaqhi.apps.googleusercontent.com" --project Lanthanum.Web
> dotnet user-secrets set "Authentication:Google:ClientSecret" "n52Zcy5E6DQnk9qI-MAwWAOD" --project Lanthanum.Web
> dotnet user-secrets set "Authentication:Facebook:AppId" "369918684828323" --project Lanthanum.Web
> dotnet user-secrets set "Authentication:Facebook:AppSecret" "4e16edc9fedd3f210d6dae4b48dc21b5" --project Lanthanum.Web
```
