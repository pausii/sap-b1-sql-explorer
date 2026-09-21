# SAP SQL Command

A lightweight web-based SQL explorer for **SAP Business One on SAP HANA**.
Browse the tables of your company schema, look up column definitions and
SAP's own table documentation, and run read-only SQL queries from the browser.

![stack](https://img.shields.io/badge/backend-ASP.NET%20Core%208-512BD4)
![stack](https://img.shields.io/badge/frontend-Vue%203%20%2B%20Vite-42B883)
![stack](https://img.shields.io/badge/db-SAP%20HANA%20(ODBC)-0FAAFF)

## Features

- Table browser grouped by SAP module, with search and descriptions
- Column list per table (data type, length, nullability, defaults)
- SQL editor (Ace) with result grid; `DELETE`, `UPDATE`, `INSERT` and
  `TRUNCATE` are rejected and a `LIMIT` is added automatically
- Table / column metadata is cached in a local SQLite database so HANA is
  only hit when needed
- Dark / light theme

## Repository layout

| Folder      | Description                                                      |
|-------------|------------------------------------------------------------------|
| `backend/`  | ASP.NET Core 8 Web API (JWT auth, HANA ODBC access, SQLite cache) |
| `frontend/` | Vue 3 + TypeScript + Vite + Tailwind single-page app             |
| `crawler/`  | Python script that scrapes public SAP B1 table documentation     |

## Prerequisites

- .NET 8 SDK
- Node.js 18+
- SAP HANA ODBC client (`HDBODBC` driver) installed on the machine running the backend
- Python 3.10+ (only for the crawler)

## Quick start

### 1. Backend

```bash
cd backend
cp appsettings.Development.json.example appsettings.Development.json
# edit appsettings.Development.json: HANA connection string, JWT key, login user
dotnet ef database update      # creates the local SQLite cache (app.db)
dotnet run                     # listens on http://localhost:5037
```

All secrets live in `appsettings.Development.json`, which is git-ignored.
You can also use environment variables, e.g. `ConnectionStrings__HanaDb`,
`Jwt__Key`, `Auth__Username`, `Auth__Password`, `GlobalSettings__MainSchema`.

### 2. Frontend

```bash
cd frontend
cp .env.example .env           # set API URL, login credentials, schema label
npm install
npm run dev
```

`npm run build:prod` builds the SPA into `backend/wwwroot` so the backend can
serve it as a single deployable.

### 3. Table documentation (optional)

The crawler collects table and column descriptions for SAP Business One and
stores them in `crawler/database.db`. Import them into the backend cache so
the table browser shows descriptions and module names:

```bash
cd crawler
pip install requests beautifulsoup4
python main.py
```

Then, from the `backend` folder, attach the crawler database to `app.db`:

```sql
ATTACH 'crawler/database.db' AS source;
INSERT INTO main.ExternalTableListEntity SELECT * FROM source.table_list;
INSERT INTO main.ExternalColumnTable   SELECT * FROM source.column_table;
DETACH source;
```

## API overview

| Method | Route                     | Description                                  |
|--------|---------------------------|----------------------------------------------|
| POST   | `/auth/login`             | Returns a JWT for the configured user        |
| GET    | `/sql/tables`             | Tables of the main schema (cached)           |
| GET    | `/sql/columns/{table}`    | Column definitions for a table               |
| POST   | `/sql/execute`            | Run a read-only query `{ "query": "...", "limitRows": 100 }` |

All `/sql/*` routes require `Authorization: Bearer <token>`.

## Security notes

- The application is intended for internal use behind your own network. It
  exposes raw SQL access to whoever can log in, so use a dedicated read-only
  HANA user and keep the JWT key secret.
- The forbidden-statement check is a simple keyword filter, not a full SQL
  parser. Rely on HANA privileges of the connecting user for real protection.

## License

MIT, see [LICENSE](LICENSE).
