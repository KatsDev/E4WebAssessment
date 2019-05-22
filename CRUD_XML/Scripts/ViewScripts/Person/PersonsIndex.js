$(document).ready(function () {
    LoadData();
});
$("#txtFirstName").click(function () {
    alert('test');
});
function SavePerson() {
    var errorFree = true;
    if ($("#txtFirstName").val() == "") {
        $('#txtFirstName').css('border-color', 'red');
        $('#lblnameErr').text('First Name Required');
        errorFree = false;
    }
    else {
        $('#txtLastName').css('border-color', '');
        $('#lblnameErr').text('');
    }

    if ($("#txtLastName").val() == "") {
        $('#txtLastName').css('border-color', 'red');
        $('#lbllastnameErr').text('Last Name Required');
        errorFree = false;
    }
    else {
        $('#txtContactNumber').css('border-color', '');
        $('#lbllastnameErr').text('');
    }

    if ($("#txtContactNumber").val() == "") {
        $('#txtContactNumber').css('border-color', 'red');
        $('#lblcontactNumErr').text('Contact Number Required');
        errorFree = false;
    }
    else {
        $('#txtContactNumber').css('border-color', '');
        $('#lblcontactNumErr').text('');
    }
    if (errorFree) {
        var personObj = {
            FirstName: $("#txtFirstName").val(),
            LastName: $("#txtLastName").val(),
            ContactNumber: $("#txtContactNumber").val()
        };
        //console.log(personObj);
        $.post("Persons/SavePerson", personObj, function (data) {
            $("#PersonMainModal").modal("hide");
            LoadData();
        });
    }
}

//function getUserByID(id) {
//    $.get("Persons/GetPersonByID/" + id, function (data) {
//        console.log(data);
//    });
//}

function AddPerson() {
    $.get("Persons/AddNewPerson", function (data) {
        $("#PersonDetailContent").html(data);
        $("#PersonMainModal").modal("show");
    });
}

function GetPersonToEdit(id) {
    $.get("Persons/AddNewPerson/" + id, function (data) {
        $("#PersonDetailContent").html(data);
        $("#PersonMainModal").modal("show");
    });
}

function DeletePerson(id) {

    bootbox.confirm("Are you sure want to delete?", function (result) {
        if (result) {
            $.post("Persons/DeletePerson/" + id, function (data) {
                LoadData();
            });
        }
    });
}

function UpdatePerson() {
    var personObj = {
        ID: $("#hdID").val(),
        FirstName: $("#txtFirstName").val(),
        LastName: $("#txtLastName").val(),
        ContactNumber: $("#txtContactNumber").val()
    };
    $.post("Persons/UpdatePerson", personObj, function (data) {
        $("#PersonMainModal").modal("hide");
        LoadData();
    });
}

function LoadData() {
    $("#personKendo").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: "/Persons/GetPersons",
                    contentType: "application/json",
                    type: "GET"
                },
                parameterMap: function (options) {
                    return kendo.stringify(options);
                }
            },
            pageSize: 50
        },
        height: 550,
        filterable: true,
        resizable: true,
        sortable: true,
        editable: "inline",
        pageable: { refresh: true, pageSizes: [5, 10, 20] },
        columns: [{
            field: "FirstName",
            title: "First Name"
        },
        {
            field: "LastName",
            title: "Last Name"
        },
        {
            field: "ContactNumber",
            title: "Contact Number"
        },
        {
            width: '10%',
            template: "<button class='btn btn-sm btn-warning' id='btnEdit' onclick='GetPersonToEdit(#=ID#)'>Edit</button>"
        },
        {
            width: '10%',
            template: "<button class='btn btn-sm btn-danger' id='btnDelete' onclick='DeletePerson(#=ID#)'>Delete</button>"
        }]
    });
}