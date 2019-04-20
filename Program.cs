using System;
using System.Collections.Generic;
using System.Linq;

namespace c__similarity
{
    using ISimilarityCalculator = org.omegat.core.matching.ISimilarityCalculator;
	using LevenshteinDistance = org.omegat.core.matching.LevenshteinDistance;
    using Token = org.omegat.util.Token;
    using ITokenizer = org.omegat.tokenizer.ITokenizer;
    class Program
    {
        protected internal const int DEFAULT_TOKENS_COUNT = 64;
//         private readonly ITokenizer tok;
//         public virtual Token[] tokenizeAll(string str)
//  {
// 		// Verbatim token comparisons are intentionally case-insensitive.
// 		// for matching purposes.
// 		str = str.ToLower();
//         Token[] result;
//         tokenizeAllCache.TryGetValue(str, out result);
// 		if (result == null)
// 		{
// 			result = tok.tokenizeVerbatim(str);
// 			tokenizeAllCache.Add(str, result);
// 		}
// 		return result;
//  }
//          IDictionary<string, Token[]> tokenizeAllCache = new Dictionary<string, Token[]>();

         public static Token[] Tokenizer(string sentence)
    {
        IList<Token> result = new List<Token>(DEFAULT_TOKENS_COUNT);
        // Split string on spaces. This will separate all the words in a string
        string[] words = sentence.Split(' ');
        foreach (string word in words)
        {
            Console.WriteLine(word);
            result.Add(new Token (word,1)); // 1 : offset (the starting position of this token in sentence) but dont use so i write constant 1
        }
        Console.WriteLine(" ");
         return  result.ToArray();    
    }

        static void Main(string[] args)
        {
        
            Token[] sentence1 = Tokenizer("this is a cat");
            Token[] sentence2 = Tokenizer("this is a dog");
            LevenshteinDistance distancecaculator = new LevenshteinDistance();
            int ld = distancecaculator.compute(sentence1,sentence2);
            int similarity = (100 * (Math.Max(sentence1.Length, sentence2.Length) - ld)) / Math.Max(sentence1.Length, sentence2.Length);
            Console.WriteLine(similarity);
        }
    }
}   
