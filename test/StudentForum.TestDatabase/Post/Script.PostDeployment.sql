IF NOT EXISTS (SELECT 1 FROM [AspNetRoles])
insert into dbo.[AspNetRoles]
	values 
		('c9728caf-cd6b-400a-95cb-e1d5536642ed', 'Admin', 'ADMIN', '2cd2b578-9b79-4ab4-8ef8-e874f17eecaf'),
		('f87b2d2c-d124-47ad-be5b-8ecddde4a5d3', 'User', 'USER', '3658b410-c73d-492d-a6cc-2cb18e67e902')

IF NOT EXISTS (SELECT 1 FROM [AspNetUsers])
insert into dbo.[AspNetUsers]
	values 
		('6bae6274-b01d-45c0-8728-3ad95b3ca565',
		'Yaraslau',
		'Pukhalski', 
		null,
		'admin@test.com',
		'ADMIN@TEST.COM', 
		'admin@test.com', 
		'ADMIN@TEST.COM',
		1, 
		'AQAAAAEAACcQAAAAEIhvPHmWf3YjKD05ATeVdglWZQlN7HFQQvV2fWDKmOiMno4jsdlQEIZKGqKGFckyUg==',
		'ET3JP3PVKFNFUVTT3DBGSP25FM4JZ3CW',
		'e3d2788d-8ea0-4751-989f-c471727ccf94', 
		'375 29 78 49 159',
		1,
		0,
		'2020-01-01',
		1,
		0),
		('da15aa3a-bc1d-43d3-8ec3-c36cd5fb039a',
		'User',
		'User',
		null,
		'user@test.com',
		'USER@TEST.COM',
		'user@test.com',
		'USER@TEST.COM',
		1,
		'AQAAAAEAACcQAAAAEFnDjHKSTDRQaesnK/B9dTgtB+15IbB2bMEX46mYmTJp5nCPZO5A022xcW0JtQWL2A==',
		'FLVH4NEPACGCKK472PUR3AMHIE6QF6HK',
		'442b4855-19f1-4e4c-95c3-094e9467bd42',
		'375 29 582 74 29',
		1,
		0,
		'2020-01-01',
		1,
		0)

IF NOT EXISTS (SELECT 1 FROM [AspNetUserRoles])
insert into dbo.[AspNetUserRoles]
	values 
		('6bae6274-b01d-45c0-8728-3ad95b3ca565', 'c9728caf-cd6b-400a-95cb-e1d5536642ed'),
		('6bae6274-b01d-45c0-8728-3ad95b3ca565', 'f87b2d2c-d124-47ad-be5b-8ecddde4a5d3'),
		('da15aa3a-bc1d-43d3-8ec3-c36cd5fb039a', 'f87b2d2c-d124-47ad-be5b-8ecddde4a5d3')
