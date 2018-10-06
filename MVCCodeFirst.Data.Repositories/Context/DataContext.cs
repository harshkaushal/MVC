using MVCCodeFirst.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Data.Repositories.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext")
        {
        }
        #region TableEntity

        public virtual DbSet<UserInfo> tbUserInfo { get; set; }
        public virtual DbSet<UserLogin> tbUserLogin { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        } 

        #endregion

        #region AddTimestamps

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

            //var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
            //    ? HttpContext.Current.User.Identity.Name
            //    : "Anonymous";
            var currentUsername = "Anonymous";
            int currentUserID = 1;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    // ((BaseModel)entity.Entity).CreatedOn = DateTime.UtcNow;
                    ((BaseModel)entity.Entity).CreatedOn = DateTime.Now;

                    if (((BaseModel)entity.Entity).CreatedBy == null)
                        ((BaseModel)entity.Entity).CreatedBy = currentUserID;
                }

                //((BaseModel)entity.Entity).ModifiedOn = DateTime.UtcNow;
                ((BaseModel)entity.Entity).ModifiedOn = DateTime.Now;
                if (((BaseModel)entity.Entity).ModifiedBy == null)
                    ((BaseModel)entity.Entity).ModifiedBy = currentUserID;

                if (((BaseModel)entity.Entity).IsActive == null)
                    ((BaseModel)entity.Entity).IsActive = true;
            }
        }


        #endregion
    }
}
