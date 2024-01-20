//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData();
});

//Load Data function
function loadData() {
    $.ajax({
        url: "/Task/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<li class="list-group-item d-flex justify-content-between align-items-start">';
                    html += '<div class="ms-2 me-auto">';
                        html += '<div class="form-check form-switch">';
                            html += '<input class="form-check-input" type="checkbox" value="" id="flexSwitchCheckDefault">';
                            html +=   item.details + '</br>';
                    html += '</div>';
                html += '</div>';
                html += '<span class="badge bg-primary rounded-pill">';
                    html += '<a href="#" onclick="return getbyID(' + item.id + ')">Edit</a>'
                html += '</span>';
                html += '<span class="badge bg-primary rounded-pill">';
                    html +=  '<a href="#" onclick="Delele(' + item.id + ')">Delete</a>'
                html += '</span>';
                
               // html += '<td><a href="#" onclick="return getbyID(' + item.Id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.Id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('#taskList').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function
function Add() {
    var result = validate();
    if (result == false) {
        return false;
    }
    
    var task = {
        Details: $('#taskDetails').val(),
    };
    
    $.ajax({
        url: "/Task",
        data: JSON.stringify(task),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            // $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function validate() {
    var isValid = true;
    if ($('#taskDetails').val().trim() == "") {
        $('#taskDetails').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#taskDetails').css('border-color', 'lightgrey');
    }
    return isValid;
}

//Function for getting the Data Based upon Employee ID
function getbyID(EmpID) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
    $('#State').css('border-color', 'lightgrey');
    $('#Country').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Home/getbyID/" + EmpID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#EmployeeID').val(result.EmployeeID);
            $('#Name').val(result.Name);
            $('#Age').val(result.Age);
            $('#State').val(result.State);
            $('#Country').val(result.Country);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating employee's record
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    
    var task = {
        Details: $('#taskDetails').val(),
    };
    
    $.ajax({
        url: "/Task",
        data: JSON.stringify(task),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#taskDetails').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record
function Delele(Id) {
        $.ajax({
            url: "/Task/" + Id,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
}

//Function for clearing the textboxes
function clearTextBox() {
    $('#taskDetails').val("");
}