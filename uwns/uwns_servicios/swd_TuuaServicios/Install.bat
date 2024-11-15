@ECHO OFF

REM The following directory is for .NET 2.0
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727
set PATH=%PATH%;%DOTNETFX2%

echo Instalando TuuaServicios...
echo ----------------------------------------------------
InstallUtil /i "C:\PROYECTOS\LAP-TUUA-8183\V 1.4.4.2\Fuentes\modulo servicios generales e interfaces\swd_TuuaServicios\bin\Debug\swd_TuuaServicios.exe"
echo ----------------------------------------------------
echo OK, Instalación completada ... 