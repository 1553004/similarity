using System;

namespace org.omegat.core.matching
{
	using Token = org.omegat.util.Token;

	/// <summary>
	/// Class to compute Levenshtein Distance.
	/// 
	/// <para>
	/// Levenshtein distance (LD) is a measure of the similarity between two strings,
	/// which we will refer to as the source string (s) and the target string (t).
	/// The distance is the number of deletions, insertions, or substitutions
	/// required to transform s into t.
	/// 
	/// </para>
	/// <para>
	/// For example,
	/// <ul>
	/// <li>If s is "test" and t is "test", then LD(s,t) = 0, because no
	/// transformations are needed. The strings are already identical.
	/// <li>If s is "test" and t is "tent", then LD(s,t) = 1, because one
	/// substitution (change "s" to "n") is sufficient to transform s into t.
	/// </ul>
	/// 
	/// </para>
	/// <para>
	/// The greater the Levenshtein distance, the more different the strings are.
	/// </para>
	/// <para>
	/// Levenshtein distance is named after the Russian scientist Vladimir
	/// Levenshtein, who devised the algorithm in 1965. If you can't spell or
	/// pronounce Levenshtein, the metric is also sometimes called edit distance.
	/// 
	/// alex73's comment: We can't make 'compute' mathod static, because in this case
	/// LevenshteinDistance will not be thread-safe(see 'd' and 'p' arrays). We can't
	/// create these arrays inside 'compute' method, because it's enough slow
	/// operation. We have to create LevenshteinDistance instance one for each thread
	/// where we will call it. It's best way for best performance.
	/// 
	/// </para>
	/// </summary>
	/// <seealso cref= <a href="http://people.cs.pitt.edu/~kirk/cs1501/Pruhs/Fall2006/Assignments/editdistance/Levenshtein%20Distance.htm">Levenshtein Distance, in Three Flavors</a>
	/// 
	/// @author Vladimir Levenshtein
	/// @author Michael Gilleland, Merriam Park Software
	/// @author Chas Emerick, Apache Software Foundation
	/// @author Maxym Mykhalchuk
	/// @author Alex Buloichik (alex73mail@gmail.com) </seealso>
	public class LevenshteinDistance : ISimilarityCalculator
	{

		/// <summary>
		/// Get minimum of three values
		/// </summary>
		private static short minimum(int a, int b, int c)
		{
			return (short) Math.Min(a, Math.Min(b, c));
		}

		/// <summary>
		/// Maximal number of items compared. </summary>
		private const int MAX_N = 1000;

		/// <summary>
		/// Cost array, horizontally. Here to avoid excessive allocation and garbage
		/// collection.
		/// </summary>
		private short[] d = new short[MAX_N + 1];
		/// <summary>
		/// "Previous" cost array, horizontally. Here to avoid excessive allocation
		/// and garbage collection.
		/// </summary>
		private short[] p = new short[MAX_N + 1];

		/// <summary>
		/// Compute Levenshtein distance between two lists.
		/// 
		/// <para> The difference between this impl. and the canonical one is that,
		/// rather than creating and retaining a matrix of size s.length()+1 by
		/// t.length()+1, we maintain two single-dimensional arrays of length
		/// s.length()+1.
		/// 
		/// </para>
		/// <para> The first, d, is the 'current working' distance array that maintains
		/// the newest distance cost counts as we iterate through the characters of
		/// String s. Each time we increment the index of String t we are comparing,
		/// d is copied to p, the second int[]. Doing so allows us to retain the
		/// previous cost counts as required by the algorithm (taking the minimum of
		/// the cost count to the left, up one, and diagonally up and to the left of
		/// </para>
		/// the current cost count being calculated). <para> (Note that the arrays
		/// aren't really copied anymore, just switched... this is clearly much
		/// better than cloning an array or doing a System.arraycopy() each time
		/// through the outer loop.)
		/// 
		/// </para>
		/// <para> Effectively, the difference between the two implementations is this
		/// one does not cause an out of memory condition when calculating the LD
		/// over two very large strings.
		/// 
		/// </para>
		/// <para> For perfomance reasons the maximal number of compared items is {@link
		/// #MAX_N}.
		/// </para>
		/// </summary>
		public virtual int compute(Token[] s, Token[] t)
		{
			if (s == null || t == null)
			{
				throw new System.ArgumentException("Dont have token");
			}
			int n = s.Length; // length of s
			int m = t.Length; // length of t

			if (n == 0)
			{
				return m;
			}
			else if (m == 0)
			{
				return n;
			}

			if (n > MAX_N)
			{
				n = MAX_N;
			}
			if (m > MAX_N)
			{
				m = MAX_N;
			}

			short[] swap; // placeholder to assist in swapping p and d

			// indexes into strings s and t
			short i; // iterates through s
			short j; // iterates through t

			Token t_j = null; // jth object of t

			short cost; // cost

			for (i = 0; i <= n; i++)
			{
				p[i] = i;
			}

			for (j = 1; j <= m; j++)
			{
				t_j = t[j - 1];
				d[0] = j;

				Token s_i = null; // ith object of s
				for (i = 1; i <= n; i++)
				{
					s_i = s[i - 1];
					cost = s_i.Equals(t_j) ? (short) 0 : (short) 1;
					// minimum of cell to the left+1, to the top+1, diagonally left
					// and up +cost
					d[i] = minimum(d[i - 1] + 1, p[i] + 1, p[i - 1] + cost);
				}

				// copy current distance counts to 'previous row' distance counts
				swap = p;
				p = d;
				d = swap;
			}

			// our last action in the above loop was to switch d and p, so p now
			// actually has the most recent cost counts
			return p[n];
		}
	}
}
