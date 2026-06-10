using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_System
{
    internal class Continous_Running_System
    {
        public bool isExit = false;

        public void continous_system()
        {

            do
            {

                // here we will add showmenu function 
                string input = Console.ReadLine();

                if (!(int.TryParse(input, out int choice)))
                {
                    Console.WriteLine("Enter correct choice number ");
                    continue;//if input invalid it skip this iteration and start from beginning of loop that is show menu
                }
              

                switch (choice)
                {

                    case 1:
                        Console.WriteLine("will show products");
                        break;

                        case 2:
                        Console.WriteLine("Will show add to card");
                        break;
                        
                    case 3:
                        Console.WriteLine("Will show view cart ");
                        break;

                    case 4:
                        Console.WriteLine("remove item");
                        break;

                    case 5:
                        Console.WriteLine("checkout");
                        break;

                    case 6:
                        Console.WriteLine("exit");
                        isExit = true;
                        break;

                     default:
                            Console.WriteLine("please enter choice from 1 to 6");
                            break;

                    }

                



            } while (!isExit);



        }
    }
}
