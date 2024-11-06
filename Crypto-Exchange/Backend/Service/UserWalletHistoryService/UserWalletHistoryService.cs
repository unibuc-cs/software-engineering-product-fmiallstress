using test_binance_api.Models.DTOs.User;
using test_binance_api.Models;
using test_binance_api.Repository.HistoryRepository;
using test_binance_api.Repository.UserRepository;
using AutoMapper;
using test_binance_api.Repository.WalletRepository;

namespace test_binance_api.Service.UserWalletHistoryService
{
    public class UserWalletHistoryService : IUserWalletHistoryService
    {
        IUserRepository _userRepository;
        IHistoryyRepository _historyRepository;
        IWalletRepository _walletRepository;
        IMapper _mapper;

        public UserWalletHistoryService(IUserRepository userRepository, IHistoryyRepository historyRepository, IMapper mapper, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _historyRepository = historyRepository;
            _mapper = mapper;
            _walletRepository = walletRepository;

        }


        //function that creates a user with his own history and wallet
        public async Task<UserDTO> CreateAsync(UserCreateDTO user)
        {
            var newUser = _mapper.Map<User>(user);

            var newHistory = new History();
            await _historyRepository.CreateAsync(newHistory);
            newUser.IdHistory = newHistory.Id;
            newUser.History = newHistory;

            var newWallet = new Wallet();
            await _walletRepository.CreateAsync(newWallet);
            newUser.IdWallet = newWallet.Id;
            newUser.Wallet = newWallet;

            await _userRepository.CreateAsync(newUser);
            return _mapper.Map<UserDTO>(newUser);
        }

    }
}
