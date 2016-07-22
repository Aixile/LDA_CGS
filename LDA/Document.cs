using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDA
{
    public class Document
    {
        public int[] Words;
        public int Length;

        public bool Init(string str, WordDictionary WD)
        {
            try
            {
                string[] doc= str.Split(' ');
                Words = new int[doc.Length];
                Length = doc.Length;
                for(int i = 0; i < Length; i++)
                {
                    Words[i]= WD.GetWords(doc[i]);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
