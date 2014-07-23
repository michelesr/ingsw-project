#!/bin/bash
coffee -co js/ *.coffee
echo
echo '============================='
echo '= Generated from app.coffee ='
echo '============================='
echo
cat js/app.js
echo
echo '====================================='
echo '= Generated from controllers.coffee ='
echo '====================================='
echo
cat js/controllers.js
echo
