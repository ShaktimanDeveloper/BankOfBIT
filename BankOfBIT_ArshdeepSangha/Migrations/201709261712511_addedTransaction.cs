namespace BankOfBIT_ArshdeepSangha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        TransactionNumber = c.Long(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        TransactionTypeId = c.Int(nullable: false),
                        Deposit = c.Double(nullable: false),
                        Withdrawal = c.Double(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionTypes", t => t.TransactionTypeId, cascadeDelete: true)
                .Index(t => t.BankAccountId)
                .Index(t => t.TransactionTypeId);
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        TransactionTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ServiceCharges = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionTypeId);
            
            CreateTable(
                "dbo.RFIDTags",
                c => new
                    {
                        RFIDTagId = c.Int(nullable: false, identity: true),
                        CardNumber = c.Long(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RFIDTagId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PayeeId);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        InstitutionId = c.Int(nullable: false, identity: true),
                        InstitutionNumber = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.InstitutionId);
            
            CreateTable(
                "dbo.NextTransactionNumbers",
                c => new
                    {
                        NextTransactionNumberId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextTransactionNumberId);
            
            CreateTable(
                "dbo.NextSavingsAccounts",
                c => new
                    {
                        NextSavingsAccountId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextSavingsAccountId);
            
            CreateTable(
                "dbo.NextInvestmentAccounts",
                c => new
                    {
                        NextInvestmentAccountId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextInvestmentAccountId);
            
            CreateTable(
                "dbo.NextMortagageAccounts",
                c => new
                    {
                        NextMortagageAccountId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextMortagageAccountId);
            
            CreateTable(
                "dbo.NextChequingAccounts",
                c => new
                    {
                        NextChequingAccountId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextChequingAccountId);
            
            CreateTable(
                "dbo.NextClientNumbers",
                c => new
                    {
                        NextClientNumberId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.NextClientNumberId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RFIDTags", new[] { "ClientId" });
            DropIndex("dbo.Transactions", new[] { "TransactionTypeId" });
            DropIndex("dbo.Transactions", new[] { "BankAccountId" });
            DropForeignKey("dbo.RFIDTags", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Transactions", "TransactionTypeId", "dbo.TransactionTypes");
            DropForeignKey("dbo.Transactions", "BankAccountId", "dbo.BankAccounts");
            DropTable("dbo.NextClientNumbers");
            DropTable("dbo.NextChequingAccounts");
            DropTable("dbo.NextMortagageAccounts");
            DropTable("dbo.NextInvestmentAccounts");
            DropTable("dbo.NextSavingsAccounts");
            DropTable("dbo.NextTransactionNumbers");
            DropTable("dbo.Institutions");
            DropTable("dbo.Payees");
            DropTable("dbo.RFIDTags");
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.Transactions");
        }
    }
}
