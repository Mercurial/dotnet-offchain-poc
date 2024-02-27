window.connectWallet = async () => {
    const api = await cardano.nami.enable();
    window.walletApi = api;
}

window.getUtxos = () => {
    return window.walletApi.getUtxos();
}