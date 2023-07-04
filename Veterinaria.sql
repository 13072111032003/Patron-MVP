create database Veterinaria
go
use Veterinaria
go
create table Pet
(
	Pet_Id int identity (100000,1) primary key,
	Pet_Nombre nvarchar (50) not null,
	Pet_Tipo nvarchar (50) not null,
	Pet_Color nvarchar (50) not null,)
go
insert into Pet values('Buttons', 'Perro', 'Blanco')
insert into Pet values('Coda', 'Gato', 'Multicolor')
insert into Pet values('Merlin', 'Lora', 'Verder-Amarrila')
insert into Pet values('Nina', 'Tortuga', 'Cafe')
insert into Pet values('Domino', 'Conejo', 'Plomo')
insert into Pet values('Luna', 'Hamster', 'Naranja')
insert into Pet values('Lucy', 'Mono', 'Cafe')
insert into Pet values('Daysi', 'Caballo', 'Negro-Blanco')
insert into Pet values('Zoe', 'Serpiente', 'Verde')
insert into Pet values('Max', 'Perico', 'Amarrilo')
insert into Pet values('Charlie', 'Ratón', 'Blanco')
insert into Pet values('Rocky', 'Ardilla', 'Cafe-Naranja')
insert into Pet values('Leo', 'Perro', 'Negro')
insert into Pet values('Loki', 'Gato', 'Marrón')
insert into Pet values('Jasper', 'Perro', 'Blanco-Cafe')
go
select *from Pet
