﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Wired_Brain_UserService;

namespace Wired_Brain_Coffee.Services
{
    public class UserService : IUserService
    {
        private readonly IDependencyService _dependencyService;

        public UserService() : this(new DependencyServiceWrapper())
        {
        }

        public UserService(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
        }


        public UserResponse RegisterUser(User user)
        {
            var apiService = _dependencyService.Get<IUserApiService>();

            if (IsUserValid(user))
            {
                return apiService.Register(user);
            }
            else
            {
                return new UserResponse() { UserEntity = user, EntityState = Status.Invalid };
            }

        }

        private bool IsUserValid(User user)
        {
            if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName) && PasswordValid(user.Password) && EmailValid(user.EmailAddress))
            {
                return true;
            }
            return false;
        }

        private bool EmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private bool PasswordValid(string password)
        {
            // passwords must be at least 6 charachters in length
            if (password.Length >= 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
