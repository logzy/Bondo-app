using System.Text.RegularExpressions;
using Bondo.Shared;
namespace Bondo.Application;
public class ValidateUserInfo
{
    public bool ValidEmail { get; set; } = true;
        public bool ValidPhone { get; set; } = true;
        public bool ValidPassword { get; set; } = true;
        public bool ValidName { get; set; } = true;

        public bool State { get {

                return ValidEmail && ValidPhone && ValidPassword && ValidName;
            } }
        public string Errors { get; set; }
        public ValidateUserInfo IsValidEmail(string email)
        {
            Regex regex = new Regex("/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,6})*$/");
            if (!regex.Match(email).Success)
            {
                ValidEmail = false;
                Errors += $"Invalid Email\n";
            }
            return this;
        }

        public ValidateUserInfo IsValidPhone(string phone)
        {
            Regex regex = new Regex("/^(?:(?:\\(?(?:00|\\+)([1-4]\\d\\d|[1-9]\\d?)\\)?)?[\\-\\.\\ \\\\\\/]?)?((?:\\(?\\d{1,}\\)?[\\-\\.\\ \\\\\\/]?){0,})(?:[\\-\\.\\ \\\\\\/]?(?:#|ext\\.?|extension|x)[\\-\\.\\ \\\\\\/]?(\\d+))?$/");

            if (!regex.Match(phone).Success)
            {
                ValidPhone = false;
                Errors += $"Invalid Phone number\n";
            }
            return this;
        }

        public ValidateUserInfo IsValidPassword(string password)
        {
            Regex regex = new Regex("/(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}/ \r\n");

            if (!regex.Match(password).Success)
            { 
                ValidPassword = false;
                Errors += $"Invalid Password\n";
            }
            return this;
        }
        public ValidateUserInfo IsValidName(string name)
        {
            Regex regex = new Regex("/^[a-zA-Z-]*$/");

            if (!regex.Match(name).Success)
            {
                ValidPassword = false;
                Errors += $"Invalid Name format\n";
            }
            return this;
        }


        public ValidationResult Result()
        {
            //bool modelIsValid = ValidEmail && ValidPhone && ValidPassword;
            return new ValidationResult(this.State, this.Errors);
        }
}
