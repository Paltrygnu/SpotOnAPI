
public record UpsertCollarRequest(
    Guid Collar_ID,
    int Longitude,
    int Latitude,
    DateTime Timestamp
);