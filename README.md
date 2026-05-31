# Sakura Anime Cafe

Classic ASP.NET MVC 5 demo website for a Japanese/anime-themed cafe and merchandise shop.

## Stack

- ASP.NET MVC 5
- .NET Framework 4.8
- Entity Framework 6
- SQL Server LocalDB
- Razor Views
- Bootstrap 5 plus custom CSS

## Run locally

1. Open `JapnesseCafe.sln` in Visual Studio.
2. Restore NuGet packages if Visual Studio does not restore them automatically.
3. Build the solution.
4. Start the project with IIS Express.

The default connection string uses LocalDB:

```xml
Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=JapnesseCafeDb;Integrated Security=True;MultipleActiveResultSets=True
```

## Database

The app uses EF Code First with `CreateDatabaseIfNotExists`. On first run it creates `JapnesseCafeDb`, creates the `MenuItems` and `Products` tables, and seeds demo records with generated PNG images stored directly in SQL Server.

If you prefer a manual database script, run:

```sql
SQL/CreateAndSeed.sql
```

The SQL script creates the tables and text seed data. Image upload through the admin pages stores binary image data in the database.

## Pages

Public pages:

- `/Home/Index`
- `/Menu/Index`
- `/Products/Index`
- `/Cart/Index`
- `/Cart/CheckoutSuccess`

Admin pages:

- `/Admin/Index`
- `/Admin/MenuItems`
- `/Admin/CreateMenuItem`
- `/Admin/EditMenuItem/{id}`
- `/Admin/DeleteMenuItem/{id}`
- `/Admin/Products`
- `/Admin/CreateProduct`
- `/Admin/EditProduct/{id}`
- `/Admin/DeleteProduct/{id}`

No authentication is used because this is a demo. The admin area is directly accessible at `/Admin`.

## Images

Images are saved in SQL Server in these columns:

- `ImageData VARBINARY(MAX)`
- `ImageMimeType NVARCHAR(80)`

Images are displayed through:

- `/Menu/Image/{id}`
- `/Products/Image/{id}`

## Demo features

- Themed home page with hero banner and large navigation cards.
- Grouped cafe menu categories: Drinks, Desserts, Snacks, Main Course Meals.
- Grouped product categories: Figures, T-Shirts, Anime Accessories.
- Session cart combining menu items and products.
- Fake checkout success flow.
- Admin CRUD for menu items and products.
- Admin image upload saved directly in SQL Server.
