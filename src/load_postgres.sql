--users
drop table if exists temp_json;
create temporary table temp_json (values text) on commit drop;
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\users.json';

DELETE FROM users;
insert into users (user_id, card_id, user_email, "password", permissions)
select (values->>'user_id')::int as user_id,
       (values->>'card_id')::int as card_id,
       values->>'user_email' as email,
       values->>'password' as "password",
       (values->>'permissions')::int as permissions      
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from users;


----cards
drop table if exists temp_json;
create temporary table temp_json (values text) on commit drop;
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\users.json';

DELETE FROM users;
insert into users (user_id, card_id, user_email, "password", permissions)
select (values->>'user_id')::int as user_id,
       (values->>'card_id')::int as card_id,
       values->>'user_email' as email,
       values->>'password' as "password",
       (values->>'permissions')::int as permissions      
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from users;drop table if exists temp_json;
create temporary table temp_json (values text) on commit drop;
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\users.json';

DELETE FROM users;
insert into users (user_id, card_id, user_email, "password", permissions)
select (values->>'user_id')::int as user_id,
       (values->>'card_id')::int as card_id,
       values->>'user_email' as email,
       values->>'password' as "password",
       (values->>'permissions')::int as permissions      
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from users;