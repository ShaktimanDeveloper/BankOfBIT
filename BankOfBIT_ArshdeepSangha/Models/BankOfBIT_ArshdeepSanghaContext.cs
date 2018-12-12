using System.Data.Entity;

namespace BankOfBIT_ArshdeepSangha.Models
{
    public class BankOfBIT_ArshdeepSanghaContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<BankOfBIT_ArshdeepSangha.Models.BankOfBIT_ArshdeepSanghaContext>());

        public BankOfBIT_ArshdeepSanghaContext() : base("name=BankOfBIT_ArshdeepSanghaContext")
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<AccountState> AccountStates { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<BronzeState> BronzeStates { get; set; }

        public DbSet<GoldState> GoldStates { get; set; }

        public DbSet<SilverState> SilverStates { get; set; }

        public DbSet<PlatinumState> PlatinumStates { get; set; }

        public DbSet<SavingsAccount> SavingsAccounts { get; set; }

        public DbSet<InvestmentAccount> InvestmentAccounts { get; set; }

        public DbSet<MortgageAccount> MortgageAccounts { get; set; }

        public DbSet<ChequingAccount> ChequingAccounts { get; set; }

        public DbSet<Payee> Payees { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionType> TransactionTypes { get; set; }

        public DbSet<NextTransactionNumber> NextTransactionNumbers { get; set; }

        public DbSet<NextSavingsAccount> NextSavingsAccounts { get; set; }

        public DbSet<NextInvestmentAccount> NextInvestmentAccounts { get; set; }

        public DbSet<NextMortagageAccount> NextMortagageAccounts { get; set; }

        public DbSet<NextChequingAccount> NextChequingAccounts { get; set; }

        public DbSet<NextClientNumber> NextClientNumbers { get; set; }

        public DbSet<RFIDTag> RFIDTags { get; set; }
    }
}
