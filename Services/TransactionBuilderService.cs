using CardanoSharp.Wallet;
using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;

public class TransactionBuilderService
{
    private readonly IConfiguration _configuration;

    private readonly PrivateKey _paymentPrivKey;
    private readonly PublicKey _paymentPubKey;
    private readonly PrivateKey _stakingPrivKey;
    private readonly PublicKey _stakingPubKey;

    public TransactionBuilderService(IConfiguration configuration)
    {
        _configuration = configuration;
        var words = configuration["TestWalletSeed"]!;

        var mnemonic = new MnemonicService().Restore(words);

        // Fluent derivation API
        var paymentDerivation = mnemonic
            .GetMasterNode()      // IMasterNodeDerivation
            .Derive(PurposeType.Shelley)    // IPurposeNodeDerivation
            .Derive(CoinType.Ada)           // ICoinNodeDerivation
            .Derive(0)                      // IAccountNodeDerivation
            .Derive(RoleType.ExternalChain) // IRoleNodeDerivation
            .Derive(0);

        PrivateKey paymentPrivKey = paymentDerivation.PrivateKey;
        PublicKey paymentPubKey = paymentDerivation.PublicKey;

        var stakingDerivation = mnemonic
            .GetMasterNode()      // IMasterNodeDerivation
            .Derive(PurposeType.Shelley)    // IPurposeNodeDerivation
            .Derive(CoinType.Ada)           // ICoinNodeDerivation
            .Derive(0)                      // IAccountNodeDerivation
            .Derive(RoleType.Staking)    // IRoleNodeDerivation
            .Derive(0);

        PrivateKey stakingPrivKey = stakingDerivation.PrivateKey;
        PublicKey stakingPubKey = stakingDerivation.PublicKey;

        _paymentPrivKey = paymentPrivKey;
        _paymentPubKey = paymentPubKey;
        _stakingPrivKey = stakingPrivKey;
        _stakingPubKey = stakingPubKey;
    }

    public async Task Lock()
    {
        
    }
}