CREATE TABLE drivers (
	id uuid NOT NULL,
	name varchar NOT NULL,
	surname varchar NOT NULL,
	patronymic varchar NULL,
	gender int4 NOT NULL,
	rightscategories int4[] NOT NULL,
	age int4 NOT NULL,
	experience int4 NOT NULL,
	transportvehicleid uuid NOT NULL,
	payment float8 NOT NULL,
	createddatetimeutc timestamp NOT NULL,
	modifieddatetimeutc timestamp NULL,
	isremoved bool NOT NULL,
	CONSTRAINT drivers_pk PRIMARY KEY (id),
	CONSTRAINT drivers_transport_vehicles_fk FOREIGN KEY (transportvehicleid) REFERENCES transport_vehicles (id)
);