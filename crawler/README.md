# Crawler

Scrapes the public SAP Business One 9.3 table reference and stores it in a
local SQLite database (`database.db`, git-ignored) with three tables:
`table_list`, `column_table` and `index_table`. The raw HTML of every page is
kept in `raw_data` for debugging.

```bash
pip install requests beautifulsoup4
python main.py
```

The full run downloads a few thousand pages, so it takes a while. Please be
considerate towards the source website and do not run it more often than
needed.
