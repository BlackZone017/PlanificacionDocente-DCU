drop database finaldcu
create database finaldcu
use finaldcu

DROP TABLE Usuarios
DROP TABLE asignacion
DROP TABLE Materia

create table Usuarios(
ID int identity(1,1) primary key,
email varchar(254) not null unique,
password varchar(128) not null
)


insert into Usuarios (email,password) values ('hola@hola.com','123123')

Create table Materia(
id int identity(1,1) primary key,
nombre varchar(60),
Usuario_ID int

constraint FK_Usuario_ID FOREIGN KEY (Usuario_ID) REFERENCES Usuarios(ID)
)

create table asignacion(
ID int identity(1,1) primary key,
Materia_ID int,					--♫ De que asignatura/materia es esta asignacion/tarea ♫
fecha date not null,			--♦ fecha en que se va a entregar o realizar la asignacion ♦ 
nombre varchar(60) not null,	--♫ Nombre de la tarea/asignacion ♫
grado varchar(40),				--♦ Basica / Bachiller ♦
año int,						--♫ Año Escolar ♫
competencia varchar(512),		--♦ Lo que hay que hacer ♦
estrategia varchar(512),		--♫ Como se va a hacer la asignacion ♫
recursos varchar(512),			--♦
evaluacion int not null,		--♫ Puntos que vale ♫

constraint FK_MAteria_ID FOREIGN KEY (Materia_ID) REFERENCES Materia(ID)
)

INSERT INTO usuarios VALUES ('g.ramirez159@gmail.com','gouri')
INSERT INTO usuarios VALUES ('d.ramos@gmail.com','darwin')
INSERT INTO usuarios VALUES ('thama@gmail.com','thama')
SELECT * FROM usuarios

INSERT INTO Materia VALUES ('Bases de Datos',4)  --Id 4 = Gouri
INSERT INTO Materia VALUES ('Programacion',5)	 --Id 5 = darwin
INSERT INTO Materia VALUES ('VideoJuegos',5)
INSERT INTO Materia VALUES ('Precalculo',6)		 --Id 6 = Thama
INSERT INTO Materia VALUES ('Diferencial',6)
SELECT * FROM Materia


DELETE FROM usuarios
