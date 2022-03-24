#!/usr/bin/env tarantool

--[[
local log = require('log')
local uuid = require('uuid')

local function init()
    box.schema.user.create('operator', {
        password = '123123', 
        if_not_exists = true
    })

    box.schema.user.grant('operator', 'read,write,execute', 
    'universe', nil, {
        if_not_exists = true
    })

    local users_space = box.schema.space.create('users', {
        if_not_exists = true
    })

    users_space:create_index('primary_id', {
        if_not_exists = true,
        type = 'HASH',
        unique = true,
        parts = {1, 'STRING'}
    })

    users_space:create_index('secondary_login', {
        if_not_exists = true,
        type = 'HASH',
        unique = true,
        parts = {3, 'STRING'}
    })

    users_space:create_index('secondary_rating', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {5, 'INT'}
    })
end

local function load_data()
    local users_space = box.space.users

    users_space:insert{uuid.str(), 
    'Ivan Ivanov', 'ivanov', 
    'iivanov@domain.com', 10}

    users_space:insert{uuid.str(), 
    'Petr Petrov', 'petrov', 
    'ppetrov@domain.com', 15}

    users_space:insert{uuid.str(), 
    'Vasily Sidorov', 'sidorov', 
    'vsidorov@domain.com', 20}
end

box.cfg
{
    pid_file = nil,
    background = false,
    log_level = 5
}

box.once('init', init)
box.once('load_data', load_data)
]]



--- индексы
--- ограничения
--- внешние поля
--- дата в карте



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




	----- users_groups
	--box.space.users_groups:drop()

	--users_groups = box.schema.space.create('users_groups', {id=1, field_count=3})
	--users_groups:format({
	--	{name = 'group_id', type = 'unsigned'},
	--	{name = 'group_name', type = 'string'},
	--	{name = 'access_rights', type = 'string'} --может массив
	--})
	--users_groups:create_index('primary', {type = 'hash', parts = {'group_id'}})
	--print('users_groups created!')





	--- slopes
	box.space.slopes:drop()
	slopes = box.schema.space.create('slopes', {id=3, field_count=4})
	slopes:format({
		{name = 'slope_id', type = 'unsigned'},
		{name = 'slope_name', type = 'string'},
		{name = 'is_open', type = 'boolean'},
		{name = 'difficulty_level', type = 'unsigned'}
	})
	slopes:create_index('primary')
	slopes:create_index('index_name', {unique = true, parts = {{field = 2, type = 'string'}}})
	print('slopes created!')




	--- lifts
	box.space.lifts:drop()
	lifts = box.schema.space.create('lifts', {id=4, field_count=6})
	lifts:format({
		{name = 'lift_id', type = 'unsigned'},
		{name = 'lift_name', type = 'string'},
		{name = 'is_open', type = 'boolean'},
		{name = 'seats_amount', type = 'unsigned'},
		{name = 'lifting_time', type = 'unsigned'},
		{name = 'queue_time', type = 'unsigned'},
	})
	print("imhere")
	lifts:create_index('primary')
	lifts:create_index('index_name', {unique = true, parts = {{field = 2, type = 'string'}}})
	print('lifts created!')



	--- lifts_slopes
	box.space.lifts_slopes:drop()
	lifts_slopes = box.schema.space.create('lifts_slopes', {id=5, field_count=3})
	lifts_slopes:format({
		{name = 'record_id', type = 'unsigned'},
		{name = 'lift_id', type = 'unsigned'},
		{name = 'slope_id', type = 'unsigned'},
	})
	lifts_slopes:create_index('primary', {type = 'hash', parts = {'record_id'}})
	print('lifts_slopes created!')


	--- turnstiles
	box.space.turnstiles:drop()
	turnstiles = box.schema.space.create('turnstiles', {id=6, field_count=3})
	turnstiles:format({
		{name = 'turnstile_id', type = 'unsigned'},
		{name = 'lift_id', type = 'unsigned'},
		{name = 'is_open', type = 'boolean'}
	})
	turnstiles:create_index('primary')
	turnstiles:create_index('index_lift_id', {parts = {{field = 'lift_id', type = 'unsigned'}}})
	print('turnstiles created!')



	--- cards
	box.space.cards:drop()
	cards = box.schema.space.create('cards', {id=7, field_count=3})
	cards:format({
		{name = 'card_id', type = 'unsigned'},
		{name = 'date_of_registration', type = 'string'},
		{name = 'card_type', type = 'string'},
	})
	cards:create_index('primary', {type = 'hash', parts = {'card_id'}})
	print('cards created!')
end

-- настроить базу данных
box.cfg {
   background = false,
   listen = 3301
}

init()
--box.once('init', init)


