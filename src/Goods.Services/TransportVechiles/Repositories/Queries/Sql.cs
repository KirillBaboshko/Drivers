namespace Goods.Services.TransportVehicles.Repositories.Queries;

internal static class Sql
{
    internal static String TransportVechiles_Save =>
        """
            INSERT INTO transport_vehicles (
                id,
                type,
                name,
                statenumber,
                averagespeed,
                fuelconsumption,
                createddatetimeutc,
                isremoved
            )
            VALUES (
                @id,
                @type,
                @name,
                @stateNumber,
                @averageSpeed,
                @fuelConsumption,
                @createdDateTimeUtc,
                @isRemoved
            )
        	ON CONFLICT (id) DO UPDATE SET
        	    type = @type,
        	    name = @name,
        	    statenumber = @stateNumber,
        	    averagespeed = @averageSpeed,
        	    fuelconsumption = @fuelConsumption,
        	    modifieddatetimeutc = @modifiedDateTimeUtc
        """;

    internal static String TransportVechiles_GetById =>
        """
            SELECT * 
            FROM transport_vehicles 
            WHERE id = @id;
        """;

    internal static String TransportVechiles_GetByName =>
        """
            SELECT * 
            FROM transport_vehicles 
            WHERE name = @name
            AND NOT isremoved;
        """;

    internal static String TransportVechiles_GetPage =>
        """
            SELECT 
                COUNT(*) OVER() as count, 
                *
            FROM transport_vehicles 
            WHERE NOT isremoved 
            ORDER BY createddatetimeutc DESC 
            OFFSET @offset 
            LIMIT @limit
        """;

    internal static String TransportVechiles_Remove =>
        """
        	UPDATE transport_vehicles
        	SET 
                isremoved = TRUE,
        		modifieddatetimeutc = @modifiedDateTimeUtc
        	WHERE id = @id
        """;
}