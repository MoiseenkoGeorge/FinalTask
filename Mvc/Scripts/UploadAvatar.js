$(document).ready(function() {
    $("#uploadFileBtn").change(UploadImageFromComputer);
    $("#role").change(ChangeRole);
    form = $("#Form");
});

function UploadImageFromComputer() {
    UploadImage(URL.createObjectURL($('#uploadFileBtn')[0].files[0]));

}

function UploadImage(url) {
    $("#Avatar").attr("src", url);
}
$(function () {
    $("#datepicker").datepicker({
        yearRange: "1960:2016",
        changeYear: true,
        changeMonth: true
    })

});

function ChangeRole() {
    if ($("#role").val() == 'Manager')
        $("#areas-div").hide();
    else
        $("#areas-div").show();
}

$('#btn-save').on('click', function(e) {
    e.preventDefault();
    $("#areas").val($('.containr span').map((index, element) => element.innerHTML).toArray().join(';'));
    if ($('#uploadFileBtn')[0].files[0] != null) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            data.append("image", $('#uploadFileBtn')[0].files[0]);
            $.ajax({
                type: "POST",
                url: '/Profile/Upload/',
                async: false,
                contentType: false,
                processData: false,
                data: data,
                success: function(result) {
                    $("#imageUrl").val(result[0].Uri);
                    form.submit();
                    return;
                },
                error: function(xhr, status, p3) {
                    alert(xhr.responseText);
                }
            });
        } else {
            alert("Браузер не поддерживает загрузку файлов HTML5!");
        }
        return;
    }
    form.submit();
});