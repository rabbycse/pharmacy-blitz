app.controller('MedicineController', ['$scope', '$http', '$filter', 'appMessage', function ($scope, $http, $filter, appMessage) {

    $scope.medicine = [];
    $scope.medicine.Id = 0;
    $scope.alerts = [];
    $scope.submitted = false;
    $scope.IsProcessing = false;
    //======================= Alerts =======================

    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    $scope.options = [
        {name: "abcd"},
        {name: "abcd"},
        {name: "abcd"},
        {name: "abcd"},
        {name: "abcd"},
        {name: "abcd"},
        {name: "abcd"},
        {name: "abcd"}
    ]

    $scope.config = {
        modelLabel: "label",
        optionLabel: "title"
    }
    //======================= Get parameter id from url=======================
    var paramId = location.search.substr(4);

    $scope.changeFocus = function (fieldName) {
        var field = document.getElementsByName(fieldName);
        field[0].focus();
        field[0].select();
    };

    $scope.save = function () {
        $scope.IsProcessing = true;
        if (!$scope.frmMedicine.$valid) {
            $scope.IsProcessing = false;
            $scope.alerts.push({ 'type': 'warning', 'msg': appMessage.required });
            return;
        }
        $http({
            method: "POST",
            url: "/Pharmacy/Medicine/SaveMedicine",
            data: $scope.medicine
        }).then(function (response) {
            $scope.medicine.Id = response.Id;
            $scope.submitted = false;
            $scope.alerts.push({ 'type': 'success', 'msg': appMessage.save });
        }, function errorCallback(response) {
            $scope.alerts.push({ 'type': 'danger', 'msg': appMessage.failure });
        });
    };

    if (paramId != "") {
        $http({
            method: "GET",
            url: "/Pharmacy/Supplier/GetMedicineById?Id=" + (paramId)
        }).then(function (response) {
            $scope.medicine = response.data;
        }, function errorCallback(response) {
            $scope.alerts.push({ 'type': 'danger', 'msg': appMessage.failure });
        });
    }

    $scope.update = function () {
        $http({
            method: "POST",
            url: "/Pharmacy/Supplier/UpdateMedicine",
            data: $scope.medicine
        }).then(function (response) {
            $scope.submitted = false;
            $scope.alerts.push({ 'type': 'success', 'msg': appMessage.update });
        }, function errorCallback(response) {
            $scope.alerts.push({ 'type': 'danger', 'msg': appMessage.failure });
        });
    };

}]);



