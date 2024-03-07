using ErrorOr;

public class Collar
{
    public const int MinLongitude = -180;
    public const int MaxLongitude = 180;
    public const int MinLatitude = -90;
    public const int MaxLatitude = 90;

    public Guid Collar_ID {get;}
    public int Longitude {get;}
    public int Latitude {get;}
    public DateTime TimeStamp {get;}


    private Collar(
        Guid collar_ID,
        int longitude,
        int latitude,
        DateTime timeStamp
    )
    {
        Collar_ID = collar_ID;
        Longitude = longitude;
        Latitude = latitude;
        TimeStamp = timeStamp;
    }

    public static ErrorOr<Collar> Create(
        int Longitude,
        int Latitude,
        DateTime TimeStamp,
        Guid? id = null
    )
    {
        List<Error> errors = new();
        if(Longitude is < MinLongitude or > MaxLongitude)
        {
            errors.Add(Errors.Collar.InvalidLongitude);
        }

        if(Latitude is < MinLatitude or > MaxLatitude)
        {
            errors.Add(Errors.Collar.InvalidLatitude);
        }

        if(errors.Count > 0)
        {
            return errors;
        }

        return new Collar(
        id ?? Guid.NewGuid(),
        Longitude,
        Latitude,
        DateTime.UtcNow
        );
    }
    

    public static ErrorOr<Collar> From(CreateCollarRequest request)
    {
        return Create(
            request.Longitude,
            request.Latitude,
            request.TimeStamp
        );
    }

    public static ErrorOr<Collar> From(Guid id, UpsertCollarRequest request)
    {
        return Create(
            request.Longitude,
            request.Latitude,
            request.Timestamp,
            id
        );
    }
}