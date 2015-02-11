// standalone function
!$(function () {
    // click on tab disable click if errors - register popup
    $("#myTabs .nav, nav-tabs").click(function (e) {
        $("#form0").validate();
        if ($("#form0").valid()) {
            return true;
        }          
        else {     
            return false;
        }          
    }); //tabs click

    // details anchor click - to load popup from catalogue
    $("a.btn-primary").on("click", function (e) {
        pcd = $(this).attr("data-prodcd");
        $("#qty").val("0");
        $("#messg2").text("");
        $("#detailsGraphic").attr("src", $("#Graphic" + pcd).attr("src"));
        $("#detailsProdName").text($("#Name" + pcd).text());
        $("#detailsDescr").text($("#Descr" + pcd).data('description'));
        $("#detailsProdcd").val(pcd);
        $("#detailsPrice").text($("#Price" + pcd).text());
    });

    $('tr:odd').css('background-color', '#D8D8D8');
});                

