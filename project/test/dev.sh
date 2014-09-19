#!/bin/bash
killall node 2&> /dev/null
coffee --no-header -bcw *.coffee
