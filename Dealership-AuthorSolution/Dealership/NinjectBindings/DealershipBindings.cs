using System;
using System.IO;
using System.Linq;
using System.Reflection;


using Dealership.Common;
using Dealership.Common.Contracts;
using Dealership.CommandHandlers.Contracts;
using Dealership.CommandHandlers;
using Dealership.Data.Contracts;
using Dealership.Data.Models;
using Dealership.Data.Services.Contracts;
using Dealership.Data.Services;
using Dealership.Engine;
using Dealership.Factories;

using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Dealership.Data.MongoDb.Repository;
using Dealership.Data.MongoDb.Services;
using Dealership.Data.MongoDb.Models;

namespace Dealership.NinjectBindings
{
    public class DealershipBindings : NinjectModule
    {
        private const string RegisterUserCommandHandlerName = "RegisterUserCommandHandler";
        private const string LoginCommandHandlerName = "LoginCommandHandler";
        private const string LogoutCommandHandlerName = "LogoutCommandHandler";
        private const string AddVehicleCommandHandlerName = "AddVehicleCommandHandler";
        private const string RemoveVehicleCommandHandlerName = "RemoveVehicleCommandHandler";
        private const string AddCommentCommandHandlerName = "AddCommentCommandHandler";
        private const string RemoveCommentCommandHandlerName = "RemoveCommentCommandHandler";
        private const string ShowUsersCommandHandlerName = "ShowUsersCommandHandler";
        private const string ShowVehiclesCommandHandlerName = "ShowVehiclesCommandHandler";
        private const string LoggedUserCommandHandlerName = "LoggedUserCommandHandler";

        private const string CarName = "Car";
        private const string MotorcycleName = "Motorcycle";
        private const string TruckName = "Truck";

        public override void Load()
        {
            Kernel.Bind(x =>
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .SelectAllClasses()
                .BindDefaultInterface()
            );

            this.Bind<Func<string>>().ToMethod(ctx => () =>
            {
                return Console.ReadLine();
            });

            this.Bind<Action<string>>().ToMethod(ctx => (param) =>
            {
                Console.Write(param);
            });

            //this.Bind<IVehicle>().To<Car>().Named("Car");
            //this.Bind<IVehicle>().To<Truck>().Named("Truck");
            //this.Bind<IVehicle>().To<Motorcycle>().Named("Motorcycle");

            this.Rebind<IUser>().To<MongoUser>();
            this.Rebind<IComment>().To<MongoComment>();

            this.Bind<IVehicle>().To<MongoCar>().Named("Car");
            this.Bind<IVehicle>().To<MongoTruck>().Named("Truck");
            this.Bind<IVehicle>().To<MongoMotorcycle>().Named("Motorcycle");

            this.Bind<ICommandFactory>().ToFactory().InSingletonScope();
            this.Bind<IDealershipFactory>().ToFactory().InSingletonScope();

            this.Bind<IVehicle>().ToMethod(ctx =>
            {
                var bindingName = (string)ctx.Parameters.First().GetValue(ctx, null);
                var parameters = ctx.Parameters.ToList();
                return ctx.Kernel.Get<IVehicle>(bindingName, parameters[1], parameters[2], parameters[3], parameters[4]);
            })
            .NamedLikeFactoryMethod((IDealershipFactory factory) => factory.GetVehicle(null, null, null, 0M, null));

            this.Bind<IIOProvider>().To<GenericIOProvider>().InSingletonScope();

            this.Bind<ICommandHandler>().To<LoggedUserCommandHandler>().Named(LoggedUserCommandHandlerName);
            this.Bind<ICommandHandler>().To<RegisterUserCommandHandler>().Named(RegisterUserCommandHandlerName);
            this.Bind<ICommandHandler>().To<LoginCommandHandler>().Named(LoginCommandHandlerName);
            this.Bind<ICommandHandler>().To<LogoutCommandHandler>().Named(LogoutCommandHandlerName);
            this.Bind<ICommandHandler>().To<AddVehicleCommandHandler>().Named(AddVehicleCommandHandlerName);
            this.Bind<ICommandHandler>().To<RemoveVehicleCommandHandler>().Named(RemoveVehicleCommandHandlerName);
            this.Bind<ICommandHandler>().To<AddCommentCommandHandler>().Named(AddCommentCommandHandlerName);
            this.Bind<ICommandHandler>().To<RemoveCommentCommandHandler>().Named(RemoveCommentCommandHandlerName);
            this.Bind<ICommandHandler>().To<ShowUsersCommandHandler>().Named(ShowUsersCommandHandlerName);
            this.Bind<ICommandHandler>().To<ShowVehiclesCommandHandler>().Named(ShowVehiclesCommandHandlerName);

            this.Bind<ICommandHandler>().ToMethod(ctx =>
            {
                var loggedUserHandler = ctx.Kernel.Get<ICommandHandler>(LoggedUserCommandHandlerName);
                var registerUserHandler = ctx.Kernel.Get<ICommandHandler>(RegisterUserCommandHandlerName);
                var loginHandler = ctx.Kernel.Get<ICommandHandler>(LoginCommandHandlerName);
                var logoutHandler = ctx.Kernel.Get<ICommandHandler>(LogoutCommandHandlerName);
                var addVehicleHandler = ctx.Kernel.Get<ICommandHandler>(AddVehicleCommandHandlerName);
                var removeVehicleHandler = ctx.Kernel.Get<ICommandHandler>(RemoveVehicleCommandHandlerName);
                var addCommnentHandler = ctx.Kernel.Get<ICommandHandler>(AddCommentCommandHandlerName);
                var removeCommentHandler = ctx.Kernel.Get<ICommandHandler>(RemoveCommentCommandHandlerName);
                var showUsersHandler = ctx.Kernel.Get<ICommandHandler>(ShowUsersCommandHandlerName);
                var showVehiclesHandler = ctx.Kernel.Get<ICommandHandler>(ShowVehiclesCommandHandlerName);

                loggedUserHandler.AddCommandHandler(registerUserHandler);
                registerUserHandler.AddCommandHandler(loginHandler);
                loginHandler.AddCommandHandler(logoutHandler);
                logoutHandler.AddCommandHandler(addVehicleHandler);
                addVehicleHandler.AddCommandHandler(removeVehicleHandler);
                removeVehicleHandler.AddCommandHandler(addCommnentHandler);
                addCommnentHandler.AddCommandHandler(removeCommentHandler);
                removeCommentHandler.AddCommandHandler(showUsersHandler);
                showUsersHandler.AddCommandHandler(showVehiclesHandler);

                return registerUserHandler;
            })
            .WhenInjectedInto<DealershipEngine>()
            .InSingletonScope();

            this.Bind<IEngine>().To<DealershipEngine>().InSingletonScope();

            //this.Bind<IUserService>().To<HashSetUserService>().InSingletonScope();

            this.Bind<IRepository<MongoUser>>().To<MongoUserRepository>().InSingletonScope();
            this.Bind<IUserService>().To<MongoDbUserService>().InSingletonScope();
        }
    }
}
