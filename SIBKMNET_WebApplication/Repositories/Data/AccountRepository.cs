using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIBKMNET_WebApplication.Context;
using SIBKMNET_WebApplication.Hendler;
using SIBKMNET_WebApplication.Models;
using SIBKMNET_WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIBKMNET_WebApplication.Repositories.Data
{
    public class AccountRepository
    {
        MyContext myContext;
        

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
            
        }

        public ResponseLogin Login(Login login)
        {
            var data = myContext.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.User.Employee)
                .FirstOrDefault(x => x.User.Employee.Email.Equals(login.Email));
            var verify = Hashing.ValidatePassword(login.Password, data.User.Password);

            if (verify)
            {
                var response = new ResponseLogin()
                {
                    Id = data.User.Employee.Id,
                    FullName = data.User.Employee.FullName,
                    Email = data.User.Employee.Email,
                    Role = data.Role.Name
                };
                return response;
            }
            return null;

        }

        internal static object Login(Func<IActionResult> login)
        {
            throw new NotImplementedException();
        }

        // register
        public int Register(Register register)
        {
            Employee employee = new Employee()
            {
                FullName = register.FullName,
                Email = register.Email
            };
            myContext.Add(employee);
            var resultEmployee = myContext.SaveChanges();
            if (resultEmployee>0)
            {
                int id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email)).Id;
                User user = new User()
                {
                    Id = id,
                    Password = Hashing.HashPassword(register.Password)

                };
                myContext.Add(user);
                var resultUser = myContext.SaveChanges();

                if (resultUser>0)
                {
                    UserRole userRole = new UserRole()
                    {
                        UserId = id,
                        RoleId = register.RoleId
                    };
                    myContext.Add(userRole);
                    var resultUserRole = myContext.SaveChanges();
                    if (resultUserRole > 0)
                        return resultUserRole;
                    return 0;
                }
                return 0;
            }
            return 0;
        }

        public int ForgotPassword(ForgotPassword forgotPassword)
        {
            var verify = myContext.Employees.SingleOrDefault(x => x.Email.Equals(forgotPassword.Email)).Email;

            if (verify != null)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(forgotPassword.Email)).Id;

                if (id>0)
                {
                    var data = myContext.User.Find(id);
                    data.Password = Hashing.HashPassword(forgotPassword.Password);

                    myContext.User.Update(data);
                    var result = myContext.SaveChanges();
                    return result;
                }

                return 0;
            }

            return 0;
        }

        public int ChangePassword(ChangePassword changePassword)

        {
            // doesn't work
            var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(changePassword.Email)).Id;
            var verify = Hashing.ValidatePassword(changePassword.Password, changePassword.Password);

            if (verify)
            {
                var data = myContext.User.Find(id);
                data.Password = Hashing.HashPassword(changePassword.Password);
                myContext.User.Update(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }






    }
}
