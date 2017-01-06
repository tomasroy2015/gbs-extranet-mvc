

function onError(e) {

    $.ajax({
        url: "/Error/getErrorCode",
        data: { id: "usman" },
        dataType: "json",
        type: "POST",
        error: function (e) {
            //alert("An error occurred." + e.responseText);
        },

        success: function (data) {
            if (data.errorCode == "UnauthorizedAccess") {
                var windowElement = $('#Window');
                windowElement.data('kendoWindow').title("Error").content("<div class='PopupDiv'>You have no rights to perform this Action<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:CloseWindow();' >OK</button> </div></div>").center().open();
            }
            else if (data.errorCode == "UnExpectedError") {
                var windowElement = $('#Window');
                windowElement.data('kendoWindow').title("Error").content("<div class='PopupDiv'>Unexpected error occurred.<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:CloseWindow();' >OK</button> </div></div>").center().open();
            }
            else if (data.errorCode == "DateTimeFormat") {
                var windowElement = $('#Window');
                windowElement.data('kendoWindow').title("Error").content("<div class='PopupDiv'>Date/Time format is not valid. Please enter Date as dd/MM/yyyy and Time as HH:mm<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:CloseWindow();' >OK</button> </div></div>").center().open();
            }
            else if (data.errorCode == "2601") {
                var windowElement = $('#Window');
                windowElement.data('kendoWindow').title("Error").content("<div class='PopupDiv'>Data already exists, it must be unique.<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:CloseWindow();' >OK</button> </div></div>").center().open();
            }

            else if (data.errorCode == "2627") {
                var windowElement = $('#Window');
                windowElement.data('kendoWindow').title("Error").content("<div class='PopupDiv'>This data already exists, it must be unique.<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:CloseWindow();' >OK</button> </div></div>").center().open();
            }
            else if (data.errorCode == "547") {
                var windowElement = $('#Window');
                windowElement.data('kendoWindow').title("Error").content("<div class='PopupDiv'>You cannot delete this record. It is already in use.<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:CloseWindow();' >OK</button></div></div>").center().open();
            }
            else if (data.errorCode == "20047") {
                var windowElement = $('#Window');
                windowElement.data('kendoWindow').title("Error").content("<div class='PopupDiv'>You are not authorised to perform this operation.<br/><div class='PopupBtn'><b><a href='/Home'><button id='btnLogOn' class='k-button' type='button' >LogOn</button></div></div> ").center().open();
                return false;
            }
            else if (data.errorCode == "EmailAlreadyExists") {
                var windowElement = $('#Window');
                windowElement.data('kendoWindow').title("Error").content("<div class='PopupDiv'>Email Already Exists, it should be unique.<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:CloseWindow();' >OK</button> </div></div>").center().open();
                return false;
            }
           
            var mygrid = $('#grid').data('kendoGrid');
            if (mygrid != undefined) { mygrid.dataSource.read(); }

        }
    });
    e.preventDefault();
}

function DisplayGridError(e, status) {

    if (e.status == "customerror") {
        OpenErrorWindow(e.errors)
        //alert(e.errors);
    }
    else {
        OpenErrorWindow("Unexpected Error Occurred");
        //alert("Generic server error.");
    }

    return false;
}

function CloseWindow() {
    var window = $('#Window').data('kendoWindow');
    window.close();
}


function ProcessingWindow(title) {
    var windowElement = $('#Window');
    windowElement.data('kendoWindow').title(title).content("<div><center><img alt='Processing please wait' src='/Images/loadingbar.gif' /> </center>  </div></div>").center().open();

}


function OpenErrorWindow(msg) {
    var windowElement = $('#Window');
    windowElement.data('kendoWindow').title("Error").content("<div class='PopupDiv'>" + msg + "<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:CloseWindow();' >OK</button> </div></div>").center().open();

}

function OpenInformationWindow(msg, title) {
    var windowElement = $('#Window');
    windowElement.data('kendoWindow').title(title).content("<div class='PopupDiv'>" + msg + "<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:CloseWindow();' >OK</button> </div></div>").center().open();

}

var Redirectlocation;
function OpenWindow_Location(msg, title, location, buttonText) {
    Redirectlocation = location;
    var windowElement = $('#Window');
    windowElement.data('kendoWindow').title(title).content("<div class='PopupDiv'>" + msg + "<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:BackToLocation();' >" + buttonText + "</button> </div></div>").center().open();

}

var RedirectlocationYES;
var RedirectlocationNO;
function OpenWindow_LocationYesNo(msg, title, locationYes, locationNo, buttonTextYes, buttonTextNo) {
    RedirectlocationYES = locationYes;
    RedirectlocationNO = locationNo;

    var windowElement = $('#Window');
    windowElement.data('kendoWindow').title(title).content("<div class='PopupDiv'>" + msg +
                    "<br/><div class='PopupBtn'><button id='btnClose' class='k-button' type='button' onclick='javascript:BackToLocationYES();' >" + buttonTextYes + "</button> <button id='btnClose' class='k-button' type='button' onclick='javascript:BackToLocationNO();' >" + buttonTextNo + "</button> </div></div>").center().open();

}


function FilenameAlreadyExistsRenameIt() {
    OpenInformationWindow("One or more Filename(s) already exists.", "Warning");
}


function BackToLocationYES() {
    location.href = RedirectlocationYES;
}

function BackToLocationNO() {
    location.href = RedirectlocationNO;
}

function BackToLocation() {
    location.href = Redirectlocation;
}

function BackToHomePage() {
    location.href = "/Home";
}


function ddl_OnLoad_OnFocus() {
    var ddl = $(this);

    ddl.closest(".t-widget").focus(function () {
        ddl.data("tDropDownList").open();
    });

}

$(document).keypress(function (e) {
    if (e.which == 13) {

        if ($("#btnClose") != undefined)
            $("#btnClose").click();

        return true;
    }
});
