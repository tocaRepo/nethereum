
## About The Project

.NET 6 and Nethereum have been used to create a REST API:
- /api/GetEthBalance/{wallet}

 the API return the balance associated with the wallet address provided.

# Usage
1. Get a free API Key at [https://infura.io/](https://infura.io/) (to access ethereum mainnet)
2. Clone the repo
3. dotnet restore
   ```sh
   dotnet restore
   ```
4. Enter your infura key in `appsettings.json`
   ```js
   "ApiConfiguration":{
    "InfuraKey":"#placeholder#"
   }
3. dotnet build
   ```sh
   dotnet build
   ```
3. dotnet run
   ```sh
   dotnet run
   ```



# What is Nethereum ?

Nethereum is the .Net integration library for Ethereum, simplifying the access and smart contract interaction with Ethereum nodes both public or permissioned like Geth, [Parity](https://www.parity.io/) or [Quorum](https://www.jpmorgan.com/global/Quorum).

Nethereum is developed targeting netstandard 1.1, netstandard 2.0, netcore 2.1, netcore 3.1, net451 and also as a portable library, hence it is compatible with all the operating systems (Windows, Linux, MacOS, Android and OSX) and has been tested on cloud, mobile, desktop, Xbox, hololens and windows IoT.

check more here:
https://github.com/Nethereum/Nethereum


