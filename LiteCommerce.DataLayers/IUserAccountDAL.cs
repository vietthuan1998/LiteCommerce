using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// kiểm tra username và password có hợp lệ ko
        /// hợp lệ trả về thông tin
        /// ko thì trả về null
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);
        /// <summary>
        /// Đổi password, forgotpassword, edit profile
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string newPassword);
        bool FindEmail(string email);
        bool ResetPassword(string email,string password);
    }
}
