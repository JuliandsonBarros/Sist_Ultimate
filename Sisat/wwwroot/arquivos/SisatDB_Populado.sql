USE [Sisat_bd]
GO
/****** Object:  Table [dbo].[Conveniados]    Script Date: 11/01/2024 10:18:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conveniados](
	[Id_Conveniado] [int] IDENTITY(1,1) NOT NULL,
	[Nom_Conveniado] [nvarchar](100) NOT NULL,
	[ID_Usuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Conveniado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Convenio_Projeto]    Script Date: 11/01/2024 10:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Convenio_Projeto](
	[Id_Con] [int] NOT NULL,
	[Id_Proj] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Con] ASC,
	[Id_Proj] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NivelDeAcesso]    Script Date: 11/01/2024 10:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NivelDeAcesso](
	[Id_NivelAcesso] [int] NOT NULL,
	[Descricao] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_NivelAcesso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pacotes_Atualizacoes]    Script Date: 11/01/2024 10:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pacotes_Atualizacoes](
	[Id_Pacote] [int] IDENTITY(1,1) NOT NULL,
	[Id_Proj] [int] NULL,
	[Num_Versao] [nvarchar](30) NOT NULL,
	[Registro_Alteracoes] [nvarchar](max) NOT NULL,
	[Dt_Lancamento] [datetime] NULL,
	[Dir_Arquivo] [nvarchar](255) NULL,
	[Ativo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Pacote] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projetos]    Script Date: 11/01/2024 10:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projetos](
	[Id_Projeto] [int] IDENTITY(1,1) NOT NULL,
	[Nom_Projeto] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Projeto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 11/01/2024 10:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](256) NOT NULL,
	[Nome] [varchar](256) NOT NULL,
	[Senha] [varchar](128) NOT NULL,
	[Login] [varchar](255) NULL,
	[Id_NivAcesso] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pacotes_Atualizacoes] ADD  DEFAULT ((1)) FOR [Ativo]
GO

ALTER TABLE [dbo].[Conveniados]  WITH CHECK ADD FOREIGN KEY([ID_Usuario])
REFERENCES [dbo].[Usuario] ([ID])
GO
ALTER TABLE [dbo].[Convenio_Projeto]  WITH CHECK ADD  CONSTRAINT [FK_ID_CONVENIADO] FOREIGN KEY([Id_Con])
REFERENCES [dbo].[Conveniados] ([Id_Conveniado])
GO
ALTER TABLE [dbo].[Convenio_Projeto] CHECK CONSTRAINT [FK_ID_CONVENIADO]
GO
ALTER TABLE [dbo].[Pacotes_Atualizacoes]  WITH CHECK ADD  CONSTRAINT [FK_ID_PROJETO] FOREIGN KEY([Id_Proj])
REFERENCES [dbo].[Projetos] ([Id_Projeto])
GO
ALTER TABLE [dbo].[Pacotes_Atualizacoes] CHECK CONSTRAINT [FK_ID_PROJETO]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([Id_NivAcesso])
REFERENCES [dbo].[NivelDeAcesso] ([Id_NivelAcesso])
GO




SELECT * FROM CONVENIADOS;
SELECT * FROM PROJETOS;
SELECT * FROM PACOTES_ATUALIZACOES;
SELECT * FROM CONVENIO_PROJETO;
SELECT * FROM NivelDeAcesso;
Select * from Usuario;

insert into Conveniados(Nom_Conveniado) values('MPM-RJ');
insert into Conveniados(Nom_Conveniado) values('MPM-GO');
insert into Conveniados(Nom_Conveniado) values('MPM-SP');
insert into Conveniados(Nom_Conveniado) values('MPM-RS');
insert into Conveniados(Nom_Conveniado) values('MPM-MG');


insert into Projetos(Nom_Projeto) values('ARGUS');
insert into Projetos(Nom_Projeto) values('SITEL');
insert into Projetos(Nom_Projeto) values('SIMBA');
insert into Projetos(Nom_Projeto) values('PAI');
insert into Projetos(Nom_Projeto) values('PASSAPORT');

insert into Pacotes_Atualizacoes(Id_Proj,Num_Versao,Registro_Alteracoes,Dt_lancamento) values(4,'4444444444','ALTERA플O SISTEMA 4','2023-10-12');
insert into Pacotes_Atualizacoes(Id_Proj,Num_Versao,Registro_Alteracoes,Dt_lancamento) values(3,'3333333333','ALTERA플O SISTEMA 3','2023-10-12');
insert into Pacotes_Atualizacoes(Id_Proj,Num_Versao,Registro_Alteracoes,Dt_lancamento) values(1,'1111111111','ALTERA플O SISTEMA 1','2023-10-12');
insert into Pacotes_Atualizacoes(Id_Proj,Num_Versao,Registro_Alteracoes,Dt_lancamento) values(2,'2222222222','ALTERA플O SISTEMA 2','2023-10-12');
insert into Pacotes_Atualizacoes(Id_Proj,Num_Versao,Registro_Alteracoes,Dt_lancamento) values(5,'5555555555','ALTERA플O SISTEMA 5','2023-10-12');

insert into Convenio_Projeto(Id_Con,ID_Proj) values(1,4);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(1,2);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(1,1);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(5,3);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(5,1);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(4,2);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(4,3);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(4,5);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(2,2);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(2,4);
insert into Convenio_Projeto(Id_Con,ID_Proj) values(3,5);

INSERT INTO Usuario(Email,Nome,Senha,Login,Id_NivAcesso)values('Juliandsonbs@gmail.com', 'Juliandson Barros Soares',123456, 'juliandson.soares',1);
INSERT INTO Usuario(Email,Nome,Senha,Login,Id_NivAcesso)values('Juliandsonbs@gmail.com', 'Anderson Barros Soares',123456, 'anderson.Barros',2);
INSERT INTO Usuario(Email,Nome,Senha,Login,Id_NivAcesso)values('Juca@gmail.com', 'Juca Santos',123456, 'Juca.santos',2);

insert into NivelDeAcesso(Id_NivelAcesso,Descricao)values(1,'ADMIN');
insert into NivelDeAcesso(Id_NivelAcesso,Descricao)values(2,'CONVENIADO');

select c.Nom_Conveniado,p.Nom_Projeto,Num_Versao,Dt_Lancamento
	from Conveniados as C inner join Projetos as p 
	on c.Id_Conveniado = p.Id_Projeto
	inner join Pacotes_Atualizacoes as pa
	on c.Id_Conveniado = pa.Id_Proj;

SELECT * FROM Usuario;

UPDATE Conveniados
SET ID_Usuario = 6
WHERE Id_Conveniado = 1;

select c.Nom_Conveniado, u.Nome from conveniados as c 
	inner join Usuario as u
	on c.Id_Conveniado = u.ID
	where u.ID = 1;

Update Usuario 
SET Nome = 'Anderson'
WHERE ID = 1003;
