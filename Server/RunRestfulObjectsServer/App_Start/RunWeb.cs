using AdventureWorksModel;
using NakedObjects.Boot;
using NakedObjects.Core.Context;
using NakedObjects.Core.NakedObjectsSystem;
using NakedObjects.EntityObjectStore;
using RestfulObjects.Bootstrap;

namespace RunRestfulObjectsServer.App_Start {
	public class RunWeb : RunRest {
		protected override NakedObjectsContext Context {
			get { return HttpContextContext.CreateInstance(); }
		}

        protected override IServicesInstaller MenuServices
        {
            get
            {
                return new ServicesInstaller(
                    new CustomerRepository(),
                    new OrderRepository(),
                    new ProductRepository(),
                    new EmployeeRepository(),
                    new SalesRepository(),
                    new SpecialOfferRepository(),
                    new ContactRepository(),
                    new VendorRepository(),
                    new PurchaseOrderRepository(),
                    new WorkOrderRepository()
                    );
            }
        }

        protected override IServicesInstaller ContributedActions
        {
            get { return new ServicesInstaller(new OrderContributedActions(), new CustomerContributedActions()); }
        }

		protected override IServicesInstaller SystemServices {
			get { return new ServicesInstaller(); }
		}

        protected override IObjectPersistorInstaller Persistor
        {
            get
            {
                // Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0"); // Code First option: for in-memory database
                // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDbContext>()); // Code First option
                var installer = new EntityPersistorInstaller();
                // installer.AddCodeFirstDbContextConstructor(() => new MyDbContext());  //Code First requirement: add each DbContext
                return installer;
            }
        }

		public static void Run() {
			new RunWeb().Start();
		}
	}
}