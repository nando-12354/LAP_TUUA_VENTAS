@ECHO OFF

REM The following directory is for .NET 2.0
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727
set PATH=%PATH%;%DOTNETFX2%

echo Desinstalando TuuaServicios...
echo ----------------------------------------------------
InstallUtil /u "C:\Archivos de programa\HIPER\App_Acceso\swd_TuuaAccesos.exe"
echo ----------------------------------------------------
echo OK, Desinstalación completada ... 