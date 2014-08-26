#!/bin/bash
killall node 2&> /dev/null
coffee --no-header -j scripts/app.js -bcw scripts/*.coffee &
coffee --no-header -j scripts/controllers.js -bcw scripts/controllers/*.coffee &
coffee --no-header -j scripts/services.js -bcw scripts/services/*.coffee &
