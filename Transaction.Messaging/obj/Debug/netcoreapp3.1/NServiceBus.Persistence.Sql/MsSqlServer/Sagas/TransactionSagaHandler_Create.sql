
/* TableNameVariable */

declare @tableName nvarchar(max) = '[' + @schema + '].[' + @tablePrefix + N'TransactionSagaHandler]';
declare @tableNameWithoutSchema nvarchar(max) = @tablePrefix + N'TransactionSagaHandler';


/* Initialize */

/* CreateTable */

if not exists
(
    select *
    from sys.objects
    where
        object_id = object_id(@tableName) and
        type in ('U')
)
begin
declare @createTable nvarchar(max);
set @createTable = '
    create table ' + @tableName + '(
        Id uniqueidentifier not null primary key,
        Metadata nvarchar(max) not null,
        Data nvarchar(max) not null,
        PersistenceVersion varchar(23) not null,
        SagaTypeVersion varchar(23) not null,
        Concurrency int not null
    )
';
exec(@createTable);
end

/* AddProperty SagaId */

if not exists
(
  select * from sys.columns
  where
    name = N'Correlation_SagaId' and
    object_id = object_id(@tableName)
)
begin
  declare @createColumn_SagaId nvarchar(max);
  set @createColumn_SagaId = '
  alter table ' + @tableName + N'
    add Correlation_SagaId nvarchar(200);';
  exec(@createColumn_SagaId);
end

/* VerifyColumnType String */

declare @dataType_SagaId nvarchar(max);
set @dataType_SagaId = (
  select data_type
  from INFORMATION_SCHEMA.COLUMNS
  where
    table_name = @tableNameWithoutSchema and
    table_schema = @schema and
    column_name = 'Correlation_SagaId'
);
if (@dataType_SagaId <> 'nvarchar')
  begin
    declare @error_SagaId nvarchar(max) = N'Incorrect data type for Correlation_SagaId. Expected nvarchar got ' + @dataType_SagaId + '.';
    throw 50000, @error_SagaId, 0
  end

/* WriteCreateIndex SagaId */

if not exists
(
    select *
    from sys.indexes
    where
        name = N'Index_Correlation_SagaId' and
        object_id = object_id(@tableName)
)
begin
  declare @createIndex_SagaId nvarchar(max);
  set @createIndex_SagaId = N'
  create unique index Index_Correlation_SagaId
  on ' + @tableName + N'(Correlation_SagaId)
  where Correlation_SagaId is not null;';
  exec(@createIndex_SagaId);
end

/* PurgeObsoleteIndex */

declare @dropIndexQuery nvarchar(max);
select @dropIndexQuery =
(
    select 'drop index ' + name + ' on ' + @tableName + ';'
    from sysindexes
    where
        Id = object_id(@tableName) and
        Name is not null and
        Name like 'Index_Correlation_%' and
        Name <> N'Index_Correlation_SagaId'
);
exec sp_executesql @dropIndexQuery

/* PurgeObsoleteProperties */

declare @dropPropertiesQuery nvarchar(max);
select @dropPropertiesQuery =
(
    select 'alter table ' + @tableName + ' drop column ' + column_name + ';'
    from INFORMATION_SCHEMA.COLUMNS
    where
        table_name = @tableNameWithoutSchema and
        table_schema = @schema and
        column_name like 'Correlation_%' and
        column_name <> N'Correlation_SagaId'
);
exec sp_executesql @dropPropertiesQuery

/* CompleteSagaScript */
