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
                        Console.Write("Enter Number 1 : ");
                        numberOne = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();
                        Console.Write("Enter Number 2 : ");
                        numberTwo = Convert.ToInt32(Console.ReadLine());
                        action = userInput == 1
                            ? "add"
                            : userInput == 2
                                ? "subtract"
                                : userInput == 3 ? "multiply" : userInput == 4 ? "divide" : string.Empty;
                        CalculatorAPI(client, action, numberOne, numberTwo).Wait();
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
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            //using entity framework
            var calculatorDiagnostics = kernel.Get<IDiagnostics>("DbEntity");
            var dummyDiagnostics = kernel.Get<IDummyDiagnostics>();

            ISimpleCalculator calculator = new SimpleCalculatorClass(calculatorDiagnostics, dummyDiagnostics);
            calculator.Add(1, 8);
            calculator.Subtract(10, 4);
            calculator.Multiply(5, 2);
            calculator.Divide(20, 10);

            //using stored procedure

         
        }

        static async Task CalculatorAPI(HttpClient client, string action, int numberOne, int numberTwo)
        {
            using (client)
            {
                var calculator = new CalculatorRequest {NumberOne = numberOne, NumberTwo = numberTwo};
                HttpResponseMessage response = await client.PostAsJsonAsync("api/v1/Calculator/" + action, calculator);
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
            Console.WriteLine("This console application provides two operations....");
            Console.WriteLine();
            Console.WriteLine("1. Output the calculation operation and stores it to Database");
            Console.WriteLine("2. Call Calculator API to perform calculation operation");
            Console.WriteLine("3. Exit");
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
