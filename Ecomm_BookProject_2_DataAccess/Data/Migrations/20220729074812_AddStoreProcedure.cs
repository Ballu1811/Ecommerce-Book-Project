using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomm_BookProject_2_DataAccess.Migrations
{
    public partial class AddStoreProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetCoverTypes
                                AS
                                select * from CoverTypes");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetCoverType
                                @Id int
                                AS
                                select * from CoverTypes where Id=@Id");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_CreateCoverType
                                @Name varchar(50)
                                AS
                                insert CoverTypes values(@Name)");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_UpdateCoverType
                                @id int,
                                @Name varchar(50)
                                AS
                                update CoverTypes set Name=@Name where Id=@id");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_DeleteCoverType
                                @id int
                                AS
                                delete from CoverTypes where Id=@id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
