﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(FysioContext))]
    [Migration("20220116172049_Changed seed data 2")]
    partial class Changedseeddata2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("PatientFileId")
                        .HasColumnType("int");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PatientFileId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Domain.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("EndHour")
                        .HasColumnType("int");

                    b.Property<int>("StartHour")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Availability");

                    b.HasData(
                        new
                        {
                            Id = 16,
                            EmployeeId = 1,
                            EndHour = 0,
                            StartHour = 0
                        },
                        new
                        {
                            Id = 1,
                            EmployeeId = 1,
                            EndHour = 0,
                            StartHour = 0
                        },
                        new
                        {
                            Id = 2,
                            EmployeeId = 1,
                            EndHour = 17,
                            StartHour = 9
                        },
                        new
                        {
                            Id = 3,
                            EmployeeId = 1,
                            EndHour = 17,
                            StartHour = 9
                        },
                        new
                        {
                            Id = 4,
                            EmployeeId = 1,
                            EndHour = 17,
                            StartHour = 9
                        },
                        new
                        {
                            Id = 5,
                            EmployeeId = 1,
                            EndHour = 17,
                            StartHour = 9
                        },
                        new
                        {
                            Id = 6,
                            EmployeeId = 1,
                            EndHour = 15,
                            StartHour = 9
                        },
                        new
                        {
                            Id = 7,
                            EmployeeId = 2,
                            EndHour = 0,
                            StartHour = 0
                        },
                        new
                        {
                            Id = 8,
                            EmployeeId = 2,
                            EndHour = 0,
                            StartHour = 0
                        },
                        new
                        {
                            Id = 9,
                            EmployeeId = 2,
                            EndHour = 0,
                            StartHour = 0
                        },
                        new
                        {
                            Id = 10,
                            EmployeeId = 2,
                            EndHour = 17,
                            StartHour = 9
                        },
                        new
                        {
                            Id = 11,
                            EmployeeId = 2,
                            EndHour = 17,
                            StartHour = 9
                        },
                        new
                        {
                            Id = 12,
                            EmployeeId = 2,
                            EndHour = 17,
                            StartHour = 9
                        },
                        new
                        {
                            Id = 13,
                            EmployeeId = 2,
                            EndHour = 17,
                            StartHour = 9
                        },
                        new
                        {
                            Id = 14,
                            EmployeeId = 2,
                            EndHour = 0,
                            StartHour = 0
                        },
                        new
                        {
                            Id = 15,
                            EmployeeId = 2,
                            EndHour = 0,
                            StartHour = 0
                        });
                });

            modelBuilder.Entity("Domain.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BigNumber")
                        .HasColumnType("int");

                    b.Property<string>("EmailAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegistrationNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BigNumber = 123456,
                            EmailAdress = "henkpanken@gmail.com",
                            Name = "Henk Panken",
                            PhoneNumber = "0615468795",
                            RegistrationNumber = 3329102
                        },
                        new
                        {
                            Id = 2,
                            EmailAdress = "larsdejong@gmail.com",
                            Name = "Lars de Jong",
                            PhoneNumber = "0684950698",
                            RegistrationNumber = 38564976
                        });
                });

            modelBuilder.Entity("Domain.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientFileId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegistrationNumber")
                        .HasColumnType("int");

                    b.HasKey("PatientId");

                    b.HasAlternateKey("EmailAdress");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            PatientId = 1,
                            Dob = new DateTime(1990, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailAdress = "janjanssen@gmail.com",
                            Gender = 0,
                            Name = "Jan Janssen",
                            PatientFileId = 1,
                            PhoneNumber = "0612345678",
                            RegistrationNumber = 123434556
                        });
                });

            modelBuilder.Entity("Domain.PatientFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfAdmission")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfDischarge")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiagnosisDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiagnosisId")
                        .HasColumnType("int");

                    b.Property<int>("IntakeById")
                        .HasColumnType("int");

                    b.Property<int?>("IntakeUnderSupervisionOfId")
                        .HasColumnType("int");

                    b.Property<int>("KindOfPatient")
                        .HasColumnType("int");

                    b.Property<int>("MainTherapistId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("TreatmentPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IntakeById");

                    b.HasIndex("IntakeUnderSupervisionOfId");

                    b.HasIndex("MainTherapistId");

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.HasIndex("TreatmentPlanId")
                        .IsUnique();

                    b.ToTable("PatientFiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Condition = "ietsitis",
                            DateOfAdmission = new DateTime(2021, 10, 28, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            DiagnosisDescription = "lorem ipsum",
                            DiagnosisId = 1168,
                            IntakeById = 1,
                            KindOfPatient = 1,
                            MainTherapistId = 1,
                            PatientId = 1,
                            TreatmentPlanId = 1
                        });
                });

            modelBuilder.Entity("Domain.Remark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientFileId")
                        .HasColumnType("int");

                    b.Property<int?>("RemarkById")
                        .HasColumnType("int");

                    b.Property<string>("RemarkText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("VisibleForPatient")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PatientFileId");

                    b.HasIndex("RemarkById");

                    b.ToTable("Remark");
                });

            modelBuilder.Entity("Domain.TreatmentPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PatientFileId")
                        .HasColumnType("int");

                    b.Property<int>("ProceedingCode")
                        .HasColumnType("int");

                    b.Property<int>("SessionCount")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("SessionDuration")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("TreatmentPlans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PatientFileId = 0,
                            ProceedingCode = 1430,
                            SessionCount = 2,
                            SessionDuration = new TimeSpan(0, 1, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("Domain.Appointment", b =>
                {
                    b.HasOne("Domain.Employee", "Employee")
                        .WithMany("Appointments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.PatientFile", null)
                        .WithMany("Appointments")
                        .HasForeignKey("PatientFileId");

                    b.HasOne("Domain.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.OwnsOne("Domain.DateRange", "DateRange", b1 =>
                        {
                            b1.Property<int>("AppointmentId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("End")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("datetime2");

                            b1.HasKey("AppointmentId");

                            b1.ToTable("Appointments");

                            b1.WithOwner()
                                .HasForeignKey("AppointmentId");
                        });

                    b.Navigation("DateRange");

                    b.Navigation("Employee");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Domain.Availability", b =>
                {
                    b.HasOne("Domain.Employee", null)
                        .WithMany("Availibility")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.PatientFile", b =>
                {
                    b.HasOne("Domain.Employee", "IntakeBy")
                        .WithMany()
                        .HasForeignKey("IntakeById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Employee", "IntakeUnderSupervisionOf")
                        .WithMany()
                        .HasForeignKey("IntakeUnderSupervisionOfId");

                    b.HasOne("Domain.Employee", "MainTherapist")
                        .WithMany()
                        .HasForeignKey("MainTherapistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Patient", "Patient")
                        .WithOne("PatientFile")
                        .HasForeignKey("Domain.PatientFile", "PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.TreatmentPlan", "TreatmentPlan")
                        .WithOne("PatientFile")
                        .HasForeignKey("Domain.PatientFile", "TreatmentPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IntakeBy");

                    b.Navigation("IntakeUnderSupervisionOf");

                    b.Navigation("MainTherapist");

                    b.Navigation("Patient");

                    b.Navigation("TreatmentPlan");
                });

            modelBuilder.Entity("Domain.Remark", b =>
                {
                    b.HasOne("Domain.PatientFile", "PatientFile")
                        .WithMany("Remarks")
                        .HasForeignKey("PatientFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Employee", "RemarkBy")
                        .WithMany()
                        .HasForeignKey("RemarkById");

                    b.Navigation("PatientFile");

                    b.Navigation("RemarkBy");
                });

            modelBuilder.Entity("Domain.Employee", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Availibility");
                });

            modelBuilder.Entity("Domain.Patient", b =>
                {
                    b.Navigation("PatientFile");
                });

            modelBuilder.Entity("Domain.PatientFile", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Remarks");
                });

            modelBuilder.Entity("Domain.TreatmentPlan", b =>
                {
                    b.Navigation("PatientFile");
                });
#pragma warning restore 612, 618
        }
    }
}
