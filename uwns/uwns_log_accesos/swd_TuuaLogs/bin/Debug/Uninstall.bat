@ECHO OFF

REM The following directory is for .NET 2.0
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727
set PATH=%PATH%;%DOTNETFX2%

echo Desinstalando TuuaLogs...
echo ----------------------------------------------------
InstallUtil /u "C:\Users\eochoa\Desktop\TUUA_WEB v1.4.3.4\modulo servicios generales e interfaces\swd_TuuaLogs\swd_TuuaLogs\bin\Debug\swd_TuuaLogs.exe"
echo ----------------------------------------------------
echo OK, Desinstalación completada ... 