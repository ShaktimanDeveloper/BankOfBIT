using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Data;

namespace BankOfBIT_ArshdeepSangha.Models
{
    /// <summary>
    /// An abstract class for accountstate .Further has 4 states.
    /// </summary>
    public abstract class AccountState
    {
        //Autoimplented property and Primary key for Accountstate class
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccountStateId { get; set; }

        //Lowerlimit for the account state
        [Display(Name="Lower\nLimit")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public double LowerLimit { get; set; }

        //Upper Limit for the account state.
        [Display(Name = "Upper\nLimit")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double UpperLimit { get; set; }

        //Interest rate for the account class
        [Display(Name = "Interest\nRate")]
        [DisplayFormat(DataFormatString ="{0:P}")]
        public double Rate { get; set; }

        //Description of the type of the state without the word state.
        [Display(Name = "Account\nState")]
        public String Description 
        {
            get
            {
                //Code below cuts out the last five characters.
                return GetType().Name.Substring(0,GetType().Name.Length-5);
            }
        }

        //Rate Adjustment double
        public virtual double RateAdjustment(BankAccount bankAccount) 
        {
            return Rate;
        }

        //Void method for the state change among the states.
        public virtual void StateChangeCheck(BankAccount bankAccount){}

        //Navigational property for Bank Account
        public ICollection<BankAccount> BankAccount { get; set; }
    }

    /// <summary>
    /// Abstract class for Bankaccount class.
    /// </summary>
    public abstract class BankAccount
    {
        //Autoimplented property and Primary key for Bankaccount class
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BankAccountId { get; set; }
        
        //Account number property
        [Display(Name="Account\nNumber")]
        public long AccountNumber { get; set; }

        //Client id foreign key
        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        //Account state id as a foreign key
        [Required]
        [ForeignKey("AccountState")]
        public int AccountStateId { get; set; }

        //Current balance property
        [Required]
        [Display(Name="Current\nBalance")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public double Balance { get; set; }

        //Opening balance property
        [Required]
        [Display(Name="Opening\nBalance")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public double OpeningBalance { get; set; }

        //Datecreated property for the bankaccount.
        [Required]
        [Display(Name="Date\nCreated")]
        [DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        //Account notes property
        [Display(Name="Account\nNotes")]
        public string Notes { get; set; }

        //Account type property
        [Display(Name="Account\nType")]
        public string Description 
        {
            get 
            {
                //Removes the word Account after the account type name
                return GetType().Name.Substring(0,GetType().Name.LastIndexOf("Account"));
            }
        }

        //Sets the next account number for the new account created
        public void SetNextAccountNumber()
        {}

        //Changed the state of the account.
        public void ChangeState() 
        {
            //The DB instance 
            BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

            //The old id of the account state
            AccountState oldId = db.AccountStates.Where(x => x.AccountStateId == this.AccountStateId).SingleOrDefault();

            //The new id of the account state which is null.
            AccountState newId = null;

            //Runs the loop until they don't match
            while (newId != oldId)
            {
                //when the ids are equal
                oldId = newId;

                //The new id fetches the id of the account state.
                newId = db.AccountStates.Where(x => x.AccountStateId == this.AccountStateId).SingleOrDefault();

                //The new id is assigned the right state
                newId.StateChangeCheck(this);
            }
        }

        //Navigational Property
        public ICollection<Transaction> Transaction { get; set; }

        //Virtual class for the client
        public virtual Client Client { get; set; }

        //Virtual class for the account state.
        public virtual AccountState AccountState { get; set; }

    }

    /// <summary>
    /// The client class containing client properties.
    /// </summary>
    public class Client
    {
        //Autoimplented property and Primary key for Accountstate class
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        //Client number Property
        [Display(Name="Client")]
        public long ClientNumber { get; set; }

        //First name property
        [Required]
        [Display(Name = "First\nName")]
        [StringLength(35, MinimumLength = 1)]
        public string FirstName { get; set; }

        //last name property
        [Required]
        [Display(Name = "Last\nName")]
        [StringLength(35, MinimumLength = 1)]
        public string LastName { get; set; }

        //Street address property
        [Required]
        [Display(Name = "Street\nAddress")]
        [StringLength(35, MinimumLength = 1)]
        public string Address { get; set; }

        //City property
        [Required]
        [Display(Name = "City")]
        [StringLength(35, MinimumLength = 1)]
        public string City { get; set; }

        //Canadian province verfication regular expression
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK)$", ErrorMessage = "Invalid Province")]
        public string Province { get; set; }

        //Canadian postal code regular expression verification
        [RegularExpression("[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage = "Invalid Postal Code")]
        [Display(Name="Postal\nCode")]
        public string PostalCode { get; set; }

        //Date Created property
        [Required]
        [Display(Name="Date\nCreated")]
        [DisplayFormat(DataFormatString = "{0:D}",ApplyFormatInEditMode=true)]
        public string DateCreated { get; set; }

        //Client Notes property
        [Display(Name="Client\nNotes")]
        public string Notes { get; set; }

        //Full name property which has First name and last name.
        [Display(Name = "Name")]
        public string FullName 
        {
            get 
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        //Full address property .
        [Display(Name="Address")]
        public string FullAddress 
        {
            get 
            {
                return string.Format("{0} {1} {2}", Address,Province, PostalCode);
            }
        }

        //Next client method, automatically sets the client number to something autogenerated
        public void SetNextClientNumber()
        {
            this.ClientNumber = (long)StoredProcedures.NextNumber("NextClientNumbers");
        }

        //ICollections for navigational Properties
        public ICollection<BankAccount> BankAccount { get; set; }
        public ICollection<RFIDTag> RFIDTag { get; set; }
    }

    /// <summary>
    /// Bronze state instance class
    /// </summary>
    public class BronzeState : AccountState
    {
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        private static BronzeState bronzeState;

        private double LOWER_LIMIT = 0;
        private double UPPER_LIMIT = 5000;
        private double RATE = 0.01;

        private BronzeState() 
        {
            LowerLimit = LOWER_LIMIT;
            UpperLimit = UPPER_LIMIT;
            Rate = RATE;
        }

        /// <summary>
        /// This method sees if there's an instance of the bronxe state , if not adds one to it
        /// </summary>
        /// <returns>the instance of bronze state</returns>
        public static BronzeState GetInstance() 
        {
            //Checks if the bronze state is null
            if (bronzeState == null)
            {
                //takes the single or default value if its null
                bronzeState = db.BronzeStates.SingleOrDefault();

                //If the bronze state is still null in the db, creates a new Bronze state and adds it to the database.
                if (bronzeState == null)
                {
                    bronzeState = new BronzeState();
                    db.AccountStates.Add(bronzeState);
                    db.SaveChanges();
                }
            }

            return bronzeState; 
        }
        
        //method to for rate adjustment
        public override double RateAdjustment(BankAccount bankAccount) 
        {
            if (bankAccount.Balance > 0)
            {
                return Rate;
            }
            else
            {
                return 0;
            }
        }

        //Changes the state if the account balance changes and if its not a mortgage account
        public override void StateChangeCheck(BankAccount bankAccount) 
        {
            if (bankAccount.Balance > UPPER_LIMIT && !bankAccount.Description.Equals("Mortgage")) 
            {
                bankAccount.AccountStateId = SilverState.GetInstance().AccountStateId;
            }

        }

    }

    /// <summary>
    /// the goldstate instance of Account state
    /// </summary>
    public class GoldState : AccountState
    {
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        private static GoldState goldState;

        private double LOWER_LIMIT = 10000;
        private double UPPER_LIMIT = 20000;
        private double RATE = 0.02;
        private double BONUS_RATE = 0.01;
        int Age = 0;

        private GoldState() 
        {
            LowerLimit = LOWER_LIMIT;
            UpperLimit = UPPER_LIMIT;
            Rate = RATE;
        }

        //Checks if the State is null
        public static GoldState GetInstance() 
        {
            if (goldState == null)
            {
                //takes the single or default value if its null
                goldState = db.GoldStates.SingleOrDefault();

                //If the state is still null in the db, creates a new state and adds it to the database.
                if (goldState == null)
                {
                    goldState = new GoldState();

                    db.AccountStates.Add(goldState);
                    db.SaveChanges();
                }
            }

            return goldState;
        }

        //method to for rate adjustment
        public override double RateAdjustment(BankAccount bankAccount)
        {
            Age = DateTime.Today.Year - bankAccount.DateCreated.Year;

            if (Age > 10)
            {
                return (Rate + BONUS_RATE);
            }
            else
            {
                return Rate;
            }
            
        }

        //Changes the state if the account balance changes and if its not a mortgage account
        public override void StateChangeCheck(BankAccount bankAccount) 
        {
            if (bankAccount.Balance > UPPER_LIMIT && !bankAccount.Description.Equals("Mortgage"))
            {
                bankAccount.AccountStateId = PlatinumState.GetInstance().AccountStateId;
            }
            if (bankAccount.Balance < LOWER_LIMIT && !bankAccount.Description.Equals("Mortgage"))
            {
                bankAccount.AccountStateId = SilverState.GetInstance().AccountStateId;
            }
        }
    }

    /// <summary>
    /// Silver state account instance.
    /// </summary>
    public class SilverState : AccountState
    {
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        private static SilverState silverState;

        private double LOWER_LIMIT = 5000;
        private double UPPER_LIMIT = 10000;
        private double RATE = 0.0125;

        private SilverState() 
        {
            LowerLimit = LOWER_LIMIT;
            UpperLimit = UPPER_LIMIT;
            Rate = RATE;
        }

        //Checks if the State is null
        public static SilverState GetInstance() 
        {
            if (silverState == null)
            {
                //takes the single or default value if its null
                silverState = db.SilverStates.SingleOrDefault();

                //If the state is still null in the db, creates a new state and adds it to the database.
                if (silverState == null)
                {
                    silverState = new SilverState();
                    db.AccountStates.Add(silverState);
                    db.SaveChanges();
                }
            }
            return silverState; 
        }

        //method to for rate adjustment
        public override double RateAdjustment(BankAccount bankAccount) 
        {
            return Rate; 
        }

        //Changes the state if the account balance changes and if its not a mortgage account
        public override void StateChangeCheck(BankAccount bankAccount) 
        {
            if (bankAccount.Balance > UPPER_LIMIT && !bankAccount.Description.Equals("Mortgage"))
            {
                bankAccount.AccountStateId = GoldState.GetInstance().AccountStateId;
            }

            if (bankAccount.Balance < LOWER_LIMIT && !bankAccount.Description.Equals("Mortgage"))
            {
                bankAccount.AccountStateId = BronzeState.GetInstance().AccountStateId;
            }
        }
    }

    /// <summary>
    /// Platinum account state class.
    /// </summary>
    public class PlatinumState : AccountState
    {
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        private static PlatinumState platinumState;

        private double LOWER_LIMIT = 20000;
        private double UPPER_LIMIT = 0;
        private double RATE = 0.0250;
        int age = 0;
        private double BONUS_RATE = 0.01;
        private double ADD_BONUS_RATE = 0.005;

        private PlatinumState() 
        {
            LowerLimit = LOWER_LIMIT;
            UpperLimit = UPPER_LIMIT;
            Rate = RATE;
        }

        //Checks if the State is null
        public static PlatinumState GetInstance() 
        {
            if (platinumState == null)
            {
                //takes the single or default value if its null
                platinumState = db.PlatinumStates.SingleOrDefault();

                //If the state is still null in the db, creates a new state and adds it to the database.
                if (platinumState == null)
                {
                    platinumState = new PlatinumState();
                    db.AccountStates.Add(platinumState);
                    db.SaveChanges();
                }
            }
            return platinumState; 
        }

        //method to for rate adjustment
        public override double RateAdjustment(BankAccount bankAccount) 
        { 
            //Calculation to determine the age of the account
            age = DateTime.Now.Year - bankAccount.DateCreated.Year;

            //If age of the account is more than or equal to 10
            if (age >= 10)
            {
                //if the bankaccount balance is more than the lower limit.
                if (bankAccount.Balance > (2 * LOWER_LIMIT))
                {
                    //Rate will include standard rate , bonus rate for more than 10 years and Additional bonus rate for high balance.
                    return (Rate + BONUS_RATE + ADD_BONUS_RATE);
                }
                else
                {
                    //Rate will include standard rate and bonus rate for more than or equal to 10 years
                    return (Rate + BONUS_RATE);
                }

            }
            else 
            {
                //If none conditions true, return normal rate.
                return Rate;
            }
        }

        //Changes the state if the account balance changes and if its not a mortgage account
        public override void StateChangeCheck(BankAccount bankAccount) 
        {
            if (bankAccount.Balance < LOWER_LIMIT && !bankAccount.Description.Equals("Mortgage"))
            {
                bankAccount.AccountStateId = GoldState.GetInstance().AccountStateId;
            }
        }
    }

    /// <summary>
    /// The Savings account instance.
    /// </summary>
    public class SavingsAccount : BankAccount
    {
        [Required]
        [Display(Name="Service\nCharges")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public double SavingsServiceCharge { get; set; }

        /// <summary>
        /// This method generate next account number automatically
        /// </summary>
        public void SetNextAccountNumber()
        {
            this.AccountNumber = (long)StoredProcedures.NextNumber("NextSavingsAccounts");
        }
    }

    /// <summary>
    /// Investment account instance.
    /// </summary>
    public class InvestmentAccount : BankAccount
    {
        [Required]
        [Display(Name="Interest\nRate")]
        [DisplayFormat(DataFormatString="{0:P}")]
        public double InterestRate { get; set; }

        /// <summary>
        /// This method generate next account number automatically
        /// </summary>
        public void SetNextAccountNumber()
        {
            this.AccountNumber = (long)StoredProcedures.NextNumber("NextInvestmentAccounts");
        }
    }

    /// <summary>
    /// Mortgage account instance
    /// </summary>
    public class MortgageAccount : BankAccount
    {
        [Required]
        [Display(Name="Interest\nRate")]
        [DisplayFormat(DataFormatString="{0:P}")]
        public double MortgageRate { get; set; }

        [Required]
        [Display(Name="Amoritization")]
        public int Amoritization { get; set; }

        /// <summary>
        /// This method generate next account number automatically
        /// </summary>
        public void SetNextAccountNumber()
        {
            this.AccountNumber = (long)StoredProcedures.NextNumber("NextMortgageAccounts");
        }
    }

    /// <summary>
    /// Chequing account instance.
    /// </summary>
    public class ChequingAccount : BankAccount
    {
        [Required]
        [Display(Name="Service\nCharges")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public double ChequingServiceCharges { get; set; }

        /// <summary>
        /// This method generate next account number automatically
        /// </summary>
        public void SetNextAccountNumber()
        {
            this.AccountNumber = (long)StoredProcedures.NextNumber("NextChequingAccounts");
        }

    }

    /// <summary>
    /// Payee Class for Payee
    /// </summary>
    public class Payee
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PayeeId { get; set; }

        [Display(Name = "Payee")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Institution Class for Institution.
    /// </summary>
    public class Institution
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InstitutionId { get; set; }

        [Display(Name = "Institution\nNumber")]
        public int InstitutionNumber { get; set; }

        [Display(Name = "Institution")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Transaction Class for Bank Account Transaction.
    /// </summary>
    public class Transaction
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Display(Name = "Transaction\nNumber")]
        public long TransactionNumber { get; set; }

        [Required]
        [ForeignKey("BankAccount")]
        public int BankAccountId { get; set; }

        [Required]
        [ForeignKey("TransactionType")]
        [Display(Name ="Transaction\nType")]
        public int TransactionTypeId { get; set; }

        [Display(Name = "Deposit")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Deposit { get; set; }

        [Display(Name = "Withdrawal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Withdrawal { get; set; }

        [Required]
        [Display(Name="Date\nCreated")]
        [DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Display(Name ="Notes")]
        public string Notes { get; set; }
        
        //Method for auto generating next transaction number
        public void SetNextTransactionNumber()
        {
            this.TransactionNumber = (long)StoredProcedures.NextNumber("NextTransactionNumbers");
        }

        //Virtual Navigational Properties.
        public virtual BankAccount BankAccount { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }

    /// <summary>
    /// TransactionType class for the type of transaction occured
    /// </summary>
    public class TransactionType
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionTypeId { get; set; }

        [Display(Name = "Transaction\nType")]
        public string Description { get; set; }

        [Display(Name = "Service\nCharges")] 
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double ServiceCharges { get; set; }

        //Navigational Property
        public ICollection<Transaction> Transaction { get; set; }
    }

    /// <summary>
    /// Auxiliary Class for Next Transaction number.
    /// </summary>
    public class NextTransactionNumber
    {
        //Instance of Database
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextTransactionNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextTransactionNumber nextTransactionNumber;

        //Private constructor
        private NextTransactionNumber() 
        {
            NextAvailableNumber = 700;
        }

        /// <summary>
        /// Method to get the instance of Next Transaction number
        /// </summary>
        /// <returns></returns>
        public static NextTransactionNumber GetInstance()
        {
            //If the nextTransactionNumber is null
            if (nextTransactionNumber == null)
            {
                //It grabs the the single or default value from Database
                nextTransactionNumber = db.NextTransactionNumbers.SingleOrDefault();

                //If there is still no value , then it creates an instance and takes it from there.
                if (nextTransactionNumber == null)
                {
                    nextTransactionNumber = new NextTransactionNumber();
                    db.NextTransactionNumbers.Add(nextTransactionNumber);
                    db.SaveChanges();
                }
            }
            return nextTransactionNumber;
        }
    }

    /// <summary>
    /// An auxiliary class for next savings account.
    /// </summary>
    public class NextSavingsAccount
    {
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextSavingsAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextSavingsAccount nextSavingsAccount;

        //Private constructor
        private NextSavingsAccount()
        {
            NextAvailableNumber = 20000;
        }

        /// <summary>
        /// Method to Check if there is an instance of NextSavingsAccount
        /// </summary>
        /// <returns></returns>
        public static NextSavingsAccount GetInstance()
        {
            if (nextSavingsAccount == null)
            {
                nextSavingsAccount = db.NextSavingsAccounts.SingleOrDefault();

                if (nextSavingsAccount == null)
                {
                    nextSavingsAccount = new NextSavingsAccount();
                    db.NextSavingsAccounts.Add(nextSavingsAccount);
                    db.SaveChanges();
                }
            }
            return nextSavingsAccount;
        }
    }

    /// <summary>
    /// An auxiliary class for next mortagage account 
    /// </summary>
    public class NextMortagageAccount
    {
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextMortagageAccountId { get; set; }

         public long NextAvailableNumber { get; set; }

        private static NextMortagageAccount nextMortagageAccount;

        //Private Constructor
        private NextMortagageAccount()
        {
            NextAvailableNumber = 200000;
        }

        /// <summary>
        /// method to get the instance of the NextMortagageAccount
        /// </summary>
        /// <returns></returns>
        public static NextMortagageAccount GetInstance()
        {
            if (nextMortagageAccount == null)
            {
                nextMortagageAccount = db.NextMortagageAccounts.SingleOrDefault();

                if (nextMortagageAccount == null)
                {
                    nextMortagageAccount = new NextMortagageAccount();
                    db.NextMortagageAccounts.Add(nextMortagageAccount);
                    db.SaveChanges();
                }
            }
            return nextMortagageAccount;
        }
    }

    /// <summary>
    /// An auxiliary class for next investment account
    /// </summary>
    public class NextInvestmentAccount
    {
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextInvestmentAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextInvestmentAccount nextInvestmentAccount;

        //Private constructor
        private NextInvestmentAccount()
        {
            NextAvailableNumber = 2000000;
        }

        /// <summary>
        /// Method to check if there is an instance of the NextInvestmentAccount.
        /// </summary>
        /// <returns></returns>
        public static NextInvestmentAccount GetInstance()
        {
            if (nextInvestmentAccount == null)
            {
                nextInvestmentAccount = db.NextInvestmentAccounts.SingleOrDefault();

                if (nextInvestmentAccount == null)
                {
                    nextInvestmentAccount = new NextInvestmentAccount();
                    db.NextInvestmentAccounts.Add(nextInvestmentAccount);
                    db.SaveChanges();
                }
            }
            return nextInvestmentAccount;
        }
    }

    /// <summary>
    /// an auxiliary class for next chequing account
    /// </summary>
    public class NextChequingAccount
    {
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextChequingAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextChequingAccount nextChequingAccount;

        //private constructor
        private NextChequingAccount()
        {
            NextAvailableNumber = 20000000;
        }

        /// <summary>
        /// Method to check if there is an instance of NextChequingAccount.
        /// </summary>
        /// <returns></returns>
        public static NextChequingAccount GetInstance()
        {
            if (nextChequingAccount == null)
            {
                nextChequingAccount = db.NextChequingAccounts.SingleOrDefault();

                if (nextChequingAccount == null)
                {
                    nextChequingAccount = new NextChequingAccount();
                    db.NextChequingAccounts.Add(nextChequingAccount);
                    db.SaveChanges();
                }
            }
            return nextChequingAccount;
        }
    }

    /// <summary>
    /// An auxiliary class for Next client number.
    /// </summary>
    public class NextClientNumber
    {
        protected static BankOfBIT_ArshdeepSanghaContext db = new BankOfBIT_ArshdeepSanghaContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextClientNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextClientNumber nextClientNumber;

        //Private constructor
        private NextClientNumber() 
        {
            NextAvailableNumber = 20000000;
        }

        //To check if the NextClientNumber is null and if it has an instance
        public static NextClientNumber GetInstance() 
        {
            if (nextClientNumber == null)
            {
                nextClientNumber = db.NextClientNumbers.SingleOrDefault();

                if (nextClientNumber == null)
                {
                    nextClientNumber = new NextClientNumber();
                    db.NextClientNumbers.Add(nextClientNumber);
                    db.SaveChanges();
                }
            }
            return nextClientNumber;
        }
    }

    /// <summary>
    /// An RFIDTag for the RFID tag.
    /// </summary>
    public class RFIDTag
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RFIDTagId { get; set; }

        [Display(Name="Card\nNumber")]
        public long CardNumber { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        //Virtual property.
        public virtual Client Client { get; set; }
    }

    /// <summary>
    /// Static class for stored procedures.
    /// </summary>
    public static class StoredProcedures
    {
        /// <summary>
        /// Public static class for generating next long number
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static long? NextNumber(string tableName) 
        {
            //Database connection instantiates a new connection to database
            SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=BankofBIT_ArshdeepSanghaContext;Integrated Security=True");
 
            //defined variable to return value
            long? returnValue = 0; 

            //assigning a for a query in database
            SqlCommand storedProcedure = new SqlCommand("next_number", connection); 

            //stored procedure command type
            storedProcedure.CommandType = CommandType.StoredProcedure; 

            //Adds new data to the database
            storedProcedure.Parameters.AddWithValue("@TableName", tableName); 

            //Assigning values
            SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt) 
            { 
                //The direction of parameter is out
                Direction = ParameterDirection.Output 
            };

            try
            {
                //Try adding parameters
                storedProcedure.Parameters.Add(outputParameter);
                connection.Open();

                //Try execute the stored procedure of setting the next number.
                storedProcedure.ExecuteNonQuery();
                returnValue = (long?)outputParameter.Value;
            }
            catch (Exception)
            {
                //Catch null exception.
                returnValue = null;
            }
            finally
            {
                //Close connection
                connection.Close();
            }

            //Return long value
            return returnValue;
        }
    }
}