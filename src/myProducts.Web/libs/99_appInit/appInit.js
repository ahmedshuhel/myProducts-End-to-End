﻿(function () {
    "use strict";

    window.app = window.app || { resolver: {} };

    if (ttMobile) {
        app.module = angular.module("myApp", ["ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "routeResolverServices", "ngStorage", "chart.js", "jmdobry.angular-cache", "ionic", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    } else {
        app.module = angular.module("myApp", ["ui.router", "ngTouch", "ngAnimate", "ngSanitize", "Thinktecture.Dialog", "Thinktecture.Toast", "mgcrea.ngStrap", "ui.bootstrap", "Thinktecture.SignalR", "Thinktecture.Authentication", "ngCookies", "pascalprecht.translate", "routeResolverServices", "angular-carousel", "frapontillo.bootstrap-switch", "ngStorage", "imageupload", "chart.js", "jmdobry.angular-cache", "angular-loading-bar", "btford.phonegap.ready", "btford.phonegap.geolocation"]);
    }

    app.module.config(function ($compileProvider, $translateProvider, cfpLoadingBarProvider, tokenAuthenticationServiceProvider) {
        FastClick.attach(document.body);

        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|ghttps?|ms-appx|x-wmapp0):/);

        cfpLoadingBarProvider.includeSpinner = false;

        tokenAuthenticationServiceProvider.setUrl(ttTools.baseUrl + "token");

        ttTools.initLogger(ttTools.baseUrl + "api/log");
        ttTools.logger.info("Configuring myApp...");

        $translateProvider.translations("de", tt.translations.de);
        $translateProvider.useStaticFilesLoader({
            prefix: "translations/locale-",
            suffix: ".json"
        });
        $translateProvider.preferredLanguage("en");
        $translateProvider.useLocalStorage();
    });
})();
