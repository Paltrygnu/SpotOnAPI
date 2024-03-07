using ErrorOr;

public static class Errors
{
    public static class Collar
    {
        public static Error InvalidLatitude => Error.NotFound(
            code: "Collar.InvalidLatitude",
            description: $"Latitude value must be at least -90"+
            $" and at most 90."
        );

        public static Error InvalidLongitude => Error.NotFound(
            code: "Collar.InvalidLongitude",
            description: $"Longitude value must be at least -180"+
            $" and at most 180."
        );

        public static Error NotFound => Error.NotFound(
            code: "Collar.NotFound",
            description: "Collar not found"
        );
    }   
}