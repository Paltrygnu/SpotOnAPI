
using ErrorOr;

public interface ICollarService
{
    ErrorOr<Created> CreateCollar(Collar collar);
    ErrorOr<Collar> GetCollar(Guid id);
    ErrorOr<UpsertedCollar> UpsertCollar(Collar collar);
    ErrorOr<Deleted> DeleteCollar(Guid id);
}