using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LDA
{
	class Program
	{

		static private CommandLineOption GetDefaultOption()
		{
			CommandLineOption option = new CommandLineOption();
			option.alpha = 0.1;
			option.beta = 0.1;
			option.topics = 10;
			option.savestep = 100;
			option.niters = 1000;
			option.twords = 30;
			option.input = @"C:\Users\yanghuaj\Workspace\LDAGibbs\LDA\bin\Release\testX.txt";
			option.outputfile = @"C:\Users\yanghuaj\Workspace\LDAGibbs\LDA\bin\Release\a";

			return option;
		}

		static void Main(string[] args)
		{

			CommandLineOption opt = GetDefaultOption();
			Parser parser = new Parser();
			var stopwatch = new Stopwatch();
			try
			{
				parser.ParseArguments(args, opt);
				LDAGibbsSampling model = new LDAGibbsSampling();
				Corpora cor = new Corpora();
				cor.LoadDataFile(opt.input);
				model.TrainNewModel(cor, opt);
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.StackTrace);
				Console.WriteLine(ex.Message);

			}

		}
	}
}
