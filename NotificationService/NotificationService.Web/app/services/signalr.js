(function () {
    'use strict';

    var serviceId = 'signalr';
    angular.module('app').factory(serviceId, ['common', '$rootScope', signalr]);

    function signalr(common, $rootScope) {
        var $q = common.$q;

        var service = {
            initialize: initialize
        };

        return service;

        function initialize() {
            var proxy = $.connection.orderHub;
            proxy.client.createdOrder = function (order) {
                $rootScope.$broadcast('createdOrder', order);
            }

            $.connection.hub.url = "http://jwsignalr.azurewebsites.net/signalr";
            $.connection.hub.start();
        }
    }
})();