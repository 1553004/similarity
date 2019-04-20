namespace org.omegat.tokenizer
{
	using Token = org.omegat.util.Token;

	/// <summary>
	/// Interface for tokenize string engine.
	/// 
	/// @author Alex Buloichik (alex73mail@gmail.com)
	/// @author Aaron Madlon-Kay
	/// </summary>
	public interface ITokenizer
	{

		/// <summary>
		/// Breaks a string into word-only tokens. Numbers, tags, and other non-word
		/// tokens are NOT included in the result. Stemming can be used depending on
		/// the supplied <seealso cref="StemmingMode"/>.
		/// <para>
		/// This method is used to find fuzzy matches and glossary entries.
		/// </para>
		/// <para>
		/// Results can be cached for better performance.
		/// </para>
		/// </summary>
		Token[] tokenizeWords(string str, ITokenizer_StemmingMode stemmingMode);

		/// <summary>
		/// Breaks a string into word-only strings. Numbers, tags, and other non-word
		/// tokens are NOT included in the result. Stemming can be used depending on
		/// the supplied <seealso cref="StemmingMode"/>.
		/// <para>
		/// When stemming is used, both the original word and its stem may be
		/// included in the results, if they differ. (The stem will come first.)
		/// </para>
		/// <para>
		/// This method used for dictionary lookup.
		/// </para>
		/// <para>
		/// Results are not cached.
		/// </para>
		/// </summary>
		string[] tokenizeWordsToStrings(string str, ITokenizer_StemmingMode stemmingMode);

		/// <summary>
		/// Breaks a string into tokens. Numbers, tags, and other non-word tokens are
		/// included in the result. Stemming is NOT used.
		/// <para>
		/// This method is used to mark string differences in the UI and to tune similarity.
		/// </para>
		/// <para>
		/// Results are not cached.
		/// </para>
		/// </summary>
		Token[] tokenizeVerbatim(string str);

		/// <summary>
		/// Breaks a string into strings. Numbers, tags, and other non-word tokens are
		/// included in the result. Stemming is NOT used.
		/// <para>
		/// This method is used to mark string differences in the UI and for debugging
		/// purposes.
		/// </para>
		/// <para>
		/// Results are not cached.
		/// </para>
		/// </summary>
		string[] tokenizeVerbatimToStrings(string str);

		/// <summary>
		/// Return an array of language strings (<code>xx-yy</code>) indicating the tokenizer's
		/// supported languages. Meant for tokenizers for which the supported languages
		/// can only be determined at runtime, like the <seealso cref="HunspellTokenizer"/>.
		/// <para>
		/// Indicate that this should be used by setting the <seealso cref="Tokenizer"/> annotation
		/// to contain only <seealso cref="Tokenizer#DISCOVER_AT_RUNTIME"/>.
		/// </para>
		/// </summary>
		string[] SupportedLanguages {get;}
	}

	public enum ITokenizer_StemmingMode
	{
		NONE,
		MATCHING,
		GLOSSARY
	}
}
