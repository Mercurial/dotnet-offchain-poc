window.connectWallet = async () => {
    const api = await cardano.nami.enable();
    window.walletApi = api;
}

window.getUtxos = () => {
    return window.walletApi.getUtxos();
}

window.getAddress = () => {
    return window.walletApi.getUsedAddresses();
}

window.signTx = (txCborHex) => {
    return window.walletApi.signTx(txCborHex);
}

window.submitTx = (txSigned) => {
    return window.walletApi.submitTx(txSigned);
}