<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html>
<html lang="en" ng-app="mainApp">
<head>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Project</title>
    <link rel="stylesheet" type="text/css" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css"/>
    <link rel="stylesheet" type="text/css" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap-theme.min.css"/>
    <link rel="stylesheet" type="text/css" href="//getbootstrap.com/examples/starter-template/starter-template.css"/>
    <script type="text/javascript" src="Content/angular/angular.min.js"></script>
    <script type="text/javascript" src="Content/angular-bootstrap/ui-bootstrap.min.js"></script>
    <script type="text/javascript" src="Content/angular-route/angular-route.min.js"></script>
    <script type="text/javascript" src="Content/angular-resource/angular-resource.min.js"></script>
    <script type="text/javascript" src="Content/js/app.js"></script>
    <script type="text/javascript" src="Content/js/controllers.js"></script>
    <script type="text/javascript" src="Content/js/services.js"></script>
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button class="navbar-toggle" data-target=".navbar-collapse" data-toggle="collapse" type="button"></button>
                <a class="navbar-brand" href="#">Project</a>
            </div>
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li class="active"><a href="#/user/list">Users</a></li>
                    <li class="active"><a href="#/product/list">Products</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="starter-template">
            <div ng-view></div>
        </div>
    </div>
</body>
</html>
