﻿using Api.Data;
using Api.Models;
using Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class UserRepository(DataContext context) : IUserInterface
    {
        public async Task<bool> CreateAsync(User user)
        {
            context!.Users.Add(user);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id  )
        {
            var getUser = await context.Users.FirstOrDefaultAsync(_ => _.Id == id);
            if (getUser != null)
            {
                context.Users.Remove(getUser);
                var result = await context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await context!.Users.ToListAsync();

        public async Task<User> GetByIdAsync(int id) => await context!.Users!.FirstOrDefaultAsync(_ => _.Id == id);

        public async Task<bool> UpdateAsync(User user)
        {
            var getUser = await context.Users.FirstOrDefaultAsync(_ => _.Id == user.Id);
            if (getUser != null)
            {
                getUser.Name = user.Name;
                getUser.Email = user.Email;
                var result = await context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
