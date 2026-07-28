@echo off
setlocal

echo =========================================
echo       AUTOMATIZACION DE GIT COMMIT & PUSH
echo =========================================
echo.

:: 1. Mostrar estado de los archivos
echo [*] Estado actual del repositorio:
git status -s
echo.

:: 2. Pedir mensaje de commit
set /p COMMIT_MSG="[?] Ingrese el mensaje del commit: "

:: Verificar que el mensaje no este vacio
if "%COMMIT_MSG%"=="" (
    echo.
    echo [X] Error: El mensaje del commit no puede estar vacio.
    pause
    exit /b
)

:: 3. Pedir la rama (por defecto 'main', podes cambiarla si usas 'master' o 'dev')
set RAMA=main
set /p RAMA="[?] Ingrese la rama de destino (presione Enter para '%RAMA%'): "

echo.
echo [*] Agregando archivos al stage (git add .)...
git add .

echo [*] Creando commit...
git commit -m "%COMMIT_MSG%"

echo [*] Subiendo cambios a origin/%RAMA%...
git push origin %RAMA%

echo.
echo =========================================
echo   ¡Proceso completado con exito!
echo =========================================
echo.

pause