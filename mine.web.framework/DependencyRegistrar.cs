﻿using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using mine.core;
using mine.core.Caching;
using mine.core.Configuration;
using mine.core.Data;
using mine.core.Fakes;
using mine.core.Infrastructure;
using mine.core.Plugins;
using mine.data;
using mine.services.Authentication;
using mine.services.Cms;
using mine.services.Common;
using mine.services.Configuration;
using mine.services.Customers;
using mine.services.Dictionary;
using mine.services.Directory;
using mine.services.Events;
using mine.services.Forums;
using mine.services.Helpers;
using mine.services.Localization;
using mine.services.Logging;
using mine.services.Media;
using mine.services.Security;
using mine.services.Seo;
using mine.services.Stores;
using mine.services.Topics;
using mine.web.framework.Mvc;
using mine.web.framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace mine.web.framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            //user agent helper
            builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();//HTTP context and other related stuff
            //datetime helper
            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();
            //pluginfinder
            builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            //store context
            builder.RegisterType<WebStoreContext>().As<IStoreContext>().InstancePerLifetimeScope();
            // theme context
            builder.RegisterType<ThemeContext>().As<IThemeContext>().InstancePerLifetimeScope();
            //theme provider
            builder.RegisterType<ThemeProvider>().As<IThemeProvider>().InstancePerLifetimeScope();
            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("mine_cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_per_request").InstancePerLifetimeScope();

            //service
            builder.RegisterType<ForumService>().As<IForumService>().InstancePerLifetimeScope();
            //pass MemoryCacheManager as cacheManager (cache locales between requests)
            builder.RegisterType<LocalizationService>().As<ILocalizationService>()
                 .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterType<PictureService>().As<IPictureService>().InstancePerLifetimeScope();
            builder.RegisterType<CountryService>().As<ICountryService>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>()
                 .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("mine_cache_static"))
                 .InstancePerLifetimeScope();
            builder.RegisterSource(new SettingsSource());
            builder.RegisterType<StoreService>().As<IStoreService>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<SubscriptionService>().As<ISubscriptionService>().InstancePerLifetimeScope();
            builder.RegisterType<TopicService>().As<ITopicService>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizedEntityService>().As<ILocalizedEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<UrlRecordService>().As<IUrlRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyService>().As<ICurrencyService>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<StoreMappingService>().As<IStoreMappingService>()
               .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("mine_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetService>().As<IWidgetService>().InstancePerLifetimeScope();
            //data layer
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.Register<IDbContext>(c=>new MineContext()).InstancePerLifetimeScope();
            //event publisher
            builder.RegisterType<EventPublisher>().As<IEventPublisher>().SingleInstance();
        }
    }

    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    var currentStoreId = c.Resolve<IStoreContext>().CurrentStore.Id;
                    //uncomment the code below if you want load settings per store only when you have two stores installed.
                    //var currentStoreId = c.Resolve<IStoreService>().GetAllStores().Count > 1
                    //    c.Resolve<IStoreContext>().CurrentStore.Id : 0;

                    //although it's better to connect to your database and execute the following SQL:
                    //DELETE FROM [Setting] WHERE [StoreId] > 0
                    return c.Resolve<ISettingService>().LoadSetting<TSettings>(currentStoreId);
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }
}
