using Dapper.AmbientContext;
using Dapper.AmbientContext.Storage;
using SimpleInjector;
using System;
using System.Threading.Tasks;

namespace Framework.Sampe.Console
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                var container = new Container();
                AmbientDbContextStorageProvider.SetStorage(new AsyncLocalContextStorage());

                container.RegisterSingleton<IDbConnectionFactory>(() => new SqlServerConnectionFactory("data source=192.168.10.23;initial catalog=RoyanAutomation;USER ID=sa;password=RoyanDb1397;MultipleActiveResultSets=True;"));
                container.RegisterSingleton<IAmbientDbContextFactory, AmbientDbContextFactory>();
                container.Register<IAmbientDbContextLocator, AmbientDbContextLocator>();

                container.Register<IContractRepository, ContractRepository>();
                var app = container.GetInstance<IContractRepository>();
                var ccc = container.GetInstance<IAmbientDbContextFactory>();
                using (var ambientContext = ccc.Create(join:true, suppress:false))
                {
	                var t2 = app.FirstOrDefault(14100013);
	                ambientContext.Commit();
                }
              
            }
            catch (System.Exception e)
            {

                throw;
            }


        }
    }
}
