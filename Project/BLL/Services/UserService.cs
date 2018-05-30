using System;
using BLL.DTO;
using BLL.Interfaces;
using AutoMapper;
using BLL.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Identity.Entities;
using DAL.Identity.Interfaces;
using DAL.Interfaces;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
        IUnitOfWorkIdentity DatabaseUsers { get; set; }

        public UserService(IUnitOfWorkIdentity uowi, IUnitOfWork uow)
        {
            DatabaseUsers = uowi;
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            var user = await DatabaseUsers.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await DatabaseUsers.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await DatabaseUsers.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                User clientProfile = new User { Id = user.Id, Surname = userDto.Surname, Name = userDto.Name };
                DatabaseUsers.ClientManager.Create(clientProfile);
                await DatabaseUsers.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await DatabaseUsers.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
                claim = await DatabaseUsers.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public UserDTO GetUserById(string id)
        {
            var user = Database.Users.Get(id);
            return Mapper.Map<User, UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return Database.Users.GetAll().Select(x => new UserDTO
            {
                Id = x.Id,
                Surname = x.Surname,
                Name = x.Name,
                Email = x.ApplicationUser.Email,
                UserName = x.ApplicationUser.UserName,
                Vacancies = Mapper.Map<IEnumerable<Vacancy>, List<VacancyDTO>>(x.Vacancies),
                Resumes = Mapper.Map<IEnumerable<Resume>, List<ResumeDTO>>(x.Resumes)
            });
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<Tuple<ClaimsIdentity, ClaimsIdentity>> FindAsync(string username, string password)
        {
            var appUser = await DatabaseUsers.UserManager.FindAsync(username, password);

            //if (appUser == null)
            //throw new AuthException("invalid_grant", "The user name or password is incorrect.");

            ClaimsIdentity oAuthIdentity = await DatabaseUsers.UserManager.CreateIdentityAsync(appUser, OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await DatabaseUsers.UserManager.CreateIdentityAsync(appUser, CookieAuthenticationDefaults.AuthenticationType);

            return new Tuple<ClaimsIdentity, ClaimsIdentity>(oAuthIdentity, cookiesIdentity);
        }

        public async Task<UserDTO> FindByIdAsync(string id)
        {
            var appUser = await DatabaseUsers.UserManager.FindByIdAsync(id);
            return Mapper.Map<ApplicationUser, UserDTO>(appUser);
        }

        public IEnumerable<VacancyDTO> GetUserVacancy(string id)
        {
            var user = Database.Users.Get(id);

            if (user == null)
                throw new ArgumentOutOfRangeException("Not found user");

            return Mapper.Map<IEnumerable<Vacancy>, List<VacancyDTO>>(user.Vacancies);
        }

        public IEnumerable<ResumeDTO> GetUserResumes(string id)
        {
            var user = Database.Users.Get(id);

            if (user == null)
                throw new ArgumentOutOfRangeException("Not found user");

            return Mapper.Map<IEnumerable<Resume>, List<ResumeDTO>>(user.Resumes);
        }
    }
}

