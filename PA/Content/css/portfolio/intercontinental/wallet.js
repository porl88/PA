var Wallet = (function () {
    'use strict';

    function isHighDensity() {
        return ((window.matchMedia && (window.matchMedia('only screen and (min-resolution: 124dpi), only screen and (min-resolution: 1.3dppx), only screen and (min-resolution: 48.8dpcm)').matches || window.matchMedia('only screen and (-webkit-min-device-pixel-ratio: 1.3), only screen and (-o-min-device-pixel-ratio: 2.6/2), only screen and (min--moz-device-pixel-ratio: 1.3), only screen and (min-device-pixel-ratio: 1.3)').matches)) || (window.devicePixelRatio && window.devicePixelRatio > 1.3));
    }

    function getRandomInt(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    function createWalletNo() {
        return getRandomInt(1, 4);
    }

    function createOfferNo() {
        return getRandomInt(1, 9);
    }

    function changeImages(selector, imageName) {
        var elements = document.querySelectorAll(selector);
        var root = isHighDensity() ? '/content/css/portfolio/intercontinental/images/2x/' : '/content/css/portfolio/intercontinental/images/';
        var imgUrl = root + imageName;
        for (var i = 0, len = elements.length; i < len; i++) {
            var element = elements[i];
            if (element.nodeName === 'IMG') {
                element.src = imgUrl;
            }
            else {
                element.style.backgroundImage = 'url(' + imgUrl + ')';
            }
        }
    }

    function openWallet(buttonSelector, walletSelector) {
        var button = document.querySelector(buttonSelector);
        if (button) {
            button.addEventListener('click', function () {
                var doc = document;
                var wallet = doc.querySelector(walletSelector);
                if (wallet) {
                    wallet.classList.add('open');
                }
            });
        }
    }

    return {

        initWallet: function () {
            var wallet = document.querySelector('.wallet');
            if (wallet) {
                var walletNo = createWalletNo();
                var offerNo = createOfferNo();
                wallet.classList.add('wallet-' + walletNo);
                changeImages('.wallet-bottom', 'packet-0' + walletNo + '.jpg');
                changeImages('.wallet-voucher', 'offer-0' + offerNo + '.jpg');
                openWallet('.btn-open-wallet', '.wallet');
            }
        }
    }

})();

