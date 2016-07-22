using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDA
{
    public class LDAGibbsSampling:LDA
    {

        protected int[][] nw;
        protected int[][] nd;
        protected int[] nwsum;
        protected int[] ndsum;
        protected double[] p;

        protected int saveAt;

        public LDAGibbsSampling()
        {
            M = 0;
            V = 0;
            K = 10;
            alpha = 0.1;
            beta = 0.1;
        }
        
        public void InitOption(CommandLineOption opt)
        {
            try
            {
                K = opt.topics;
                alpha = opt.alpha;
                beta = opt.beta;
                saveAt = opt.savestep;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void InitModel(Corpora cor)
        {
            M = cor.totalDocuments;
            V = cor.MaxWordID();

            p = new double[K];
            Random rnd = new Random();

            nw = new int[V][];
            for (int w = 0; w < V; w++)
            {
                nw[w] = new int[K];
            }
            for (int m = 0; m < M; m++)
            {
                nd[m] = new int[K];
            }

            nwsum = new int[K];
            ndsum = new int[M];

            words = new int[cor.totalWords];
            doc = new int[cor.totalWords];
            z = new int[cor.totalWords];
            wn = 0;
            for (int i = 0; i < M; i++)
            {
                int l = cor.Docs[i].Length;
                for (int j = 0; j < l; j++)
                {
                    words[wn] = cor.Docs[i].Words[j];
                    doc[wn] = i;
                    wn++;
                }
                ndsum[i] = l;
            }
            for(int i=0;i< wn; i++)
            {

                int topic = rnd.Next(K);
                nw[words[i]][topic] += 1;
                nd[doc[i]][topic] += 1;
                nwsum[topic] += 1;
                z[i] = topic;
            }

            theta = new double[M][];
            for (int m = 0; m < M; m++)
            {
                theta[m] = new double[K];
            }
            phi = new double[K][];
            for (int k = 0; k < K; k++)
            {
                phi[k] = new double[V];
            }

        }

        public void GibbsSampling(int totalIter)
        {
            for (int iter = 1; iter <= totalIter; iter++)
            {
                Console.Write("Iteration " + iter + ":");
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                for (int i = 0; i < wn; i++)
                {
                    int topic = doSampling(i);
                    z[i] = topic;
                }

                stopWatch.Stop();
                Console.WriteLine(stopWatch.ElapsedMilliseconds / 1000.0 + " seconds");
                if (iter % saveAt == 0)
                {
                    calcParameter();
                }
            }
        }

        private int doSampling(int i)
        {
            int oldZ = z[i];
            int w = words[i];
            int m = doc[i];

            nw[w][oldZ] -= 1;
            nd[m][oldZ] -= 1;
            nwsum[oldZ] -= 1;

            double Vbeta = V * beta;
            double Kalpha = K * alpha;
            for (int k = 0; k < K; k++)
            {
                p[k] = (nw[w][k] + beta) / (nwsum[k] + Vbeta) * (nd[m][k] + alpha) / (ndsum[m] + Kalpha);
            }
            for (int k = 1; k < K; k++)
            {
                p[k] += p[k - 1];
            }
            Random rnd = new Random();
            double cp = rnd.NextDouble() * p[K - 1];

            int newZ;
            for (newZ = 0; newZ < K; newZ++)
            {
                if (cp < newZ)
                {
                    break;
                }
            }
            if (newZ == K) newZ--;
            nw[w][newZ] += 1;
            nd[m][newZ] += 1;
            nwsum[newZ] += 1;
            return newZ;
        }

        void calcParameter()
        {
            for (int m = 0; m < M; m++)
            {
                for (int k = 0; k < K; k++)
                {
                    theta[m][k] = (nd[m][k] + alpha) / (ndsum[m] + K * alpha);
                }
            }

            for (int k = 0; k < K; k++)
            {
                for (int w = 0; w < V; w++)
                {
                    phi[k][w] = (nw[w][k] + beta) / (nwsum[k] + V * beta);
                }
            }
        }
    }
}
