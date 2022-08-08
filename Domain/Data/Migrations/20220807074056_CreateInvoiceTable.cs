using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Data.Migrations
{
    public partial class CreateInvoiceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthyAccounts",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: false),
                    IsHealthy = table.Column<bool>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthyAccounts", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(maxLength: 200, nullable: false),
                    IssuedDate = table.Column<DateTime>(nullable: false),
                    OriginalAmount = table.Column<Decimal>(nullable: false),
                    OutstandingAmount = table.Column<Decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_HealthyAccounts_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "HealthyAccounts",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");
            migrationBuilder.CreateIndex(
                name: "IX_Invoices_IssuedDate",
                table: "Invoices",
                column: "IssuedDate");

            // seeding sample data

            migrationBuilder.Sql(
                @"
INSERT INTO [HealthyAccounts]([CustomerId],[CustomerName],[IsHealthy],[LastUpdated])
VALUES
			(1,'Client AB',null,GETDATE())
		   ,(2,'Client ASB',null,GETDATE())
           ,(3,'Client CFB',null,GETDATE())
           ,(4,'Client ACB',null,GETDATE())
           ,(5,'Client ABB',null,GETDATE())
           ,(6,'Client BAB',null,GETDATE())
           ,(7,'Client HAB',null,GETDATE())
           ,(8,'Client BAB',null,GETDATE())
		   ,(9,'Client AB1',null,GETDATE())
           ,(10,'Client 13B',null,GETDATE())
           ,(12,'Client GSB',null,GETDATE())
           ,(13,'Client ANB',null,GETDATE())
           ,(14,'Client ACB',null,GETDATE())
           ,(15,'Client ABB',null,GETDATE())
           ,(16,'Client BAB',null,GETDATE())
           ,(17,'Client HAB',null,GETDATE())
           ,(18,'Client BAB',null,GETDATE())
		   ,(19,'Client CAB',null,GETDATE())

GO

INSERT INTO [dbo].[Invoices]
           ([Id]
           ,[CustomerId]
           ,[CustomerName]
           ,[IssuedDate]
           ,[OriginalAmount]
           ,[OutstandingAmount])
     VALUES
           (1	,2,	'Client ABB',	'2022-05-20',	120000.00	,12000	)
			,(2	,8,	'Client EAB',	'2021-05-20',	320000.00	,0.00	)
			,(3	,7,	'Client ANS',	'2022-06-20',	500000.00	,0.00	)
			,(4	,1,	'Client ESG',	'2022-02-20',	2000000.0	,20000	)
			,(5	,4,	'Client EDN',	'2022-01-20',	320000.00	,12000	)
			,(6	,4,	'Client ABB',	'2022-06-20',	500000.00	,0.00	)
			,(7	,6,	'Client ABB',	'2022-07-20',	50000.00	,0.00	)
			,(8	,6,	'Client APB',	'2022-07-21',	50000.00	,0.00	)
			,(9	,6,	'Client ALB',	'2022-07-20',	50000.00	,0.00	)
			,(10	,6,	'Client AKB',	'2022-07-21',	50000.00	,0.00	)
			,(11	,1,	'Client AVB',	'2021-07-21',	150000.00	,0.00	)
			,(12	,1,	'Client ABB',	'2021-07-21',	150000.00	,0.00	)

GO"
                );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "HealthyAccounts");
        }
    }
}
