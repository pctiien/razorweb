using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;
using razorEntity.Models;

#nullable disable

namespace razorEntity.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
            Randomizer.Seed = new Random(8675309);
            var fakeObj  = new Faker<Article>("vi");
            fakeObj.RuleFor(a=>a.Title,f=>f.Lorem.Sentence(5,10));
            fakeObj.RuleFor(a=>a.Created,f=>f.Date.Between(new DateTime(2015,1,1),new DateTime(2023,1,1)));
            fakeObj.RuleFor(a=>a.Content,f=>f.Lorem.Paragraphs(1,4));
            for(int i=0;i<50;i++)
            {
                var article = fakeObj.Generate();
                migrationBuilder.InsertData(
                    table:"articles",
                    columns:new[]{"Title","Created","Content"},
                    values:new object[] {
                        article.Title,
                        article.Created,
                        article.Content
                    }
            );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
