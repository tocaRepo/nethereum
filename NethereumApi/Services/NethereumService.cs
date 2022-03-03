

using System.Numerics;
using Microsoft.Extensions.Options;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using NethereumApi.Configuration;

namespace NethereumApi.Services;

public class NethereumService : INethereumService
{
    private readonly ILogger<NethereumService> _logger;
    private readonly ApiConfiguration _config;

    private readonly Web3 _web3Client;

    public NethereumService(ILogger<NethereumService> logger, IOptions<ApiConfiguration> configOptions)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        if (configOptions != null)
        {
            _config = configOptions.Value;
        }
        else
        {
            throw new ArgumentNullException(nameof(configOptions));
        }
        _web3Client = new Web3($"https://mainnet.infura.io/v3/{_config.InfuraKey}");

    }

    public async Task<decimal> GetEthBalanceByWalletIdAsync(string wallet)
    {
        //example wallet: 0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae
        var balance = await _web3Client.Eth.GetBalance.SendRequestAsync(wallet);
        _logger.LogInformation($"Balance in Wei: {balance.Value}");
        var etherAmount = Web3.Convert.FromWei(balance.Value);
        _logger.LogInformation($"Balance in Ether: {etherAmount}");
        return etherAmount;
    }

  
}