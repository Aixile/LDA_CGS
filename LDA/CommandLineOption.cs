using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace LDA
{
    public class CommandLineOption
    {
        [Option("topic")]
        public int topics { get; set; }

        [Option("savestep")]
		public int savestep { get; set; }

        [Option("alpha")]
        public double alpha { get; set; }

        [Option("beta")]
        public double beta { get; set; }

        [Option("niters")]
		public int niters { get; set; }

		[Option("input")]
		public string input { get; set; }

		[Option("outputdir")]
		public string outputdir { get; set; }
    }
}
