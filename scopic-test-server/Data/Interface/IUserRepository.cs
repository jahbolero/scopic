using scopic_test_server.DTO;

namespace scopic_test_server.Interface
{
    public interface IUserRepository
    {
        UserDto GetUser();
    }
}