using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Survey");

            migrationBuilder.CreateTable(
                name: "GeneralInformations",
                schema: "Survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralInformations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OfferedAnswers",
                schema: "Survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferedAnswers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "Survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                schema: "Survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.id);
                    table.ForeignKey(
                        name: "FK_Participants_SurveyId",
                        column: x => x.SurveyId,
                        principalSchema: "Survey",
                        principalTable: "GeneralInformations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOfferedAnswerRelations",
                schema: "Survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OfferedAnswerId = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOfferedAnswerRelations", x => x.id);
                    table.ForeignKey(
                        name: "FK_QuestionOfferedAnswerRelations_OfferedAnswerId",
                        column: x => x.OfferedAnswerId,
                        principalSchema: "Survey",
                        principalTable: "OfferedAnswers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionOfferedAnswerRelations_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Survey",
                        principalTable: "Questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestionRelations",
                schema: "Survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestionRelations", x => x.id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestionRelations_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Survey",
                        principalTable: "Questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyQuestionRelations_SurveyId",
                        column: x => x.SurveyId,
                        principalSchema: "Survey",
                        principalTable: "GeneralInformations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                schema: "Survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionAnswersId = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Answers_OfferedAnswersId",
                        column: x => x.QuestionAnswersId,
                        principalSchema: "Survey",
                        principalTable: "OfferedAnswers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_ParticipantId",
                        column: x => x.ParticipantId,
                        principalSchema: "Survey",
                        principalTable: "Participants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Survey",
                        principalTable: "Questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_SurveyId",
                        column: x => x.SurveyId,
                        principalSchema: "Survey",
                        principalTable: "GeneralInformations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionAnswersId",
                schema: "Survey",
                table: "Answers",
                column: "QuestionAnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                schema: "Survey",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SurveyId",
                schema: "Survey",
                table: "Answers",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "UN_Answers_ParticipantId_SurveyId_QuestionId_QuestionAnswersId",
                schema: "Survey",
                table: "Answers",
                columns: new[] { "ParticipantId", "SurveyId", "QuestionId", "QuestionAnswersId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_SurveyId",
                schema: "Survey",
                table: "Participants",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOfferedAnswerRelations_OfferedAnswerId",
                schema: "Survey",
                table: "QuestionOfferedAnswerRelations",
                column: "OfferedAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOfferedAnswerRelations_QuestionId",
                schema: "Survey",
                table: "QuestionOfferedAnswerRelations",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionRelations_QuestionId",
                schema: "Survey",
                table: "SurveyQuestionRelations",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionRelations_SurveyId",
                schema: "Survey",
                table: "SurveyQuestionRelations",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers",
                schema: "Survey");

            migrationBuilder.DropTable(
                name: "QuestionOfferedAnswerRelations",
                schema: "Survey");

            migrationBuilder.DropTable(
                name: "SurveyQuestionRelations",
                schema: "Survey");

            migrationBuilder.DropTable(
                name: "Participants",
                schema: "Survey");

            migrationBuilder.DropTable(
                name: "OfferedAnswers",
                schema: "Survey");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "Survey");

            migrationBuilder.DropTable(
                name: "GeneralInformations",
                schema: "Survey");
        }
    }
}
