using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// lưu thông tin liên quan đến tài khoản đăng nhập
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// ID của tài khoản đăng nhập vào hệ thống
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// Tên đầy đủ của user
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Tên file ảnh của user
        /// </summary>
        public string Photo { get; set; }
        public string Title { get; set; }
        public string GroupName { get; set; }
    }
}
