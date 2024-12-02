using Microsoft.EntityFrameworkCore;

namespace api.Data.Repository.Users;

public class UsersRepository(DataAppContext _context) : IUsersRepository
{
    public async Task<object> GetLastMonths(string adminId)
    {
        return await _context
            .Users.Where(u => u.Id != adminId)
            .Join(
                _context.UserRoles,
                u => u.Id,
                ur => ur.UserId,
                (u, ur) => new { User = u, UserRole = ur }
            )
            .Join(
                _context.Roles,
                ur => ur.UserRole.RoleId,
                r => r.Id,
                (ur, r) => new { UserRole = ur, Role = r }
            )
            .GroupBy(uv => new { uv.UserRole.User.UserName, uv.UserRole.User.Email })
            .Select(r => new
            {
                user = r.Select(s => new
                {
                    s.UserRole.User.Id,
                    s.UserRole.User.Picture,
                    s.UserRole.User.Email,
                    s.UserRole.User.FirstName,
                    s.UserRole.User.LastName,
                    s.UserRole.User.UserName,
                }),
                Role = string.Join(",", r.Select(c => c.Role.Name).ToArray()),
            })
            .ToListAsync();
    }
}
