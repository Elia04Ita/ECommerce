#!/bin/bash

rem Delete the "obj" folder
rd /s /q obj

rem Delete the "bin" folder
rd /s /q bin

del "ECommerce.sln"

git add .

commit_message="Test"

git commit -m Vipregobasta

git push

del "ECommerce.sln"

pause