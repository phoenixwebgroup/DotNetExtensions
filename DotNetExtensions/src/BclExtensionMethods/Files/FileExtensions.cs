namespace BclExtensionMethods.Files
{
	using System.Collections.Generic;
	using System.Linq;

	public static class FileExtensions
	{
		public static Dictionary<string, string> Dictionary { get; set; }

		public const string PdfMimeType = "application/pdf";

		static FileExtensions()
		{
			Dictionary = new Dictionary<string, string>
			             	{
			             		{"application/andrew-inset", "ez"},
			             		{"application/mac-binhex40", "hqx"},
			             		{"application/mac-compactpro", "cpt"},
			             		{"application/mathml+xml", "mathml"},
			             		{"application/msword", "doc"},
			             		{"application/octet-stream", "bin"},
			             		{"application/oda", "oda"},
			             		{"application/ogg", "ogg"},
			             		{PdfMimeType, "pdf"},
			             		{"application/postscript", "ai"},
			             		{"application/rdf+xml", "rdf"},
			             		{"application/smil", "smi"},
			             		{"application/srgs", "gram"},
			             		{"application/srgs+xml", "grxml"},
			             		{"application/vnd.mif", "mif"},
			             		{"application/vnd.mozilla.xul+xml", "xul"},
			             		{"application/vnd.ms-excel", "xls"},
			             		{"application/vnd.ms-powerpoint", "ppt"},
			             		{"application/vnd.wap.wbxml", "wbxml"},
			             		{"application/vnd.wap.wmlc", "wmlc"},
			             		{"application/vnd.wap.wmlscriptc", "wmlsc"},
			             		{"application/voicexml+xml", "vxml"},
			             		{"application/x-bcpio", "bcpio"},
			             		{"application/x-cdlink", "vcd"},
			             		{"application/x-chess-pgn", "pgn"},
			             		{"application/x-cpio", "cpio"},
			             		{"application/x-csh", "csh"},
			             		{"application/x-director", "dcr"},
			             		{"application/x-dvi", "dvi"},
			             		{"application/x-futuresplash", "spl"},
			             		{"application/x-gtar", "gtar"},
			             		{"application/x-hdf", "hdf"},
			             		{"application/x-httpd-php", ".php"},
			             		{"application/x-httpd-php-source", ".phps"},
			             		{"application/x-javascript", "js"},
			             		{"application/x-koan", "skp"},
			             		{"application/x-latex", "latex"},
			             		{"application/x-netcdf", "nc"},
			             		{"application/x-pkcs7-crl", "crl"},
			             		{"application/x-sh", "sh"},
			             		{"application/x-shar", "shar"},
			             		{"application/x-shockwave-flash", "swf"},
			             		{"application/x-stuffit", "sit"},
			             		{"application/x-sv4cpio", "sv4cpio"},
			             		{"application/x-sv4crc", "sv4crc"},
			             		{"application/x-tar", "tar"},
			             		{"application/x-tcl", "tcl"},
			             		{"application/x-tex", "tex"},
			             		{"application/x-texinfo", "texinfo"},
			             		{"application/x-troff", "roff"},
			             		{"application/x-troff-man", "man"},
			             		{"application/x-troff-me", "me"},
			             		{"application/x-troff-ms", "ms"},
			             		{"application/x-ustar", "ustar"},
			             		{"application/x-wais-source", "src"},
			             		{"application/x-x509-ca-cert", "crt"},
			             		{"application/xhtml+xml", "xhtml"},
			             		{"application/xml", "xml"},
			             		{"application/xml-dtd", "dtd"},
			             		{"application/xslt+xml", "xslt"},
			             		{"application/zip", "zip"},
			             		{"audio/basic", "au"},
			             		{"audio/midi", "mid"},
			             		{"audio/mpeg", "mp3"},
			             		{"audio/x-aiff", "aif"},
			             		{"audio/x-mpegurl", "m3u"},
			             		{"audio/x-pn-realaudio", "rm"},
			             		{"audio/x-pn-realaudio-plugin", "rpm"},
			             		{"audio/x-realaudio", "ra"},
			             		{"audio/x-wav", "wav"},
			             		{"chemical/x-pdb", "pdb"},
			             		{"chemical/x-xyz", "xyz"},
			             		{"image/bmp", "bmp"},
			             		{"image/cgm", "cgm"},
			             		{"image/gif", "gif"},
			             		{"image/ief", "ief"},
			             		{"image/jpeg", "jpg"},
			             		{"image/png", "png"},
			             		{"image/svg+xml", "svg"},
			             		{"image/tiff", "tiff"},
			             		{"image/vnd.djvu", "djv"},
			             		{"image/vnd.wap.wbmp", "wbmp"},
			             		{"image/x-cmu-raster", "ras"},
			             		{"image/x-icon", "ico"},
			             		{"image/x-portable-anymap", "pnm"},
			             		{"image/x-portable-bitmap", "pbm"},
			             		{"image/x-portable-graymap", "pgm"},
			             		{"image/x-portable-pixmap", "ppm"},
			             		{"image/x-rgb", "rgb"},
			             		{"image/x-xbitmap", "xbm"},
			             		{"image/x-xpixmap", "xpm"},
			             		{"image/x-xwindowdump", "xwd"},
			             		{"model/iges", "iges"},
			             		{"model/mesh", "mesh"},
			             		{"model/vrml", "vrml"},
			             		{"text/calendar", "ics"},
			             		{"text/css", "css"},
			             		{"text/html", "htm"},
			             		{"text/plain", "txt"},
			             		{"text/richtext", "rtx"},
			             		{"text/rtf", "rtf"},
			             		{"text/sgml", "sgml"},
			             		{"text/tab-separated-values", "tsv"},
			             		{"text/vnd.wap.wml", "wml"},
			             		{"text/vnd.wap.wmlscript", "wmls"},
			             		{"text/x-setext", "etx"},
			             		{"video/mpeg", "mpg"},
			             		{"video/quicktime", "mov"},
			             		{"video/vnd.mpegurl", "mxu"},
			             		{"video/x-msvideo", "avi"},
			             		{"video/x-sgi-movie", "movie"},
			             		{"x-conference/x-cooltalk", "ice"},
			             		{"application/vnd.ms-word.document.macroEnabled.12", "docm"},
			             		{"application/vnd.openxmlformats-officedocument.wordprocessingml.document", "docx"},
			             		{"application/vnd.ms-word.template.macroEnabled.12", "dotm"},
			             		{"application/vnd.openxmlformats-officedocument.wordprocessingml.template", "dotx"},
			             		{"application/vnd.ms-powerpoint.template.macroEnabled.12", "potm"},
			             		{"application/vnd.openxmlformats-officedocument.presentationml.template", "potx"},
			             		{"application/vnd.ms-powerpoint.addin.macroEnabled.12", "ppam"},
			             		{"application/vnd.ms-powerpoint.slideshow.macroEnabled.12", "ppsm"},
			             		{"application/vnd.openxmlformats-officedocument.presentationml.slideshow", "ppsx"},
			             		{"application/vnd.ms-powerpoint.presentation.macroEnabled.12", "pptm"},
			             		{"application/vnd.openxmlformats-officedocument.presentationml.presentation", "pptx"},
			             		{"application/vnd.ms-excel.addin.macroEnabled.12", "xlam"},
			             		{"application/vnd.ms-excel.sheet.binary.macroEnabled.12", "xlsb"},
			             		{"application/vnd.ms-excel.sheet.macroEnabled.12", "xlsm"},
			             		{"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "xlsx"},
			             		{"application/vnd.ms-excel.template.macroEnabled.12", "xltm"},
			             		{"application/vnd.openxmlformats-officedocument.spreadsheetml.template", "xltx"}
			             	};
		}

		public static string ExtensionFor(this string mimeType)
		{
			var key = mimeType.ToLower();

			if (Dictionary.DoesNotContainKey(key))
			{
				return null;
			}
			return Dictionary[key];
		}

		public static string MimeTypeFor(this string extension)
		{
			extension = extension.ToLower();

			if (Dictionary.DoesNotContainValue(extension))
			{
				return null;
			}
			return Dictionary.FirstOrDefault(d => d.Value == extension).Key;
		}
	}
}