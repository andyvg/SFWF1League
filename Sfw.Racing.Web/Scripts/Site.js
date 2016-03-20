
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