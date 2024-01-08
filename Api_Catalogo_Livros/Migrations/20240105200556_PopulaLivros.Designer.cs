﻿// <auto-generated />
using System;
using Api_Catalogo_Livros.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api_Catalogo_Livros.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240105200556_PopulaLivros")]
    partial class PopulaLivros
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api_Catalogo_Livros.Models.Autores", b =>
                {
                    b.Property<int>("AutorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AutorId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AutorId");

                    b.HasIndex(new[] { "Nome" }, "Ix_Nome");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("Api_Catalogo_Livros.Models.Editoras", b =>
                {
                    b.Property<int>("EditoraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EditoraId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EditoraId");

                    b.HasIndex(new[] { "Nome" }, "Ix_Nome");

                    b.ToTable("Editoras");
                });

            modelBuilder.Entity("Api_Catalogo_Livros.Models.Livros", b =>
                {
                    b.Property<int>("LivroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LivroId"));

                    b.Property<string>("Ano_Publicacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<int?>("AutoresAutorId")
                        .HasColumnType("int");

                    b.Property<int>("EditoraId")
                        .HasColumnType("int");

                    b.Property<int?>("EditorasEditoraId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("LivroId");

                    b.HasIndex("AutoresAutorId");

                    b.HasIndex("EditorasEditoraId");

                    b.HasIndex(new[] { "Nome" }, "Ix_Nome");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("Api_Catalogo_Livros.Models.Livros", b =>
                {
                    b.HasOne("Api_Catalogo_Livros.Models.Autores", "Autores")
                        .WithMany("Livros")
                        .HasForeignKey("AutoresAutorId");

                    b.HasOne("Api_Catalogo_Livros.Models.Editoras", "Editoras")
                        .WithMany("Livros")
                        .HasForeignKey("EditorasEditoraId");

                    b.Navigation("Autores");

                    b.Navigation("Editoras");
                });

            modelBuilder.Entity("Api_Catalogo_Livros.Models.Autores", b =>
                {
                    b.Navigation("Livros");
                });

            modelBuilder.Entity("Api_Catalogo_Livros.Models.Editoras", b =>
                {
                    b.Navigation("Livros");
                });
#pragma warning restore 612, 618
        }
    }
}