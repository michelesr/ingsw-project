<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html>
<html lang="en" ng-app="myApp">
<head runat="server">
    <meta charset="utf-8"></meta>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"></meta>
    <meta name="viewport" content="width=device-width, initial-scale=1"></meta>
    <meta name="author" content="Antonio Esposito, Michele Sorcinelli"></meta>
    <meta name="description" content="A simple web app writter in Angular.js and ASP.NET"></meta>
	<title>Project</title>
	<link rel="stylesheet" type="text/css" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css"></link>
	<link rel="stylesheet" type="text/css" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap-theme.min.css"></link>
    <link rel="stylesheet" type="text/css" href="//getbootstrap.com/examples/starter-template/starter-template.css"></link>
</head>
<body ng-controller="myController as store">
    <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button class="navbar-toggle" data-target=".navbar-collapse" data-toggle="collapse" type="button"></button>
                <a class="navbar-brand" href="#">ciao</a>
            </div>
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li class="active"><a href="#">About</a></li>
                    <li class="active"><a href="#">Contacts</a></li>
                    <li class="active"><a href="#">Users</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="starter-template">
            <h1>Project</h1>
            <p>
                <%= Html.Encode(ViewData["Message"]) %><br></br>
                <%= Html.Encode(ViewData["dbOut"]) %><br></br>
                Test: 2 + 2 = {{2 + 2}}<br></br>
                Models: - store.product.name: {{ store.product.name }}
            </p>
        </div>
    </div>
	<script type="text/javascript" src="Content/angular/angular.min.js"></script>
	<script type="text/javascript" src="Content/app.js"></script>
</body>
</html>
