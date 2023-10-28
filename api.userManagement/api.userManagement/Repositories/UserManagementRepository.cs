using api.userManagement.Models;
using api.userManagement.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.userManagement.Repositories
{
    public class UserManagementRepository
    {
        private readonly DB_UserMoviesContext db_UserMoviesContext;

        // Constructor
        // Inject AppDbContext inside the constructor to make access to the AppDbContext 
        public UserManagementRepository(DB_UserMoviesContext dbUserMoviesContext)
        {
            db_UserMoviesContext = dbUserMoviesContext;
        }

        public async Task<List<UserProfile>> ListUserProfile()
        {
            return await db_UserMoviesContext.UserProfiles.OrderBy(m => m.Nama).ToListAsync();
        }
        public async Task<UserProfile> GetUserProfileByUserID(int? userID)
        {
            return await db_UserMoviesContext.UserProfiles.Where(m => m.UserId == userID).FirstOrDefaultAsync();
        } 
        public async Task<UserAkun> Login(string email, string password)
        {
            return await db_UserMoviesContext.UserAkuns.Where(a => a.Email == email && a.Password == password).FirstOrDefaultAsync();
        }
        public async Task<UserAkun> UserAkunByEmail(string email)
        {
            return await db_UserMoviesContext.UserAkuns.Where(a => a.Email == email ).FirstOrDefaultAsync();
        }
        public async Task<UserProfile> Registrasi(UserVM user)
        {
            if (user is not null)
            {
                UserAkun akun = new UserAkun();
                akun.Email = user.Email;
                akun.Password = user.Password;

                await db_UserMoviesContext.UserAkuns.AddAsync(akun);
                await db_UserMoviesContext.SaveChangesAsync();

                akun = await UserAkunByEmail(user.Email);

                UserProfile profile = new UserProfile();
                profile.UserId = akun.UserId;
                profile.Nama = user.Nama;
                profile.Alamat = user.Alamat;
                profile.Email = user.Email;

                await db_UserMoviesContext.UserProfiles.AddAsync(profile);
                await db_UserMoviesContext.SaveChangesAsync();

                return profile;
            }
            else
            {
                return null;
            }
        }
        public async Task<UserProfile> EditProfile(EditProfileVM editProfileVM)
        {
            UserProfile profile = await GetUserProfileByUserID(editProfileVM.UserId);
            if (profile is not null && editProfileVM is not null)
            {
                profile.Nama = editProfileVM.Nama;
                profile.Alamat = editProfileVM.Alamat;
                await db_UserMoviesContext.SaveChangesAsync();
            }
            return profile;
        }
        public async Task<UserProfile> ForgotPassword(forgotPasswordVM forgotPasswordVM)
        {
            UserAkun profile = await UserAkunByEmail(forgotPasswordVM.Email);
            if (profile is not null && forgotPasswordVM is not null && profile.Password.ToLower() == forgotPasswordVM.oldPassword.ToLower())
            {
                profile.Password = forgotPasswordVM.newPassword;
                await db_UserMoviesContext.SaveChangesAsync();

                UserProfile resultProfile = await GetUserProfileByUserID(profile.UserId);
                return resultProfile;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> Delete(int? userID)
        {
            var profile = await GetUserProfileByUserID(userID);
            if (profile is not null)
            {
                db_UserMoviesContext.UserProfiles.Remove(profile);
                db_UserMoviesContext.SaveChanges();
                var akun = await UserAkunByEmail(profile.Email);
                if (akun is not null)
                {
                    db_UserMoviesContext.UserAkuns.Remove(akun);
                    db_UserMoviesContext.SaveChanges();
                }
                return true;
            }
            return false;
        }
    }
}
