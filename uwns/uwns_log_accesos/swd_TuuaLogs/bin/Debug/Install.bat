@ECHO OFF

REM The following directory is for .NET 2.0
set DOTNETFX2=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727
set PATH=%PATH%;%DOTNETFX2%

echo Instalando TuuaLogs...
echo ----------------------------------------------------
InstallUtil /i D:\bin\swd_TuuaLogs.exe
echo ----------------------------------------------------
echo OK, Instalación completada ... 
pause