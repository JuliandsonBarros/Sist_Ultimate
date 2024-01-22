using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sisat.Models
{
    public partial class SisatContext : DbContext
    {
        public SisatContext()
        {
        }

        public SisatContext(DbContextOptions<SisatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conveniados> Conveniados { get; set; } = null!;
        public virtual DbSet<ConvenioProjeto> ConvenioProjeto { get; set; } = null!;
        public virtual DbSet<NivelDeAcesso> NivelDeAcesso { get; set; } = null!;
        public virtual DbSet<PacotesAtualizacoes> PacotesAtualizacoes { get; set; } = null!;
        public virtual DbSet<Projetos> Projetos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;
        public virtual DbSet<Chat> Chat { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("data Source=HOME_PC\\SQLEXPRESS;Initial Catalog=Teste101; Integrated Security=True;");
                optionsBuilder.UseSqlServer("data Source=CPADSI39\\sqlexpress;Initial Catalog=Teste115; Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conveniados>(entity =>
            {
                entity.HasKey(e => e.IdConveniado)
                    .HasName("PK__Convenia__24F606D169C33262");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Conveniados)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Conveniad__ID_Us__5165187F");
            });

            modelBuilder.Entity<ConvenioProjeto>(entity =>
            {
                entity.HasKey(e => new { e.IdCon, e.IdProj })
                    .HasName("PK__Convenio__1D45BDBDCFA658A1");

                entity.Property(e => e.IdConProj).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdConNavigation)
                    .WithMany(p => p.ConvenioProjeto)
                    .HasForeignKey(d => d.IdCon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_CONVENIADO");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasOne(d => d.IdDestinatarioNavigation)
                    .WithMany(p => p.ChatIdDestinatarioNavigation)
                    .HasForeignKey(d => d.IdDestinatario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Id_Destinatario");

                entity.HasOne(d => d.IdRemetenteNavigation)
                    .WithMany(p => p.ChatIdRemetenteNavigation)
                    .HasForeignKey(d => d.IdRemetente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Id_Remetente");
            });

            modelBuilder.Entity<NivelDeAcesso>(entity =>
            {
                entity.HasKey(e => e.IdNivelAcesso)
                    .HasName("PK__NivelDeA__6CF66357F7410F45");

                entity.Property(e => e.IdNivelAcesso).ValueGeneratedNever();
            });

            modelBuilder.Entity<PacotesAtualizacoes>(entity =>
            {
                entity.HasKey(e => e.IdPacote)
                    .HasName("PK__Pacotes___B2C7136C41AD75AD");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdProjNavigation)
                    .WithMany(p => p.PacotesAtualizacoes)
                    .HasForeignKey(d => d.IdProj)
                    .HasConstraintName("FK_ID_PROJETO");
            });

            modelBuilder.Entity<Projetos>(entity =>
            {
                entity.HasKey(e => e.IdProjeto)
                    .HasName("PK__Projetos__6701DEA94675308B");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasOne(d => d.IdNivAcessoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdNivAcesso)
                    .HasConstraintName("FK__Usuario__Id_NivA__5441852A");
            });

            //modelBuilder.Entity<PacotesAtualizacoes>()
            //     .Property(so => so.DtLancamento)
            //     .HasColumnName("Dt_Lancamento")
            //     .HasColumnType("datetime2");

            //modelBuilder.Entity<Chat>()
            //      .Property(so => so.DataEnvio)
            //      .HasColumnName("DataEnvio")
            //      .HasColumnType("datetime2");


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
