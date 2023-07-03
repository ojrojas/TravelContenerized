namespace Identity.Core.Helpers;

public static class TransformString
{
	public static Dictionary<string, string> TransformStringtoDictionary(string dictionaryString)
	{
		Dictionary<string, string> dictionary = new();
		var urls = dictionaryString.Split(",");

		foreach(var dic in urls)
		{
			var value = dic.Split("@");
			dictionary.Add(value[0], value[1]);
		}

		return dictionary;
	}
}