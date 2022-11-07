using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration config;
        private readonly FundooContext fundooContext;
        public UserRL(IConfiguration config, FundooContext fundooContext)
        {
            this.config = config;
            this.fundooContext = fundooContext;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = EncryptPassword(userRegistrationModel.Password);
                fundooContext.userTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                var LoginResult = fundooContext.userTable.Where(UserEmail => UserEmail.Email == userLogin.Email).FirstOrDefault();
                if(LoginResult != null && Decryption(LoginResult.Password)==userLogin.Password)
                {
                    var Token = GenerateSecurityToken(LoginResult.Email, LoginResult.UserId);
                    return Token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GenerateSecurityToken(string email, long userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("JWT:key")]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userID", userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public string Forgot(string email )
        {
            try
            {
                var emailCheck=fundooContext.userTable.FirstOrDefault(x => x.Email == email);
                if(emailCheck != null)
                {
                    var token = GenerateSecurityToken(emailCheck.Email, emailCheck.UserId);
                    MSMQmodel msmqmodel = new MSMQmodel();
                    msmqmodel.sendData2Queue(token);
                    return token.ToString();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Reset( string email , string Password , string ConfirmPassword)
        {
            try
            {
                if (Password.Equals(ConfirmPassword))
                {
                    var emailcheck=fundooContext.userTable.FirstOrDefault(x => x.Email == email); 
                    emailcheck.Password = Password;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;

                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        public string EncryptPassword(string password)
        {
            string Key = "secretkey#^hladf";
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public static string Decryption(string encryptedPass)
        {
            string Key = "secretkey#^hladf";
            if (string.IsNullOrEmpty(encryptedPass))
            {
                return "";
            }
            var encodeBytes = Convert.FromBase64String(encryptedPass);
            var result = Encoding.UTF8.GetString(encodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }

    }
}
