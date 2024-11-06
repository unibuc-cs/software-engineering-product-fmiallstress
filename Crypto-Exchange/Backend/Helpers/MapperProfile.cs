    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using test_binance_api.Models;
    using test_binance_api.Models.DTOs;
    using test_binance_api.Models.DTOs.User;

    namespace test_binance_api.Helpers
    {
        public class MapperProfile : AutoMapper.Profile
        {


            public MapperProfile()
            {
                var hasher = new PasswordHasher<User>();



                CreateMap<User, UserDTO>();
                CreateMap<UserDTO, User>()
                    .ForMember(u => u.Id,
                        opt => opt.MapFrom(src => new Guid()));

                CreateMap<UserCreateDTO, User>()
                    .ForMember(u => u.Id, opt =>
                            opt.MapFrom(src => new Guid()))
                    .ForMember(u => u.PasswordHash, opt =>
                        opt.MapFrom(src => hasher.HashPassword(null, src.Password)))
                    .ForMember(u => u.LockoutEnabled, opt =>
                        opt.MapFrom(src => false))
                    .ForMember(u => u.SecurityStamp, opt =>
                        opt.Ignore());

                CreateMap<User, UserUpdateDTO>();
                CreateMap<UserUpdateDTO, User>();

                CreateMap<UserSignUpDTO, User>()
                    .ForMember(u => u.Id, opt =>
                        opt.MapFrom(src => new Guid()))
                    .ForMember(u => u.PasswordHash, opt =>
                        opt.MapFrom(src => hasher.HashPassword(null, src.Password)))
                    .ForMember(u => u.LockoutEnabled, opt =>
                        opt.MapFrom(src => false))
                    .ForMember(u => u.SecurityStamp, opt =>
                        opt.Ignore());

                CreateMap<Coin, CoinDTO>();
                CreateMap<CoinDTO, Coin>();

                CreateMap<Coin, CoinCreateDTO>();
                CreateMap<CoinCreateDTO, Coin>();


                CreateMap<Coin, CoinShowDTO>();
                CreateMap<CoinShowDTO, Coin>();
            }

        }
    }
