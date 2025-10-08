
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace dotnet_sp_api.Models.DTOs
{

    /// <summary>
    /// Holds the log in data.
    /// </summary>
    public class Login
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Holds the User info.
    /// </summary>
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MemberID { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiredDate { get; set; }
        public string CurrentStatus { get; set; } = string.Empty;
    }

    /// <summary>
    ///  Stores registered user info.
    /// </summary>
    public class Register
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
        public string Month { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string ProfileType { get; set; } = string.Empty;

    }

    /// <summary>
    /// Stores new registered user info.
    /// </summary>
    public class NewRegisteredUser
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stores code and name for forgot password.
    /// </summary>
    public class CodeAndNameForgotPwdModel
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    public class RefreshToken
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime DateExpired { get; set; }
    }

    /// <summary>
    /// holds validated new registered user data 
    /// </summary>
    public class ValidateNewRegisteredUser
    {
        public string MemberId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}

