using AuthManagementService.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AuthManagementService.Services
{
    public interface IUserService
    {
        User AuthenticateUser(string firstname, string password);
    }
    public class UserService : IUserService
    {
        private readonly List<User> Users = new List<User>
        {
            new User
            {
                Id=1,
                Name="Okan",
                Surname="Karadağ",
                Username = "okank",
                Password="Test", //Normal şartlar altında db de hashli olarak tutulmalı
            }
        };
        public User AuthenticateUser(string username, string password)
        {
            return Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
