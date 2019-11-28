/*USUARIO INICIAL
	"UserEmail": "erp@bi-dss.com"
    "UserPassword": "Aa123456!"
*/

SET IDENTITY_INSERT [dbo].[GrupoEstado] ON 
GO
INSERT [dbo].[GrupoEstado] ([Id], [Nombre], [Modulo], [FechaCreacion], [FechaModificacion], [UsuarioCreacion], [UsuarioModificacion]) VALUES (1, N'Activo/Inactivo', N'Todos', CAST(N'2019-03-15T17:21:16.6966667' AS DateTime2), CAST(N'2019-03-15T17:21:16.6966667' AS DateTime2), N'freddy.chinchilla@bi-dss.com', N'freddy.chinchilla@bi-dss.com')
GO
SET IDENTITY_INSERT [dbo].[GrupoEstado] OFF
GO



SET IDENTITY_INSERT [dbo].[Estados] ON 
GO
INSERT [dbo].[Estados] ([IdEstado], [NombreEstado], [DescripcionEstado], [IdGrupoEstado], [UsuarioCreacion], [UsuarioModificacion], [FechaCreacion], [FechaModificacion]) VALUES (1, N'Activo', N'Activo', 1, N'freddy.chinchilla@bi-dss.com', N'celia.suazo@bi-dss.com', CAST(N'2019-03-15T17:21:16.6966667' AS DateTime2), CAST(N'2019-08-11T19:17:11.7570904' AS DateTime2))
GO
INSERT [dbo].[Estados] ([IdEstado], [NombreEstado], [DescripcionEstado], [IdGrupoEstado], [UsuarioCreacion], [UsuarioModificacion], [FechaCreacion], [FechaModificacion]) VALUES (2, N'Inactivo', N'Estado Inactivo', 1, N'freddy.chinchilla@bi-dss.com', N'tania.sosa@bi-dss.com', CAST(N'2019-03-15T17:21:16.7000000' AS DateTime2), CAST(N'2019-09-02T14:43:28.9760922' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Estados] OFF
GO


SET IDENTITY_INSERT [dbo].[Currency] ON 
GO
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [IdEstado], [Estado], [CurrencyCode], [Description], [UsuarioCreacion], [UsuarioModificacion], [FechaCreacion], [FechaModificacion]) VALUES (1, N'Lempiras', 0, NULL, N'LPS', N'Lempiras', NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [IdEstado], [Estado], [CurrencyCode], [Description], [UsuarioCreacion], [UsuarioModificacion], [FechaCreacion], [FechaModificacion]) VALUES (2, N'Dolares americanos', 1, N'Activo', N'USD', N'Dolares americanos', NULL, N'celia.suazo@bi-dss.com', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2019-08-16T11:44:54.9526747' AS DateTime2))
GO
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [IdEstado], [Estado], [CurrencyCode], [Description], [UsuarioCreacion], [UsuarioModificacion], [FechaCreacion], [FechaModificacion]) VALUES (4, N'Euros', 1, N'Activo', N'€', N'Euros (EU)', N'celia.suazo@bi-dss.com', N'celia.suazo@bi-dss.com', CAST(N'2019-08-16T11:34:32.4805562' AS DateTime2), CAST(N'2019-08-30T12:59:44.9381546' AS DateTime2))
GO
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [IdEstado], [Estado], [CurrencyCode], [Description], [UsuarioCreacion], [UsuarioModificacion], [FechaCreacion], [FechaModificacion]) VALUES (11, N'6521876387', 2, N'Inactivo', N'española', N'Nínguna', N'celia.suazo@bi-dss.com', N'celia.suazo@bi-dss.com', CAST(N'2019-08-30T13:12:18.0394000' AS DateTime2), CAST(N'2019-08-30T13:12:18.0394006' AS DateTime2))
GO
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [IdEstado], [Estado], [CurrencyCode], [Description], [UsuarioCreacion], [UsuarioModificacion], [FechaCreacion], [FechaModificacion]) VALUES (12, N'6521876387', 2, N'Inactivo', N'española', N'Nínguna', N'celia.suazo@bi-dss.com', N'celia.suazo@bi-dss.com', CAST(N'2019-08-30T13:12:19.0473960' AS DateTime2), CAST(N'2019-08-30T13:12:19.0473968' AS DateTime2))
GO
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [IdEstado], [Estado], [CurrencyCode], [Description], [UsuarioCreacion], [UsuarioModificacion], [FechaCreacion], [FechaModificacion]) VALUES (13, N'6521876387', 2, N'Inactivo', N'española', N'Nínguna', N'celia.suazo@bi-dss.com', N'celia.suazo@bi-dss.com', CAST(N'2019-08-30T13:12:19.9459732' AS DateTime2), CAST(N'2019-08-30T13:12:19.9459738' AS DateTime2))
GO
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [IdEstado], [Estado], [CurrencyCode], [Description], [UsuarioCreacion], [UsuarioModificacion], [FechaCreacion], [FechaModificacion]) VALUES (14, N'6521876387', 2, N'Inactivo', N'española', N'Nínguna', N'celia.suazo@bi-dss.com', N'celia.suazo@bi-dss.com', CAST(N'2019-08-30T13:12:21.0759620' AS DateTime2), CAST(N'2019-08-30T13:12:21.0759627' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Currency] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 
GO
INSERT [dbo].[Country] ([Id], [SortName], [Name], [PhoneCode], [GAFI], [ListaId], [ListaName], [Actualizacion], [NivelRiesgo], [NivelRiesgoName], [TipoAlertaId], [TipoAlertaName], [AccionId], [AccionName], [SeguimientoId], [SeguimientoName], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion], [Comments], [Estado], [IdEstado]) VALUES (2, N'Honduras', N'Honduras', 504, 0, 0, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, 0, NULL, 0, NULL, 0, NULL, N'celia.suazo@bi-dss.com', N'celia.suazo@bi-dss.com', CAST(N'2019-08-13T10:09:34.0678525' AS DateTime2), CAST(N'2019-08-13T10:09:34.0678539' AS DateTime2), NULL, NULL, 0)
GO
INSERT [dbo].[Country] ([Id], [SortName], [Name], [PhoneCode], [GAFI], [ListaId], [ListaName], [Actualizacion], [NivelRiesgo], [NivelRiesgoName], [TipoAlertaId], [TipoAlertaName], [AccionId], [AccionName], [SeguimientoId], [SeguimientoName], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion], [Comments], [Estado], [IdEstado]) VALUES (3, N'Costa Rica', N'Costa Rica', 505, 0, 0, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, 0, NULL, 0, NULL, 0, NULL, N'celia.suazo@bi-dss.com', N'erp@bi-dss.com', CAST(N'2019-08-13T22:45:24.5822543' AS DateTime2), CAST(N'2019-08-23T13:28:53.4954186' AS DateTime2), NULL, NULL, 0)
GO
INSERT [dbo].[Country] ([Id], [SortName], [Name], [PhoneCode], [GAFI], [ListaId], [ListaName], [Actualizacion], [NivelRiesgo], [NivelRiesgoName], [TipoAlertaId], [TipoAlertaName], [AccionId], [AccionName], [SeguimientoId], [SeguimientoName], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion], [Comments], [Estado], [IdEstado]) VALUES (4, N'Guatemala', N'Guatemala', 502, 0, 0, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, 0, NULL, 0, NULL, 0, NULL, N'celia.suazo@bi-dss.com', N'erp@bi-dss.com', CAST(N'2019-08-14T15:20:55.1091082' AS DateTime2), CAST(N'2019-08-23T13:29:07.6479801' AS DateTime2), NULL, NULL, 0)
GO
INSERT [dbo].[Country] ([Id], [SortName], [Name], [PhoneCode], [GAFI], [ListaId], [ListaName], [Actualizacion], [NivelRiesgo], [NivelRiesgoName], [TipoAlertaId], [TipoAlertaName], [AccionId], [AccionName], [SeguimientoId], [SeguimientoName], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion], [Comments], [Estado], [IdEstado]) VALUES (5, N'Korea', N'Korea', NULL, 1, 36, N'Roja', CAST(N'1901-01-01T12:00:00.0000000' AS DateTime2), 39, N'Alto', 41, N'No aceptable', 43, N'No dejar ingresar', 45, N'Requerir información al cliente', N'erp@bi-dss.com', N'erp@bi-dss.com', CAST(N'2019-09-05T16:01:40.4408195' AS DateTime2), CAST(N'2019-09-05T22:30:57.4835045' AS DateTime2), NULL, NULL, 0)
GO
INSERT [dbo].[Country] ([Id], [SortName], [Name], [PhoneCode], [GAFI], [ListaId], [ListaName], [Actualizacion], [NivelRiesgo], [NivelRiesgoName], [TipoAlertaId], [TipoAlertaName], [AccionId], [AccionName], [SeguimientoId], [SeguimientoName], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion], [Comments], [Estado], [IdEstado]) VALUES (6, N'Nicaragua', N'Nicaragua', 505, 0, 0, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, 0, NULL, 0, NULL, 0, NULL, N'erp@bi-dss.com', N'erp@bi-dss.com', CAST(N'2019-09-05T16:29:43.9003723' AS DateTime2), CAST(N'2019-09-05T16:29:43.9003736' AS DateTime2), NULL, NULL, 0)
GO
INSERT [dbo].[Country] ([Id], [SortName], [Name], [PhoneCode], [GAFI], [ListaId], [ListaName], [Actualizacion], [NivelRiesgo], [NivelRiesgoName], [TipoAlertaId], [TipoAlertaName], [AccionId], [AccionName], [SeguimientoId], [SeguimientoName], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion], [Comments], [Estado], [IdEstado]) VALUES (7, N'Iran', N'Iran', NULL, 1, 0, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, 0, NULL, 0, NULL, 0, NULL, N'erp@bi-dss.com', N'erp@bi-dss.com', CAST(N'2019-09-05T16:31:37.7688853' AS DateTime2), CAST(N'2019-09-05T16:31:37.7688860' AS DateTime2), NULL, NULL, 0)
GO
INSERT [dbo].[Country] ([Id], [SortName], [Name], [PhoneCode], [GAFI], [ListaId], [ListaName], [Actualizacion], [NivelRiesgo], [NivelRiesgoName], [TipoAlertaId], [TipoAlertaName], [AccionId], [AccionName], [SeguimientoId], [SeguimientoName], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion], [Comments], [Estado], [IdEstado]) VALUES (8, N'Bahamas', N'Bahamas', NULL, 1, 0, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, 0, NULL, 0, NULL, 0, NULL, N'erp@bi-dss.com', N'erp@bi-dss.com', CAST(N'2019-09-05T16:32:45.3798730' AS DateTime2), CAST(N'2019-09-05T16:32:45.3798737' AS DateTime2), NULL, NULL, 0)
GO
INSERT [dbo].[Country] ([Id], [SortName], [Name], [PhoneCode], [GAFI], [ListaId], [ListaName], [Actualizacion], [NivelRiesgo], [NivelRiesgoName], [TipoAlertaId], [TipoAlertaName], [AccionId], [AccionName], [SeguimientoId], [SeguimientoName], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion], [Comments], [Estado], [IdEstado]) VALUES (9, N'', N'Panama', 404, 1, 38, N'Gris', CAST(N'1901-05-01T12:00:00.0000000' AS DateTime2), 40, N'Medio', 42, N'Aceptable', 44, N'Solo generar alerta para monitoreo', 46, N'Monitorear que los ingresos esten de acorde al perfil', N'erp@bi-dss.com', N'erp@bi-dss.com', CAST(N'2019-09-16T12:18:21.7664212' AS DateTime2), CAST(N'2019-09-16T12:18:21.7664225' AS DateTime2), NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[State] ON 
GO
INSERT [dbo].[State] ([Id], [Name], [CountryId], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion]) VALUES (1, N'Francisco Morazan', 2, NULL, N'erp@bi-dss.com', NULL, CAST(N'2019-09-01T18:51:59.9241001' AS DateTime2))
GO
INSERT [dbo].[State] ([Id], [Name], [CountryId], [Usuariocreacion], [Usuariomodificacion], [FechaCreacion], [FechaModificacion]) VALUES (2, N'Cortes', 2, NULL, N'erp@bi-dss.com', NULL, CAST(N'2019-09-01T18:52:14.0883761' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[State] OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 
GO
INSERT [dbo].[City] ([Id], [Name], [Description], [CountryId], [StateId]) VALUES (2, N'Tegucigalpa', NULL, 2, 1)
GO
INSERT [dbo].[City] ([Id], [Name], [Description], [CountryId], [StateId]) VALUES (3, N'San Pedro Sula', NULL, 2, 2)
GO

SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Branch] ON 
GO
INSERT [dbo].[Branch] ([BranchId], [BranchCode], [Numero], [BranchName], [Description], [CurrencyId], [CurrencyName], [Address], [CityId], [City], [CountryId], [CountryName], [LimitCNBS], [StateId], [State], [ZipCode], [Phone], [Email], [ContactPerson], [UsuarioCreacion], [IdEstado], [Estado], [UsuarioModificacion], [FechaCreacion], [FechaModificacion], [CustomerId], [CustomerName]) VALUES (1, N'000', 0, N'Sucursal Inicial', N'Sucursal Inicial', 1, N'Lempiras', N'Ninguna', 3, N'San Pedro Sula', 2, N'Honduras', NULL, 2, N'Cortes', N'345', N'22356799', N'admin', N'admin', N'admin@bi-dss.com', 1, N'Activo', N'erp@bi-dss.com', CAST(N'2019-08-13T22:23:58.4634224' AS DateTime2), CAST(N'2019-09-01T23:45:41.9543092' AS DateTime2), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO


INSERT dbo.AspNetUsers(Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, BranchId, IsEnabled, LastPasswordChangedDate)
 VALUES ('fc405b7d-9fe3-43c9-97b5-d87a174cab8a', N'erp@bi-dss.com', N'ERP@BI-DSS.COM', N'erp@bi-dss.com', N'ERP@BI-DSS.COM', CONVERT(bit, 'False'), N'AQAAAAEAACcQAAAAEB5L3ZP3Bpk0O3IgrIeSN3rGrrGauHAbwQ4ChaVZ42KTDXNNTu+qCmcHmHSzH0y7iw==', N'XF4HG6MSQ22VZLWDER4TLPWRJO2QOGLH', N'90679b58-5a10-4dc0-b285-a8e042edd68d', NULL, CONVERT(bit, 'False'), CONVERT(bit, 'False'), NULL, CONVERT(bit, 'True'), 0, '0001-01-01 00:00:00.0000000', '0001-01-01 00:00:00.0000000', NULL, NULL, 1, CONVERT(bit, 'True'), '2019-08-14 07:48:48.0463866')
GO


INSERT dbo.AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) VALUES ('7b9c3671-43f7-4339-7699-08d71082fbac', N'GA', N'GA', NULL, '2019-07-24 16:05:04.9071491', '2019-07-24 16:05:04.9071503', N'erp@bi-dss.com', N'erp@bi-dss.com')
INSERT dbo.AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) VALUES ('bc0e4be0-dca1-4530-b2e4-1645f6caf87c', N'GG', N'Gerente General', N'9ab83e3b-4299-4838-9ec7-dea18a277712', '0001-01-01 00:00:00.0000000', '0001-01-01 00:00:00.0000000', N'', N'')
INSERT dbo.AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) VALUES ('ca1c6c85-63cd-4309-8176-3ea2d64943c8', N'CONTG', N'CONTG', N'2b7785ba-43d4-41f9-888f-a9d733c7ccd8', '0001-01-01 00:00:00.0000000', '0001-01-01 00:00:00.0000000', N'', N'')
INSERT dbo.AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) VALUES ('1605e84a-604f-4cf0-abb7-69e996c97885', N'SOP', N'Supervisor de operaciones y riesgo', NULL, '0001-01-01 00:00:00.0000000', '0001-01-01 00:00:00.0000000', N'', N'')
INSERT dbo.AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) VALUES ('6f708482-a918-430d-b56c-778914afbe4e', N'Admin', N'Admin', NULL, '0001-01-01 00:00:00.0000000', '0001-01-01 00:00:00.0000000', N'', N'')
GO


INSERT dbo.AspNetUserRoles(UserId, RoleId, UserName, RoleName, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) VALUES ('fc405b7d-9fe3-43c9-97b5-d87a174cab8a', 'bc0e4be0-dca1-4530-b2e4-1645f6caf87c', NULL, NULL, '0001-01-01 00:00:00.0000000', '0001-01-01 00:00:00.0000000', N'', N'')
INSERT dbo.AspNetUserRoles(UserId, RoleId, UserName, RoleName, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) VALUES ('fc405b7d-9fe3-43c9-97b5-d87a174cab8a', '6f708482-a918-430d-b56c-778914afbe4e', NULL, NULL, '0001-01-01 00:00:00.0000000', '0001-01-01 00:00:00.0000000', N'', N'')
GO

INSERT dbo.Policy(Id, Name, Description, type, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, Estado, IdEstado) VALUES ('727f9108-fe76-4c7e-3efd-08d71a9a2950', N'GG', N'GG', N'Roles', N'erp@bi-dss.com', N'erp@bi-dss.com', '2019-08-06 12:16:11.5123064', '2019-08-06 12:16:11.5123081', N'Activo', 1)
INSERT dbo.Policy(Id, Name, Description, type, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, Estado, IdEstado) VALUES ('5be222c7-375d-4efc-b02f-575d8a4d2f95', N'EscrituraAdmon', N'Escritura para modulos de administracion', N'UserClaimRequirement', N'freddy.chinchilla@bi-dss.com', N'freddy.chinchilla@bi-dss.com', '2019-04-02 08:44:49.5633333', '2019-04-02 08:44:49.5633333', NULL, 0)
INSERT dbo.Policy(Id, Name, Description, type, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, Estado, IdEstado) VALUES ('b64b6d6f-8a4f-4a5b-a608-cdd4442f305a', N'Admin', N'Permisos administrativos.', N'Roles', N'freddy.chinchilla@bi-dss.com', N'freddy.chinchilla@bi-dss.com', '2019-04-01 00:00:00.0000000', '2019-04-01 00:00:00.0000000', NULL, 0)
GO

INSERT dbo.PolicyRoles(Id, IdPolicy, IdRol, UsuarioCreacion, UsuarioModificacion, Estado, IdEstado) VALUES ('560ba0c8-5e90-4cc5-a3f2-04f605969490', 'b64b6d6f-8a4f-4a5b-a608-cdd4442f305a', '6f708482-a918-430d-b56c-778914afbe4e', N'freddy.chinchilla@bi-dss.com', N'freddy.chinchilla@bi-dss.com', NULL, 0)
INSERT dbo.PolicyRoles(Id, IdPolicy, IdRol, UsuarioCreacion, UsuarioModificacion, Estado, IdEstado) VALUES ('ed463f83-c71a-4ec8-f382-08d71a9a4b63', '727f9108-fe76-4c7e-3efd-08d71a9a2950', 'bc0e4be0-dca1-4530-b2e4-1645f6caf87c', N'erp@bi-dss.com', N'erp@bi-dss.com', N'Activo', 1)
GO

--INSERT dbo.AspNetUserClaims(Id, UserId, ClaimType, ClaimValue, PolicyId) VALUES (1, 'fc405b7d-9fe3-43c9-97b5-d87a174cab8a', N'EscrituraAdministracion', N'1', '5be222c7-375d-4efc-b02f-575d8a4d2f95')
--GO