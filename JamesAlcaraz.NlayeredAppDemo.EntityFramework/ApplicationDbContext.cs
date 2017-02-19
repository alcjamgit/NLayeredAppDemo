﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JamesAlcaraz.NlayeredAppDemo.Core.Entities;
using JamesAlcaraz.NlayeredAppDemo.Core.Entities.Spefications;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JamesAlcaraz.NlayeredAppDemo.EntityFramework
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new DbEntityEntry Entry<TEntity>(TEntity entity) where TEntity: class
        {
            return base.Entry<TEntity>(entity);
        }

        private void AddTimeStamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is IEntityAudited && (x.State == EntityState.Added || x.State == EntityState.Modified));

            if (!entities.Any())
                return;

            var currentUserName = "";

            if (System.Web.HttpContext.Current != null 
                && System.Web.HttpContext.Current.User != null 
                && System.Web.HttpContext.Current.User.Identity != null
                && System.Web.HttpContext.Current.User.Identity.Name != null)
            {
                currentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            }


            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IEntityAudited)entity.Entity).DateCreated = DateTime.UtcNow;
                    ((IEntityAudited)entity.Entity).UserCreated = currentUserName;
                }

                ((IEntityAudited)entity.Entity).DateModified = DateTime.UtcNow;
                ((IEntityAudited)entity.Entity).UserModified = currentUserName;
            }
        }
    }
}