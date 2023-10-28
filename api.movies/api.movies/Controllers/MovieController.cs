using api.movies.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using api.movies.Models;
using api.movies.ViewModels;

namespace api.movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieRepository _userManagementRepository;
        public MovieController(MovieRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        [HttpGet("GetAllMovie")]
        public async Task<ActionResult<List<Movie>>> Get()
        {
            try
            {
                List<Movie> data = await _userManagementRepository.ListMovie();

                if (data == null || data.Count == 0)
                {
                    return NotFound("No movie profiles found.");
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("GetMovieByID")]
        public async Task<ActionResult<UserProfile>> GetMovieByID(int? movieID)
        {
            try
            {
                if (!movieID.HasValue)
                {
                    return BadRequest("Invalid movie ID.");
                }

                Movie data = await _userManagementRepository.GetMovieByMovieID(movieID);

                if (data == null)
                {
                    return NotFound("Movie not found.");
                }

                return Ok(new { message = "Get Data Success", data = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost("AddMovie")]
        public async Task<ActionResult> AddMovie(MovieVM movieVM)
        {
            try
            {
                if (movieVM == null)
                {
                    return BadRequest("Invalid movie data.");
                }

                Movie data = await _userManagementRepository.AddMovie(movieVM);

                if (data == null)
                {
                    return BadRequest("Add Movie failed. Please check your input.");
                }

                var response = new
                {
                    message = "Add Movie Success",
                    data = data
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPut("EditMovie")]
        public async Task<ActionResult> EditProfile(EditMovieVM editMovieVM)
        {
            try
            {
                if (editMovieVM == null)
                {
                    return BadRequest("Invalid movie data.");
                }

                Movie data = await _userManagementRepository.EditMovie(editMovieVM);

                if (data == null)
                {
                    return BadRequest("Failed to update movie. Please check your input.");
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
        [HttpGet("GetAllUlasanMovie")]
        public async Task<ActionResult<List<MovieReview>>> GetAllUlasanMovie()
        {
            try
            {
                List<MovieReview> data = await _userManagementRepository.ListUlasanMovie();

                if (data == null || data.Count == 0)
                {
                    return NotFound("No Review Movie found.");
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("GetUlasanByUserID")]
        public async Task<ActionResult<MovieReview>> GetUlasanByUserID(int? userID)
        {
            try
            {
                if (!userID.HasValue)
                {
                    return BadRequest("Invalid user ID.");
                }

                List<MovieReview> data = await _userManagementRepository.GetUlasanMovieByMovieID(userID);

                if (data == null)
                {
                    return NotFound("Review Movie not found.");
                }

                return Ok(new { message = "Get Data Success", data = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost("AddUlasanMovie")]
        public async Task<ActionResult> AddUlasanMovie(UlasanMovieVM ulasanMovieVM)
        {
            try
            {
                if (ulasanMovieVM == null)
                {
                    return BadRequest("Invalid Review Movie data.");
                }

                MovieReview data = await _userManagementRepository.AddUlasanMovie(ulasanMovieVM);

                if (data == null)
                {
                    return BadRequest("Add Review Movie failed. Please check your input.");
                }

                var response = new
                {
                    message = "Add Review Movie Success",
                    data = data
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
        [HttpPost("AddUserMovie")]
        public async Task<ActionResult> AddUserMovie(UserMovieVM userMovie)
        {
            try
            {
                if (userMovie == null)
                {
                    return BadRequest("Invalid User Movie data.");
                }

                UserMovie data = await _userManagementRepository.AddUserMovie(userMovie);

                if (data == null)
                {
                    return BadRequest("Add User Movie failed. Please check your input.");
                }

                var response = new
                {
                    message = "Add User Movie Success",
                    data = data
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("GetUserMovieByUserID")]
        public async Task<ActionResult<UserMovie>> GetUserMovieByUserID(int? userID)
        {
            try
            {
                if (!userID.HasValue)
                {
                    return BadRequest("Invalid user ID.");
                }

                List<UserMovie> data = await _userManagementRepository.GetUserMovieByUserID(userID);

                if (data == null)
                {
                    return NotFound("User Movie not found.");
                }

                return Ok(new { message = "Get Data Success", data = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}
