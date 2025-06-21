import sqlite3, requests
from bs4 import BeautifulSoup
from pprint import pprint

conn = sqlite3.connect('database.db')
cur = conn.cursor()
cur.execute('''
    CREATE TABLE IF NOT EXISTS raw_data (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        url varchar(255) NOT NULL,
        content TEXT NOT NULL,
        created_at DATETIME
    )
''')
cur.execute('''
    CREATE TABLE IF NOT EXISTS table_list (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        table_name varchar(50) NOT NULL,
        product varchar(50) NULL,
        version varchar(10) NULL,
        total_columns INTEGER NULL,
        total_indexs INTEGER NULL,
        description TEXT NULL,
        module varchar(255) NULL
    );
''')
cur.execute('''
    CREATE TABLE IF NOT EXISTS column_table (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        table_name varchar(50) NOT NULL,
        `order` INTEGER NULL,
        column_name varchar(100) NOT NULL,
        description TEXT NULL,
        sql_type varchar(50) NULL,
        length INTEGER NULL,
        decimals INTEGER NULL,
        relation varchar(50) NULL,
        default_value TEXT NULL,
        constraints varchar(255) NULL
    )
''')
cur.execute('''
    CREATE TABLE IF NOT EXISTS index_table (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        table_name varchar(50) NOT NULL,
        name varchar(100) NOT NULL,
        `primary` varchar(50) NULL,
        `unique` varchar(50) NULL,
        type varchar(50) NULL,
        columns varchar(255) NULL
    )
''')

"""
CREATE INDEX IF NOT EXIST idx_table_list_table_name ON table_list (table_name);
CREATE INDEX IF NOT EXIST idx_table_list_product ON table_list (product);

CREATE INDEX IF NOT EXIST idx_column_table_table_name ON column_table (table_name);
CREATE INDEX IF NOT EXIST idx_column_table_column_name ON column_table (column_name);

"""

conn.commit()

def insert_raw_data(url, content):
    cur.execute('INSERT INTO raw_data (url, content) VALUES (?, ?)', (url, content))
    conn.commit()

def get_tables():
    # get tables
    tables = []
    response = requests.get('https://sap.erpref.com/?schema=BusinessOne9.3')
    if response.status_code == 200:
        # print(response.text)
        soup = BeautifulSoup(response.text, 'html.parser')
        content_div = soup.find('div', class_='content')
        table = content_div.find('table')
        rows = table.find_all('tr')
        
        for row in rows:
            cols = [col.text.strip() for col in row.find_all(['th', 'td'])]
            if len(cols) > 8:
                continue
            
            cols.pop(0) # remove index 1
            cur.execute('INSERT INTO table_list (table_name, product, version, total_columns, total_indexs, description, module) VALUES (?, ?, ?, ?, ?, ?, ?)', cols)
            conn.commit()
            print(cols)
            tables.append(cols[2])

        insert_raw_data(response.url, response.text)
        print(response.url)
        return tables
    else:
        raise Exception(response.status_code, "Failed to get tables")

def get_data_table(table_name):
    res = requests.get(f'https://sap.erpref.com/?schema=BusinessOne9.3&table={table_name}')
    if res.status_code == 200:
        # print(response.text)
        soup = BeautifulSoup(res.text, 'html.parser')
        content_div = soup.find('form', id='columnselectcollection')
        # console.log(content_div)
        table = content_div.find('table')
        rows = table.find_all('tr')
    
        for row in rows:
            cols = [col.text.strip() for col in row.find_all(['th', 'td'])]
            cols.pop(0) # remove index 1
            cols.pop(9)
            cols.insert(0, table_name)

            if cols[1] == 'Column' and cols[2] == 'Field':
                continue

            cur.execute('INSERT INTO column_table (table_name, `order`, column_name, description, sql_type, length, decimals, constraints, relation, default_value) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)', cols)
            conn.commit()            
            
            print(cols)

        # index
        div = soup.find('div', id='indexes')

        # Cari tabel setelahnya
        table = div.find_next('table', class_='main')

        rows = table.find_all('tr')
        for row in rows:
            cols = [col.text.strip() for col in row.find_all(['th', 'td'])]
            if cols[0] == 'Name' and cols[1] == 'Primary':
                continue

            cols.insert(0, table_name)
            cur.execute('INSERT INTO index_table (table_name, name, `primary`, `unique`, type, columns) VALUES (?, ?, ?, ?, ?, ?)', cols)
            conn.commit()

            print(cols)

        insert_raw_data(res.url, res.text)
        print(res.url)
    else:
        raise Exception(response.status_code, "Failed to get tables")

def main():
    cur.execute('DELETE FROM table_list')
    cur.execute('DELETE FROM column_table')
    cur.execute('DELETE FROM index_table')
    conn.commit()

    tables = get_tables()
    i = 0
    total = len(tables)
    for table in tables:
        i += 1
        print(f'{i}/{total} {table}')
        get_data_table(table)
    # get_data_table('ONCM')

if __name__ == "__main__":
    main()