using ErrorOr;
public class CollarService : ICollarService
{

    private static readonly Dictionary<Guid, Collar> _coller = new();

    public ErrorOr<Created> CreateCollar(Collar collar)
    {
        _coller.Add(collar.Collar_ID, collar);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteCollar(Guid id)
    {
        _coller.Remove(id);
    
        return Result.Deleted;
    }

    public ErrorOr<Collar> GetCollar(Guid id)
    {
        if(_coller.TryGetValue(id, out var collar))
        {
            return collar;
        }
        return Errors.Collar.NotFound;
    }

    public ErrorOr<UpsertedCollar> UpsertCollar(Collar collar)
    {
        var IsNewlyCreated = !_coller.ContainsKey(collar.Collar_ID);
        _coller[collar.Collar_ID] = collar;

        return new UpsertedCollar(IsNewlyCreated);
    }
}