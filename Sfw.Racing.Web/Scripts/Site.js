
$.validator.setDefaults({
    ignore: [] // or ignore: "" depending on the jquery version
});

$(document).ready(function () {

    $(".table").tablesorter();

    $('form').validate({
        ignore: []
    });

    $.validator.setDefaults({
        ignore: [] // or ignore: "" depending on the jquery version
    });

    $(".DriverSelection").change(function () {

        var driver = $(this);

        if (driver.val() == "") {
            var prev = driver.prev();
            prev.find(".DriverPic").attr("src", "/Content/drivers/unknown.png");
            prev.find(".DriverConstructor").html("&nbsp;");
            prev.find(".DriverName").html("&nbsp;");
            prev.find(".Cost").text(0);
            return;
        }

        $.ajax({
            url: '/Driver/Driver', // to get the right path to controller from TableRoutes of Asp.Net MVC
            dataType: "json", //to work with json format
            data: { DriverId: driver.val() },
            contentType: 'application/json', //define a contentType of your request
            cache: false, //avoid caching results
            success: function (data) {
                // data is your result from controller
                if (data.success) {
                    var prev = driver.prev();
                    prev.find(".DriverPic").attr("src", "/" + (data.model.Image || "Content/drivers/unknown.png"));
                    prev.find(".DriverConstructor").text(data.model.ConstructorName);
                    prev.find(".DriverName").text(data.model.Name);
                    prev.find(".Cost").text(data.model.Cost);

                    updateDriverSelectionDropDown();

                    updateCost();

                    checkMaxChanges();
                }
            },
            error: function (xhr) {
                alert('error');
            }
        });
    });

    $(".EngineSelection").change(function () {

        var engine = $(this);

        if (engine.val() == "") {
            var prev = engine.prev();
            prev.find(".EnginePic").attr("src", "/Content/engines/unknown.png");
            prev.find(".EngineName").html("&nbsp;");
            prev.find(".Cost").text(0);
            return;
        }

        $.ajax({
            url: '/Engine/Engine', // to get the right path to controller from TableRoutes of Asp.Net MVC
            dataType: "json", //to work with json format
            data: { EngineId: engine.val() },
            contentType: 'application/json', //define a contentType of your request
            cache: false, //avoid caching results
            success: function (data) {
                // data is your result from controller
                if (data.success) {
                    var prev = engine.prev();
                    prev.find(".EnginePic").attr("src", "/" + (data.model.Image || "Content/engines/unknown.png"));
                    prev.find(".EngineName").text(data.model.Name);
                    prev.find(".Cost").text(data.model.Cost);

                    updateEngineSelectionDropDown();

                    updateCost();

                    checkMaxChanges();
                }
            },
            error: function (xhr) {
                alert('error');
            }
        });
    });

    $(".ConstructorSelection").change(function () {

        var constructor = $(this);

        if (constructor.val() == "") {
            var prev = constructor.prev();
            prev.find(".ConstructorPic").attr("src", "/Content/constructors/unknown.png");
            prev.find(".ConstructorName").html("&nbsp;");
            prev.find(".Cost").text(0);
            return;
        }

        $.ajax({
            url: '/Constructor/Constructor', // to get the right path to controller from TableRoutes of Asp.Net MVC
            dataType: "json", //to work with json format
            data: { ConstructorId: constructor.val() },
            contentType: 'application/json', //define a contentType of your request
            cache: false, //avoid caching results
            success: function (data) {
                // data is your result from controller
                if (data.success) {
                    var prev = constructor.prev();
                    prev.find(".ConstructorPic").attr("src", "/" + (data.model.Image || "Content/constructors/unknown.png"));
                    prev.find(".ConstructorName").text(data.model.Name);
                    prev.find(".Cost").text(data.model.Cost);

                    updateConstructorSelectionDropDown();

                    updateCost();

                    checkMaxChanges();
                }
            },
            error: function (xhr) {
                alert('error');
            }
        });
    });

    $.validator.addMethod("enforcetrue", function (value, element, param) {
        return false;
    });
    $.validator.unobtrusive.adapters.addBool("enforcetrue");

    updateDriverSelectionDropDown();

    updateEngineSelectionDropDown();

    updateConstructorSelectionDropDown();

    setTimeout("checkMaxChanges()", 100);
});

function updateDriverSelectionDropDown() {

    $(".DriverSelection option").removeClass("disabled");

    $(".DriverSelection").each(function () {

        var val = $(this).val();
        $(".DriverSelection option[value=" + val + "]").addClass("disabled");
        $(this).find("option[value=" + val + "]").removeClass("disabled");
    });   
}

function updateEngineSelectionDropDown() {

    $(".EngineSelection option").removeClass("disabled");

    $(".EngineSelection").each(function () {

        var val = $(this).val();
        $(".EngineSelection option[value=" + val + "]").addClass("disabled");
        $(this).find("option[value=" + val + "]").removeClass("disabled");
    });
}

function updateConstructorSelectionDropDown() {

    $(".ConstructorSelection option").removeClass("disabled");

    $(".ConstructorSelection").each(function () {

        var val = $(this).val();
        $(".ConstructorSelection option[value=" + val + "]").addClass("disabled");
        $(this).find("option[value=" + val + "]").removeClass("disabled");
    });
}

function updateCost() {
    var totalCost = 0;
    $(".Cost").each(function () {
        totalCost += Number($(this).text());
    })
    $(".TotalCost").text(totalCost);

    var MaxBudget = Number($(".MaxBudget").text());

    if (totalCost > MaxBudget) {
        $(".Budget").removeClass("label-success");
        $(".Budget").addClass("label-danger");
    } else {
        $(".Budget").removeClass("label-danger");
        $(".Budget").addClass("label-success");
    }
}

var components = [];

function setComponents(selectedComponents) {
    components = selectedComponents;
}

var maxChangesAllowed;

function setMaxChangesAllowed(changesAllowed) {
    maxChangesAllowed = changesAllowed;
}

function checkMaxChanges() {
    if (components !== undefined && components.length > 0) {

        var checkComponents = components.slice(0); //clone array

        //if previous race contains D0, then there was no previous selection;
        if (checkComponents.indexOf("D0") >= 0) return;

        $(".DriverSelection").each(function () {
            checkComponents.push("D"+$(this).val());
        });
        $(".ConstructorSelection").each(function () {
            checkComponents.push("C"+$(this).val());
        });
        $(".EngineSelection").each(function () {
            checkComponents.push("E"+$(this).val());
        });

        var uniqueComponents = checkComponents.filter(onlyUnique);        

        if (uniqueComponents.length - 8 > maxChangesAllowed) {
            $(".maxChangesAllowed span").removeClass("label-success").addClass("label-danger").text("Error: You've made " + (uniqueComponents.length - 8) + " out of your allowed " + maxChangesAllowed + " changes");
            $(".saveBtn").hide();
            $(".saveChangesAlert").show();
        } else {
            $(".maxChangesAllowed span").removeClass("label-danger").addClass("label-success").text("You've made " + (uniqueComponents.length - 8) + " out of a possible " + maxChangesAllowed + " changes");
            $(".saveBtn").show();
            $(".saveChangesAlert").hide();
        }
    }
}

function onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
}

function arraysEqual(a1, a2) {
    return JSON.stringify(a1) == JSON.stringify(a2);
}

var finalEntry;
var finalEntryTimer;

function setFinalEntry(date) {
    finalEntry = new Date(date);
    updateFinalEntry();

    finalEntryTimer = setInterval("updateFinalEntry()", 1000)
}

function treatAsUTC(date) {
    var result = new Date(date);
    result.setMinutes(result.getMinutes() - result.getTimezoneOffset());
    return result;
}

function daysBetween(startDate, endDate) {
    var millisecondsPerDay = 24 * 60 * 60 * 1000;
    return (treatAsUTC(endDate) - treatAsUTC(startDate)) / millisecondsPerDay;
}

function updateFinalEntry() {

    var now = new Date().getTime();

    var dateNow = new Date(finalEntry - now);

    var days = Math.floor(daysBetween(now, finalEntry));

    if (days < 0) {
        $("#FinalEntry").addClass("label label-danger").text("closed");
        clearInterval(finalEntryTimer);
    }
    else if (days >= 0) {
        $("#FinalEntry").text(formatTime((24 * days) + dateNow.getHours()) + "h " + formatTime(dateNow.getMinutes()) + "m " + formatTime(dateNow.getSeconds()) +"s");
    }
}

function formatTime(time) {
    if ((time + "").length == 1) {
        return "0" + time;
    }
    return time;
}