# 0API
Web API build with ASP.NET Core 9, utilizing Entity Framework Core for interaction with a MySQL database.

### Requirements
- .NET 9
- MySQL 10.5.18 =<

### Project Structure
`Program.cs` - main entry points of the app.

`Data/AppDbContext.cs` - database context.

`Models/User.cs` - user model.

`Models/Statistics.cs` - statistics model.

`Controllers/UserController.cs` - manages user-related operations.

`Controllers/StatisticsController.cs` - manages statistics-related operations.

`Services/UserService.cs` - business logic related to users.

`Services/StatisticsService.cs` - business logic related to statistics.

### API Endpoints

#### UserController
`POST /user/check` - checks and adds a user based on HWID.

`POST /user/heartbeat` - updates the user's last activity.

`GET /user/count` - returns the count of active users.

`GET /user/total` - returns the total count of users.

#### StatisticsController
`GET /statistics` - returns total pentests count and app openings count.

