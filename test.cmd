@echo off
pushd "%~dp0"
call :main %*
popd
goto :EOF

:main
setlocal
call build ^
  && call :test netcoreapp2.0 Debug ^
  && call :test netcoreapp2.0 Release ^
  && call :test net45 Debug ^
  && call :test net45 Release
goto :EOF

:test
setlocal
echo Testing %1 (%2)...
set XUNIT_RUNNER=tools\xunit.runner.console\tools\net452\xunit.console.exe
if %1==net45 (
    if not exist "%XUNIT_RUNNER%" (
        nuget install xunit.runner.console -Version 2.3.1 -Output tools ^
            && ren tools\xunit.runner.console.2.3.1 xunit.runner.console ^
            || exit /b 1
    )
    "%XUNIT_RUNNER%" tests\bin\%2\net45\Mannex.Tests.dll
) else (
    dotnet test --no-build --no-restore -f %1 -c %2 tests
)
goto :EOF
