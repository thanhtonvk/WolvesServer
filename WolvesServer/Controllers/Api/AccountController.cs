using WolvesServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Firebase.Auth;
using Newtonsoft.Json;
using WolvesServer.Models;

namespace WolvesServer.Controllers.Api
{

    public class AccountController : ApiController
    {
        DBContext db = new DBContext();

        [HttpPost]
        [Route("account/register")]
        public async Task<IHttpActionResult> Register(AccountRegister account)
        {
            try
            {
               
                
                var result = db.Register(account.PhoneNumber.ToString(), account.Email.ToString(),
                    account.FirstName.ToString(), account.LastName.ToString(),
                    DateTime.Parse(account.DateOfBirth.ToString()),
                    account.Avatar.ToString());
                bool finishedTask = false;
                if (result > 0)
                {
                    FirebaseAuthProvider authProvider =
                        new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBaz6R-fqMhsSytluHgncdaSUDytgflLQM"));
                    await authProvider
                        .CreateUserWithEmailAndPasswordAsync(account.Email.ToString(),
                            account.Password.ToString(),
                            account.LastName.ToString(), true, "vi")
                        .ContinueWith(
                            task =>
                            {
                                if (task.IsCompleted)
                                {
                                    finishedTask = true;
                                }
                            });
                }

                if (finishedTask) return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("account/login")]
        public async Task<IHttpActionResult> Login(string email, string password)
        {
            try
            {
                User user = null;
                FirebaseAuthProvider authProvider =
                    new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBaz6R-fqMhsSytluHgncdaSUDytgflLQM"));
                await authProvider.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        user = task.Result.User;
                    }
                });
                if (user != null)
                {
                    if (user.IsEmailVerified)
                    {
                        return Ok(db.LoginAccount(user.Email));
                    }
                    else
                    {
                        return Ok(new List<Account>());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("account/change-password")]
        public async Task<IHttpActionResult> ChangePassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                bool check = false;
                FirebaseAuthProvider authProvider =
                    new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBaz6R-fqMhsSytluHgncdaSUDytgflLQM"));
                await authProvider.SignInWithEmailAndPasswordAsync(email, oldPassword).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        string token = task.Result.FirebaseToken;
                        authProvider.ChangeUserPassword(token, newPassword).ContinueWith(task1 =>
                        {
                            if (task1.IsCompleted)
                            {
                                check = true;
                            }
                        });
                    }
                });
                if (check) return Ok();
            }
            catch
            {
                return BadRequest();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("account/update-account")]
        public IHttpActionResult UpdateAccount(Account account)
        {
            var result = db.UpdateAccount(account.Id, account.PhoneNumber, account.Email, account.FirstName,
                account.LastName, account.DateOfBirth, account.Avatar);
            if (result > 0) return Ok();
            return BadRequest();
        }


        [HttpPost]
        [Route("account/forgot-password")]
        public async Task<IHttpActionResult> ForgotPassword(string email)
        {
            if (db.LoginAccount(email).Any())
            {
                bool result = false;
                var authProvider =
                    new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBaz6R-fqMhsSytluHgncdaSUDytgflLQM"));
                await authProvider.SendPasswordResetEmailAsync(email).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        result = true;
                    }
                });
                return Ok();
            }

            return BadRequest();
        }
        [HttpGet]
        [Route("account/get-id-by-email")]
        public IHttpActionResult GetIdByEmail(string email)
        {
            return Ok(db.GetIDByEmail(email));
        }

        [HttpPost]
        [Route("account/block")]
        public IHttpActionResult Block(int id)
        {
            return Ok(db.BlockAccount(id));
        }
    }
}
