using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace todobasic
{
    class Program
    {
        public static List<Task> task = new List<Task>();


        static void Main(string[] args)
        {
            // Creating an array of the txt file
            string txtDest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Todo.txt");
            string[] sortedArray = File.ReadAllLines(txtDest);

            // Printing the array into a list with Todo objects
            for (int i = 0; i < sortedArray.Length; i++)
            {
                if (sortedArray[i] != "")
                {
                    DateTime datel = Convert.ToDateTime(sortedArray[i]);
                    i++;
                    string titlel = sortedArray[i];
                    i++;
                    string descriptl = sortedArray[i];

                    task.Add(new Task(titlel, descriptl, datel));
                }
                // Leaving the loop if the array gives an empty item
                else
                {
                    break;
                }
            }



            bool whileLoop = false;

            //Using while loop
            while (whileLoop == false)
            {
                Console.WriteLine("This is My TODOs List\n\n");
                Console.WriteLine("1) List of all the Tasks");
                Console.WriteLine("2) Add New Task");
                Console.WriteLine("3) Edit A Task");
                Console.WriteLine("4) Save and Quit\n\n");

                string userInput = Console.ReadLine();
                Console.Clear();
                //Menu to Navigate through the options
                switch (userInput)
                {
                    case "1":
                        listFunction(task);
                        break;
                    case "2":
                        addTodo(task);
                        break;
                    case "3":
                        removeTodo();
                        break;
                    case "4":
                        quitTodo();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
        static void quitTodo()
        {            
            Console.WriteLine("Quit and Save\n");
            listFunction(task, true);
        }

        //Function to print the List of all TODOs
        static void listFunction(List<Task> task, bool exit = false)
        {
            string txtDest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Todo.txt");

            if (task.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou Dont have any Tasks");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine($"\nThe Number of Task(s) {task.Count}\n");
                Console.WriteLine("ID " + "TITLE".PadRight(10) + "Description".PadRight(20) + "Date".PadRight(10) + "\n");
                //Sorted by date and then title
                List<Task> sortedByDates = task.OrderBy(task => task.Date).ThenBy(task => task.Title).ToList();
                for (int i = 0; i < task.Count; i++)
                {
                    Console.WriteLine($"{i + 1}  {sortedByDates[i].Title.PadRight(10)}{sortedByDates[i].Description.PadRight(20)}{sortedByDates[i].Date.ToString("yyyy-MM-dd")}");
                }


                // Creating an array to make the list into a txt file
                string[] potato = new string[sortedByDates.Count * 3 + 1];
                int starti = 0;
                foreach (Task taski in sortedByDates)
                {
                    potato[starti] = taski.Date.ToString("yyyy-MM-dd");
                    starti++;
                    potato[starti] = taski.Title;
                    starti++;
                    potato[starti] = taski.Description;
                    starti++;
                }
                // Saving the array to txt file
                File.WriteAllLines(txtDest, potato);
            }

            if (exit == true)
            {
                Environment.Exit(42069);
            }

            Console.WriteLine("\nPress any button to Continue");
            Console.ReadKey();
            Console.Clear();
        }
        //Function to Add Todos 


        static void addTodo(List<Task> task)
        {
            DateTime date = DateTime.Now;
            //Getting input from the user
            Console.Write("Add a todo: ");
            string title = Console.ReadLine();
            Console.Write("Enter Description: ");
            string desc = Console.ReadLine();

        enterDateAgain: try
            {
                Console.Write("Enter the Todo Date - yyyy-MM-dd: ");
                date = Convert.ToDateTime(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is a DateTime Exception: Try using yyyy-MM-dd:");
                Console.ResetColor();
                goto enterDateAgain;
            }



            //creating an object
            Task task1 = new Task(title, desc, date);
            task.Add(task1);
            Console.WriteLine("\nTask Successfully Added\n");
            Console.WriteLine("Press any button to Continue");
            Console.ReadKey();
            Console.Clear();
        }
        //Function to Edit the TOdos using the primary key or index
        static void removeTodo()
        {
            Console.Write("Type the number you want to delete\n ");

            int indexOf;
            try
            {
                Console.WriteLine("ID " + "TITLE".PadRight(10) + "Description".PadRight(20) + "Date".PadRight(10) + "\n");
                List<Task> sortedByDates = task.OrderBy(task => task.Date).ThenBy(task => task.Title).ToList();
                for (int i = 0; i < task.Count; i++)
                {
                    Console.WriteLine($"{i + 1}  {sortedByDates[i].Title.PadRight(10)}{sortedByDates[i].Description.PadRight(20)}{sortedByDates[i].Date.ToString("yyyy-MM-dd")}");
                }
                indexOf = Convert.ToInt32(Console.ReadLine());
                indexOf -= 1;
                task.RemoveAt(indexOf);
                Console.WriteLine("\nTask Sucessfully Deleted");
                Console.WriteLine("\nPress any Button to continue");
                Console.ReadKey();
                Console.Clear();
            }
            //Catching exception if there is any
            catch (Exception)
            {
                Console.WriteLine("\nYou entered the wrong Index\n");
                Console.WriteLine("Press any Button to Continue");
                Console.ReadKey();
                Console.Clear();
            }

        }
    }
    class Task
    {
        public Task(string title, string description, DateTime date)
        {
            Title = title;
            Description = description;
            Date = date;

        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }


    }

}