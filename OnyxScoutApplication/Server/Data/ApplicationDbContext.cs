using OnyxScoutApplication.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MySql.Data.EntityFramework;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.CustomeEventModels;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Server.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<ScoutFormFormat> ScoutFormFormats { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Form> ScoutForms { get; set; }
        
        public Microsoft.EntityFrameworkCore.DbSet<CustomEvent> Events { get; set; }
        
        public Microsoft.EntityFrameworkCore.DbSet<Field> Fields { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));

            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));

            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));
            builder.Entity<ApplicationUser>()
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            
            builder.Entity<FieldsInStage>()
                .HasMany(x => x.Fields)
                .WithOne(x => x.FieldsInStage)
                .HasForeignKey(p => p.FieldStageId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Field>()
                .HasMany(x => x.CascadeFields)
                .WithOne(x => x.ParentField)
                .HasForeignKey(p => p.FieldId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
