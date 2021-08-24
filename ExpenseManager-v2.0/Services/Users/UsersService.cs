namespace ExpenseManager_v2._0.Services.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ExpenseManager_v2._0.Data;
    using ExpenseManager_v2._0.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly ExpenseManagerDbContext data;
        private readonly IMapper _mapper;

        public UsersService(ExpenseManagerDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this._mapper = mapper;
        }        

        public IEnumerable<UserListingModel> GetAllUsers()
        {
            var users = this.data
                .Users
                .Where(u => u.IsDeleted != true)
                .ProjectTo<UserListingModel>(this._mapper.ConfigurationProvider)
                .ToList();

            return users;
        }

        public bool DeleteUser(string userId)
        {
            var deletedUser = FindUser(userId);

            if (deletedUser == null || deletedUser.IsDeleted == true)
            {
                return false;
            }

            deletedUser.IsDeleted = true;

            data.SaveChanges();
            return true;
        }

        public bool IsUserExist(string userId)
            => data
                .Users
                .Any(e => e.Id == userId && e.IsDeleted != true);

        public ApplicationUser FindUser(string userId)
            => this.data
                    .Users
                    .Where(s => s.Id == userId)
                    .FirstOrDefault();
    }
}
