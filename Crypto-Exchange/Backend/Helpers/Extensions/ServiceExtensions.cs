using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using test_binance_api.Helpers.Seeders;
using test_binance_api.Models;
using test_binance_api.Repository.CoinRepository;
using test_binance_api.Repository.GenericRepository;
using test_binance_api.Repository.HistoryRepository;
using test_binance_api.Repository.UserRepository;
using test_binance_api.Repository.WalletRepository;
using test_binance_api.Service.CoinService;
using test_binance_api.Service.TradingService;
using test_binance_api.Service.UserService;
using test_binance_api.Service.UserWalletHistoryService;
using Mailing.Service.Services;
using test_binance_api.Repository.CandleStickRepository;
using test_binance_api.Service.CandleStickService;

namespace test_binance_api.Helpers.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IHistoryyRepository, HistoryyRepository>();
            services.AddTransient<ICoinRepository, CoinRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<ICandleStickRepository, CandleStickRepository>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<RoleManager<IdentityRole<Guid>>>();
            services.AddHttpClient();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICoinService, CoinService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserWalletHistoryService, UserWalletHistoryService>();
            services.AddTransient<ITradingService, TradingService>();
            services.AddTransient<ICandleStickService, CandleStickService>();
            services.AddScoped<IEmailService, EmailService>();
            
            return services;
        }

        public static IServiceCollection AddSeeder(this IServiceCollection services)
        {
            services.AddTransient<UserSeeder>();
            services.AddTransient<RoleSeeder>();
            services.AddTransient<UserRoleSeeder>();
            services.AddTransient<CoinSeeder>();

            return services;
        }


    }
}

