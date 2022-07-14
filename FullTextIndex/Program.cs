// See https://aka.ms/new-console-template for more information

using System.Globalization;


var list = TopicsExtractor.ArticleSet().Take(10000).ToArray();

class TopicsExtractor
{
    public static IEnumerable<string> ArticleSet()
    {
        return ReadArticleSet("records.csv")
            .Concat(ReadArticleSet("titles.csv"))
            .Concat(ReadArticleSet("topics.csv"));
    }

    private static IEnumerable<string> ReadArticleSet(string fileName)
    {
        using (var reader = new CsvHelper.CsvReader(
                   File.OpenText(Path.Combine(@"C:\Projects\OpenSources\FullTextIndex", fileName)),
                   CultureInfo.InvariantCulture))
        {
            reader.Read();
            reader.ReadHeader();
            while (reader.Read())
            {
                var content = reader["content"];
                yield return content;
            }
        }
    }
}