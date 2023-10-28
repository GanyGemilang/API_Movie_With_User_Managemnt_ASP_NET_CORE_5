using api.userManagement.Models;
using api.userManagement.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using api.userManagement.ViewModels;

namespace api.userManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManagementRepository _userManagementRepository;
        public UserManagementController(UserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult<List<UserProfile>>> Get()
        {
            try
            {
                List<UserProfile> data = await _userManagementRepository.ListUserProfile();

                if (data == null || data.Count == 0)
                {
                    return NotFound("No user profiles found.");
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("GetUserByUserID")]
        public async Task<ActionResult<UserProfile>> GetUserByUserID(int? userID)
        {
            try
            {
                if (!userID.HasValue)
                {
                    return BadRequest("Invalid user ID.");
                }

                UserProfile data = await _userManagementRepository.GetUserProfileByUserID(userID);

                if (data == null)
                {
                    return NotFound("User profile not found.");
                }

                return Ok(new { message = "Get Data Success", data = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }


        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return BadRequest("Email and password are required.");
                }

                UserAkun data = await _userManagementRepository.Login(email, password);

                if (data == null)
                {
                    return NotFound("Incorrect email or password.");
                }

                UserProfile dataProfile = await _userManagementRepository.GetUserProfileByUserID(data.UserId);

                if (dataProfile == null)
                {
                    return NotFound("User profile not found.");
                }

                return StatusCode(200, new { message = "Login Success", data = dataProfile });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost("Registration")]
        public async Task<ActionResult> Registration(UserVM userVM)
        {
            try
            {
                if (userVM == null)
                {
                    return BadRequest("Invalid user data.");
                }

                UserProfile data = await _userManagementRepository.Registrasi(userVM);

                if (data == null)
                {
                    return BadRequest("Registration failed. Please check your input.");
                }

                var response = new
                {
                    message = "Registration Success",
                    data = data
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPut("EditProfile")]
        public async Task<ActionResult> EditProfile(EditProfileVM editProfileVM)
        {
            try
            {
                if (editProfileVM == null)
                {
                    return BadRequest("Invalid profile data.");
                }

                UserProfile data = await _userManagementRepository.EditProfile(editProfileVM);

                if (data == null)
                {
                    return BadRequest("Failed to update profile. Please check your input.");
                }

                var response = new
                {
                    message = "Update Data Success",
                    data = data
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPut("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(forgotPasswordVM forgotPassword)
        {
            try
            {
                if (forgotPassword == null)
                {
                    return BadRequest("Invalid forgot password request.");
                }

                UserProfile data = await _userManagementRepository.ForgotPassword(forgotPassword);

                if (data == null)
                {
                    return BadRequest("Password reset failed. Please check your input.");
                }

                var response = new
                {
                    message = "Password reset successful",
                    data = data
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(int? id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid user ID.");
                }

                bool data = await _userManagementRepository.Delete(id);

                if (!data)
                {
                    return NotFound("User not found or deletion failed.");
                }

                var response = new
                {
                    message = "Delete Data Success"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

    }
}
