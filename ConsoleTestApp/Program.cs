using System;
using System.Linq;

using System.Collections.Generic;
using System.Text;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var list = GetList();

            int page = 0;
            int perPage = 20;
            var sortBy = SortByColumn.IdASC;


    

            IEnumerable<Student> response = new List<Student>();

            switch(sortBy)
            {
                case SortByColumn.NameASC:
                case SortByColumn.NameDESC:

                    response = SearchByRequest(list, page, perPage, sortBy, x => x.Name);
                    break;

                case SortByColumn.IdASC:
                case SortByColumn.IdDESC:

                    response = SearchByRequest(list, page, perPage, sortBy, x => x.Id);
                    break;
            }






            //list.ForEach(x => Console.WriteLine(x.Name + "   " + x.Id));
            //Console.WriteLine("--------------------------------------");
            response.ToList().ForEach(x => Console.WriteLine(x.Name + "      " + x.Id));

        }
        public static IEnumerable<Student> SearchByRequest(IEnumerable<Student> students, int page, int perPage, SortByColumn sortBy, Func<Student, object> sort)
        {
            if ((int)sortBy % 2 == 0)
            {
                return students.OrderBy(sort)
                                .Skip(page * perPage)
                                .Take(perPage).ToList();
            }

            return students.OrderByDescending(sort)
                                .Skip(page * perPage)
                                .Take(perPage).ToList();

        }


        public static Student GetRandomStudent()
        {

            string chars = "abcd";
            StringBuilder stringChars = new StringBuilder();
            
            var random = new Random();

            for (int i = 0; i < 8; i++)
            {
                stringChars.Append(chars.ElementAt(random.Next(chars.Length)));
            }

            return new Student { Name = Convert.ToString(stringChars), Id = random.Next(0, 100) };
        }


        public static List<Student> GetList()
        {
            List<Student> list = new List<Student>();

            for(int i = 0; i < 100; i++)
            {
                list.Add(GetRandomStudent());
            }

            return list;
        }

    }


    public class Student {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public enum SortByColumn
    {
        NameASC = 0,
        IdASC = 2,
        NameDESC = 1,
        IdDESC = 3
    }

}
