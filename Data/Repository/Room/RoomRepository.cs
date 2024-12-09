using api.Models.Room;

namespace api.Data.Repository.Room
{
    public class RoomRepository(DataAppContext context)
        : Repository<RoomModel>(context),
            IRoomRepository { }
}
