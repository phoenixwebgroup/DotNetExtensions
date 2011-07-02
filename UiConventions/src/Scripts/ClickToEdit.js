function InitializeClickToEdit(selector, updateContext) {

    $(selector).live("change", function (e) {
        var self = $(this);
        var contextLevel = GetClickToEditContextLevel(self);
        var context = contextLevel.attr("ClickToEditContext");
        var actionLevel = GetClickToEditActionLevel(self);
        var action = actionLevel.attr("ClickToEditAction");
        var newJson;
        if (context == null) {
            newJson = {};
        }
        else {
            newJson = $.evalJSON(context);
        }
		var values = GetClickToEditValues(actionLevel);
        $.extend(newJson, values);
        var options = {url: action,
            type: "POST",
            data: $.toJSON(newJson),
            contentType: 'application/json; charset=utf-8',
            };
        if(updateContext == true)
        {
            $.extend(options, 
            {
                dataType: "json",  
                success: function (data) { 
                contextLevel.attr("ClickToEditContext",$.toJSON(data));
                }
            });
        }
        $.ajax(options);
    });
}

function GetClickToEditContextLevel(self) {
    var parent = self;
    var context = parent.attr("ClickToEditContext");
    while (context == null) {
        parent = parent.parent();
        if (parent.is('table'))
            break;
        context = parent.attr("ClickToEditContext")
    }
    return parent;
}

function GetClickToEditActionLevel(self) {
    var parent = self;
    var action = parent.attr("ClickToEditAction");
    while (action == null) {
        parent = parent.parent();
        if (parent.is('table'))
            break;
        action = parent.attr("ClickToEditAction")
    }
    return parent;
}

function GetClickToEditValues(actionLevel) {
    var json = {};
    if (actionLevel.hasClass("clickToEdit")) {
        AddClickToEditValue(actionLevel, json);
		return json;
    }
    var clickToEdits = actionLevel.find(".clickToEdit");
    clickToEdits.each(function (clickToEdit) {
       AddClickToEditValue($(clickToEdit), json);
    });
    return json;
}

function AddClickToEditValue(value, json) {
	var key = value.attr("name");
	json[key] = value.val();
}