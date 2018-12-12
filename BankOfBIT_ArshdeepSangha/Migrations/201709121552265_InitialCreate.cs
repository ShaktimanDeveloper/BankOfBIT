namespace BankOfBIT_ArshdeepSangha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientNumber = c.Long(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 35),
                        LastName = c.String(nullable: false, maxLength: 35),
                        Address = c.String(nullable: false, maxLength: 35),
                        City = c.String(nullable: false, maxLength: 35),
                        Province = c.String(),
                        PostalCode = c.String(),
                        DateCreated = c.String(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.AccountStates",
                c => new
                    {
                        AccountStateId = c.Int(nullable: false, identity: true),
                        LowerLimit = c.Double(nullable: false),
                        UpperLimit = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.AccountStateId);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountId = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Long(nullable: false),
                        ClientId = c.Int(nullable: false),
                        AccountStateId = c.Int(nullable: false),
                        Balance = c.Double(nullable: false),
                        OpeningBalance = c.Double(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Notes = c.String(),
                        SavingsServiceCharge = c.Double(),
                        InterestRate = c.Double(),
                        MortgageRate = c.Double(),
                        Amoritization = c.Int(),
                        ChequingServiceCharges = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BankAccountId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.AccountStates", t => t.AccountStateId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.AccountStateId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BankAccounts", new[] { "AccountStateId" });
            DropIndex("dbo.BankAccounts", new[] { "ClientId" });
            DropForeignKey("dbo.BankAccounts", "AccountStateId", "dbo.AccountStates");
            DropForeignKey("dbo.BankAccounts", "ClientId", "dbo.Clients");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.AccountStates");
            DropTable("dbo.Clients");
        }
    }
}
