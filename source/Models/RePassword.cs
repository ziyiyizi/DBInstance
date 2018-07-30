using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class RePassword
    {

        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "旧密码")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{2}到{1}个字符")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "新密码")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "{2}到{1}个字符")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Compare("NewPassword", ErrorMessage = "两次输入的密码不一致")]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}