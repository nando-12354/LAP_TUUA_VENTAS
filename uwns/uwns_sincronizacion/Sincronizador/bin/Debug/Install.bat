@ECHO OFF

REM The following directory is for .NET 2.0
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727
set PATH=%PATH%;%DOTNETFX2%

echo Instalando TuuaServicios...
echo ----------------------------------------------------
InstallUtil /i "G:\PROYECTOS\LAP-TUUA-6533\FUENTES\SINCRO\Sincronizador\bin\Debug\Sincronizador.exe"
echo ----------------------------------------------------
echo OK, Instalaci�n completada ... 