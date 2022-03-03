using System.Numerics;

namespace NethereumApi.Services;

public interface INethereumService
{
    Task<decimal> GetEthBalanceByWalletIdAsync(string wallet);
}