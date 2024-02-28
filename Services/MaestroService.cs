using System.Text.Json;
using Cardano.Sync.Data.Models.Datums;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using CardanoSharp.Wallet.Models;
using Address = CardanoSharp.Wallet.Models.Addresses.Address;

public class MaestroService(IHttpClientFactory _httpClientFactory)
{
    private static string ApiKey => "8o8807pF6O23Nt6PInCSydNOgmxbGkKO";
    private static string BaseUrl => "https://preview.gomaestro-api.org";

    public async Task<IEnumerable<Utxo>> GetUtxosAsync(string address)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(BaseUrl);
        httpClient.DefaultRequestHeaders.Add("api-key", ApiKey);

        var maestroUtxosResponse = await httpClient.GetFromJsonAsync<JsonElement>($"/v1/addresses/{address}/utxos?with_cbor=true");
        var maestroUtxos = maestroUtxosResponse.GetProperty("data").EnumerateArray();
        var utxos = maestroUtxos.Select(utxo => {
            var txOutCbor = utxo.GetProperty("txout_cbor").GetString()!;
            var txOut = CoinectaUtils.ConvertTxOutputListCbor([txOutCbor]).FirstOrDefault()!;
            return new Utxo()
            {
                TxHash = utxo.GetProperty("tx_hash").GetString()!,
                TxIndex = utxo.GetProperty("index").GetUInt32(),
                Balance =  txOut.Value.GetBalance(),
                OutputAddress = new Address(txOut.Address).ToString(),
                OutputDatumOption = txOut.DatumOption
            };
        });
        return utxos;
    }
}