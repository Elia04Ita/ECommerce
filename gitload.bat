#!/bin/bash

rem Delete the "obj" folder
rd /s /q obj

rem Delete the "bin" folder
rd /s /q bin

del "ECommerce.sln"

git add .

git commit -am "Test"

git push

del "ECommerce.sln"
