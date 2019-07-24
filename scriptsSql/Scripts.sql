scripts banco

CREATE TABLE Empresa(
	IdEmpresa INT NOT NULL IDENTITY (1,1),
	DsCnpj VARCHAR(50) NOT NULL UNIQUE,
	DsRazaoSocial VARCHAR(50),
	NmFantasia VARCHAR(50)  NOT NULL
)
go

CREATE TABLE Usuario(
	Idusuario INT NOT NULL IDENTITY (1,1),
	IdEmpresa INT NOT NULL,
	CdCpf VARCHAR(20) NOT NULL UNIQUE,
	DsEmail VARCHAR(50) NOT NULL UNIQUE,
	DsCelular VARCHAR(11) NOT NULL UNIQUE,
	CdPassword VARCHAR(11) NOT NULL,
	DtNascimento DATE NOT NULL,
	NmUsuario VARCHAR(50),
	CdIsAdmin bit DEFAULT 0 NOT NULL,
	CdAtivo bit DEFAULT 1 NOT NULL,
	UNIQUE(Idusuario,IdEmpresa)
)
go

CREATE TABLE Perfil(
	DsPerfil VARCHAR(50) NOT NULL
)
go

CREATE TABLE RelacPerfilUsuario(
	DsPerfil VARCHAR(50) NOT NULL,
	Idusuario INT NOT NULL
)
go


CREATE TABLE Ponto(
	Idusuario INT NOT NULL,
	IdEmpresa INT NOT NULL,
	DtRegistro DATETIME NOT NULL,
	CdLat DECIMAL(10,6) NOT NULL,
	CdLng DECIMAL(10,6) NOT NULL
)
go



--CHAVES PRIMARIAS
ALTER TABLE Empresa
	ADD PRIMARY KEY(IdEmpresa)
go


ALTER TABLE Usuario
	ADD PRIMARY KEY(IdUsuario)
go

ALTER TABLE Perfil
	ADD PRIMARY KEY(DsPerfil)
go

ALTER TABLE RelacPerfilUsuario
	ADD PRIMARY KEY(DsPerfil,Idusuario)
go

ALTER TABLE Ponto
	ADD PRIMARY KEY(Idusuario,IdEmpresa,DtRegistro)
go


--CHAVES ESTRANGEIRAS
ALTER TABLE Usuario
	ADD FOREIGN KEY(IdEmpresa)
		REFERENCES Empresa
go


ALTER TABLE RelacPerfilUsuario
	ADD FOREIGN KEY(DsPerfil)
		REFERENCES Perfil
go

ALTER TABLE RelacPerfilUsuario
	ADD FOREIGN KEY(Idusuario)
		REFERENCES Usuario
go


ALTER TABLE Ponto
	ADD FOREIGN KEY(Idusuario)
		REFERENCES Usuario
go