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
            
            $.each(result.value, function (key, item) {
                html += '<li class="list-group-item d-flex justify-content-between align-items-center">';
                    html += '<div class="ms-2 me-auto">';
                        html += '<div class="form-check form-switch">';
                        
                        let checkboxId = "checkbox" + item.id;
                        if (item.isDone) {
                            html += '<input class="form-check-input" type="checkbox" value="" id="'+checkboxId+'" onchange=Mark("'+item.id+'") checked>';
                            html +=  '<s>' + item.details + '</s>' + '</br>';
                        }
                        else {
                            html += '<input class="form-check-input" type="checkbox" value="" id="'+checkboxId+'" onchange=Mark("'+item.id+'") >';
                            html +=   item.details + '</br>';
                        }
                        
                    html += '</div>';
                html += '</div>';
                
                //edit
                // html += '<span class="badge bg-primary rounded-pill">';
                //     html += '<button href="#" onclick="return getbyID("'+item.id+'")">Edit</button>'
                // html += '</span>';
                <!-- Button trigger modal -->
                
                // working
                html += '<button class="btn btn-outline-warning "  href="#" onclick=getbyID("'+item.id+'")>Edit</button>'
                
                // delete
                html +=  '<button class="btn btn-outline-danger  ms-3" href="#" onclick=Delete("'+item.id+'")>Delete</button>'
                    
                html += '</tr>';
            });
            $('#taskList').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function loadTaskList(result){
    let html = '';
    $.each(result.value, function (key, item) {
        console.log("tasklist Item " + item)
        
        html += '<li class="list-group-item d-flex justify-content-between align-items-center">';
        html += '<div class="ms-2 me-auto">';
        html += '<div class="form-check form-switch">';

        let checkboxId = "checkbox" + item.id;
        if (item.isDone) {
            html += '<input class="form-check-input" type="checkbox" value="" id="'+checkboxId+'" onchange=Mark("'+item.id+'") checked>';
            html +=  '<s>' + item.details + '</s>' + '</br>';
        }
        else {
            html += '<input class="form-check-input" type="checkbox" value="" id="'+checkboxId+'" onchange=Mark("'+item.id+'") >';
            html +=   item.details + '</br>';
        }

        html += '</div>';
        html += '</div>';

        //edit
        // html += '<span class="badge bg-primary rounded-pill">';
        //     html += '<button href="#" onclick="return getbyID("'+item.id+'")">Edit</button>'
        // html += '</span>';
        <!-- Button trigger modal -->

        // working
        html += '<button class="btn btn-outline-warning "  href="#" onclick=getbyID("'+item.id+'")>Edit</button>'

        // delete
        html +=  '<button class="btn btn-outline-danger  ms-3" href="#" onclick=Delete("'+item.id+'")>Delete</button>'

        html += '</tr>';
    });
    $('#taskList').html(html);
}

//Add Data Function
function Add() {
    var result = validateAdd();
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
            SortDataAndLoad();
            // $('#myModal').modal('hide');
            clearTextBox();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon  ID
function getbyID(id) {
    $.ajax({
        url: "/Task/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#taskIdEdit').val(result.value.id);
            $('#taskDetailsEditInput').val(result.value.details);
            
            $('#exampleModal').modal('show');
            $("#btnTaskUpdate").click(Update);
            // $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating 
function Update() {
    var result = validateEdit();
    if (result == false) {
        return false;
    }
    
    var task = {
        Id: $('#taskIdEdit').val(),
        Details: $('#taskDetailsEditInput').val(),
    };
    
    $.ajax({
        url: "/Task/" + task.Id,
        data: JSON.stringify(task),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            SortDataAndLoad();
            $('#taskDetails').val("");
            $('#exampleModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record
function Delete(Id) {
    const stringId = String(Id);
        $.ajax({
            url: "/Task/" + stringId,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                SortDataAndLoad();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
}

function Mark(taskId) {
    const stringId = String(taskId);

    let checkbox = document.getElementById(`checkbox${stringId}`);
    let state = checkbox.checked;

    $.ajax({
        url: "/Task/Mark/" + stringId,
        data: JSON.stringify(state),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            SortDataAndLoad();
            $('#taskDetails').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for clearing the textboxes
function clearTextBox() {
    $('#taskDetails').val("");
    $('#taskDetailsEditInput').val("");
}

$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

$(".dropdown-menu a").click(function(){
    let text = $(this).text();
    let value = ConverrDropdownText(text);

    // save filter to localStorage
    localStorage.setItem('taskFilter', value);
    
    SortDataAndLoad()
});

function SortDataAndLoad(){
    let value = localStorage.getItem('taskFilter');
    
    if (value == null ||  value.trim() == "" || value == "show_all" ) return loadData();

    $.ajax({
        url: "/Task/List/Filter?option=" + value,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result)
        {
            loadTaskList(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function ConverrDropdownText(text){
    let result = 'show_all';
    
    if (text == "Show pending"){
        result = "show_pending"
    }
    else if (text == "Show completed"){
        result = "show_completed"
    }

    
    return result;
}