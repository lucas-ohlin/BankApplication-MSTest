using BankApplication;
using System.IO;
using System.Linq;

namespace BankApplication_MSTest {

    [TestClass]
    public class BankSystemTest {

        [TestMethod]
        [Description("Create a customer localy and see if it now exists.")]
        public void CustomerCreationWithValidInput_And_NotAlreadyInTheCustomerList() {

            //Arrange
            //Create the default customers 
            Users.DefaultUserCreation();
            //Valid inputs for the customer creation function
            string name = "Lucas1";
            string pass = "12345678";

            //Act
            //Generate a new customer based on the name and the pass
            //and add it to the customer list
            Users.customerList.Add(new Customer(name, pass, new Dictionary<string, List<string>>()));

            //Assert
            //Check if the customer now exists
            Assert.IsTrue(Users.customerList.Exists(x => x.Name == name));

        }

        [TestMethod]
        [Description("Update the exhange rate as well as see if it succsseded.")]
        public void UpdateTheExchangeRate_AndCheckEqual_FirstLineOfValidFilePath() {

            //Arrange
            //Create the new interaste rate variable along with clearing out the file
            string filePath = "ExchangeRateTest.txt";
            string newInterestRate = "10";
            File.WriteAllText(filePath, String.Empty);

            //Act
            //We write the new interest rate to the file as well as the current date
            //on the line below it, and then close the streamwriter
            using StreamWriter sw = File.CreateText(filePath);
            sw.WriteLine(newInterestRate);
            sw.WriteLine(DateTime.Now.ToString());
            sw.Close();
            //Get the first line in exhangeRate.txt
            string interestRateLine = File.ReadLines(filePath).FirstOrDefault();

            //Assert
            Assert.AreEqual(newInterestRate, interestRateLine);

        }

        [TestMethod]
        public void TransferMoneyBetweenCustomers() {

            //Arrange
            //Create the default customers and make a connection to two of them
            //aswell as the variables used in the exhange rate function
            Users.DefaultUserCreation();
            Customer customer1 = Users.customerList.Find(x => x.Name == "Tobias");
            Customer customer2 = Users.customerList.Find(x => x.Name == "Anas");
            string account1 = "Sparkonto";
            string account2 = "Sparkonto";
            float transferAmount = 100f;
            string expectedAmount = "1100";
    
            //Act
            //Transfer the 100f from customer1's account1 to cusstomer2's account2
            BankSystem.ExchangeRate(customer1, customer2, account1, account2, transferAmount);

            //Assert
            Assert.AreEqual(customer2.accounts["Sparkonto"][0], expectedAmount);

        }

    }

}