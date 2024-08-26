using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.Data.Migrations
{
    /// <inheritdoc />
    public partial class participantsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.AddColumn<int>(
                name: "Answer",
                table: "Question",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParticipantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParticipantLastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ParticipantId);
                });

            migrationBuilder.CreateTable(
                name: "SurveyHeaders_Participants",
                columns: table => new
                {
                    SurveyHeader_ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurveyHeaderSurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyHeaders_Participants", x => x.SurveyHeader_ParticipantId);
                    table.ForeignKey(
                        name: "FK_SurveyHeaders_Participants_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyHeaders_Participants_SurveyHeaders_SurveyHeaderSurveyId",
                        column: x => x.SurveyHeaderSurveyId,
                        principalTable: "SurveyHeaders",
                        principalColumn: "SurveyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyHeaders_Participants_ParticipantId",
                table: "SurveyHeaders_Participants",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyHeaders_Participants_SurveyHeaderSurveyId",
                table: "SurveyHeaders_Participants",
                column: "SurveyHeaderSurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyHeaders_Participants");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Question");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionValue = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_Options_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                column: "QuestionId");
        }
    }
}
