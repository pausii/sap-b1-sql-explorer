# Backend (ASP.NET Core 8)

## Configuration

Copy `appsettings.Development.json.example` to `appsettings.Development.json`
and fill in:

| Key                          | Description                                    |
|------------------------------|------------------------------------------------|
| `ConnectionStrings:HanaDb`   | ODBC connection string to SAP HANA             |
| `ConnectionStrings:DefaultConnection` | SQLite cache file (default `app.db`) |
| `Jwt:Key`                    | Random secret, at least 32 characters          |
| `Jwt:Issuer` / `Jwt:Audience`| Token issuer / audience                        |
| `Auth:Username` / `Auth:Password` | The single login user                     |
| `GlobalSettings:MainSchema`  | HANA schema to browse                          |

The same keys can be supplied as environment variables using `__` as the
separator (for example `Jwt__Key`).

## Run

```bash
dotnet ef database update
dotnet run
```

## Example requests

```bash
# login
curl -X POST http://localhost:5037/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"your-login-password"}'

# run a query
curl -X POST http://localhost:5037/sql/execute \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <token>" \
  -d '{"query":"SELECT * FROM OITM","limitRows":10}'
```

## Importing table documentation from the crawler

```sql
ATTACH 'crawler/database.db' AS source;
INSERT INTO main.ExternalTableListEntity SELECT * FROM source.table_list;
INSERT INTO main.ExternalColumnTable   SELECT * FROM source.column_table;
DETACH source;
```
