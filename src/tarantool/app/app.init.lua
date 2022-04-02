#!/usr/bin/env tarantool


--- роли


print('here!')

local function init()



	print('in init!')
	--
	--box.schema.user.create('ski_admin', {if_not_exists = true}, {password = 'Tty454r293300'})
	--box.schema.user.passwd('ski_admin', 'Tty454r293300')
	--box.schema.user.grant('ski_admin', 'read,write,execute,create,alter,drop', 'universe')

	-- lsof -i :3301
	-- удаление
	-- connection.call('box.space.tester:drop', ())
	-- connection.flush_schema()


	box.space.lifts_slopes:drop()
	box.space.slopes:drop()
	box.space.lifts:drop()
	box.space.turnstiles:drop()
	box.space.card_readings:drop()
	box.space.cards:drop()
	box.space.users:drop()
	box.space.messages:drop()

	--- users
	users = box.schema.space.create('users', {field_count=5})
	users:format({
		{name = 'user_id', type = 'unsigned'},
		{name = 'card_id', type = 'unsigned'},
		{name = 'user_email', type = 'string'},
		{name = 'password', type = 'string'},
		{name = 'permissions', type = 'unsigned'}
	})
	users:create_index('primary')
	print('users created!')
	
	--- cards
	cards = box.schema.space.create('cards', {field_count=3})
	cards:format({
		{name = 'card_id', type = 'unsigned'},
		{name = 'activation_time', type = 'unsigned'},
		{name = 'type', type = 'string'},
	})
	cards:create_index('primary')
	print('cards created!')
	
	--- card_readings
	card_readings = box.schema.space.create('card_readings', {field_count=4})
	card_readings:format({
		{name = 'record_id', type = 'unsigned'},
		{name = 'turnstile_id', type = 'unsigned'},
		{name = 'card_id', type = 'unsigned'},
		{name = 'reading_time', type = 'unsigned'},
	})
	card_readings:create_index('primary')
	card_readings:create_index('index_turnstile', {unique = false, parts = {'turnstile_id'}})
	print('card_readings created!')
	
	--- turnstiles
	turnstiles = box.schema.space.create('turnstiles', {field_count=3})
	turnstiles:format({
		{name = 'turnstile_id', type = 'unsigned'},
		{name = 'lift_id', type = 'unsigned'},
		{name = 'is_open', type = 'boolean'}
	})
	turnstiles:create_index('primary')
	turnstiles:create_index('index_lift_id', {unique = false, parts = {'lift_id'}})
	print('turnstiles created!')
	
	--- lifts
	lifts = box.schema.space.create('lifts', {field_count=6})
	lifts:format({
		{name = 'lift_id', type = 'unsigned'},
		{name = 'lift_name', type = 'string'},
		{name = 'is_open', type = 'boolean'},
		{name = 'seats_amount', type = 'unsigned'},
		{name = 'lifting_time', type = 'unsigned'},
		{name = 'queue_time', type = 'unsigned'},
	})
	lifts:create_index('primary')
	lifts:create_index('index_name', {parts = {'lift_name'}})
	print('lifts created!')


	--- slopes
	slopes = box.schema.space.create('slopes', {field_count=4})
	slopes:format({
		{name = 'slope_id', type = 'unsigned'},
		{name = 'slope_name', type = 'string'},
		{name = 'is_open', type = 'boolean'},
		{name = 'difficulty_level', type = 'unsigned'}
	})
	slopes:create_index('primary')
	slopes:create_index('index_name', {parts = {'slope_name'}})
	print('slopes created!')


	--- lifts_slopes
	lifts_slopes = box.schema.space.create('lifts_slopes', {field_count=3})
	lifts_slopes:format({
		{name = 'record_id', type = 'unsigned'},
		{name = 'lift_id', type = 'unsigned'},
		{name = 'slope_id', type = 'unsigned'},
	})
	lifts_slopes:create_index('primary')
	lifts_slopes:create_index('index_lift_id', {unique = false, parts = {'lift_id'}})
	lifts_slopes:create_index('index_slope_id', {unique = false, parts = {'slope_id'}})
	print('lifts_slopes created!')
	
	
	--- messages
	messages = box.schema.space.create('messages', {field_count=4})
	messages:format({
		{name = 'message_id', type = 'unsigned'},
		{name = 'sender_id', type = 'unsigned'},
		{name = 'checked_by_id', type = 'unsigned'},
		{name = 'text', type = 'string'},
	})
	messages:create_index('primary')
	print('messages created!')

	
end











--function lifts_to_slope(slope_id)
--	local result = {}
--   for k,v in box.space.lifts_slopes:pairs() do
--       box.space.users:update(v[1], {{'+', 5, 1}}) 
--    end
--end

function dump(o)
   if type(o) == 'table' then
      local s = '{ '
      for k,v in pairs(o) do
         if type(k) ~= 'number' then k = '"'..k..'"' end
         s = s .. '['..k..'] = ' .. dump(v) .. ','
      end
      return s .. '} '
   else
      return tostring(o)
   end
end

















------------------------------------------------------------------fill tables
json=require('json')
msgpack=require('msgpack')
json_data_dir = "/usr/local/share/tarantool/json_data/"


local function load_users_data()
    local cur_space = box.space.users
	local cur_filename = "users.json"
	
	local file = io.open(json_data_dir .. cur_filename, "r")
	a = file:read("*a")
	file:close()

	cur_table = json.decode(a)
	
	for k,v in pairs(cur_table) do
		cur_space:insert{v["user_id"], v["card_id"], v["user_email"], v["password"], v["permissions"]}
    end
end

local function load_cards_data()
    local cur_space = box.space.cards
	local cur_filename = "cards.json"
	
	local file = io.open(json_data_dir .. cur_filename, "r")
	a = file:read("*a")
	file:close()

	cur_table = json.decode(a)
	
	for k,v in pairs(cur_table) do
		cur_space:insert{v["card_id"], v["activation_time"], v["type"]}
    end
end

local function load_turnstiles_data()
    local cur_space = box.space.turnstiles
	local cur_filename = "turnstiles.json"
	
	local file = io.open(json_data_dir .. cur_filename, "r")
	a = file:read("*a")
	file:close()

	cur_table = json.decode(a)
	
	for k,v in pairs(cur_table) do
		cur_space:insert{v["turnstile_id"], v["lift_id"], v["is_open"]}
    end
end

local function load_lifts_data()
    local cur_space = box.space.lifts
	local cur_filename = "lifts.json"
	
	local file = io.open(json_data_dir .. cur_filename, "r")
	a = file:read("*a")
	file:close()

	cur_table = json.decode(a)
	
	for k,v in pairs(cur_table) do
		cur_space:insert{v["lift_id"], v["lift_name"], v["is_open"], v["seats_amount"], v["lifting_time"], v["queue_time"]}
    end
end


local function load_slopes_data()
    local cur_space = box.space.slopes
	local cur_filename = "slopes.json"
	
	local file = io.open(json_data_dir .. cur_filename, "r")
	a = file:read("*a")
	file:close()

	cur_table = json.decode(a)
	
	for k,v in pairs(cur_table) do
		cur_space:insert{v["slope_id"], v["slope_name"], v["is_open"], v["difficulty_level"]}
    end
end

local function load_lifts_slopes_data()
    local cur_space = box.space.lifts_slopes
	local cur_filename = "lifts_slopes.json"
	
	local file = io.open(json_data_dir .. cur_filename, "r")
	a = file:read("*a")
	file:close()

	cur_table = json.decode(a)
	
	for k,v in pairs(cur_table) do
		cur_space:insert{v["record_id"], v["lift_id"], v["slope_id"]}
    end
end

local function load_messages_data()
    local cur_space = box.space.messages
	local cur_filename = "messages.json"
	
	local file = io.open(json_data_dir .. cur_filename, "r")
	a = file:read("*a")
	file:close()

	cur_table = json.decode(a)
	
	for k,v in pairs(cur_table) do
		cur_space:insert{v["message_id"], v["sender_id"], v["checked_by_id"], v["text"]}
    end
end


local function load__data()
	load_users_data()
	load_cards_data()
	load_turnstiles_data()
	load_lifts_data()
	load_slopes_data()
	load_lifts_slopes_data()
end

--------------------------------------------------------------------------------










box.cfg {
   background = false,
   listen = 3301
}

init()
--load__data()

