export interface ApiTableResponse {
  module: string
  schema: string
  tables: ApiTable[]
}

export interface ApiTable {
  id: number
  tableName: string
  totalColumns: number
  description: string
  aiDesc?: string | null // AI generated description, optional and can be null
}


export interface ApiColumnItems {
  SCHEMA_NAME: string
  TABLE_NAME: string
  TABLE_OID: number
  COLUMN_NAME: string
  POSITION: number
  DATA_TYPE_ID: number
  DATA_TYPE_NAME: string
  OFFSET: number
  LENGTH: number
  // SCALE: Scale
  IS_NULLABLE: string
  DEFAULT_VALUE: string
  COLLATION: string
  // COMMENTS: Comments
  // MAX_VALUE: MaxValue
  // MIN_VALUE: MinValue
  CS_DATA_TYPE_ID: number
  CS_DATA_TYPE_NAME: string
  DDIC_DATA_TYPE_ID: number
  DDIC_DATA_TYPE_NAME: string
  COMPRESSION_TYPE: string
  INDEX_TYPE: string
  COLUMN_ID: number
  PRELOAD: string
  // GENERATED_ALWAYS_AS: GeneratedAlwaysAs
  HAS_SCHEMA_FLEXIBILITY: string
  FUZZY_SEARCH_INDEX: string
  // FUZZY_SEARCH_MODE: FuzzySearchMode
  // MEMORY_THRESHOLD: MemoryThreshold
  LOAD_UNIT: string
  // GENERATION_TYPE: GenerationType
  IS_CACHABLE: string
  IS_CACHE_KEY: string
  // ROW_ORDER_POSITION: RowOrderPosition
}