#!/bin/bash
jade -P base.jade
jade -Po partials partials/*.jade
jade -Po partials/resource partials/resource/*.jade
