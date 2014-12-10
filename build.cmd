@echo off

REM Mannex - Extension methods for .NET
REM Copyright (c) 2009 Atif Aziz. All rights reserved.
REM
REM  Author(s):
REM
REM      Atif Aziz, http://www.raboof.com
REM
REM Licensed under the Apache License, Version 2.0 (the "License");
REM you may not use this file except in compliance with the License.
REM You may obtain a copy of the License at
REM
REM    http://www.apache.org/licenses/LICENSE-2.0
REM
REM Unless required by applicable law or agreed to in writing, software
REM distributed under the License is distributed on an "AS IS" BASIS,
REM WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
REM See the License for the specific language governing permissions and
REM limitations under the License.

@echo off
setlocal
cd "%~dp0"
set MSBUILD=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
if not exist "%MSBUILD%" (
    echo The .NET Framework 4.0 does not appear to be installed on this 
    echo machine, which is required to build the solution.
    exit /b 1
)
call :build 3.5 Debug   %* && ^
call :build 3.5 Release %* && ^
call :build 4.0 Debug   %* && ^
call :build 4.0 Release %* && ^
call :build 4.5 Debug   %* && ^
call :build 4.5 Release %*
goto :EOF

:build
"%MSBUILD%" Mannex.sln /p:Configuration="NETFX %1 %2" /v:m %3 %4 %5 %6 %7 %8 %9
exit /b %errorlevel%

