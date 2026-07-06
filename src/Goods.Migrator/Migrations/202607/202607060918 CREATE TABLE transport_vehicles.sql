CREATE TABLE transport_vehicles (
	id uuid NOT NULL,
	type int4 NOT NULL,
	name varchar NOT NULL,
	statenumber varchar NOT NULL,
	averagespeed float8 NOT NULL,
	fuelconsumption float8 NOT NULL,
	createddatetimeutc timestamp NOT NULL,
	modifieddatetimeutc timestamp NULL,
	isremoved bool NOT NULL,
	CONSTRAINT transport_vehicles_pk PRIMARY KEY (id)
);