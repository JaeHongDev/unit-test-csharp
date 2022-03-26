using System;
using NUnit.Framework;

namespace NunitTdd
{
    [TestFixture]
    public class VirtualClass
    {
        public VirtualClass()
        {
            
        }
        [Test]
        public void VirtualKeyworkTest()
        {
            UserFactory userFactory = new UserFactory();
            Console.WriteLine(userFactory.User.IsUser("userId"));

        }
    }

    public class User : IUser
    {
        public bool IsUser(string id)
        {
            return true;
        }
    }
    public class UserFactory
    {
        public virtual IUser User => new User();
    }
    public interface IUser
    {
        bool IsUser(string id);
    }

    public class TestUser : UserFactory
    {
        public TestUser()
        {
        }
    }
}
