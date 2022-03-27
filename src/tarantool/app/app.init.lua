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


	box.space.lifts_slopes:drop()
	box.space.slopes:drop()
	box.space.lifts:drop()
	box.space.turnstiles:drop()
	box.space.card_readings:drop()
	box.space.cards:drop()
	box.space.users:drop()

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

	
end

-- настроить базу данных
box.cfg {
   background = false,
   listen = 3301
}

init()
--box.once('init', init)

