INSERT INTO TUA_ParameGeneral (Identificador, Descripcion, TipoParametro, TipoParametroDet, TipoValor, Valor, ValorMin, ValorMax, ValorDefault,
                               CampoLista, IdentificadorPadre, Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, xml, Num_Indice)
VALUES (N'SFAV', N'Configuración Ruta Generación de Archivo de Ventas SFTP', N'I', N'0', N'TXT',
        N'modo=1;host=192.168.0.157;user=sftp_tuua;password=tuua;puerto=22;directorio=/var/sftp/tuua/;path=C:\LAP_PRD\LAP_INTRANET\SAP_TUUA',
        null, null, null, null, N'CAV', N'U000070', N'20220822', N'124451', null, null);


INSERT INTO TUA_ParameGeneral (Identificador, Descripcion, TipoParametro, TipoParametroDet, TipoValor,
                                               Valor, ValorMin, ValorMax, ValorDefault, CampoLista, IdentificadorPadre,
                                               Log_Usuario_Mod, Log_Fecha_Mod, Log_Hora_Mod, xml, Num_Indice)
VALUES (N'MEAT', N'Modo de envío de Archivos de Venta', N'I', N'0', N'TXT', N'FTP', null, null, null, null, N'CAV',
        N'U000070', N'20220822', N'124451', null, null);