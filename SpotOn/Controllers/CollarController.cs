using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace SpotOn.Controllers;

public class CollarController : ApiController
{
    private readonly ICollarService _collarService;

    public CollarController(ICollarService collarService)
    {
        _collarService = collarService;
    }

    [HttpPost()]
    public IActionResult CreateCollar(CreateCollarRequest request)
    {
        ErrorOr<Collar> requestToCollarResult = Collar.From(request);

        if(requestToCollarResult.IsError)
        {
            return Problem(requestToCollarResult.Errors);
        }

        var collar = requestToCollarResult.Value;

        //save to DB
        ErrorOr<Created> createCollarResult =  _collarService.CreateCollar(collar);

        return createCollarResult.Match(
            created => CreatedAtGetCollar(collar),
            errors=> Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetCollar(Guid id)
    {
        ErrorOr<Collar> getCollarResult = _collarService.GetCollar(id);

        return getCollarResult.Match(
            collar => Ok(MapCollarResponse(collar)),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertCollar(Guid id,UpsertCollarRequest request)
    {
        ErrorOr<Collar> requestToCollarResult = Collar.From(id, request);

        if(requestToCollarResult.IsError)
        {
            return Problem(requestToCollarResult.Errors);
        }

        var collar = requestToCollarResult.Value;
        ErrorOr<UpsertedCollar> upsertedCollarResult = _collarService.UpsertCollar(collar);

        return upsertedCollarResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetCollar(collar) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCollar(Guid id)
    {
        ErrorOr<Deleted> deleteCollarResult = _collarService.DeleteCollar(id);
        _collarService.DeleteCollar(id);

        return deleteCollarResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }


//Maping Functions
private static CollarResponse MapCollarResponse(Collar collar)
    {
        return new CollarResponse(
            collar.Collar_ID,
            collar.Longitude,
            collar.Latitude,
            collar.TimeStamp
        );
    }

private CreatedAtActionResult CreatedAtGetCollar(Collar collar)
    {
        return CreatedAtAction(
            actionName: nameof(GetCollar),
            routeValues: new{ id = collar.Collar_ID},
            value: MapCollarResponse(collar)
        );
    }
}
