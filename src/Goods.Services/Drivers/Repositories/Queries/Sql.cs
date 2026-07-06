namespace Goods.Services.Drivers.Repositories.Queries;

internal static class Sql
{
    internal static String Drivers_Save =>
        """
            INSERT INTO drivers (
                id,
                name,
                surname,
                patronymic,
                gender,
                rightscategories,
                age,
                experience,
                transportvehicleid,
                payment,
                createddatetimeutc,
                isremoved
            )
            VALUES (
                @id,
                @name,
                @surname,
                @patronymic,
                @gender,
                @rightsCategories,
                @age,
                @experience,
                @transportVehicleId,
                @payment,
                @createdDateTimeUtc,
                @isRemoved
            )
        	ON CONFLICT (id) DO UPDATE SET
        	    name = @name,
        	    surname = @surname,
        	    patronymic = @patronymic,
        	    gender = @gender,
        	    rightscategories = @rightsCategories,
        	    age = @age,
        	    experience = @experience,
        	    transportvehicleid = @transportVehicleId,
        	    payment = @payment,
        	    modifieddatetimeutc = @modifiedDateTimeUtc
        """;

    internal static String Drivers_GetById =>
        """
            SELECT 
                d.id,
                d.name,
                d.surname,
                d.patronymic,
                d.gender,
                d.rightscategories,
                d.age,
                d.experience,
                d.transportvehicleid,
                d.payment,
                d.createddatetimeutc,
                d.modifieddatetimeutc,
                d.isremoved,
                v.id as vehicle_id,
                v.type as vehicle_type,
                v.name as vehicle_name,
                v.statenumber as vehicle_statenumber,
                v.averagespeed as vehicle_averagespeed,
                v.fuelconsumption as vehicle_fuelconsumption,
                v.createddatetimeutc as vehicle_createddatetimeutc,
                v.modifieddatetimeutc as vehicle_modifieddatetimeutc,
                v.isremoved as vehicle_isremoved
            FROM drivers d
            JOIN transport_vehicles v ON v.id = d.transportvehicleid
            WHERE d.id = @id;
        """;

    internal static String Drivers_GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count,
                d.id,
                d.name,
                d.surname,
                d.patronymic,
                d.gender,
                d.rightscategories,
                d.age,
                d.experience,
                d.transportvehicleid,
                d.payment,
                d.createddatetimeutc,
                d.modifieddatetimeutc,
                d.isremoved,
                v.id as vehicle_id,
                v.type as vehicle_type,
                v.name as vehicle_name,
                v.statenumber as vehicle_statenumber,
                v.averagespeed as vehicle_averagespeed,
                v.fuelconsumption as vehicle_fuelconsumption,
                v.createddatetimeutc as vehicle_createddatetimeutc,
                v.modifieddatetimeutc as vehicle_modifieddatetimeutc,
                v.isremoved as vehicle_isremoved
            FROM drivers d
            JOIN transport_vehicles v ON v.id = d.transportvehicleid
            WHERE NOT d.isremoved 
            ORDER BY d.createddatetimeutc DESC 
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String Drivers_Remove =>
        """
        	UPDATE drivers
        	SET 
                isremoved = TRUE,
        		modifieddatetimeutc = @modifiedDateTimeUtc
        	WHERE id = @id
        """;
}