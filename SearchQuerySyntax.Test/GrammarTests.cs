using Eto.Parse.Grammars;

namespace SearchQuerySyntax.Test;

public class GrammarTests
{
	[Theory]
	[InlineData("oneterm")]
	[InlineData("4lph4num3ric")]
	[InlineData("one-dash")]
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
	[InlineData("(one OR (two AND nested))")]
	[InlineData("((one AND nested) OR two)")]
	public void IsoStyleEbnf_ValidQuery_Success(string query)
	{
		var ebnf = """
query := (expr, {(whitespace, expr)});
expr := op_expr | paren_expr | term;
paren_expr := ("(", expr, ")");
op_expr := (expr, operator, expr);
term := text;
text := { character };
character := letter | digit | '-';
operator := (' AND ' | ' OR ');
whitespace := " ";
""";

		var grammar = new EbnfGrammar(EbnfStyle.Iso14977).Build(ebnf, startParserName: "query");

		var match = grammar.Match(query);

		Assert.True(match.Success, match.GetErrorMessage(detailed: true));
	}

}