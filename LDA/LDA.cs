using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDA
{
    public class LDA
    {
        public int M; //#Documents
        public int V; //#Words
        public int K; //#Topics
        public double alpha; // Dirichlet Prior Parameter for Document->Topic
        public double beta; // Dirichlet Prior Parameter for Topic->Word

		public double[][] theta; //Document -> Topic Distributions
        public double[][] phi; // Topic->Word Distributions

        protected int[] words;
        protected int wn;
        protected int[] doc;
        protected int[] z;
    }
}
