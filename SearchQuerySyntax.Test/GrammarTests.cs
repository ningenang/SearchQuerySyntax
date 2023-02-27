using Eto.Parse.Grammars;

namespace SearchQuerySyntax.Test;

public class GrammarTests
{
	[Theory]
	[InlineData("oneterm")]
	[InlineData("4lph4num3ric")]
	[InlineData("two terms")]
	[InlineData("one two many terms")]
	[InlineData("one OR two")]
	[InlineData("(one OR two)")]
	[InlineData("one AND two")]
	[InlineData("(one AND two)")]
	[InlineData("one OR two OR three")]
	[InlineData("(one OR two) OR three")]
	[InlineData("one OR (two OR three)")]
	[InlineData("one AND two AND three")]
	[InlineData("(one AND two) AND three")]
	[InlineData("one AND (two AND three)")]
	[InlineData("one AND two OR three")]
	[InlineData("one AND (two OR three)")]
	[InlineData("(one AND two) OR three")]
	[InlineData("one OR two AND three")]
	[InlineData("one OR (two AND three)")]
	[InlineData("(one OR two) AND three")]
	[InlineData("(one AND two) OR (three AND four)")]
	[InlineData("(one OR two) AND (three OR four)")]
	public void IsoStyleEbnf_ValidQuery_Success(string query)
	{
		var ebnf = """
character := letter | digit;

expr = (term, (' OR ' | ' AND '), expr) | term;
term = ("(", expr, ")") | text;
text = {character};
""";

		var grammar = new EbnfGrammar(EbnfStyle.Iso14977).Build(ebnf, startParserName: "expr");

		var match = grammar.Match(query);

		Assert.True(match.Success, match.GetErrorMessage(detailed: true));
	}
}