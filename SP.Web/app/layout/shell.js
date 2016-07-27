﻿(function () { 
    'use strict';
    
    var controllerId = 'shell';
    angular.module('app').controller(controllerId,
        ['$rootScope', 'common', 'config', 'tokenStorageService', 'AUTH_EVENTS',shell]);

    function shell($rootScope, common, config, tokenStorageService, AUTH_EVENTS) {
        var vm = this;
        var log = common.logger.getLogFn(controllerId);
        var events = config.events;
        //vm.busyMessage = 'Please wait ...';
        vm.isBusy = true;
        vm.spinnerOptions = {
            radius: 40,
            lines: 7,
            length: 0,
            width: 30,
            speed: 1.7,
            corners: 1.0,
            trail: 100,
            color: '#F58A00'
        };

        activate();

        function activate() {
            log.success('Simulation Planner loaded!');
            common.activateController([], controllerId);
        }

        function toggleSpinner(on) { vm.isBusy = on; }

        $rootScope.$on('$routeChangeStart',newRoute);

        var _isGuid = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/;
        function newRoute(event, next, current) {
            if (event.defaultPrevented) { return; }
            if (current.params && current.params.id === "new" && next.params && _isGuid.test(next.params.id)) {
                return;
            }
            toggleSpinner(true);
        }
        
        $rootScope.$on(events.controllerActivateSuccess,
            function (data) { toggleSpinner(false); }
        );

        $rootScope.$on(events.spinnerToggle,
            function (data) { toggleSpinner(data.show); }
        );

    }
})();