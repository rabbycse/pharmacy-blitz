var app = angular.module('plunker', []);

app.config(function($routeProvider) {
  $routeProvider
    .when("/", {
      template: "<b>Home!</b>"
    }) 
    .when("/1", {
      template: "<b>Page 1!</b>"
    })
    .when("/2", {
      template: "<b>Page 2!</b>"
    })
    .otherwise({
      redirectTo: "/"
    });
});

app.controller('MainCtrl', function($scope, flash) {
  $scope.msgTitle = 'Alert';
  $scope.msgBody  = 'The Tomatoes Exploded!';
  $scope.msgType  = 'warning';

  $scope.flash = flash;
}); 
 
app.factory("flash", function($rootScope) {

  return {

    pop: function(message) {
      switch(message.type) {
        case 'success':
          toastr.success(message.body, message.title);
          break;
        case 'info':
          toastr.info(message.body, message.title);
          break;
        case 'warning':
          toastr.warning(message.body, message.title);
          break;
        case 'error':
          toastr.error(message.body, message.title);
          break;
      }
    }
  };
});