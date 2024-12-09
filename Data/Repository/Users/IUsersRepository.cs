namespace api.Data.Repository.Users;

public interface IUsersRepository
{
    public Task<object> GetLastMonths(string adminId);
}
