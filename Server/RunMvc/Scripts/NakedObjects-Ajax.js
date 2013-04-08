var nakedObjects = (function () {

    var api = {};
    api.ajaxCount = 0;
    var disabledSubmits;
    var inAjaxLink;

    api.getDisabledSubmits = function () {
        return disabledSubmits;
    };

    // see select handler for autocomplete
    var ignoreNextKey = false;

    api.checkForEnter = function (event) {

        if (ignoreNextKey) {
            ignoreNextKey = false;
            return false;
        }

        if (event.keyCode === 13) { // enter key 

            if (this.nodeName.toLowerCase() === "button") {
                this.click();
                return false;
            }

            var form = $(this).closest("form");

            var okButton = form.find("button.nof-ok:first");
            if (okButton.length > 0) {
                okButton.get(0).focus();
                okButton.get(0).click();
                return false;
            }

            var submitButton = form.find("button:submit[name='']:first");
            if (submitButton.length > 0) {
                submitButton.get(0).focus();
                submitButton.get(0).click();
                return false;
            }
            return true;
        } else {
            // for ie8 and earlier the enter button event is not triggering here on an input field. 
            // This means when form is submitted wrong button is selected. So clear previous button here 
            // and pick up in 'getButton'. 
            $("form button.nof-lastClicked").removeClass("nof-lastClicked");
            return true;
        }
    };

    function focusOnFirstText(parent) {

        var firstText;
        var validationError;
        var textSelector = "input[type='text'], input[type='checkbox'], select, textarea";
        var validationSelector = ".input-validation-error";

        if (parent) {
            firstText = parent.find(textSelector).filter(":enabled").first();
            validationError = parent.find(validationSelector).filter(":enabled").first();
        } else {
            firstText = $(textSelector).filter(":enabled").first();
            validationError = $(validationSelector).filter(":enabled").first();
        }

        firstText.focus();
        validationError.focus();
    }

    api.focusOnFirst = function () {

        var okButton = $("button.nof-ok:first");
        var saveButton = $("button.nof-save:first");

        if (okButton.length > 0) {
            var dialog = okButton.closest(".nof-actiondialog");
            focusOnFirstText(dialog);
        } else if (saveButton.length > 0) {
            var object = saveButton.closest(".nof-objectedit");
            focusOnFirstText(object);
        } else {
            focusOnFirstText();
        }
    };

    api.clearHistory = function () {

        api.ajaxCount++;
        $.post($(this).attr("action"), $(this).serialize(), function (response) {

            // check attribute value exists for ie8 support no need ie9+ and ff/chrome
            if ($(".nof-history button").attr("value") && $(".nof-history button").attr("value").split("=")[1].toLowerCase() === 'true') {
                $(".nof-history div.nof-object").replaceWith("");
                $(".nof-history > form").replaceWith($(""));
            } else {
                $(".nof-history div.nof-object:not(:last)").replaceWith("");
                $(".nof-history div.nof-object:first").replaceWith($(".nof-history div.nof-object:first", response));
                $(".nof-history button").attr("value", "clearAll=False"); // keep last value of clearAll flag (which must be false or no button)
            }

            bindToNewHtml(false);
            api.ajaxCount--;
        });
        return false;
    };

    function getButton(event) {

        var button = $("form button.nof-lastClicked").get(0);

        if (!button) {
            // for ie8 and earlier the enter button event is not triggering on an input field. 
            // This means when form is submitted wrong button is selected. Previous button was cleared in 
            // 'checkForEnter' so look for OK or submit button here. 

            var form1 = $(event.target).closest("form");
            var okButton1 = form1.find("button.nof-ok:first");
            if (okButton1.length > 0) {
                button = okButton1.get(0);
            } else {
                var submitButton1 = form1.find("button:submit[name='']:first");
                if (submitButton1.length > 0) {
                    button = submitButton1.get(0);
                }
            }
        }
        return button;
    }

    function errorDialog(title, msg) {

        $(".main-content").append("<div id='_errorMessage' title='" + title + "'>" + msg + "</div>");
        $("#_errorMessage").dialog({ draggable: true, height: '500', width: '1000', close: function () { $("#_errorMessage").remove(); } });
    }

    function startSubmitFeedBack(button) {

        $(button).effect("highlight", {}, 500);
        disabledSubmits = $(":submit, this");
        disabledSubmits.attr("disabled", "disabled");
        $("body").css("cursor", "progress");
    }

    function endSubmitFeedBack() {
        if (disabledSubmits) {
            disabledSubmits.removeAttr("disabled");
            $("body").css("cursor", "auto");
            disabledSubmits = null;
            return true;
        }
        return false;
    }

    function startLinkFeedBack() {
        inAjaxLink = true;
        $("a").css("cursor", "progress");
    }

    function endLinkFeedBack() {
        if (inAjaxLink) {
            inAjaxLink = null;
            $("a").css("cursor", "pointer");
            return true;
        }
        return false;
    }

    api.bindAjaxError = function () {
        $(".main-content").ajaxError(function (e, xhr, settings) {
            // check if we were doing a ajax call - if not ignore - must have been a validate 
            if (endSubmitFeedBack() || endLinkFeedBack()) {
                api.ajaxCount--;
            }
            errorDialog('Ajax Error', "Error in: " + settings.url + " \n" + "error:\n" + xhr.responseText);
        });
    };

    function setCollectionButtonStates() {
        $("div.nof-collection-table button.nof-summary").show();
        $("div.nof-collection-table button.nof-list").show();
        $("div.nof-collection-table button.nof-table").hide();
        $("div.nof-collection-list button.nof-summary").show();
        $("div.nof-collection-list button.nof-list").hide();
        $("div.nof-collection-list button.nof-table").show();
        $("div.nof-collection-summary button.nof-summary").hide();
        $("div.nof-collection-summary button.nof-list").show();
        $("div.nof-collection-summary button.nof-table").show();
    }

    function setMinMaxButtonStates() {
        $("div.nof-property > div.nof-object > a").closest("div.nof-property").find("> div.nof-object > form button.nof-minimize").hide();
        $("div.nof-property > div.nof-object > a").closest("div.nof-property").find("> div.nof-object > form button.nof-maximize").show();
        $("div.nof-property > div.nof-object > div.nof-propertylist").closest("div.nof-property").find("> div.nof-object > form button.nof-maximize").hide();
        $("div.nof-property > div.nof-object > div.nof-propertylist").closest("div.nof-property").find("> div.nof-object > form button.nof-minimize").show();
    }

    function prefixAttribute(elements, attributeName, prefix) {
        elements.each(function (index, elem) {
            var existingId = $(elem).attr(attributeName);
            if (existingId) {
                $(elem).attr(attributeName, prefix + existingId);
            }
        });
    }


    api.redisplayInlineProperty = function (event) {
        api.ajaxCount++;

        var button = getButton(event);

        if (!button || button.name !== "Redisplay") {
            api.ajaxCount--;
            return true;
        }

        startSubmitFeedBack(button);

        var formSerialized = $(this).serializeArray();
        formSerialized[formSerialized.length] = { name: button.name, value: button.value };

        $.post($(this).attr("action"), formSerialized, function (response) {

            handleLoginForm(response);
            var property = $(button).closest("div.nof-property");
            var obj = property.find("> div.nof-object");
            var link = obj.find("> a");
            var propertyList = $("div.nof-propertylist", response);
            propertyList.find("> form").remove();

            var prefix = property.attr("id") + "-";
            prefixAttribute(propertyList.find("*"), "id", prefix);

            link.toggle();

            if ($(button).hasClass("nof-maximize")) {
                obj.append(propertyList);
                obj.find("> img").after($("<span>" + link.text() + "</span>"));
            } else {
                obj.find("> div.nof-propertylist").remove();
                obj.find("> span").remove();
            }

            setMinMaxButtonStates();
            setCollectionButtonStates();

            endSubmitFeedBack();
            api.ajaxCount--;
        });

        return false;
    };

    function truncateId(idToTruncate) {
        var subIds = idToTruncate.split("-");
        var count = subIds.length;
        return subIds[count - 2] + "-" + subIds[count - 1];
    }


    api.redisplayProperty = function (event) {
        api.ajaxCount++;

        var button = getButton(event);

        if (!button || button.name !== "Redisplay") {
            api.ajaxCount--;
            return true;
        }

        startSubmitFeedBack(button);

        var formSerialized = $(this).serializeArray();
        formSerialized[formSerialized.length] = { name: button.name, value: button.value };

        $.post($(this).attr("action"), formSerialized, function (response) {
            handleLoginForm(response);
            var property = $(button).closest("div.nof-property");
            var idToMatch = truncateId(property.attr("id"));
            var replaceWith = $(response).find("div#" + idToMatch);

            if (replaceWith.length > 0) {
                // replace the actual property
                property.replaceWith(replaceWith);
                // replace the hidden input fields to persist the setting 
                var value = $("input[id$='-displayFormats']:first", response).attr("value");
                $("form  input[id$='-displayFormats']").attr("value", value);
            } else {
                // replace the main body of the page
                $(".main-content").replaceWith($(".main-content", response));
            }

            setMinMaxButtonStates();
            setCollectionButtonStates();

            endSubmitFeedBack();
            api.ajaxCount--;
        });

        return false;
    };

    api.updateTitle = function (response) {
        var pattern = /<title>\s*?(.*?)\s*?<\/title>/;
        var matches = response.match(pattern);
        var title = matches ? matches[1] : "";
        document.title = $.trim(title);
    };

    function replace(tag, response) {

        var newvalue = $(tag, response);

        if (newvalue.length > 0) {
            $(tag).replaceWith(newvalue);
            return true;
        }
        return false;
    }

    function replacePageBody(response) {
        if (replace(".main-content", response)) {
            return;
        }
        if (replace("body", response)) {
            return;
        }
        return;
    }

    function isTransientId() {
        if ($("div.nof-objectedit").hasClass("nof-transient")) {
            var action = $("div.nof-objectedit form.nof-edit").attr("action");
            if (action) {
                return getIdFromUrl(action);
            }
        }
        return false;
    }

    api.getLinkFromHistory = function () {
        var isDialog = $(".main-content > div.nof-actiondialog").length > 0;
        var transientId = isTransientId();

        var link;
        if (isDialog) {
            link = $("div.nof-actiondialog > form").attr("action");
        } else if (transientId) {
            link = "/Transient?id=" + escape(transientId);
        } else {
            link = $(".nof-history div.nof-object:last a").attr("href");
            var isEdit = $(".main-content > div.nof-objectedit").length > 0;

            if (isEdit) {
                link = link.replace("/Details?", "/EditObject?");
            }
        }

        return link;
    };

    function updateLinkFromHistory() {
        var link = api.getLinkFromHistory();
        $.address.value(link);
    }

    function cacheIfTransient() {
        var transientId = isTransientId();
        if (transientId) {
            $.jStorage.set("transient:" + transientId, $(".main-content").html());
        }
    }

    api.cacheFormValues = function (formSerialized) {
        $("form div.nof-parameter:has(div.nof-object), form div.nof-property:has(div.nof-object)").each(function (index, element) {
            $.jStorage.set(element.id, $(element).html());
        });

        var nameValues = {};

        for (var i = 0; i < formSerialized.length; i++) {

            // only write value if it evaluates to true or no previous value has been written (ie true or any value takes priority
            // over false or null/undefined)  
            if ((formSerialized[i].value.toLowerCase() !== 'false') || !nameValues[formSerialized[i].name]) {
                nameValues[formSerialized[i].name] = formSerialized[i].value;
            }
        }

        for (nv in nameValues) {
            $.jStorage.set(nv, nameValues[nv]);
        }
    };

    function cacheAllFormValues() {
        $("form.nof-edit, form.nof-dialog").each(function () {
            var formSerialized = $(this).serializeArray();
            api.cacheFormValues(formSerialized);
        });
    }

    function replaceFormValues() {
        $("form div.nof-parameter:has(div.nof-object), form div.nof-property:has(div.nof-object)").each(function (index, element) {
            var replaceWith = $.jStorage.get(element.id, null);
            if (replaceWith) {
                $(element).html(replaceWith);
            }
        });

        $("form input, form select, form textarea").each(function (index, element) {
            var replaceWith = $.jStorage.get($(element).attr("name"), null);
            if (replaceWith) {
                if ($(element).attr("type") === 'checkbox') {
                    if (replaceWith.toLowerCase() === 'true') {
                        $(element).attr('checked', 'checked');
                    } else {
                        $(element).removeAttr('checked');
                    }
                } else if ($(element).attr("type") !== 'hidden' || !!$(element).id) {
                    $(element).val(replaceWith);
                }
            }
        });
    }

    api.updateChoices = function () {
        api.ajaxCount++;

        var choicesData = $(this).closest("div[data-choices]");

        var selects = choicesData.find("select");

        if (choicesData.length == 0 || selects.length == 0) {
            api.ajaxCount--;
            return true;
        }

        var form = $(this).closest("form");
        var url = choicesData.attr("data-choices");
        var parmsString = choicesData.attr("data-choices-parameters");
        var parms = parmsString.split(",");
        var encParms = $.map(parms, function (item) {
            return "-encryptedField-" + item;
        });

        if (!parmsString || ($.inArray($(this).attr('id'), parms) === -1 && $.inArray($(this).attr('id'), encParms) === -1)) {
            // not monitoring this field so return
            api.ajaxCount--;
            return true;
        }

        var inData = {};

        var formSerialized = form.serializeArray();

        function findValue(id) {
            for (var j = 0; j < formSerialized.length; j++) {
                var o = formSerialized[j];
                if (o.name === id) {
                    return o.value;
                }
            }
            return "";
        }

        for (var i = 0; i < parms.length; i++) {
            var parmId = parms[i];
            var encryptParmId = "-encryptedField-" + parmId;
            var encryptValue = findValue(encryptParmId);

            if (encryptValue) {
                inData[encryptParmId] = encryptValue;
            } else {
                inData[parmId] = findValue(parmId);
            }
        }

        $.ajaxSetup({ cache: false });
        $.getJSON(url, inData, function (data) {

            selects.each(function () {

                var id = $(this).attr("id");

                if (typeof data[id] !== "undefined") {
                    var content = data[id][1];
                    var options = $(this).find("option");

                    function contentEqualsOptions() {

                        if (content.length + 1 != options.length) {
                            return false;
                        }

                        for (var k = 0; k < content.length; k++) {
                            // manually single space here as ie8 handles differently from FF ie9 etc

                            var contentSingleSpace = content[k].replace(/\s+/g, ' ');
                            var optionSingleSpace = options[k + 1].text.replace(/\s+/g, ' ');

                            if (contentSingleSpace != optionSingleSpace) {
                                return false;
                            }
                        }

                        return true;
                    }

                    if (!contentEqualsOptions()) {
                        var values = data[id][0];
                        options.replaceWith("");
                        $(this).append("<option/>");
                        for (var j = 0; j < values.length; j++) {
                            $(this).append($("<option value='" + values[j] + "'>" + content[j] + "</option>"));
                        }
                    }
                }
            });
            api.ajaxCount--;
        });

        return true; // ie8 needs two tabs to leave field otherwise  
    };

    function handleLoginForm(response) {
        // catch case when a login form comes back from the server (session timeout)
        // reload page to get a full login page. 

        if ($("section#loginForm", response).length > 0) {
            $.jStorage.flush();
            window.location.reload(true);
        }
    }

    api.updatePageFromAction = function (event) {
        api.ajaxCount++;

        var button = getButton(event);

        if (!button || button.name === "Redisplay") {
            api.ajaxCount--;
            return true;
        }

        if ($(button).closest("form").find(":input[type=file]").length > 0) {
            api.ajaxCount--;
            return true;
        }

        // cache before disabled
        cacheAllFormValues();
        startSubmitFeedBack(button);
        var formSerialized = $(this).serializeArray();
        formSerialized[formSerialized.length] = { name: button.name, value: button.value };
        var isDialog = $(this).attr("class") === "nof-dialog";

        $.post($(this).attr("action"), formSerialized, function (response) {

            handleLoginForm(response);

            if (button.name === "Finder" || button.name === "Selector" || button.name === "ActionAsFinder" || button.name === "InvokeActionAsFinder" || button.name === "InvokeActionAsSave") {

                var divClass = isDialog ? "nof-parameter" : "nof-property";

                var propertySelector = "div." + divClass + ":has(button[value='" + $(button).attr("value") + "'])";
                var propertyId = $(propertySelector).attr("id");

                var replaceWith = $("div#" + propertyId, response);

                if (replaceWith.length > 0) {
                    $(propertySelector).replaceWith(replaceWith);
                    $("#" + propertyId).find(":input").each(api.updateChoices);
                } else {
                    replacePageBody(response);
                }
            } else {
                api.updateTitle(response);
                replacePageBody(response);
            }

            bindToNewHtml(true);

            cacheIfTransient();

            endSubmitFeedBack();
            api.ajaxCount--;
        });
        return false;
    };

    function getIdFromUrl(url) {
        var id = url.substring(url.indexOf('id=') + 3);
        return unescape(id);
    }

    api.isValid = function (draggable, droppable) {
        api.ajaxCount++;

        var url = droppable.attr("data-validate");

        if (!url) {
            api.ajaxCount--;
            return;
        }

        var draggableUrl = draggable.find("a").attr("href");
        var value = getIdFromUrl(draggableUrl);
        var inData = { value: value };

        $.ajaxSetup({ cache: false });
        $.getJSON(url, inData, function (data) {
            if (data === true) {
                droppable.addClass("nof-validdrop");

                // if we go valid when already inside droppable trigger hover
                var dd = $.ui.ddmanager.current;

                $.ui.ddmanager.prepareOffsets(dd);
                $.each($.ui.ddmanager.droppables[dd.options.scope] || [], function () {
                    if ($.ui.intersect(dd, this, dd.options.tolerance || 'intersect')) {
                        if (this.element.hasClass("nof-validdrop")) {
                            this.element.addClass("nof-withindrop");
                        }
                    }
                });
            }
            api.ajaxCount--;
        });
    };

    function updateHiddenValueBehindField(hiddenInput, newValue) {
        $(hiddenInput).attr("value", newValue);

        // if encrypted remove indication 
        var name = $(hiddenInput).attr("name");

        if (name.indexOf("-encryptedField-") === 0) {
            name = name.substring(16);
            $(hiddenInput).attr("name", name);
        }
    }


    api.updateOnSelect = function () {
        var propOrParm = $(this).closest("div.nof-property > div.nof-object");

        if (propOrParm.length == 0) {
            propOrParm = $(this).closest("div.nof-parameter > div.nof-object");
        }

        var newObject = $(this).closest("div.nof-object");

        var autoComplete = propOrParm.find("input[data-completions]");
        var a = propOrParm.find("> a");
        var newA = newObject.find("a");
        var newObjectUrl = newA.attr("href");
        var value = getIdFromUrl(newObjectUrl);

        var linkTitle = newObject.contents().filter(function () { return this.nodeType == 3; }).text();


        if (autoComplete.length > 0) {
            $(autoComplete).val(linkTitle);
        } else {
            if (a.length > 0) {
                a.replaceWith(newA);
            } else {
                propOrParm.prepend(newA);
            }
            propOrParm.find("> a").text(linkTitle);
        }

        var img = propOrParm.find("> img");
        var newImg = newObject.find("img");

        if (img.length > 0) {
            img.replaceWith(newImg);
        } else {
            propOrParm.prepend(newImg);
        }

        var hiddenInput = propOrParm.find("input:last");

        updateHiddenValueBehindField(hiddenInput, value);

        $(this).closest("div.nof-collection-list").remove();

        $(hiddenInput).each(api.updateChoices);
    };

    function setupAutoComplete(index, item) {
        var sourceHandler = function (request, response) {
            api.ajaxCount++;

            var inData = {};

            inData["autoCompleteParm"] = request.term;
            var url = $(item).attr("data-completions");


            $.ajaxSetup({ cache: false });
            $.getJSON(url, inData, function (data) {
                response(data);
                api.ajaxCount--;
            });
        };

        var selectHandler = function (event, ui) {

            if ($(item).parent(".nof-object").length > 0) {
                // refence fielsd so update image and underlying value 

                var img = $(item).siblings("img");

                if (img.length == 0) {
                    $(item).before($("<img/>"));
                }

                $(item).siblings("img").attr("src", ui.item.src);
                $(item).siblings("img").attr("alt", ui.item.alt);

                var hiddenInput = $(item).siblings("input");

                updateHiddenValueBehindField(hiddenInput, ui.item.link);
            }

            // kludge - want enter to select item but not submit form 
            ignoreNextKey = true;
            return true;
        };

        var clearHandler = function () {
            var value = $(this).val();

            if (value.length == 0) {
                updateHiddenValueBehindField($(this).siblings("input:last"), "");
            
            }
        };

        var minLength = $(item).attr("data-completions-minlength");

        $(item).autocomplete({ autoFocus: true, minLength: minLength, source: sourceHandler, select: selectHandler });

        $(item).change(clearHandler);
    }

    function bindToNewHtml(updateLink) {

        $("div.nof-history div.nof-object").draggable({
            helper: 'clone',
            start: function () {
                var draggable = $(this);
                $(".ui-droppable").each(function () {
                    api.isValid(draggable, $(this));
                });
            }
        });

        $("form div.nof-object").droppable({

            drop: function (event, ui) {
                var draggableUrl = ui.helper.find("a").attr("href");
                var value = getIdFromUrl(draggableUrl);

                var a = $(this).find("a");
                var newA = ui.helper.find("a");
                var input = $(this).find("input[data-completions]");

                if (a.length > 0) {
                    a.replaceWith(newA);
                }
                else if (input.length > 0) {
                    $(input).attr("value", newA.text());
                }
                else {
                    $(this).prepend(newA);
                }

                var img = $(this).find("img");
                var newImg = ui.helper.find("img");

                if (img.length > 0) {
                    img.replaceWith(newImg);
                }
                else {
                    $(this).prepend(newImg);
                }

                var hiddenInput = $(this).find("input:last");

                updateHiddenValueBehindField(hiddenInput, value);

            },

            deactivate: function () {
                $(".nof-validdrop").removeClass("nof-validdrop");
                $(".nof-withindrop").removeClass("nof-withindrop");
            },

            accept: function () {
                return $(this).hasClass("nof-validdrop");
            },

            hoverClass: 'nof-withindrop'

        });
        api.focusOnFirst();
        if (updateLink) {
            updateLinkFromHistory();
        }

        $.validator.unobtrusive.parse($(".main-content"));
        // if we are reloading html from cache may still have hasDatepicker attribute. This will prevent 
        // date picker being attached 
        $("input.datetime").removeClass("hasDatepicker");
        $("input.datetime").datepicker();

        // if datepicker has focus a click will not pop it up - so manually do it 
        $("input.datetime:focus").click(function (e) { $(e).datepicker('show'); });

        $.validator.setDefaults({ onkeyup: false });

        // bug in Chrome means that date validation doesn't work if not US (chrome defaults to US dates). 
        // so for the moment just turn off date validation on chrome browsers - date will still be validated on the server
        // there are a bunch of fixes around (eg using dateITA in additional-methods) but so far all have tested out unsatisfactory
        // just turning off for chrome seems safest pending a proper fix 
        // https://github.com/jzaefferer/jquery-validation/issues/549

        // rather than check browser check if underlying bug shows up
        // http://geekswithblogs.net/EltonStoneman/archive/2009/10/29/jquery-date-validation-in-chrome.aspx

        if (isNaN(new Date('29/10/2009').getTime())) {
            // browser cannot handle non-us dates - turn off date validation 
            $.validator.methods["date"] = function () { return true; };
        }

        // autocomplete 

        $("input[data-completions]").each(setupAutoComplete);

        setMinMaxButtonStates();
        setCollectionButtonStates();

        api.bindAjaxError();
    }

    function updatePage(doc, updateLink) {
        api.ajaxCount++;
        $.ajaxSetup({ cache: false });
        var link = doc.attr("href");

        cacheAllFormValues();

        if (link.substring(0, 14) === '/Transient?id=') {

            var id = unescape(link.substring(14));
            var cachedPage = $.jStorage.get("transient:" + id);

            if (cachedPage) {
                $(".main-content").html(cachedPage);
                replaceFormValues();
                bindToNewHtml(updateLink);
                api.ajaxCount--;
                return false;
            }
        }


        $.get(link, function (response) {

            handleLoginForm(response);

            api.updateTitle(response);
            replacePageBody(response);
            replaceFormValues();
            bindToNewHtml(updateLink);
            endLinkFeedBack();
            api.ajaxCount--;
        });
        return false;
    }

    api.updatePageFromLink = function () {
        startLinkFeedBack();
        return updatePage($(this), true);
    };

    api.syncPageToAddress = function () {
        startLinkFeedBack();
        return updatePage($(this), false);
    };

    api.markedClicked = function () {
        $("form button.nof-lastClicked").removeClass("nof-lastClicked");
        $(this).addClass("nof-lastClicked");
        return true;
    };

    api.allowSubmit = function () {
        // this allows these buttons to submit form even if invalid - for finders/selectors etc
        $(this).closest("form").validate().cancelSubmit = true;
        return true;
    };

    return api;
} ());

$.address.externalChange(function (event) {
    if (event.value === "/") {
        // ignore root as it causes problems on IIS installations 
        return;
    }
    $("<a href='" + event.value + "'></a>").click(nakedObjects.syncPageToAddress).click();
});

$(window).unload(function () {
    $("form.nof-edit, form.nof-dialog").each(function () {
        var formSerialized = $(this).serializeArray();
        nakedObjects.cacheFormValues(formSerialized);
    });
});

$(window).load(function () {
    var url = location.href;
    if (url.indexOf('#') === -1) {
        var homepath = $("div.no-home-path > a").attr("href") || $("header nav a").attr("href");
        if (location.pathname !== homepath) {
            var link = nakedObjects.getLinkFromHistory();
            if (link) {
                location.href = location.protocol + "//" + location.host + homepath + "/#" + link;
            }
        }
    }
    return true;
});

window.onerror = function (msg, url, linenumber) {
    alert('Error message: ' + msg + '\nURL: ' + url + '\nLine Number: ' + linenumber);
    return true;
};

nakedObjects.bindAjaxError();

// jquery live binds
$(function () { $("form button").live("click", nakedObjects.markedClicked); });
$(function () { $("form button[name=Finder], form button[name=Redisplay], form button[name=Selector], form button[name=ActionAsFinder], form button[name=InvokeActionAsFinder], form button[name=InvokeActionAsSave]").live('click', nakedObjects.allowSubmit); });
$(function () { $("form button[title=Select]").live('click', nakedObjects.updateOnSelect); });
$(function () { $(":input").live("change", nakedObjects.updateChoices); });
$(function () { $(".nof-history form").live("submit", nakedObjects.clearHistory); });
$(function () { $("form:has(button.nof-summary)").live("submit", nakedObjects.redisplayProperty); });
$(function () { $("form:has(button.nof-maximize)").live("submit", nakedObjects.redisplayInlineProperty); });
$(function () { $("form.nof-edit").live("submit", nakedObjects.redisplayProperty); });
$(function () { $(".nof-menu form.nof-action, .nof-propertylist form.nof-action, .nof-objectedit form.nof-action, .nof-standalonetable form.nof-action, form.nof-edit, form.nof-dialog, .nof-actiondialog form.nof-action").live("submit", nakedObjects.updatePageFromAction); });
$(function () { $("div.nof-object > a").live("click", nakedObjects.updatePageFromLink); });
$(function () { $('#checkboxAll').live("change", function () { $("input[type='checkbox']").attr('checked', $('#checkboxAll').is(':checked')); }); }); // use change not click for ie8
$(function () { $("form :input:not(textarea)").live("keydown", nakedObjects.checkForEnter); });