namespace org.omegat.core.matching
{
	using Token = org.omegat.util.Token;

	/// <summary>
	/// Interface for similarity calculator. It require to implement more than one
	/// token comparison algorithm.
	/// 
	/// @author Alex Buloichik
	/// </summary>
	public interface ISimilarityCalculator
	{
		/// <summary>
		/// Compute similarity.
		/// </summary>
		/// <param name="source">
		///            source segment </param>
		/// <param name="target">
		///            target segment </param>
		/// <returns> similarity </returns>
		int compute(Token[] source, Token[] target);
	}
}
