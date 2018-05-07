using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPClassLibrary
{
    public interface IUserOperation
    {
        void Test(User oUser);
        void Test2(User oUser, User oUser2);

    }
}
