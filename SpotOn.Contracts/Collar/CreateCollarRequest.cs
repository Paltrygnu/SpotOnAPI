
using System.ComponentModel.DataAnnotations;

public record CreateCollarRequest(
    Guid Collar_ID,
    int Longitude,
    int Latitude,
    DateTime TimeStamp
);