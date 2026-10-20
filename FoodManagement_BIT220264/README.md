# FoodManagement_BIT220264

ASP.NET Core MVC + EF Core + SQL Server LocalDB. Target: .NET 8.

## Run in Visual Studio
1. Install Visual Studio 2022 with **ASP.NET and web development** and SQL Server Express LocalDB.
2. Extract this ZIP; open `FoodManagement_BIT220264.csproj` (or open the folder) in Visual Studio.
3. Wait for NuGet restore. Choose the project as Startup Project.
4. Press **F5**. On first run, `EnsureCreated()` creates `BIT220264_MID` and the three prefixed tables and inserts 3 categories / 5 dishes.
5. SQL Server Object Explorer → `(localdb)\MSSQLLocalDB` → Databases → `BIT220264_MID` to inspect schema.

Connection string is in `appsettings.json`. If LocalDB is unavailable, change it to your SQL Server instance, e.g. `Server=localhost;Database=BIT220264_MID;Trusted_Connection=True;TrustServerCertificate=True`.

## Implemented
- Three MSSV-named models; SQL table/column names explicitly mapped with MSSV prefix.
- Category/Dish/Image relationships; seed data; create/edit/delete dishes and categories.
- Search, category/status filters, price range, sorting; filtering is done by EF Core in SQL.
- Validation and duplicate dish name within category; category deletion blocked when used.
- Image URL management, multiple images, thumbnail selection; SQL filtered unique index allows at most one thumbnail per dish.
- English default; switch language via navigation and session persistence; core dish UI translated.
- Detail handles missing IDs with HTTP 404; empty result and no-image states are displayed.

## Important note for the exam
This starter uses `Database.EnsureCreated()` to keep setup simple for a timed lab. If the database already exists from an earlier schema, `EnsureCreated()` does not update it. For a fresh exam database, delete the old `BIT220264_MID` database first or use EF migrations. Before submitting, build and test Create/Edit, image selection, filters, and language switching in your Visual Studio version.
