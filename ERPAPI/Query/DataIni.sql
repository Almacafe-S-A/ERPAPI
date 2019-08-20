
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


INSERT dbo.AspNetUserClaims(Id, UserId, ClaimType, ClaimValue, PolicyId) VALUES (1, 'fc405b7d-9fe3-43c9-97b5-d87a174cab8a', N'EscrituraAdministracion', N'1', '5be222c7-375d-4efc-b02f-575d8a4d2f95')
GO



