using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webcam
{
    class Detect
    {
        static void detect(string[] args)
        {

            //void test_func()
            //{
            //    //Console.WriteLine("START");
            //    //Console.WriteLine("");
            //}

            //test_func();


            List<string> lst = new List<string> { "Rezvan", "Rezvan", "Iman", "Iman", "Rezvan", "Reza", "Shahab" };


            #region raw list

            //Console.WriteLine("Unique in list");
            //foreach (string s in lst)
            //Console.WriteLine(s);
            //Console.WriteLine("");
            //Console.WriteLine("--------------------------------------------");

            #endregion

            #region n last item
            List<string> last_item(List<string> l1, int c)
            {
                List<string> reverse_l1 = l1;
                reverse_l1.Reverse();

                var p_list = reverse_l1.Take(c);
                List<string> f_List = p_list.ToList();

                return f_List;
                //return asList;
            }


            //List<string> reverse_list = lst;
            //reverse_list.Reverse();

            //var process_list = reverse_list.Take(2);
            //List<string> asList = process_list.ToList();

            List<string> asList = last_item(lst, 2);


            /*
            Console.WriteLine(asList.Count() + "  last Item");

            foreach (string s in asList)
                Console.WriteLine(s);

            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            */
            #endregion



            #region Unique

            List<string> unique(List<string> l1)
            {
                //Console.WriteLine("Unique in list");
                var unique_list_f = l1.Distinct().ToList();

                return unique_list_f;
            }

            List<string> unique_list = unique(lst);
            /*
            foreach (string s in unique_list)
                Console.WriteLine(s);
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            */
            #endregion

            #region count unique 
            //Console.WriteLine("Count Unique");

            List<string> count_unique(List<string> l1)
            {
                List<string> count_u = new List<string> { };

                var query = lst.SelectMany(x => l1).GroupBy(x => x, (y, z) => new { Name = y, Count = z.Count() });

                foreach (var result in query)
                {
                    if (result.Count / ((lst.Count)) >= 5)
                    {
                        count_u.Add(result.Name);
                        Console.WriteLine(result.Name + "   " + (result.Count / ((lst.Count))));
                    }
                    //Console.WriteLine("Name: {0}, Count: {1}", result.Name, result.Count);
                }

                return count_u;

            }

            //var query = lst.SelectMany(x => lst).GroupBy(x => x, (y, z) => new { Name = y, Count = z.Count() });




            List<string> count_unique_f = count_unique(lst);


            /*
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            foreach (string s in count_unique_f)
                Console.WriteLine(s);

            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------");
            */
            #endregion






            #region RealTime
            //Console.WriteLine("Real Time");
            for (int i = 0; i < 10; i++)
            {
                var random = new Random();
                int index = random.Next(lst.Count);

                Console.WriteLine("Frame    " + lst[index]);

                lst.Add(lst[index]);


                /////////////////////////////////////////////////////////
                // Look Back
                int cc = 10;

                if (cc > lst.Count())
                {
                    cc = lst.Count();
                }
                /////////////////////////////////////////////////////////
                ///

                List<string> n_last_frame = last_item(lst, cc);
                //Console.WriteLine("Len list Master " + lst.Count()+ "    "  + cc);

                List<string> stable_list = count_unique(n_last_frame);



                Console.WriteLine("");
                Console.WriteLine("Stable List");
                foreach (string s in stable_list)
                {
                    DateTime now = DateTime.Now;
                    Console.WriteLine(s + "     " + now.ToString("HH:mm:ss tt") + "    " + now.ToString("dd:M"));
                }





                System.Threading.Thread.Sleep(500);

                //Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine("------------------------------------------------------------------------------");
            }
            #endregion





            Console.ReadKey();


        }
    }
}
