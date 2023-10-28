using api.movies.Models;
using api.movies.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.movies.Repositories
{
    public class MovieRepository
    {
        private readonly DB_UserMoviesContext db_UserMoviesContext;

        // Constructor
        // Inject AppDbContext inside the constructor to make access to the AppDbContext 
        public MovieRepository(DB_UserMoviesContext dbUserMoviesContext)
        {
            db_UserMoviesContext = dbUserMoviesContext;
        }
        public async Task<List<Movie>> ListMovie()
        {
            return await db_UserMoviesContext.Movies.OrderBy(m => m.Judul).ToListAsync();
        }
        public async Task<Movie> GetMovieByMovieID(int? MovieId)
        {
            return await db_UserMoviesContext.Movies.Where(m => m.MovieId == MovieId).FirstOrDefaultAsync();
        }
        public async Task<Movie> AddMovie(MovieVM movieVM)
        {
            if (movieVM is not null)
            {
                Movie movie = new Movie();
                movie.Judul = movieVM.Judul;
                movie.TahunRilis = movieVM.TahunRilis;

                await db_UserMoviesContext.Movies.AddAsync(movie);
                await db_UserMoviesContext.SaveChangesAsync();

                return movie;
            }
            else
            {
                return null;
            }
        }
        public async Task<Movie> EditMovie(EditMovieVM movieVM)
        {
            Movie movie = await GetMovieByMovieID(movieVM.MovieId);
            if (movie is not null && movieVM is not null)
            {
                movie.Judul = movieVM.Judul;
                movie.TahunRilis = movieVM.TahunRilis;
                await db_UserMoviesContext.SaveChangesAsync();
            }
            return movie;
        }
        public async Task<List<MovieReview>> ListUlasanMovie()
        {
            return await db_UserMoviesContext.MovieReviews.OrderBy(m => m.TanggalReview).ToListAsync();
        }
        public async Task<List<MovieReview>> GetUlasanMovieByMovieID(int? UserID)
        {
            return await db_UserMoviesContext.MovieReviews.Where(m => m.UserId == UserID).ToListAsync();
        }
        public async Task<MovieReview> AddUlasanMovie(UlasanMovieVM ulasanMovieVM)
        {
            if (ulasanMovieVM is not null)
            {
                MovieReview movie = new MovieReview();
                movie.MovieId = ulasanMovieVM.MovieId;
                movie.UserId = ulasanMovieVM.UserId;
                movie.Rating = ulasanMovieVM.Rating;
                movie.Komentar = ulasanMovieVM.Komentar;
                movie.TanggalReview = ulasanMovieVM.TanggalReview;

                await db_UserMoviesContext.MovieReviews.AddAsync(movie);
                await db_UserMoviesContext.SaveChangesAsync();

                return movie;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<UserMovie>> GetUserMovieByUserID(int? UserID)
        {
            return await db_UserMoviesContext.UserMovies.Where(m => m.UserId == UserID).ToListAsync();
        }
        public async Task<UserMovie> AddUserMovie(UserMovieVM userMovie)
        {
            if (userMovie is not null)
            {
                UserMovie user = new UserMovie();
                user.MovieId = userMovie.MovieId;
                user.UserId = userMovie.UserId;
                await db_UserMoviesContext.UserMovies.AddAsync(user);
                await db_UserMoviesContext.SaveChangesAsync();

                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
