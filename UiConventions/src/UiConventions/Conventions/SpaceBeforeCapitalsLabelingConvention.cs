namespace HtmlTags.UI.Conventions
{
	using System.Text;
	using FubuCore.Reflection;

	public class SpaceBeforeCapitalsLabelingConvention : ILabelingConvention
	{
		public string GetLabelText(Accessor accessor)
		{
			var text = accessor.Name;
			return GetLabelText(text);
		}

		public string GetLabelText(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return string.Empty;
			}

			var newText = new StringBuilder(text.Length*2);
			newText.Append(text[0]);
			var lastWasLower = false; // this way we don't have spaces after the first character (was causing spaces after two upper case letters started out a word.
			for (var i = 1; i < text.Length; i++)
			{
				var nextIsLower = (i < text.Length - 1 && char.IsLower(text[i + 1]));
				if (char.IsUpper(text[i]) && (lastWasLower || nextIsLower))
				{
					newText.Append(' ');
				}
				newText.Append(text[i]);
				lastWasLower = char.IsLower(text[i]);
			}
			return newText.ToString();
		}
	}
}