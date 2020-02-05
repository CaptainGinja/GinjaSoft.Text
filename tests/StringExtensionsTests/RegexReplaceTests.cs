namespace GinjaSoft.Text.Tests.StringExtensionsTests
{
  using System;
  using System.Collections.Generic;
  using Xunit;


  public class RegexReplaceTests
  {
    [Fact]
    public void Basic()
    {
      const string text = "foobarfoobaz";

      const string regexPattern = @"(foo)";
      var tokenReplacements = new Dictionary<string, string> { { "foo", "XXX" } };

      const string expectedResult = "XXXbarXXXbaz";

      var result = Replace(text, regexPattern, tokenReplacements);
      Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void MatchSqlDatabaseName()
    {
      const string text = @"
SELECT
  *
FROM
  [foo].[WimbledonCommon].[Wombles] AS w
    INNER JOIN [bar].[Trumpton].[Firemen] AS f
    ON w.id = f.id
      INNER JOIN [baz].[MagicRoundabout].[Characters] AS mr
      ON mr.id = w.id
WHERE
  w.name = 'Tomsk';";

      const string regexPattern = @"\[([a-zA-Z0-9]+)\]\.\[[a-zA-Z0-9]+\]\.\[[a-zA-Z0-9]+\]";
      var tokenReplacements = new Dictionary<string, string> { { "foo", "foo_ss_123" }, { "bar", "bar_ss_234" } };

      const string expectedResult = @"
SELECT
  *
FROM
  [foo_ss_123].[WimbledonCommon].[Wombles] AS w
    INNER JOIN [bar_ss_234].[Trumpton].[Firemen] AS f
    ON w.id = f.id
      INNER JOIN [baz].[MagicRoundabout].[Characters] AS mr
      ON mr.id = w.id
WHERE
  w.name = 'Tomsk';";

      var result = Replace(text, regexPattern, tokenReplacements);
      Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void PatternMatchesNothing()
    {
      const string text = "foobarfoobaz";

      const string regexPattern = @"(sdfsdf)";
      var tokenReplacements = new Dictionary<string, string> { { "foo", "XXX" } };

      const string expectedResult = "foobarfoobaz";

      var result = Replace(text, regexPattern, tokenReplacements);
      Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void PatternMatchesButEmptyTokenReplacements()
    {
      const string text = "foobarfoobaz";

      const string regexPattern = @"(foo)";
      var tokenReplacements = new Dictionary<string, string>();

      const string expectedResult = "foobarfoobaz";

      var result = Replace(text, regexPattern, tokenReplacements);
      Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void PatternMatchesButNoMatchingTokenReplacements()
    {
      const string text = "foobarfoobaz";

      const string regexPattern = @"(foo)";
      var tokenReplacements = new Dictionary<string, string> { { "sdfsd", "dfsdfsd" } };

      const string expectedResult = "foobarfoobaz";

      var result = Replace(text, regexPattern, tokenReplacements);
      Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void MultipleGroupsInPattern_ThrowsException()
    {
      const string text = "foobarfoobaz";

      const string regexPattern = @"(foo)(bar)";
      var tokenReplacements = new Dictionary<string, string> { { "sdfsd", "dfsdfsd" } };

      Assert.Throws<ArgumentException>(() => Replace(text, regexPattern, tokenReplacements));
    }

    [Fact]
    public void PatternWithQuantifierGeneratesMultipleCaptures_ThrowsException()
    {
      const string text = "foofoobaz";

      const string regexPattern = @"(foo)+";
      var tokenReplacements = new Dictionary<string, string> { { "sdfsd", "dfsdfsd" } };

      Assert.Throws<ArgumentException>(() => Replace(text, regexPattern, tokenReplacements));
    }

    [Fact]
    public void MatchSqlDatabaseNameWithTag()
    {
      //
      // This is the use case that originally promted the creation of this string extension function.
      //
      // We have a collection of SQL query templates where all tables are explicitly written using three part names
      // (database.schema.table) and we want to pre-process these templates in order to create legitimate SQL queries
      // that will then be executed on a connection to a SQL Server instance.  In the templates the database part of
      // the three part table names is the part that we want to match and replace with an actual database name that
      // will be determined based on the database name string from the template and other run-time context.  The
      // convention is that all parts of the three part name will be wrapped in square braces (the SQL Server name
      // quoting convention).  The database part will consist of a logical database name and optionally a # character
      // and a tag (which will indicate a version of the actual database, e.g. 'dev', 'qa', 'test', etc.).
      //
      // The run-time context will consist of a collection of database snapshot names each of which is created from an
      // underlying database (the logical database name from the template).  Different snapshots are associated with
      // different tags.
      //


      const string text = @"
SELECT
  *
FROM
  [foo#qa].[WimbledonCommon].[Wombles] AS w
    INNER JOIN [foo#live].[Trumpton].[Firemen] AS f
    ON w.id = f.id
      INNER JOIN [baz].[MagicRoundabout].[Characters] AS mr
      ON mr.id = w.id
WHERE
  w.name = 'Tomsk';";

      const string regexPattern = @"\[([a-zA-Z0-9#]+)\]\.\[[a-zA-Z0-9]+\]\.\[[a-zA-Z0-9]+\]";
      var tokenReplacements = new Dictionary<string, Dictionary<string, string>> {
        {
          "foo", new Dictionary<string, string> {
            { "qa", "foo_ss_123" },
            { "live", "foo_ss_124" }
          }
        }
      };

      const string expectedResult = @"
SELECT
  *
FROM
  [foo_ss_123].[WimbledonCommon].[Wombles] AS w
    INNER JOIN [foo_ss_124].[Trumpton].[Firemen] AS f
    ON w.id = f.id
      INNER JOIN [baz].[MagicRoundabout].[Characters] AS mr
      ON mr.id = w.id
WHERE
  w.name = 'Tomsk';";

      var result = text.RegexReplaceTokens(
        regexPattern,
        t => {
          var parts = t.Split('#');
          if(parts.Length != 2) return t;
          var db = parts[0];
          var tag = parts[1];
          if(!tokenReplacements.TryGetValue(db, out var innerDictionary)) return t;
          return innerDictionary.TryGetValue(tag, out var ret) ? ret : t;
        });

      Assert.Equal(expectedResult, result);
    }


    //
    // Private methods
    //

    private static string Replace(string text, string pattern, IReadOnlyDictionary<string, string> replacements)
    {
      return text.RegexReplaceTokens(pattern, t => replacements.TryGetValue(t, out var ret) ? ret : t);
    }
  }
}
