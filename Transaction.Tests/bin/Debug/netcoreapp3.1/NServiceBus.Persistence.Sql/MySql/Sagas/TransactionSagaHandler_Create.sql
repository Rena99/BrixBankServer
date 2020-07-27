
/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'TransactionSagaHandler`');
set @tableNameNonQuoted = concat(@tablePrefix, 'TransactionSagaHandler');


/* Initialize */

drop procedure if exists sqlpersistence_raiseerror;
create procedure sqlpersistence_raiseerror(message varchar(256))
begin
signal sqlstate
    'ERROR'
set
    message_text = message,
    mysql_errno = '45000';
end;

/* CreateTable */

set @createTable = concat('
    create table if not exists ', @tableNameQuoted, '(
        Id varchar(38) not null,
        Metadata json not null,
        Data json not null,
        PersistenceVersion varchar(23) not null,
        SagaTypeVersion varchar(23) not null,
        Concurrency int not null,
        primary key (Id)
    ) default charset=ascii;
');
prepare script from @createTable;
execute script;
deallocate prepare script;

/* AddProperty SagaId */

select count(*)
into @exist
from information_schema.columns
where table_schema = database() and
      column_name = 'Correlation_SagaId' and
      table_name = @tableNameNonQuoted;

set @query = IF(
    @exist <= 0,
    concat('alter table ', @tableNameQuoted, ' add column Correlation_SagaId varchar(200) character set utf8mb4'), 'select \'Column Exists\' status');

prepare script from @query;
execute script;
deallocate prepare script;

/* VerifyColumnType String */

set @column_type_SagaId = (
  select concat(column_type,' character set ', character_set_name)
  from information_schema.columns
  where
    table_schema = database() and
    table_name = @tableNameNonQuoted and
    column_name = 'Correlation_SagaId'
);

set @query = IF(
    @column_type_SagaId <> 'varchar(200) character set utf8mb4',
    'call sqlpersistence_raiseerror(concat(\'Incorrect data type for Correlation_SagaId. Expected varchar(200) character set utf8mb4 got \', @column_type_SagaId, \'.\'));',
    'select \'Column Type OK\' status');

prepare script from @query;
execute script;
deallocate prepare script;

/* WriteCreateIndex SagaId */

select count(*)
into @exist
from information_schema.statistics
where
    table_schema = database() and
    index_name = 'Index_Correlation_SagaId' and
    table_name = @tableNameNonQuoted;

set @query = IF(
    @exist <= 0,
    concat('create unique index Index_Correlation_SagaId on ', @tableNameQuoted, '(Correlation_SagaId)'), 'select \'Index Exists\' status');

prepare script from @query;
execute script;
deallocate prepare script;

/* PurgeObsoleteIndex */

select concat('drop index ', index_name, ' on ', @tableNameQuoted, ';')
from information_schema.statistics
where
    table_schema = database() and
    table_name = @tableNameNonQuoted and
    index_name like 'Index_Correlation_%' and
    index_name <> 'Index_Correlation_SagaId' and
    table_schema = database()
into @dropIndexQuery;
select if (
    @dropIndexQuery is not null,
    @dropIndexQuery,
    'select ''no index to delete'';')
    into @dropIndexQuery;

prepare script from @dropIndexQuery;
execute script;
deallocate prepare script;

/* PurgeObsoleteProperties */

select concat('alter table ', table_name, ' drop column ', column_name, ';')
from information_schema.columns
where
    table_schema = database() and
    table_name = @tableNameNonQuoted and
    column_name like 'Correlation_%' and
    column_name <> 'Correlation_SagaId'
into @dropPropertiesQuery;

select if (
    @dropPropertiesQuery is not null,
    @dropPropertiesQuery,
    'select ''no property to delete'';')
    into @dropPropertiesQuery;

prepare script from @dropPropertiesQuery;
execute script;
deallocate prepare script;

/* CompleteSagaScript */
