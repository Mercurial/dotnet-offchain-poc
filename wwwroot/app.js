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

window.signTx = (txCborHex, partialSign = false) => {
    return window.walletApi.signTx(txCborHex, partialSign);
}

window.submitTx = (txSigned) => {
    return window.walletApi.submitTx(txSigned);
}