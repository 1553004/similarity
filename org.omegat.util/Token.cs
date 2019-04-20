
using System.Text.RegularExpressions;
namespace org.omegat.util
{

	/// <summary>
	/// Offset marks the display offset of character - this might be different than
	/// the characters position in the char array due existence of multi-char
	/// characters.
	/// <para>
	/// Since 1.6 strips '&' in given token text.
	/// 
	/// @author Keith Godfrey
	/// @author Maxym Mykhalchuk
	/// @author Henry Pijffers (henry.pijffers@saxnot.com)
	/// @author Zoltan Bartko
	/// @author Aaron Madlon-Kay
	/// </para>
	/// </summary>
	public class Token
	{
		/// <summary>
		/// Two tokens are thought equal if their hash code is equal.
		/// </summary>
		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}
			if (other is Token)
			{
				return hash == ((Token) other).hash;
			}
			return false;
		}

		/// <summary>
		/// Check that all fields are equal (unlike <seealso cref="#equals(Object)"/>).
		/// </summary>
		public virtual bool deepEquals(Token other)
		{
			return Equals(other) && this.offset == other.offset && this.length == other.length;
		}

		/// <summary>
		/// -1 if text is null, text's hashcode otherwise.
		/// </summary>
		private int hash;

		public override int GetHashCode()
		{
			return hash;
		}

		private static readonly Regex AMP = new Regex(@"\\d+");

		private string stripAmpersand(string s)
		{
			return AMP.Replace(s," ");
		}

		/// <summary>
		/// Creates a new token.
		/// </summary>
		/// <param name="text">
		///            the text of the token </param>
		/// <param name="offset">
		///            the starting position of this token in parent string </param>
		public Token(string text, int offset) : this(text, offset, text.Length)
		{
		}

		/// <summary>
		/// Creates a new token.
		/// </summary>
		/// <param name="text">
		///            the text of the token </param>
		/// <param name="offset">
		///            the starting position of this token in parent string </param>
		/// <param name="length">
		///            length of token </param>
        public Token(string text, int offset, int length)
		{
			this.length = length;
			this.hash = (string.ReferenceEquals(text, null)) ? -1 : stripAmpersand(text).GetHashCode();
			this.offset = offset;
		}
		private readonly int length;
		private readonly int offset;

		/// <summary>
		/// Returns the length of a token. </summary>
		public int Length
		{
			get
			{
				return length;
			}
		}

		/// <summary>
		/// Returns token's offset in a source string. </summary>
		public int Offset
		{
			get
			{
				return offset;
			}
		}

		public sealed override string ToString()
		{
			return hash + "@" + offset;
		}

		/// <summary>
		/// Return the section of the string denoted by the token
		/// </summary>
		public virtual string getTextFromString(string input)
		{
			return input.Substring(offset, length);
		}

		/// <summary>
		/// Get the strings represented by the provided tokens, from the original string
		/// they were produced from. For debugging purposes.
		/// </summary>
		public static string[] getTextsFromString(Token[] tokens, string @string)
		{
			string[] result = new string[tokens.Length];
			for (int i = 0; i < tokens.Length; i++)
			{
				result[i] = tokens[i].getTextFromString(@string);
			}
			return result;
		}
	}
}
