import angular from 'angular';

    'use strict';
    var controllerId = 'changePassword';
export default angular.module('app')
            .controller(controllerId, ['$http', 'common',changePassword]).name;

    function changePassword($http,common) {
        var vm = this;
        var log = common.logger.getLogFn(controllerId);
        vm.user = {
            oldPassword: '',
            newPassword: '',
            confirmPassword:''
        };
        vm.errors = '';
        vm.passwordRequired = true;
        vm.submit = submit;

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () {
                    log('Activated Change Password View');
                });
        }

        function submit(credentials) {
            vm.errors = '';
            $http({
                method: 'POST',
                url: 'api/Account/ChangePassword',
                data: mapObjectToPascalCase(credentials)
            }).then(function (_response) {
                log.success('password changed');
            }, function (response) {
                log.error({ msg: 'change password error', data: response });
                vm.errors = ''; //todo here
            });
        }

        function mapObjectToPascalCase(obj) {
            var returnVar = {};
            for (var propName in obj) {
                if (propName.length) {
                    returnVar[propName[0].toUpperCase() + propName.substr(1)] = obj[propName];
                }
            }
            return returnVar;
        }
    }
