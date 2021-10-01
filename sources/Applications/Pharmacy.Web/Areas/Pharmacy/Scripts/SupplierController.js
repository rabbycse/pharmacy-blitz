app.controller('SupplierController', ['$scope', '$http', '$filter', 'appMessage', function ($scope, $http, $filter, appMessage) {

    $scope.supplier = [];
    $scope.supplier.Id = 0;
    $scope.alerts = [];
    $scope.submitted = false;
    $scope.IsProcessing = false;
    //======================= Alerts =======================

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    //======================= Get parameter id from url=======================
    var paramId = location.search.substr(4);

    $scope.changeFocus = function (fieldName) {
        var field = document.getElementsByName(fieldName);
        field[0].focus();
        field[0].select();
    };

    $scope.save = function () {
        $scope.IsProcessing = true;
        if (!$scope.frmSupplier.$valid) {
            $scope.IsProcessing = false;
            $scope.alerts.push({ 'type': 'warning', 'msg': appMessage.required });
            return;
        }
        $http({
            method: "POST",
            url: "/Pharmacy/Supplier/SaveSupplier",
            data: $scope.supplier
        }).then(function (response) {
            $scope.supplier.Id = response.Id;
            $scope.submitted = false;
            $scope.alerts.push({ 'type': 'success', 'msg': appMessage.save });
        }, function errorCallback(response) {
            $scope.alerts.push({ 'type': 'danger', 'msg': appMessage.failure });
        });
    };

    if (paramId != "") {
        $http({
            method: "GET",
            url: "/Pharmacy/Supplier/GetSupplierById?Id=" + (paramId)
        }).then(function (response) {
            $scope.supplier = response.data;
        }, function errorCallback(response) {
            $scope.alerts.push({ 'type': 'danger', 'msg': appMessage.failure });
        });
    }

    $scope.update = function () {
        $http({
            method: "POST",
            url: "/Pharmacy/Supplier/UpdateSupplier",
            data: $scope.supplier
        }).then(function (response) {
            $scope.submitted = false;
            $scope.alerts.push({ 'type': 'success', 'msg': appMessage.update });
        }, function errorCallback(response) {
            $scope.alerts.push({ 'type': 'danger', 'msg': appMessage.failure });
        });
    };

}]);



