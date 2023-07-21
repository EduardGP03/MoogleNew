namespace MoogleEngine;


public class Moogle
{
    SearchEngine engine;
    public Moogle()
    {
        engine = new SearchEngine("/home/eduard/moogle/Content",0.4f);
    }
    public SearchResult Query(string query) {
        SearchResult result = engine.Search(query);
        return result.Count == 0 ? new SearchResult(result.Items().ToArray(),engine.Suggestion) : result;
    }
}