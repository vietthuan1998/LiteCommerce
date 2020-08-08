using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// Định nghĩa danh sách các Role của user
    /// </summary>
    public class WebUserRoles
    {
        /// <summary>
        /// Không xác định
        /// </summary>
        public const string ANONYMOUS = "anonymous";
        /// <summary>
        /// Nhân viên
        /// </summary>
        public const string STAFF = "staff";
        /// <summary>
        /// Quản trị hệ thống
        /// </summary>
        public const string ADMINISTRATOR = "administrator";
        /// <summary>
        /// Nhân viên bán hàng
        /// </summary>
        public const string SALEMAN = "Sale Manager";
        /// <summary>
        /// Nhân viên quản trị dữ liệu
        /// </summary>
        public const string DATAMAN = "Data Manager";
        /// <summary>
        /// Nhân viên quản trị tài khoản
        /// </summary>
        public const string ACCOUNTMAN = "Account Manager";
    }
}