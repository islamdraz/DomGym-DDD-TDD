namespace DomeGym.Domain;

public class Participant
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new List<Guid>();

    public Participant(Guid userId, Guid? id)
    {
        _userId = userId;
        this._id = id ?? Guid.NewGuid();
    }

    public Guid Id
    {
        get
        {
            return this._id;

        }
    }
}