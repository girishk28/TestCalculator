using System;
using System.IO;
using System.Reflection;
using Calculator;
using Ninject;
using System.Net.Http;
using System.Threading.Tasks;
using SimpleCalculatorClass = Calculator.SimpleCalculator;

namespace SimpleCalculatorConsole
{
    /// <summary>
    /// Console Application that write diagnostics information to database using Entity Framework and Stored Procedure.
    /// This application also calls the Calculator API to perform it's calculations
    /// </summary>
    
    class Program
    {
        static int userInput = 0;
        private const string resourceUrlEntity = "api/v1/Calculator/EF/";
        private const string resourceUrlStoredProcedure = "api/v1/Calculator/SP/";

        static void Main(string[] args)
        {
        do
          {
                userInput = DisplayMainMenu();
              switch (userInput)
              {
                    case 1:
                        OutputToDatabase();
                      break;
                    case 2:
                      CallCalculatorAPI();
                      break;
                    case 3:
                      break;
              }


          } while (userInput != 3);
            
        } 

        /// <summary>
        /// Call the Calculator API to perform calculation operations
        /// </summary>
        public static void CallCalculatorAPI()
        {
            //calling the Web API using HttpClient

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50382/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            userInput = 0;
            int numberOne = 0;
            int numberTwo = 0;
            int accessType;
            string action = string.Empty;
            do
            {
                userInput = DisplayCalculatorMenu();

                switch (userInput)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        Console.WriteLine();
                        Console.Write("Enter the first number : ");
                        numberOne = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        Console.Write("Enter the second number : ");
                        numberTwo = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Choose an Access Type : ");
                        Console.WriteLine();
                        Console.WriteLine("1. Using Entity Framework");
                        Console.WriteLine("2. Using Stored Procedure");
                        Console.WriteLine();
                        Console.Write("Enter your choice : ");
                        accessType = Convert.ToInt32(Console.ReadLine());
                        action = userInput == 1
                            ? "add"
                            : userInput == 2
                                ? "subtract"
                                : userInput == 3 ? "multiply" : userInput == 4 ? "divide" : string.Empty;
                        CalculatorAPI(client, action, numberOne, numberTwo, accessType).Wait();
                        break;
                    case 5:
                        break;
                }

            } while (userInput != 5);
        }

        /// <summary>
        /// Outputs the diagnostics information to the console
        /// </summary>
        public static void OutputToDatabase()
        {
              int result  = 0;
            
            do
            {
                Console.WriteLine();
                Console.WriteLine("Output Diagnostics to Database");
                Console.WriteLine();
                Console.WriteLine("1. Using Entity Framework");
                Console.WriteLine("2. Using Stored Procedure Framework");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.Write("Enter your choice : ");

                result =   Convert.ToInt32(Console.ReadLine());

                switch (result)
                {
                    case 1:
                          DataAccess("DbEntity");
                        break;
                    case 2:
                          DataAccess("DbStoredProc");
                        break;
                    case 3:
                        break;
                }
            } while (result != 3);
           
       }

        /// <summary>
        /// Performs the data access operation bases on the selected access layer
        /// </summary>
        /// <param name="source"></param>
        public static void DataAccess(string source)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());


            var calculatorDiagnostics = kernel.Get<IDiagnostics>(source);
            var dummyDiagnostics = kernel.Get<IDummyDiagnostics>();

            ISimpleCalculator calculator = new SimpleCalculatorClass(calculatorDiagnostics, dummyDiagnostics);

            Console.WriteLine();
            Console.Write("Enter the first number : ");

            var firstNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Enter the second number : ");
            var secondNumber = Convert.ToInt32(Console.ReadLine());


            calculator.Add(firstNumber, secondNumber);
            calculator.Subtract(firstNumber, secondNumber);
            calculator.Multiply(firstNumber, secondNumber);
            calculator.Divide(firstNumber, secondNumber);
            Console.WriteLine();
            Console.WriteLine("Successfully updated to database !");
        }

        /// <summary>
        /// Calls the Calculator API
        /// </summary>
        /// <param name="client">Http Client</param>
        /// <param name="action">The operation to be performed</param>
        /// <param name="numberOne">The first number</param>
        /// <param name="numberTwo">The second number</param>
        /// <returns></returns>
        static async Task CalculatorAPI(HttpClient client, string action, int numberOne, int numberTwo, int accessType)
        {
            using (client)
            {
                var calculator = new CalculatorRequest {NumberOne = numberOne, NumberTwo = numberTwo};
                var resourceUrl = accessType == 1 ? resourceUrlEntity : resourceUrlStoredProcedure;
                HttpResponseMessage response = await client.PostAsJsonAsync(resourceUrl + action, calculator);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("----------------------- Calling Calculator API ----------------------");
                    Console.WriteLine();
                    var calculatorResponse = await response.Content.ReadAsAsync<CalculatorResponse>();
                    string result = string.Empty;
                    switch(action)
                    {
                        case "add":
                        result = "The addition result of " +calculator.NumberOne  + " + " + calculator.NumberTwo + " = " + calculatorResponse.Result;
                            break;
                        case "subtract":
                            result = "The subtraction result of " +calculator.NumberOne + " - " + calculator.NumberTwo + " = " + calculatorResponse.Result;
                            break;
                        case "multiply":
                                result = "The multiplication result of " +calculator.NumberOne + " - " + calculator.NumberTwo + " = " + calculatorResponse.Result;
                            break;
                        case "divide":
                            result = "The division result of " + calculator.NumberOne + " - " + calculator.NumberTwo + " = " + calculatorResponse.Result;
                            break;

                    }
                    Console.WriteLine(result);
                    Console.WriteLine();
                    Console.WriteLine("------------------ Successfully Called Calculator API ---------------");
                    Console.ReadLine();
                }
            }
        }
        /// <summary>
        /// Display Calculator Menu
        /// </summary>
        /// <returns></returns>
        public static int DisplayCalculatorMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Calculator API");
            Console.WriteLine();
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Multiply");
            Console.WriteLine("4. Divide");
            Console.WriteLine("5. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice : ");
            
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Display Main Menu
        /// </summary>
        /// <returns></returns>
        public static int DisplayMainMenu()
        {
            Console.WriteLine("\n*********** CALCULATOR APPLICATION MAIN MENU *******************");
            Console.WriteLine();
            Console.WriteLine("1. Output the calculation operation and stores it to Database");
            Console.WriteLine("2. Call Calculator API to perform calculation operation");
            Console.WriteLine("3. Exit");
            Console.WriteLine("\n****************************************************************");
            Console.WriteLine();
            Console.Write("Enter your choice : ");

            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }
    }


    public class CalculatorRequest
    {
        public int NumberOne { get; set; }
        public int NumberTwo { get; set; }
    }
    public class CalculatorResponse
    {
        public int Result { get; set; }
    }
}
