app.controller('SupplierListController', ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    var link = document.querySelector('a[href="/Pharmacy/Supplier/Index"]');
    link.style.display = 'none';
    $scope.filteredSuppliers = [];
    $scope.itemsPerPage = 4;
    $scope.currentPage = 1;
    $scope.maxSize = 4;
    $scope.DataList = [];

    $scope.GetSuppliers = function () {
        $http({
            method: "GET",
            url: "/Pharmacy/Supplier/GetAllSuppliers"

        }).then(function (response) {
            $scope.Suppliers = response.data;
            $scope.DataList = response.data;
            $scope.figureOutSupplierToDisplay();

        }, function errorCallback(response) {
        });
    };

    $scope.figureOutSupplierToDisplay = function () {
        var begin = (($scope.currentPage - 1) * $scope.itemsPerPage);
        var end = begin + $scope.itemsPerPage;
        $scope.filteredSuppliers = $scope.DataList.slice(begin, end);
    };

    $scope.GetSuppliers();


    $scope.pageChanged = function () {
        $scope.figureOutSupplierToDisplay();
    };

    $scope.searchFromList = function () {
        $scope.DataList = $filter('filter')($scope.Suppliers, $scope.searchText);
        $scope.figureOutSupplierToDisplay();
    };

}]);