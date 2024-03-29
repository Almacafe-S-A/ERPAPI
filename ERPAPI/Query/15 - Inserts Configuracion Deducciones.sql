﻿USE ERP
GO

SET IDENTITY_INSERT GrupoConfiguracion ON
GO
INSERT INTO GrupoConfiguracion(IdConfiguracion,Nombreconfiguracion,Tipoconfiguracion, IdZona, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(90,'Configuración ISR','RRHH',0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO GrupoConfiguracion(IdConfiguracion,Nombreconfiguracion,Tipoconfiguracion, IdZona, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(95,'Configuración RAP','RRHH',0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO GrupoConfiguracion(IdConfiguracion,Nombreconfiguracion,Tipoconfiguracion, IdZona, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(100,'Configuración IHSS','RRHH',0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
SET IDENTITY_INSERT GrupoConfiguracion OFF
GO

SET IDENTITY_INSERT ElementoConfiguracion ON
GO
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(123,'CUOTAS ISR','En cuantas cuotas se va deducir el ISR a los empleados',1,'Activo',90, 12,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(124,'TECHO RAP','Salario minimo para aportación al RAP',1,'Activo',95, 8882.30,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(125,'PORCENTAJE RAP EMPLEADO','Porcentaje aportación RAP, empleado',1,'Activo',95, 1.5,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(126,'PORCENTAJE RAP EMPLEADOR','Porcentaje aportación RAP, empleador',1,'Activo',95, 1.5,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(127,'TECHO RAP FACTOR','Factor de multiplicación para aportación cesantia',1,'Activo',95, 3,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(128,'TECHO IHSS IVM','Techo maximo IHSS IVM',1,'Activo',100, 9326.42,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(129,'IHSS IVM EMPLEADO','Porcentaje aportación IHSS IVM Empleado',1,'Activo',100, 2.5,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(130,'IHSS IVM EMPLEADOR','Porcentaje aportación IHSS IVM Empleador',1,'Activo',100, 3.5,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(131,'TECHO IHSS SALUD','Techo maximo IHSS SALUD',1,'Activo',100, 8933.97,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(132,'IHSS SALUD EMPLEADO','Porcentaje aportación IHSS Salud Empleado',1,'Activo',100, 2.5,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(133,'IHSS SALUD EMPLEADOR','Porcentaje aportación IHSS Salud Empleado',1,'Activo',100, 3.5,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

SET IDENTITY_INSERT ElementoConfiguracion OFF
GO


SET IDENTITY_INSERT GrupoEstado ON
GO
INSERT INTO GrupoEstado (Id,Nombre,Modulo,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(5,'Estado Biometrico','RRHH','2020-01-01','2020-01-01','erp@bi-dss.com','erp@bi-dss.com')
GO
SET IDENTITY_INSERT GrupoEstado OFF
GO


SET IDENTITY_INSERT Estados ON
GO
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(60,'Cargado','Archivo Biometrico Cargado',5,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(61,'Rechazado','Archivo Biometrico Rechazado',5,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(62,'Aprobado','Archivo Biometrico Aprobado',5,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
GO
SET IDENTITY_INSERT Estados OFF
GO

SET IDENTITY_INSERT Deduction ON
GO

INSERT INTO Deduction (DeductionId,[Description],DeductionTypeId,DeductionType,Formula,Fortnight,FechaCreacion,FechaModificacion,UsuarioModificacion,UsuarioCreacion,EsPorcentaje)
VALUES(1,'ISR',1,'Por Ley',0,2,GetDate(),GetDate(),'erp@bi-dss.com','erp@bi-dss.com',0)
INSERT INTO Deduction (DeductionId,[Description],DeductionTypeId,DeductionType,Formula,Fortnight,FechaCreacion,FechaModificacion,UsuarioModificacion,UsuarioCreacion,EsPorcentaje)
VALUES(2,'RAP',1,'Por Ley',0,2,GetDate(),GetDate(),'erp@bi-dss.com','erp@bi-dss.com',0)
INSERT INTO Deduction (DeductionId,[Description],DeductionTypeId,DeductionType,Formula,Fortnight,FechaCreacion,FechaModificacion,UsuarioModificacion,UsuarioCreacion,EsPorcentaje)
VALUES(3,'IHSS',1,'Por Ley',0,2,GetDate(),GetDate(),'erp@bi-dss.com','erp@bi-dss.com',0)
INSERT INTO Deduction (DeductionId,[Description],DeductionTypeId,DeductionType,Formula,Fortnight,FechaCreacion,FechaModificacion,UsuarioModificacion,UsuarioCreacion,EsPorcentaje)
VALUES(4,'IMP. VECINAL',1,'Por Ley',0,2,GetDate(),GetDate(),'erp@bi-dss.com','erp@bi-dss.com',0)


SET IDENTITY_INSERT Deduction OFF
GO

SET IDENTITY_INSERT ISRConfiguracion ON
GO

INSERT INTO ISRConfiguracion(Id, De, Hasta, Tramo, Porcentaje, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(1, 0,165482.06,'Sin Pago',0,GetDate(),GetDate(),'erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ISRConfiguracion(Id, De, Hasta, Tramo, Porcentaje, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(2, 165482.07,252330.80,'Escala 1',15,GetDate(),GetDate(),'erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ISRConfiguracion(Id, De, Hasta, Tramo, Porcentaje, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(3, 252330.81,586815.84,'Escala 2',20,GetDate(),GetDate(),'erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ISRConfiguracion(Id, De, Hasta, Tramo, Porcentaje, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(4, 586815.85,10000000,'Escala 3',25,GetDate(),GetDate(),'erp@bi-dss.com','erp@bi-dss.com')

SET IDENTITY_INSERT ISRConfiguracion OFF
GO


SET IDENTITY_INSERT GrupoConfiguracion ON
GO

INSERT INTO GrupoConfiguracion(IdConfiguracion,Nombreconfiguracion,Tipoconfiguracion, IdZona, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(110,'Parametros RRHH','RRHH',0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
GO

SET IDENTITY_INSERT GrupoConfiguracion OFF
GO

SET IDENTITY_INSERT ElementoConfiguracion ON
GO
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(140,'MINUTOS DE GRACIA ENTRADA','Minutos de gracia para considerar una llegada tarde',1,'Activo',110, 5,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(141,'MINUTOS DE GRACIA SALIDA','Minutos de gracia para considerar una salida tarde, posible hora extra',1,'Activo',110, 60,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
GO
SET IDENTITY_INSERT ElementoConfiguracion OFF
GO


SET IDENTITY_INSERT GrupoEstado ON
GO
INSERT INTO GrupoEstado (Id,Nombre,Modulo,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(10,'Estado Llegadas Tarde Horas Extra','RRHH','2020-01-01','2020-01-01','erp@bi-dss.com','erp@bi-dss.com')
GO
SET IDENTITY_INSERT GrupoEstado OFF
GO


SET IDENTITY_INSERT Estados ON
GO
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(70,'Cargado','Registro Cargado',10,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(71,'Aprobado','Registro Aprobado',10,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(72,'Rechazado','Registro Rechazado',10,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
GO
SET IDENTITY_INSERT Estados OFF
GO


SET IDENTITY_INSERT GrupoConfiguracion ON
GO

INSERT INTO GrupoConfiguracion(IdConfiguracion,Nombreconfiguracion,Tipoconfiguracion, IdZona, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(120,'Tipos de Inasistencias','RRHH',0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
GO

SET IDENTITY_INSERT GrupoConfiguracion OFF
GO

SET IDENTITY_INSERT ElementoConfiguracion ON
GO
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(150,'DESCONOCIDO','Se desconoce la razón de la inasistencia',1,'Activo',120, 0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(151,'LLEGADA TARDE','Llegada Tarde',1,'Activo',120, 0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(152,'DOMINGO O DÍA LIBRE','Domingo o Día Libre',1,'Activo',120, 0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(153,'INCAPACIDAD','Incapacidad',1,'Activo',120, 0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(154,'VACACIONES','Vacaciones',1,'Activo',120, 0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(155,'PERMISO','Permiso',1,'Activo',120, 0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

GO
SET IDENTITY_INSERT ElementoConfiguracion OFF
GO

SET IDENTITY_INSERT GrupoEstado ON
GO
INSERT INTO GrupoEstado (Id,Nombre,Modulo,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(20,'Estados Inasistencias','RRHH','2020-01-01','2020-01-01','erp@bi-dss.com','erp@bi-dss.com')
GO
SET IDENTITY_INSERT GrupoEstado OFF
GO


SET IDENTITY_INSERT Estados ON
GO
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(80,'Creado','Registro Creado',20,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(81,'Anulado','Registro Anulado',20,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(82,'Aprobado','Registro Aprobado',20,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(83,'Rechazado','Registro Rechazado',20,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
GO
SET IDENTITY_INSERT Estados OFF
GO

SET IDENTITY_INSERT GrupoEstado ON
GO
INSERT INTO GrupoEstado (Id,Nombre,Modulo,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(30,'Estados Bonificaciones','RRHH','2020-01-01','2020-01-01','erp@bi-dss.com','erp@bi-dss.com')
GO
SET IDENTITY_INSERT GrupoEstado OFF
GO


SET IDENTITY_INSERT Estados ON
GO
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(90,'Activo','Registro Activo',30,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
INSERT INTO Estados (IdEstado,NombreEstado,DescripcionEstado,IdGrupoEstado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion)
VALUES(91,'Anulado','Registro Anulado',30,'erp@bi-dss.com','erp@bi-dss.com','2020-01-01','2020-01-01')
GO
SET IDENTITY_INSERT Estados OFF
GO

SET IDENTITY_INSERT TiposBonificaciones ON
GO
INSERT INTO TiposBonificaciones (Id, Nombre, EstadoId, FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(1,'Bono Estudiantil',90,'2020-01-01','2020-01-01','erp@bi-dss.com','erp@bi-dss.com')
GO
SET IDENTITY_INSERT TiposBonificaciones OFF
GO

SET IDENTITY_INSERT GrupoConfiguracion ON
GO
INSERT INTO GrupoConfiguracion(IdConfiguracion,Nombreconfiguracion,Tipoconfiguracion, IdZona, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion)
VALUES(130,'Planillas','RRHH',0,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
GO
SET IDENTITY_INSERT GrupoConfiguracion OFF
GO

SET IDENTITY_INSERT ElementoConfiguracion ON
GO
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(160,'SALARIO MINIMO','Salario minimo aprobado por ALMACAFE',1,'Activo',130, 9271.95,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
GO
SET IDENTITY_INSERT ElementoConfiguracion OFF
GO

SET IDENTITY_INSERT ElementoConfiguracion ON
GO
INSERT INTO ElementoConfiguracion(Id,Nombre,Descripcion,IdEstado,Estado,Idconfiguracion,Valordecimal,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion)
VALUES(161,'PORC. RAP CESANTIA','Porcentaje de Aportación Mensual del Patrono para fondo de cesantias',1,'Activo',95, 4,'2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
SET IDENTITY_INSERT ElementoConfiguracion OFF
GO

SET IDENTITY_INSERT ImpuestoVecinalConfiguraciones ON
GO

INSERT INTO ImpuestoVecinalConfiguraciones (Id, De, Hasta, FactorMillar, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) 
VALUES(1, 1, 5000, 1.5, '2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ImpuestoVecinalConfiguraciones (Id, De, Hasta, FactorMillar, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) 
VALUES(2, 5001, 10000, 2.0, '2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ImpuestoVecinalConfiguraciones (Id, De, Hasta, FactorMillar, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) 
VALUES(3, 10001, 20000, 2.5, '2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ImpuestoVecinalConfiguraciones (Id, De, Hasta, FactorMillar, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) 
VALUES(4, 20001, 30000, 3.0, '2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ImpuestoVecinalConfiguraciones (Id, De, Hasta, FactorMillar, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) 
VALUES(5, 30001, 50000, 3.5, '2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ImpuestoVecinalConfiguraciones (Id, De, Hasta, FactorMillar, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) 
VALUES(6, 50001, 75000, 3.75, '2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ImpuestoVecinalConfiguraciones (Id, De, Hasta, FactorMillar, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) 
VALUES(7, 75001, 100000, 4.00, '2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ImpuestoVecinalConfiguraciones (Id, De, Hasta, FactorMillar, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) 
VALUES(8, 100001, 150000, 5.00, '2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')
INSERT INTO ImpuestoVecinalConfiguraciones (Id, De, Hasta, FactorMillar, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) 
VALUES(9, 150001, 9999999, 5.25, '2020-01-31','2020-01-31','erp@bi-dss.com','erp@bi-dss.com')

SET IDENTITY_INSERT ImpuestoVecinalConfiguraciones OFF
GO

UPDATE Employees SET IdTipoPlanilla = null
GO

DELETE FROM TipoPlanillas
GO

