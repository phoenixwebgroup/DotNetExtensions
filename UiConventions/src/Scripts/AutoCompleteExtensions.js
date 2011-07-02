$.autoCompleteExtensions = {
	createAutoComplete: function (textBoxId, url) {
		$('#' + textBoxId).autocomplete(url);
	},

	setupGetResult : function(textBoxId, valueBoxId) {
		$('#' + textBoxId).result(function(e, d, f) {{ $('#' + valueBoxId).val(d[1]); }});
	}
}