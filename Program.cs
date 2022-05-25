using System;
using System.Linq;
using System.Data;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Evaluator();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error has occurred: \n {0} \n\n Please press Enter and try again.", ex.Message);
                Console.ReadLine();
                Evaluator();
            }
        }

        private static void Evaluator()
        {
            try
            {
                Console.WriteLine("Please input the desired character string: ");
                string sUserInput = Console.ReadLine();

                DetermineInput(sUserInput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("You managed to break text input somehow? \nWell done I guess...\nThe error is: {0}", ex.Message);
                Console.ReadLine();
            }
        }

        private static void DetermineInput(string sUserInput)
        {
            try
            {
                bool var = double.TryParse(sUserInput, out double iUserNumbers);
                //Check if numeric 
                if (double.TryParse(sUserInput, out double iUserNumber)) // Does not handle floats, but used for simplicity's sake
                {
                    Console.WriteLine("The string passed is a numeric value. Which character should be counted? \n");
                    char cUserAnswer = Console.ReadLine().First(); //Realistically I'd allow a string and parse it using regex to find occurences of a substring, 
                                                                   //but I'm going for simplicity here

                    CountGivenCharacter(sUserInput, cUserAnswer); // Original string value is used to avoid an extra unnecessary and costly conversion back to a string
                }
                else if (sUserInput.Contains("+") || sUserInput.Contains("-") || sUserInput.Contains("/") || sUserInput.Contains("*"))
                {
                    Console.WriteLine("The string passed is an equation. \n");
                    SolveUsersEquation(sUserInput);
                } else
                {
                    Console.WriteLine("The string passed is not purely numeric or an equation. \n");
                    CountStringLength(sUserInput);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            
        }

        private static void SolveUsersEquation(string sUserInput)
        {
            try
            {
                /*
                This one I actually looked up, as I was sure there was a simpler way to do this.
                Funny, the kind of functionalities hidden in seemingly random Microsoft libraries.
                If I had to do this manually... I wouldn't. But, if I absolutely had to, 
                I'd String.Split on each different kind of operator and evaluate the expressions in a loop
                Something like this:
                
                 String[] iArrNumericValues = sUserInput.Split('+', '-', '*', '/')

                 foreach (var iNumeric in iNumericValues)
                 etc. etc.

                I'd also iterate through the original string to find which of the separators were present, and throw those in an array to be used 
                in between each numeric values. 

                For anything more complex than this I'd just use something like Matheval.
                 */
                DataTable dt = new DataTable();
                var iDTAnswer = dt.Compute(sUserInput, "");
                Console.WriteLine("The value of your equation is {0} \n" ,iDTAnswer.ToString());
                Console.ReadLine();
                ProgramLooper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static void CountStringLength(string sUserInput)
        {
            try
            {
                //Again, realistically I would probably use a regex whitelist here. 
                //I could potentially also iterate through the string as an array and user Char.IsLetter or similar, 
                //though I'd have to get rid of whitespace anyway. Many ways to skin a cat, but no sense reinventing the wheel.
                Console.WriteLine("Your string is {0} letters long \n", sUserInput.Replace(" ", "").Replace(".", "").Replace(",", "").Replace("'", "").Replace("\"", "").Length.ToString());
                Console.ReadLine();
                ProgramLooper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            
        }

        private static void ProgramLooper()
        {
            Console.WriteLine("Would you like to input a different string? Y/N\n");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                Console.Clear();
                Evaluator();
            }
            else Environment.Exit(1);
        }

        private static void CountGivenCharacter(string sUserInput, char cUserAnswer)
        {
            try
            {
                int count = sUserInput.Count(c => (c == cUserAnswer)); //LINQ may not be too efficient, but it's quick and simple
                Console.WriteLine("There are {0} occurences of {1} in the string {2} \n", count, cUserAnswer, sUserInput);
                Console.ReadLine();
                ProgramLooper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            };
        }
    }
}
