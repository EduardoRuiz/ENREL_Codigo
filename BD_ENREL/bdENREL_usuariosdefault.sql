USE [BDENREL_QA]
GO

INSERT INTO [dbo].[CatEmpresas] VALUES('SENER-ADM','','',1,'',1,'',1,'',1,1,1,1,'',1,'',1,'',1,'','','2016-01-01','2016-01-01','2016-01-01',1)
INSERT INTO [dbo].[CatUsuarios] VALUES(1,4,'Administrador1','4fff15d32e9451fc92985e8ff8fa799035b918e733402017a82814c607f249c63abf242f04f80959888d1c38ac64f53693c084f0c3b415373dd1385f6c8e6235','administrador@sindominio.com','2016-01-01',getdate(),1, null, null,0)

CREATE USER inere FOR LOGIN inere
GRANT SELECT ON dbo.ProyectosINERE TO inere

--exec sp_addrolemember 'db_owner', 'inere'
exec sp_addrolemember 'db_datareader', 'inere'
--exec sp_addrolemember 'db_datawriter', 'inere'
