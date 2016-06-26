$(document).ready(function() {
    $("#uploadFileBtn").change(UploadImageFromComputer);
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


//$('#btn-save').on('click', function (e) {

//    e.preventDefault();
//    if (window.FormData !== undefined) {
//        var data = new FormData();
//        data.append("image", $('#uploadFileBtn')[0].files[0]);
//        $.ajax({
//            type: "POST",
//            url: '/Profile/Upload',
//            async: false,
//            contentType: false,
//            processData: false,
//            data: data,
//            success: function (result) {
//                form.submit();
//                return;
//            },
//            error: function (xhr, status, p3) {
//                alert(xhr.responseText);
//            }
//        });
//    } else {
//        alert("Браузер не поддерживает загрузку файлов HTML5!");
//    }
//    return;
//});