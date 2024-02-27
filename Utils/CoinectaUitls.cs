using CardanoSharp.Wallet.Models.Transactions;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using PeterO.Cbor2;

public class CoinectaUtils
{
    public static IEnumerable<TransactionInput> ConvertInputsCbor(IEnumerable<string> inputCbors)
    {
        return inputCbors.Select(cbor => {
            var txInputCbor = CBORObject.DecodeFromBytes(Convert.FromHexString(cbor));
            return txInputCbor.GetTransactionInput();
        }).ToList();
    }
}