#region Header

/*
* BreakIterator
*
* Attempt to mimic Java's BreakIterator class
* to analyze word boundaries
*
* @author: Quan Nguyen
* @version: 1.3, 20 July 2005
*/

#endregion Header

using System.Text.RegularExpressions;

public class BreakIterator
{
    #region Fields

    public static readonly int DONE = -1;

    static readonly Regex regex = new Regex(@"\b.", RegexOptions.Compiled | RegexOptions.Singleline);

    static int index;
    private static BreakIterator instance;
    static MatchCollection mc;

    private string text;

    #endregion Fields

    #region Constructors

    private BreakIterator()
    {
    }

    #endregion Constructors

    #region Properties

    public string Text
    {
        set { text = value; }
    }

    #endregion Properties

    #region Methods

    public static BreakIterator GetWordInstance()
    {
        if (instance == null)
        {
            instance = new BreakIterator();
        }

        return instance;
    }

    public int First()
    {
        mc = regex.Matches(text); // collection of all word beginnings
        //		for (int i = 0; i < mc.Count; i++)
        //		{
        //			System.Console.WriteLine("Found '{0}' at position {1}", mc[i].Value, mc[i].Index);
        //		}
        index = 0;
        if (mc.Count > 0)
        {
            return mc[0].Index;
        }
        else
        {
            return index;
        }
    }

    public int Last()
    {
        index = mc.Count;
        return text.Length;
    }

    public int Next()
    {
        while (index < mc.Count)
        {
            index++;
            if (index >= mc.Count)
            {
                return text.Length;
            }
            else
            {
                return mc[index].Index;
            }
        }

        index = mc.Count;
        return DONE;
    }

    public int Previous()
    {
        while (index > 0)
        {
            index--;
            if (index < 0)
            {
                return 0;
            }
            else
            {
                return mc[index].Index;
            }
        }

        index = 0;
        return DONE;
    }

    #endregion Methods
}