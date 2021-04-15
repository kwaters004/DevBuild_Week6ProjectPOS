using System;
using System.Collections.Generic;
using System.Threading;

namespace Week6Project
{
    public class Messages
    {
        static public string sryTryAgain { get; private set; } = "Sorry, that was not a valid option. Please try again";

        static public void printSryTryAgain()
        {
            Console.WriteLine(sryTryAgain);
        }
    }
    public class Validator
    {
        static public bool CheckIntProduct(string item)
        {
            bool isInt = Int32.TryParse(item, out int result);
            if (isInt && result > 0 && result <= ProductList.products.Count + 1)
            {
                return true;
            }
            Messages.printSryTryAgain();
            return false;
        }

        static public bool CheckQuantity(string quant)
        {
            bool isInt = Int32.TryParse(quant, out int result);
            if (isInt && result > 0 && result <= 100) { return true; }
            if (result > 100) { Console.WriteLine("Sorry, you can't order that much. Please try again."); }
            else { Messages.printSryTryAgain(); }
            return false;
        }

        static public bool CheckTenderedCash(string tenderedStr, decimal total)
        {
            bool isDec = Decimal.TryParse(tenderedStr, out decimal result);
            if (isDec && result < total)
            {
                Console.WriteLine("Sorry, that's not enough money");
                return false;
            }
            else if (isDec)
            {
                return true;
            }
            Console.WriteLine("Sorry, that's not a valid amount");
            return false;
        }
    }
    public class ProductList
    {
        static public List<Product> products { get; set; } = new List<Product>
        {
            new Product("Cheeseburger", "Sandwich", "A bun, a beef patty, and a slice of American cheese", 3.99m),
            new Product("Double Cheeseburger", "Sandwich", "A bun, two beef patties, and two sliceds of American cheese", 5.49m),
            new Product("Triple Cheeseburger", "Sandwich", "A bun, three beef patties, and three slices of American cheese", 7.79m),
            new Product("Large Coke", "Beverage", "A large cup of Sprite", 1.99m),
            new Product("Medium Coke", "Beverage", "A medium cup of Sprite", 1.49m),
            new Product("Small Coke", "Beverage", "A small cup of Sprite", .99m),
            new Product("Large Fry", "Side", "A large french fry", 3.49m),
            new Product("Medium Fry", "Side", "A medium french fry", 2.49m),
            new Product("Small Fry", "Side", "A small french fry", 1.49m),
            new Product("Large Sprite", "Beverage", "A large cup of Sprite", 1.99m),
            new Product("Medium Sprite", "Beverage", "A medium cup of Sprite", 1.49m),
            new Product("Small Sprite", "Beverage", "A small cup of Sprite", .99m)

        };

        static public void PrintList()
        {
            string titleFormat = "{0, -4}{1, -20}{2, 10}";
            string format = "{0, 2}. {1, -20}{2, 10:$0.00}";

            Console.WriteLine(String.Format(titleFormat, "No.", "Item", "Price"));
            string line = new string('-', 40);
            Console.WriteLine(line);
            int counter = 1;
            foreach (Product product in products)
            {
                Console.WriteLine(String.Format(format, counter, product.name, product.price));
                counter += 1;
            }
            Console.WriteLine($"{ counter}. Complete Order");
        }

        //      |^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^|
        //      |                                            | 
        //      |                                            | 
        //      | Triple Cheeseburger      x                 | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //      |                                            | 
        //       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^


    }
    public class Product
    {
        public string name { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }

        public Product(string pName, string pCategory, string pDescription, decimal pPrice)
        {
            name = pName;
            category = pCategory;
            description = pDescription;
            price = pPrice;
        }

        static public decimal LineTotal(decimal price, int count)
        {
            return price * count;
        }

        static public decimal TaxTotal(decimal total)
        {
            decimal taxTotal = Math.Round(total * 1.06m, 2, MidpointRounding.AwayFromZero);
            return taxTotal;
        }


        static public decimal GrandTotal(Dictionary<string, int> list)
        {
            decimal total = 0;
            foreach (Product product in ProductList.products)
            {
                foreach (KeyValuePair<string, int> keyValuePair in list)
                {
                    if (keyValuePair.Key == product.name)
                    {
                        total += keyValuePair.Value * product.price;
                    }
                }
            }
            return total;
        }


    }
    public class Program
    {
        static public void PrintLineItem(int choice, Dictionary<string, int> orderList)
        {
            string name = ProductList.products[choice].name;
            int quantity = orderList[ProductList.products[choice].name];
            decimal total = Product.LineTotal(ProductList.products[choice].price, quantity);
            Console.WriteLine($"\nAdded {name} x {quantity} = ${total:0.00}");
        }
        static public decimal DetermineChange(decimal total, decimal tendered)
        {
            return tendered - total;
        }
        static public Dictionary<string, int> AddToList(int choice, Dictionary<string, int> orderList)
        {
            string quantityStr = "";
            do
            {
                Console.Write($"Enter the quantity for '{ProductList.products[choice].name}': ");
                quantityStr = Console.ReadLine();
            } while (!Validator.CheckQuantity(quantityStr));
            int quantity = Int32.Parse(quantityStr);
            if (orderList.ContainsKey(ProductList.products[choice].name))
            {
                orderList[ProductList.products[choice].name] += quantity;
            }
            else
            {
                orderList.Add(ProductList.products[choice].name, quantity);
            }
            return orderList;

        }
        static public string ReceiptFormatting(string receiptLine)
        {
            return "  |  " + receiptLine + "  |  ";
        }
        static public void PrintOrderList(Dictionary<string, int> orderList, decimal total, decimal taxTotal, decimal tax, bool isReceipt, decimal tendered)
        {
            string itemFormat = "{0, -20}x   {1, -3}{2, 10:$0.00}"; // total width is 37 before lines on side, then 47 with lines
            string format = "{0, -20}{1,7}{2, 10:$0.00}";
            string cashFormat = "{0, -27}{1, 10:$0.00}";

            if (!isReceipt)
            {


                Console.WriteLine("Here is your completed order: \n");
                foreach (Product product in ProductList.products)
                {
                    if (orderList.ContainsKey(product.name))
                    {
                        Console.WriteLine(String.Format(itemFormat, product.name, orderList[product.name], Product.LineTotal(product.price, orderList[product.name])));
                    }
                }
                string line = new string('-', 40);
                Console.WriteLine(line);
                Console.WriteLine(String.Format(format, "Total", "", total));
                Console.WriteLine(String.Format(format, "Tax", "", tax));
                line = new string('=', 40);
                Console.WriteLine(line);
                Console.WriteLine(String.Format(format, "Grand Total", "", taxTotal));

            }
            else
            {

                // Need to add in the padding and edges of the receipt

                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine();
                }

                Console.WriteLine("  |" + new string('^', 41) + "|  ");
                for (int i = 0; i < 8; i++)
                {
                    if (i == 2)
                    {
                        string restaurantName = "Burger Cove";
                        int afterNameSpacing = (37 - restaurantName.Length) / 2;
                        int nameSpacing = afterNameSpacing + restaurantName.Length;
                        string restNameSpacing = "{0, " + nameSpacing +"}{1, " + afterNameSpacing + "}";
                        Console.WriteLine(ReceiptFormatting(String.Format(restNameSpacing, restaurantName, "")));
                    }
                    else if (i == 4)
                    {
                        DateTime now = DateTime.Now;
                        string time = now.ToString();
                        
                        int afterTimeSpacing = (37 - time.Length) / 2;
                        int timeSpacing = afterTimeSpacing + time.Length;
                        string restTimeSpacing = "";
                        if (now.Hour < 10)
                        {
                            restTimeSpacing = "{0, " + timeSpacing + "}{1, " + (afterTimeSpacing + 1) + "}";
                        }
                        else
                        {
                            restTimeSpacing = "{0, " + timeSpacing + "}{1, " + afterTimeSpacing + "}";
                        }
                        Console.WriteLine(ReceiptFormatting(String.Format(restTimeSpacing, time, "")));
                    }
                    else
                    {
                        Console.WriteLine("  |" + new string(' ', 41) + "|  ");
                    }
                }


                foreach (Product product in ProductList.products)
                {
                    if (orderList.ContainsKey(product.name))
                    {
                        string addedReceipt = ReceiptFormatting(String.Format(itemFormat, product.name, orderList[product.name], Product.LineTotal(product.price, orderList[product.name])));

                        Console.WriteLine(addedReceipt);
                    }
                }
                string line = new string('-', 37);
                Console.WriteLine(ReceiptFormatting(line));
                Console.WriteLine(ReceiptFormatting(String.Format(format, "Total", "", total)));
                Console.WriteLine(ReceiptFormatting(String.Format(format, "Tax", "", tax)));
                line = new string('=', 37);
                Console.WriteLine(ReceiptFormatting(line));
                Console.WriteLine(ReceiptFormatting(String.Format(format, "Grand Total", "", taxTotal)));
                Console.WriteLine(ReceiptFormatting(new string(' ', 37)));
                Console.WriteLine(ReceiptFormatting(String.Format(cashFormat, "Cash Tendered:", tendered)));
                Console.WriteLine(ReceiptFormatting(String.Format(cashFormat, "Change Returned", tendered - taxTotal)));

                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine("  |" + new string(' ', 41) + "|  ");
                }
                Console.WriteLine("   " + new string('^', 41) + "   ");
                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine();
                }

            }
        }
        static public void Run()
        {
            Dictionary<string, int> orderList = new Dictionary<string, int>();
            bool doneOrdering = false;
            while (!doneOrdering)
            {
                string choiceStr = "";
                Console.WriteLine("Welcome to Burger Cove!\n");
                Console.WriteLine("Burger Cove Menu\n");
                ProductList.PrintList();
                do
                {
                    Console.Write("\nSelect an item by the corresponding number: ");
                    choiceStr = Console.ReadLine();
                } while (!Validator.CheckIntProduct(choiceStr));
                int choice = Int32.Parse(choiceStr) - 1;
                if (choice < ProductList.products.Count)
                {
                    AddToList(choice, orderList);
                    PrintLineItem(choice, orderList);
                    Console.Write("\nPress ENTER to continue...");
                    Console.ReadLine();
                    Console.Clear();

                }
                else
                {
                    string tenderedStr = "";
                    decimal tendered = 0;
                    doneOrdering = true;
                    decimal total = Product.GrandTotal(orderList);
                    decimal taxTotal = Product.TaxTotal(total);
                    decimal tax = taxTotal - total;
                    Console.Clear();
                    PrintOrderList(orderList, total, taxTotal, tax, false, tendered);

                    do
                    {
                        Console.Write("\nHow much cash are you playing with?: $");
                        tenderedStr = Console.ReadLine();
                    } while (!Validator.CheckTenderedCash(tenderedStr, taxTotal));
                    tendered = Decimal.Parse(tenderedStr);
                    Console.WriteLine($"\nHere's your change: ${DetermineChange(taxTotal, tendered)}");
                    Console.Write("\nJust a moment while your receipt prints.");
                    for (int i = 0; i < 4; i++)
                    {
                        Thread.Sleep(500);
                        Console.Write(".");
                    }
                    Console.Clear();
                    PrintOrderList(orderList, total, taxTotal, tax, true, tendered);



                }


            }
        }
        static void Main(string[] args)
        {
            Run();
        }
    }
}
