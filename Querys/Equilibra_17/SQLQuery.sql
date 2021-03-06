/****** Script para el comando SelectTopNRows de SSMS  ******/
USE SSales

DELETE FROM Persona WHERE Codigo = 0

UPDATE Persona SET Nombre = 'Arthur Galiano' WHERE Codigo = 85598

CREATE TABLE Cliente (
ClienteId int identity primary key not null,
Codigo int not null,
Nombre varchar(30) not null,
Rubro varchar (25) not null,
Sector varchar(20) not null,
)
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES( 
                    5454869,'Jose Rodriguez','Agrario','Privado' 
					5411433,'Arthur Galiano','Agrario','Publico';
					5422269,'Pedro Palomino','Industrial','Privado';
					5434543,'Arthur Galiano','Pesquero','Publico';
					5422269,'Pierre Carreño','Industrial','Privado';
					5434543,'Alison Vega','Pesquero','Publico'
)
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(2525877,'Pedro Rodriguez','Agrario','Privado')
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(5252686,'Juana Galiano','Agrario','Publico' )
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(4444449,'Lisseth Palomino','Industrial','Privado')
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(2222743,'Gisela Ferrer','Pesquero','Publico')
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(1112269,'Jostyn Carreño','Industrial','Privado')
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(7777743,'Alison Guerra','Pesquero','Publico')

INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(2525877,'Pedro Rodriguez','Agrario','Privado')
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(5252686,'Juana Galiano','Agrario','Publico' )
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(4444449,'Lisseth Palomino','Industrial','Privado')
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(2222743,'Gisela Ferrer','Pesquero','Publico')
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(1112269,'Jostyn Carreño','Industrial','Privado')
INSERT INTO Cliente(Codigo,Nombre,Rubro,Sector) VALUES(7777743,'Alison Guerra','Pesquero','Publico')

SELECT*FROM Cliente 
GO 
SELECT DISTINCT Codigo,Nombre FROM Cliente 
GO
SELECT DISTINCT Sector FROM Cliente
GO

alter procedure sp_BuscarNombre
 @Dato varchar(30)
 AS 
 BEGIN (
   SELECT c.Codigo,c.Nombre,c.Rubro,c.Sector  FROM Cliente c
   WHERE c.Nombre LIKE '%'+ @Dato +'%'
 )
 END  
 GO

 exec sp_BuscarNombre 'P'
 GO

 create procedure sp_BuscarCod
 @Dato varchar(30)
 AS 
 BEGIN (
   SELECT c.Codigo,c.Nombre,c.Rubro,c.Sector  FROM Cliente c
   WHERE c.Codigo LIKE '%'+ @Dato +'%'
 )
 END  
 GO
 exec sp_BuscarCod '45'


