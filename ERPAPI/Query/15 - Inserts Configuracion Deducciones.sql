SET IDENTITY_INSERT GrupoConfiguracion ON
GO
INSERT INTO GrupoConfiguracion(IdConfiguracion,Nombreconfiguracion,Tipoconfiguracion, IdZona, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(80,'Tipos de Deducciones RRHH','RRHH',0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
SET IDENTITY_INSERT GrupoConfiguracion OFF
GO

SET IDENTITY_INSERT ElementoConfiguracion ON
GO
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(120,'POR LEY','Deducciones por ley',1,'Activo',80, 1,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(121,'EVENTUAL','Deducciones eventual a aplicar a un empleado',1,'Activo',80, 2,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
SET IDENTITY_INSERT ElementoConfiguracion OFF
GO