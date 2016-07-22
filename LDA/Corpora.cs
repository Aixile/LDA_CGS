using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDA
{
    public class Corpora
    {
        public int totalWords;
        public int totalDocuments;
        public Document[] Docs;
        public WordDictionary WD;

        public Corpora(){
            WD = new WordDictionary();
            totalDocuments = 0;
            totalWords = 0;
        }

        public int MaxWordID()
        {
            return WD.Count;
        }

        public bool LoadDataFile(string file)
        {
            try
            {
                string[] f = File.ReadAllLines(file);
                totalDocuments = f.Length;
                Docs = new Document[totalDocuments];
                for(int i=0;i< totalDocuments;i++)
                {
                    Docs[i].Init(f[i],WD);
                    totalWords += Docs[i].Length;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
