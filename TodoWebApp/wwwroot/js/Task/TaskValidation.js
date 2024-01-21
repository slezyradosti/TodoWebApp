function validateAdd() {
    let isValid = true;
    let input=$('#taskDetails');
    let errorElement = $('#inputDetailsError')
    
    if(input.val().trim() == "")
    {
        input.addClass('border-danger');
        errorElement.removeClass("error").addClass("error_show");
        isValid=false;
    }
    else
    {
        input.removeClass('border-danger');
        errorElement.removeClass("error_show").addClass("error");
    }
    return isValid;
}

function validateEdit() {
    let isValid = true;
    let input=$('#taskDetailsEditInput');
    let errorElement = $('#editDetailsError')

    if(input.val().trim() == "")
    {
        input.addClass('border-danger');
        errorElement.removeClass("error").addClass("error_show");
        isValid=false;
    }
    else
    {
        input.removeClass('border-danger');
        errorElement.removeClass("error_show").addClass("error");
    }
    return isValid;
}

$('#taskDetails').on('input', function() {
    let input=$('#taskDetails');
    let errorElement = $('#inputDetailsError')

    if(input.val().trim() != "")
    {
        input.removeClass('border-danger');
        errorElement.removeClass("error_show").addClass("error");
    }
});

$('#taskDetailsEditInput').on('input', function() {
    let input=$('#taskDetailsEditInput');
    let errorElement = $('#editDetailsError')

    if(input.val().trim() != "")
    {
        input.removeClass('border-danger');
        errorElement.removeClass("error_show").addClass("error");
    }
});