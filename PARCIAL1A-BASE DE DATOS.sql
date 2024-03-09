CREATE TABLE Autores(
Id int primary key identity,
Nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Posts(
Id int primary key identity NOT NULL,
Titulo VARCHAR(50) NOT NULL,
Contenido VARCHAR(500) NOT NULL,
FechaPublicacion DATETIME NOT NULL
); 

CREATE TABLE AutorLibro(
AutorId int NOT NULL,
LibroId int NOT NULL,
primary key (AutorId,LibroId),
Orden int NOT NULL,
);

CREATE TABLE Libros(
Id int primary key identity NOT NULL,
Titulo VARCHAR(50) NOT NULL
);

select * from Libros;
--Insertar datos en la tabla Autores:

INSERT INTO Autores (Nombre) VALUES ('Manuel Rosales');
INSERT INTO Autores (Nombre) VALUES ('Martin Gonzales');
INSERT INTO Autores (Nombre) VALUES ('Silvia Ling');

--Insertar datos en la tabla Posts:

INSERT INTO Posts (Titulo, Contenido, FechaPublicacion) VALUES ('Principita', 'Habia una vez una niña....', GETDATE());
INSERT INTO Posts (Titulo, Contenido, FechaPublicacion) VALUES ('Corre', 'La maraton del 57....', GETDATE());
INSERT INTO Posts (Titulo, Contenido, FechaPublicacion) VALUES ('Te ve', 'Los ojos....', GETDATE());
INSERT INTO Posts (Titulo, Contenido, FechaPublicacion) VALUES ('El camino', 'El camino...', GETDATE());

--Insertar datos en la tabla Libros:

INSERT INTO Libros (Titulo) VALUES ('Principita');
INSERT INTO Libros (Titulo) VALUES ('Corre');
INSERT INTO Libros (Titulo) VALUES ('Te ve..');
INSERT INTO Libros (titulo) VALUES ('El camino');

--Insertar datos en la tabla AutorLibro:

INSERT INTO AutorLibro (AutorId, LibroId, Orden) VALUES (1, 1, 1); -- Manuel Rosales escribió Principita
INSERT INTO AutorLibro (AutorId, LibroId, Orden) VALUES (2, 2, 1); -- Martin Gonzales escribió Corre
INSERT INTO AutorLibro (AutorId, LibroId, Orden) VALUES (3, 3, 1); -- Silvia Ling escribió Te ve
INSERT INTO AutorLibro (AutorId, LibroId, Orden) VALUES (1, 4, 2); -- Manuel Rosales escribio El camino

select * FROM  Autores,Libros;



