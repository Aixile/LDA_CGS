using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDA
{
    class Program
    {

        private CommandLineOption GetDefaultOption()
        {
            CommandLineOption option = new CommandLineOption();
            option.alpha = 0.1;
            option.beta = 0.1;
            option.topics = 10;
            option.nsave = 100;
			return option;
        }

        static void Main(string[] args)
        {
            
            var parser = new Parser();

        }
    }
}
