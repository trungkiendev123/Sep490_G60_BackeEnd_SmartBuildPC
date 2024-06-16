using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.FileSystemGlobbing;
using Sep490_G60_Backend_SmartBuildPC.Models;
using System.Text.RegularExpressions;
using Sep490_G60_Backend_SmartBuildPC.Requests;
using System.Numerics;
using System.Linq;

namespace Sep490_G60_Backend_SmartBuildPC.Validation
{
    public class ValidateAccount
    {
        private readonly SMARTPCContext _context;
        public ValidateAccount(SMARTPCContext context)
        {
            _context = context;
        }
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            string regex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex pattern = new Regex(regex);
            return pattern.IsMatch(email);
        }
        public bool isExistEmail(string email)
        {
            try
            {
                var account = _context.Accounts.FirstOrDefault(x => x.Email == email);
                if (account != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool isValidLength(string input)
        {
            try
            {
                if (input.Length > 0 && input.Length < 50)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool isValidPhone(string phone)
        {
            try
            {
                string regex = @"^\d{10}$";
                Regex pattern = new Regex(regex);
                return pattern.IsMatch(phone);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool isValidPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return false;
                }

                string regex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$";
                Regex pattern = new Regex(regex);
                return pattern.IsMatch(password);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<string> validateAccount(RegisterFormRequest request)
        {
            List<string> errors = new List<string>();
            try
            {
                if (String.IsNullOrEmpty(request.Address))
                {
                    errors.Add("Address must not null or empty");
                }
                if(String.IsNullOrEmpty(request.Username) || request.Username.Length > 50) {
                    errors.Add("Username must not null,empty and < 50 characters");
                }
                if (request.Email.Length > 50 || !IsValidEmail(request.Email))
                {
                    errors.Add("Email must in email format and < 50 characters");
                }
                if (String.IsNullOrEmpty(request.FullName) || request.FullName.Length > 50)
                {
                    errors.Add("FullName must not null,empty and < 50 characters");
                }
                if (isExistEmail(request.Email))
                {
                    errors.Add("This email is exist");
                }
                if(!isValidPhone(request.Phone))
                {
                    errors.Add("Phone must contains only 10 digit characters");
                }
                if (!isValidPassword(request.Password))
                {
                    errors.Add("Password have 8 -> 20 characters,at least 1 digit,1 uppercase,1 lowercase,1 special");
                }

            }
            catch (Exception e)
            {
                errors.Add(e.Message);
            }
            return errors;
        }
        public List<string> validateAddAccount(AddAccountRequest request)
        {
            List<string> errors = new List<string>();
            try
            {
                if (String.IsNullOrEmpty(request.Username) || request.Username.Length > 50)
                {
                    errors.Add("Username must not null,empty and < 50 characters");
                }
                if (request.Email.Length > 50 || !IsValidEmail(request.Email))
                {
                    errors.Add("Email must in email format and < 50 characters");
                }
                if (request.AccounType.Equals("STAFF"))
                {
                    if (String.IsNullOrEmpty(request.FullName) || request.FullName.Length > 50)
                    {
                        errors.Add("FullName must not null,empty and < 50 characters");
                    }
                    if(request.StoreID == null)
                    {
                        errors.Add("StoreID must not null,empty");
                    }
                    var ids = _context.Stores.ToList().Select(x => x.StoreId);
                    if (!ids.Contains((int)request.StoreID))
                    {
                        errors.Add("StoreID not exist");
                    }
                }
               
                if (isExistEmail(request.Email))
                {
                    errors.Add("This email is exist");
                }
                if (!isValidPassword(request.Password))
                {
                    errors.Add("Password have 8 -> 20 characters,at least 1 digit,1 uppercase,1 lowercase,1 special");
                }

            }
            catch (Exception e)
            {
                errors.Add(e.Message);
            }
            return errors;
        }
    }
}
