using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        

        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                return iuserRL.Login(userLogin);
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
            
        }
        public string Forgot(string email)
        {
            try
            {
                return iuserRL.Forgot(email);
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
        public bool Reset(string email, string Password, string ConfirmPassword)
        {
            try
            {
                return iuserRL.Reset(email, Password, ConfirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}