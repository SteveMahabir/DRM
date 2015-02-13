USE master
GO

CREATE LOGIN [IIS APPPOOL\Info3067Pool] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
USE eStoreDB
GO
CREATE USER [WebSiteUser] FOR LOGIN [IIS APPPOOL\Info3067Pool] 
GO
EXEC sp_addrolemember 'db_datareader', 'WebSiteUser'
GO
EXEC sp_addrolemember 'db_datawriter', 'WebSiteUser'
GO
GRANT EXECUTE TO [WebSiteUser]
GO