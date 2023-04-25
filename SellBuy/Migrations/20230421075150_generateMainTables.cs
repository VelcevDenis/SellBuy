using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellBuy.Migrations
{
    /// <inheritdoc />
    public partial class generateMainTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    JSONSubPayments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomizedSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JSONSettings = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomizedSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExperiationAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Json = table.Column<bool>(type: "bit", nullable: false),
                    SubPayments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Values = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "RegisteredAt", "Role", "Status", "Username" },
                values: new object[] { 1, "it-admin@sellBuy.com", "Test123!", new DateTime(2023, 4, 21, 7, 51, 50, 620, DateTimeKind.Utc).AddTicks(3452), 0, 0, "it-admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_OrderId",
                table: "Images",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.InsertData(
               table: "CustomizedSettings",
               columns: new[] { "Type", "JSONSettings" },
               values: new object[] {"PhoneCuntryCodes", "[{\"ukraine\":380},\r\n {\"romania\":40},\r\n {\"russia\":7},\r\n {\"france\":33},\r\n {\"germany\":49},\r\n {\"ireland\":353},\r\n {\"israel\":972},\r\n {\"italy\":39},\r\n {\"netherlands\":31},\r\n {\"australia\":61},\r\n {\"austria\":43},\r\n {\"azerbaijan\":994},\r\n {\"åland_islands\":358},\r\n {\"albania\":355},\r\n {\"algeria\":213},\r\n {\"american_samoa\":1},\r\n {\"anguilla\":1},\r\n {\"angola\":244},\r\n {\"andorra\":376},\r\n {\"antigua_and_barbuda\":1},\r\n {\"aomin\":853},\r\n {\"argentina\":54},\r\n {\"armenia\":374},\r\n {\"aruba\":297},\r\n {\"afghanistan\":93},\r\n {\"bahamas\":1},\r\n {\"bangladesh\":880},\r\n {\"barbados\":1},\r\n {\"bahrain\":973},\r\n {\"belarus\":375},\r\n {\"belize\":501},\r\n {\"belgium\":32},\r\n {\"benin\":229},\r\n {\"bermuda\":1},\r\n {\"bulgaria\":359},\r\n {\"bolivia\":591},\r\n {\"bonaire_and_sint_eustatius_and_saba\":599},\r\n {\"bosnia_and_herzegovina\":387},\r\n {\"botswana\":267},\r\n {\"brazil\":55},\r\n {\"british_indian_ocean_territory\":246},\r\n {\"brunei_darussalam\":673},\r\n {\"burkina_faso\":226},\r\n {\"burundi\":257},\r\n {\"butane\":975},\r\n {\"vanuatu\":678},\r\n {\"vatican\":379},\r\n {\"hungary\":36},\r\n {\"venezuela\":58},\r\n {\"virgin_islands_britain\":1},\r\n {\"virgin_islands_usa\":1},\r\n {\"east_timor\":670},\r\n {\"vietnam\":84},\r\n {\"gabon\":241},\r\n {\"haiti\":509},\r\n {\"guyana\":592},\r\n {\"gambia\":220},\r\n {\"ghana\":233},\r\n {\"guadeloupe\":590},\r\n {\"guatemala\":502},\r\n {\"guinea\":224},\r\n {\"guinea-bissau\":245},\r\n {\"germany\":49},\r\n {\"guernsey\":44},\r\n {\"gibraltar\":350},\r\n {\"honduras\":504},\r\n {\"hong_kong\":852},\r\n {\"grenada\":1},\r\n {\"greenland\":299},\r\n {\"greece\":30},\r\n {\"georgia\":995},\r\n {\"guam\":1},\r\n {\"denmark\":45},\r\n {\"jersey\":44},\r\n {\"djibouti\":253},\r\n {\"dominica\":1},\r\n {\"dominican_republic\":1},\r\n {\"egypt\":20},\r\n {\"zambia\":260},\r\n {\"west_sahara\":212},\r\n {\"zimbabwe\":263},\r\n {\"israel\":972},\r\n {\"india\":91},\r\n {\"indonesia\":62},\r\n {\"jordan\":962},\r\n {\"iraq\":964},\r\n {\"iran\":98},\r\n {\"ireland\":353},\r\n {\"iceland\":354},\r\n {\"spain\":34},\r\n {\"italy\":39},\r\n {\"yemen\":967},\r\n {\"cape_verde\":238},\r\n {\"kazakhstan\":7},\r\n {\"cayman_islands\":1},\r\n {\"cambodia\":855},\r\n {\"cameroon\":237},\r\n {\"canada\":1},\r\n {\"qatar\":974},\r\n {\"kenya\":254},\r\n {\"cyprus\":357},\r\n {\"kiribati\":686},\r\n {\"china\":86},\r\n {\"cocos_islands\":61},\r\n {\"colombia\":57},\r\n {\"comoros\":269},\r\n {\"congo\":242},\r\n {\"congo_and_democratic_republic\":243},\r\n {\"democratic_peoples_republic_of_korea\":850},\r\n {\"costa_rica\":506},\r\n {\"cote_divoire\":225},\r\n {\"cuba\":53},\r\n {\"kuwait\":965},\r\n {\"kyrgyzstan\":996},\r\n {\"curacao\":599},\r\n {\"lao_peoples_democratic_republic\":856},\r\n {\"latvia\":371},\r\n {\"lesotho\":266},\r\n {\"liberia\":231},\r\n {\"lebanon\":961},\r\n {\"libya\":218},\r\n {\"lithuania\":370},\r\n {\"liechtenstein\":423},\r\n {\"luxembourg\":352},\r\n {\"mauritius\":230},\r\n {\"mauritania\":222},\r\n {\"madagascar\":261},\r\n {\"mayot\":262},\r\n {\"malawi\":265},\r\n {\"malaysia\":60},\r\n {\"mali\":223},\r\n {\"maldives\":960},\r\n {\"malta\":356},\r\n {\"morocco\":212},\r\n {\"martinique\":596},\r\n {\"marshall_islands\":692},\r\n {\"mexico\":52},\r\n {\"mozambique\":258},\r\n {\"moldova\":373},\r\n {\"monaco\":377},\r\n {\"mongolia\":976},\r\n {\"montserrat\":1},\r\n {\"myanmar\":95},\r\n {\"namibia\":264},\r\n {\"nauru\":674},\r\n {\"nepal\":977},\r\n {\"niger\":227},\r\n {\"nigeria\":234},\r\n {\"netherlands\":31},\r\n {\"nicaragua\":505},\r\n {\"niue\":683},\r\n {\"new_zealand\":64},\r\n {\"new_caledonia\":687},\r\n {\"norway\":47},\r\n {\"united_arab_emirates\":971},\r\n {\"oman\":968},\r\n {\"ascension_island\":247},\r\n {\"isle_of_man\":44},\r\n {\"norfolk_island\":672},\r\n {\"christmas_island\":61},\r\n {\"saint_helena_and_ascension_island_and_tristan_da_cunha\":290},\r\n {\"cook_islands\":682},\r\n {\"turks_and_caikos_islands\":1},\r\n {\"northern_mariana_islands\":1},\r\n {\"pakistan\":92},\r\n {\"palau\":680},\r\n {\"palestine\":970},\r\n {\"panama\":507},\r\n {\"papua_new_guinea\":675},\r\n {\"paraguay\":595},\r\n {\"peru\":51},\r\n {\"poland\":48},\r\n {\"portugal\":351},\r\n {\"puerto_rico\":1},\r\n {\"the_republic_of_korea\":82},\r\n {\"republic_of_macedonia\":389},\r\n {\"reunion\":262},\r\n {\"russia\":7},\r\n {\"rwanda\":250},\r\n {\"romania\":40},\r\n {\"salvador\":503},\r\n {\"samoa\":685},\r\n {\"san_marino\":378},\r\n {\"sao_tome_and_principe\":239},\r\n {\"saudi_arabia\":966},\r\n {\"swaziland\":268},\r\n {\"seychelles\":248},\r\n {\"saint_barthelemy\":590},\r\n {\"saint_martin_france\":590},\r\n {\"saint_pierre_and_miquelon\":508},\r\n {\"senegal\":221},\r\n {\"saint_vincent_and_the_grenadines\":1},\r\n {\"saint_kitts_and_nevis\":1},\r\n {\"saint_lucia\":1},\r\n {\"serbia\":381},\r\n {\"singapore\":65},\r\n {\"sint_maarten_dutch_part\":1},\t\r\n {\"syrian_arab_republic\":963},\r\n {\"slovakia\":421},\r\n {\"slovenia\":386},\r\n {\"united_kingdom\":44},\r\n {\"united_states\":1},\r\n {\"solomon_islands\":677},\r\n {\"somalia\":252},\r\n {\"sudan\":249},\r\n {\"suriname\":597},\r\n {\"sierra_leone\":232},\r\n {\"tajikistan\":992},\r\n {\"thailand\":66},\r\n {\"taiwan\":886},\r\n {\"tanzania\":255},\r\n {\"togo\":228},\r\n {\"tokelau\":690},\r\n {\"tonga\":676},\r\n {\"trinidad_and_tobago\":1},\r\n {\"tristan_da_cunha\":290},\r\n {\"tuvalu\":688},\r\n {\"tunisia\":216},\r\n {\"turkmenistan\":993},\r\n {\"turkey\":90},\r\n {\"uganda\":256},\r\n {\"uzbekistan\":998},\r\n {\"ukraine\":380},\r\n {\"wallace_and_futana\":681},\r\n {\"uruguay\":598},\r\n {\"faroe_islands\":298},\r\n {\"federated_states_of_micronesia\":691},\r\n {\"fiji\":679},\r\n {\"philippines\":63},\r\n {\"finland\":358},\r\n {\"falkland_malvinas\":500},\r\n {\"france\":33},\r\n {\"french_guiana\":594},\r\n {\"french_polynesia\":689},\r\n {\"croatia\":385},\r\n {\"central_african_republic\":236},\r\n {\"chad\":235},\r\n {\"montenegro\":382},\r\n {\"czech_republic\":420},\r\n {\"chile\":56},\r\n {\"switzerland\":41},\r\n {\"sweden\":46},\r\n {\"spitsbergen_and_jan_mayen\":47},\r\n {\"sri_lanka\":94},\r\n {\"ecuador\":593},\r\n {\"equatorial_guinea\":240},\r\n {\"eritrea\":291},\r\n {\"estonia\":372},\r\n {\"ethiopia\":251},\r\n {\"south_africa\":27},\r\n {\"south_sudan\":211},\r\n {\"jamaica\":1},\r\n {\"japan\":81}\r\n ]" }
               );            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CustomizedSettings");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "SubSections");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
