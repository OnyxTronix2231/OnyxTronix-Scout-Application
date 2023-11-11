window.blazorFuncs = {
    registerClient: function (caller) {
        window['updateAvailable']
            .then(isAvailable => {
                if (isAvailable) {
                    caller.invokeMethodAsync("onUpdateAvailable").then(r => console.log(r));
                }
            });
    }
};

window.updateAvailable = new Promise(function (resolve, reject) {
    if ('serviceWorker' in navigator) {
        navigator.serviceWorker.register('/service-worker.js', { updateViaCache: 'none' }) 
            .then(function (registration) {
                console.log('Registration successful, scope is:', registration.scope);
                registration.onupdatefound = () => {
                    const installingWorker = registration.installing;
                    installingWorker.onstatechange = () => {
                        switch (installingWorker.state) {
                            case 'installed':
                                if (navigator.serviceWorker.controller) {
                                    resolve(true);
                                } else {
                                    resolve(false);
                                }
                                break;
                            default:
                        }
                    };
                };
            })
            .catch(error =>
                console.log('Service worker registration failed, error:', error));
    }
});
