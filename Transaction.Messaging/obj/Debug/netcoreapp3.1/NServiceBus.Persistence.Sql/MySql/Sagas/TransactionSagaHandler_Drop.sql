
/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'TransactionSagaHandler`');
set @tableNameNonQuoted = concat(@tablePrefix, 'TransactionSagaHandler');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableNameQuoted);
prepare script from @dropTable;
execute script;
deallocate prepare script;
