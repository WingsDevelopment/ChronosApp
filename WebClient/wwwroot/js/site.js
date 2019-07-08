// Write your JavaScript code.
// updateovati ove funkcije
function UpdateTimeTo() {
    var dateFrom = $("#date-from").datepicker("getDate");
    var dateTo = $("#date-to").datepicker("getDate");

    if (dateFrom > dateTo) {
        $("#date-to").datepicker('setDate', dateFrom);
        dateTo = dateFrom;
    }
    updateBusinessDayElement(dateFrom, dateTo);
}
function UpdateTimeFrom() {
    var dateFrom = $("#date-from").datepicker("getDate");
    var dateTo = $("#date-to").datepicker("getDate");

    if (dateFrom > dateTo) {
        $("#date-from").datepicker('setDate', dateTo);
        dateFrom = dateTo;
    }
    updateBusinessDayElement(dateFrom, dateTo);
}

function updateBusinessDayElement(dateFrom, dateTo) {
    var numberOfBusinessDays = getBusinessDatesCount(dateFrom, dateTo);
    var workingDaysElement = document.getElementById("working-days");
    workingDaysElement.innerText = numberOfBusinessDays.toString();
}

function getBusinessDatesCount(startDate, endDate) {
    if (startDate == endDate)
        return 1;

    var count = 0;
    var curDate = startDate;
    while (curDate <= endDate) {
        var dayOfWeek = curDate.getDay();
        if (!((dayOfWeek == 6) || (dayOfWeek == 0)))
            count++;
        curDate.setDate(curDate.getDate() + 1);
    }
    return count;
}

function showFilterData(dataDict, slideDiraction) {
    var filterDataElement = document.getElementById("chrono-filter-data");

    for (var key in dataDict) {
        var value = dataDict[key];

        var label = document.createElement("label");
        label.innerHTML = key.bold();

        var para = document.createElement("p");
        var node = document.createTextNode(value);
        para.appendChild(node);
        para.style.marginLeft = "5px";
        para.style.marginRight = "25px";

        filterDataElement.appendChild(label);
        filterDataElement.appendChild(para);
    }

    var para = document.createElement("p");
    var node = document.createTextNode("]");
    para.appendChild(node);
    para.style.marginLeft = "-20px";
    filterDataElement.appendChild(para);

    $("#chrono-filter-data").show("slide", { direction: slideDiraction }, 1000);
}

function checkInput(element) {
    if (element.value <= 0) {
        element.setCustomValidity('The number must be greater than zero.');
    } else {
        // input is fine -- reset the error message
        element.setCustomValidity('');
    }
}

function configureDatePickerAndSetToFirstDayOfMonth(elementId) {
    var date = new Date();
    var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);

    $('#' + elementId).datepicker({
        dateFormat: "dd-M-yy",
        beforeShowDay: $.datepicker.noWeekends
    });

    $('#' + elementId).datepicker('setDate', firstDay);
}

function configureDatePickerAndSetToLastDayOfMonth(elementId) {
    var date = new Date();
    var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);

    $('#' + elementId).datepicker({
        dateFormat: "dd-M-yy",
        beforeShowDay: $.datepicker.noWeekends
    });
    $('#' + elementId).datepicker('setDate', lastDay);
}