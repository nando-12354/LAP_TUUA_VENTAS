IF EXISTS (SELECT [name] 
           FROM sys.assemblies 
           WHERE [name] = N'CLRVuelosProgramados.XmlSerializers')
DROP ASSEMBLY [CLRVuelosProgramados.XmlSerializers] 
WITH NO DEPENDENTS