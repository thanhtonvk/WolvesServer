using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WolvesServer.Models
{
    public class AccountRegister
    {
        private string phoneNumber, email, firstName, lastName, dateOfBirth, avatar, password;

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Avatar { get => avatar; set => avatar = value; }
        public string Password { get => password; set => password = value; }
    }
}