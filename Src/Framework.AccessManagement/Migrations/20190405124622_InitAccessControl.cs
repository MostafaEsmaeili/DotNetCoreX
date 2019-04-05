using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Framework.AccessManagement.Migrations
{
    public partial class InitAccessControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sec");

            migrationBuilder.CreateTable(
                name: "Resource",
                schema: "sec",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    ModifiedAt = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(maxLength: 64, nullable: true),
                    Title = table.Column<string>(maxLength: 128, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    MethodType = table.Column<string>(maxLength: 8, nullable: true),
                    ElementResource_Title = table.Column<string>(maxLength: 128, nullable: true),
                    ElementTitleEn = table.Column<string>(maxLength: 128, nullable: true),
                    PageResourceId = table.Column<int>(nullable: true),
                    MenuResource_Title = table.Column<string>(maxLength: 128, nullable: true),
                    MenuOrder = table.Column<int>(nullable: true),
                    MenuTitleEn = table.Column<string>(maxLength: 128, nullable: true),
                    ParenMenuResourceId = table.Column<int>(nullable: true),
                    MenuPageId = table.Column<int>(nullable: true),
                    MenuIcon = table.Column<string>(maxLength: 50, nullable: true),
                    PageResource_Title = table.Column<string>(maxLength: 128, nullable: true),
                    PageLink = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementResource_PageResource",
                        column: x => x.PageResourceId,
                        principalSchema: "sec",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuResource_PageResource",
                        column: x => x.MenuPageId,
                        principalSchema: "sec",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParentMenu",
                        column: x => x.ParenMenuResourceId,
                        principalSchema: "sec",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessControl",
                schema: "sec",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResourceId = table.Column<int>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 128, nullable: false),
                    UserName = table.Column<string>(maxLength: 128, nullable: false),
                    Access = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    ModifiedAt = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessControl_ApiResource",
                        column: x => x.ResourceId,
                        principalSchema: "sec",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessControl_ElementResource",
                        column: x => x.ResourceId,
                        principalSchema: "sec",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessControl_MenuResource",
                        column: x => x.ResourceId,
                        principalSchema: "sec",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessControl_PageResource",
                        column: x => x.ResourceId,
                        principalSchema: "sec",
                        principalTable: "Resource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessControl_ResourceId",
                schema: "sec",
                table: "AccessControl",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "ResourceNameIndex",
                schema: "sec",
                table: "Resource",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resource_PageResourceId",
                schema: "sec",
                table: "Resource",
                column: "PageResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_MenuPageId",
                schema: "sec",
                table: "Resource",
                column: "MenuPageId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_ParenMenuResourceId",
                schema: "sec",
                table: "Resource",
                column: "ParenMenuResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessControl",
                schema: "sec");

            migrationBuilder.DropTable(
                name: "Resource",
                schema: "sec");
        }
    }
}
