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
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\cards.json';

DELETE FROM cards;
insert into cards (card_id, activation_time, "type")
select (values->>'card_id')::int as card_id,
       (values->>'activation_time')::int as activation_time,
       values->>'type' as "type"    
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from cards;



----card_readings
drop table if exists temp_json;
create temporary table temp_json (values text) on commit drop;
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\card_readings.json';

DELETE FROM card_readings;
insert into card_readings (record_id, turnstile_id, card_id, reading_time)
select (values->>'record_id')::int as record_id,
       (values->>'turnstile_id')::int as turnstile_id,
       (values->>'card_id')::int as card_id,
       (values->>'reading_time')::int as reading_time
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from card_readings;




----turnstiles
drop table if exists temp_json;
create temporary table temp_json (values text) on commit drop;
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\turnstiles.json';

DELETE FROM turnstiles;
insert into turnstiles (turnstile_id, lift_id, is_open)
select 
       (values->>'turnstile_id')::int as turnstile_id,
       (values->>'lift_id')::int as lift_id,
       (values->>'is_open')::bool as is_open
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from turnstiles;



----messages
drop table if exists temp_json;
create temporary table temp_json (values text) on commit drop;
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\messages.json';

DELETE FROM messages;
insert into messages (message_id, sender_id, checked_by_id, "text")
select 
       (values->>'message_id')::int as message_id,
       (values->>'sender_id')::int as sender_id,
       (values->>'checked_by_id')::int as checked_by_id,
       values->>'text' as "text"    
       
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from messages;



----lifts
drop table if exists temp_json;
create temporary table temp_json (values text) on commit drop;
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\lifts.json';

DELETE FROM lifts;
insert into lifts (lift_id, lift_name, is_open, seats_amount, lifting_time, queue_time)
select 
       (values->>'lift_id')::int as lift_id,
       values->>'lift_name' as lift_name,
       (values->>'is_open')::bool as is_open,
       (values->>'seats_amount')::int as seats_amount,
       (values->>'lifting_time')::int as lifting_time,
       (values->>'queue_time')::int as queue_time
       
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from lifts;



----slopes
drop table if exists temp_json;
create temporary table temp_json (values text) on commit drop;
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\slopes.json';

DELETE FROM slopes;
insert into slopes (slope_id, slope_name, is_open, difficulty_level)
select 
       (values->>'slope_id')::int as slope_id,
       values->>'slope_name' as slope_name,
       (values->>'is_open')::bool as is_open,
       (values->>'difficulty_level')::int as difficulty_level
       
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from slopes;




----lifts_slopes
drop table if exists temp_json;
create temporary table temp_json (values text) on commit drop;
copy temp_json from 'C:\BMSTU_6sem_software_design\src\tarantool\app\json_data\lifts_slopes.json';

DELETE FROM lifts_slopes;
insert into lifts_slopes (record_id, lift_id, slope_id)
select 
       (values->>'record_id')::int as record_id,
       (values->>'lift_id')::int as lift_id,
       (values->>'slope_id')::int as slope_id
       
from   (
           select json_array_elements(replace(values,'\','\\')::json) as values 
           from   temp_json
       ) a; 
select * from lifts_slopes;
