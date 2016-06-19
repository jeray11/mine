using mine.core.Domain;
using mine.core.Domain.Forums;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mine.data
{
    public class MineContext:DbContext,IDbContext
    {
        public bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }

            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        public bool AutoDetectChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }

            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        public MineContext() : base("name=MineContext") { }
        public MineContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter) this).ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
               type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            //...or do it manually below. For example,
            //modelBuilder.Configurations.Add(new LanguageMap());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            return this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = default(int?), params object[] parameters)
        {
            
        }

        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
