﻿using Core;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace Repository
{
    public class UserRepository : AbstractRepository
    {
        public UserRepository(DbContext daoContext) : base(daoContext)
        {
        }

        public bool IsRegistred(User user)
        {
            return Set<User>().Any(x => x.Email == user.Email);
        }

        public IObservable<bool> IsRegistredAsync(User user)
        {
            return SingleObservable.Create(() => IsRegistred(user));
        }

        public User Login(ICredential credential)
        {
            return Set<User>()
                .Include(u => u.UserRoles)
                    .ThenInclude(userRoles => userRoles.Role)
                .Single(x => x.Login == credential.Login && x.Password == credential.Password);
        }


        public IObservable<User> LoginAsync(ICredential credential)
        {
            return SingleObservable.Create(() => Login(credential));
        }
    }
}
