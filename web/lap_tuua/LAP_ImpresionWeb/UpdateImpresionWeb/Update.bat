@echo off

echo Copiando archivo RXTXcomm.jar
copy .\lib\RXTXcomm.jar C:\Archiv~1\Java\jre6\lib\ext

echo Copiando archivo jdom.jar
copy .\lib\jdom.jar C:\Archiv~1\Java\jre6\lib\ext

echo Copiando archivo rxtxSerial.dll
copy .\lib\rxtxSerial.dll C:\Archiv~1\Java\jre6\bin

@pause