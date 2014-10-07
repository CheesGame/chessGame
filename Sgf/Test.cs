using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgf
{
    class Test
    {
        static void Main(string[] args)
        {
            Step stepFile = new Step();
            stepFile.fromFile("E://hehe.SGF");
         //   stepFile.toString();
            Console.ReadLine();
            stepFile.saveToFile("E://TESTTT.SGF");
            Console.ReadLine();
            //Console.WriteLine(System.DateTime.Now);
            //Console.ReadLine();
        }
    }
}
