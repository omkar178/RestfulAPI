using RestfulAPI.Model;

namespace RestfulAPI.Repository.IRepository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IUserRepository

    {
        bool IsUniqueUser(string username);
        Users Authenticate(string username,string password);    
        Users register (string username,string password);
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
