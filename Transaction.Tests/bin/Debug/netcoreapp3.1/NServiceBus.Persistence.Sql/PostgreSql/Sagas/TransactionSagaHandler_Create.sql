
/* TableNameVariable */

/* Initialize */

/* CreateTable */

create or replace function pg_temp.create_saga_table_TransactionSagaHandler(tablePrefix varchar, schema varchar)
    returns integer as
    $body$
    declare
        tableNameNonQuoted varchar;
        script text;
        count int;
        columnType varchar;
        columnToDelete text;
    begin
        tableNameNonQuoted := tablePrefix || 'TransactionSagaHandler';
        script = 'create table if not exists "' || schema || '"."' || tableNameNonQuoted || '"
(
    "Id" uuid not null,
    "Metadata" text not null,
    "Data" jsonb not null,
    "PersistenceVersion" character varying(23),
    "SagaTypeVersion" character varying(23),
    "Concurrency" int not null,
    primary key("Id")
);';
        execute script;

/* AddProperty SagaId */

        script = 'alter table "' || schema || '"."' || tableNameNonQuoted || '" add column if not exists "Correlation_SagaId" character varying(200)';
        execute script;

/* VerifyColumnType String */

        columnType := (
            select data_type
            from information_schema.columns
            where
            table_schema = schema and
            table_name = tableNameNonQuoted and
            column_name = 'Correlation_SagaId'
        );
        if columnType <> 'character varying' then
            raise exception 'Incorrect data type for Correlation_SagaId. Expected "character varying" got "%"', columnType;
        end if;

/* WriteCreateIndex SagaId */

        script = 'create unique index if not exists "' || tablePrefix || '_i_EA56F6B7F345877C9E58166D4E741398E7489F8C" on "' || schema || '"."' || tableNameNonQuoted || '" using btree ("Correlation_SagaId" asc);';
        execute script;
/* PurgeObsoleteIndex */

/* PurgeObsoleteProperties */

for columnToDelete in
(
    select column_name
    from information_schema.columns
    where
        table_name = tableNameNonQuoted and
        column_name LIKE 'Correlation_%' and
        column_name <> 'Correlation_SagaId'
)
loop
	script = '
alter table "' || schema || '"."' || tableNameNonQuoted || '"
drop column "' || columnToDelete || '"';
    execute script;
end loop;

/* CompleteSagaScript */

        return 0;
    end;
    $body$
language 'plpgsql';

select pg_temp.create_saga_table_TransactionSagaHandler(@tablePrefix, @schema);
