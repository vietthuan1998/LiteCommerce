using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class CustomerUserAccountDAL:IUserAccountDAL
    {
        private string connectionString;
        public CustomerUserAccountDAL (string connectionString)
        {
            this.connectionString = connectionString;
        }

        public UserAccount Authorize(string username, string password)
        {
            //TODO:aaaaaaaaa
            return new UserAccount()
            {
                UserID = username,
                FullName = "ABC...XYZ",
                Photo = "user2-160x160.jpg"
            };
        }

        public bool ChangePassword(string userName, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool FindEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string email,string password)
        {
            throw new NotImplementedException();
        }
    }
}
