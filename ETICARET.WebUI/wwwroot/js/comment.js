CommentBodyId = "#comment";
var productid = -1;


$(document).ready(function () {
    var url = $("#comment").data("url");
    $("#comment").load(url);
    productid = $("#comment").data("product-id");
    $(CommentBodyId).load("/Comment/ShowProductComments?id="+productId)
})

function imageBox(smallImg) {
    var fullImg = document.getElementById("imageBox");
    fullImg.src = smallImg.src;
}

function doComment(btn, e, commentid, spanid) {
    var button = $(btn);
    var mode = button.data("edit-mode");
    var editableContent = $("#comment_text_"+commentid)
    if (e == "new_clicked") {
        var txt = $("#new_comment_text").val();

        $.ajax({
            method: "POST",
            url: "/Comment/Create/",
            data: {"text":txt,"productid":productid}
        }).done(function (data) {
            if (data.result) {
                $(CommentBodyId).load("/Comment/ShowProductComments?id=" + productid)
            }
            else {
                alert("Yorum yapılamadı...")
            }
        }).fail(function (e) {
            console.log(e);
            alert("Sunucu ile bağlantı kurulamadı...")
        })
    }
    else if (e == "delete_clicked") {
        console.log(e);
        var dialog_res = confirm("Yorum silinsin mi?");
        if (!dialog_res) return false;

        $.ajax({
            method: "POST",
            url: "/Comment/Delete?id="+commentid
        }).done(function (data) {
            if (data.result) {
                $(CommentBodyId).load("/Comment/ShowProductComments?id=" + productid)
            }
            else {
                alert("Yorum silinemedi...")
            }
        }).fail(function (e) {
            console.log(e);
            alert("Sunucu ile bağlantı kurulamadı...")
        })
    }
    else if (e == "edit_clicked") {
        if (!mode) {
            button.data("edit-mode", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");
            var btnSpan = button.find("span");
            btnSpan.removeClass("fa-edit");
            btnSpan.addClass("fa-check");
            editableContent.addClass("editable-content");

            $(spanid).attr("contenteditable", true)
        }
        else {
            button.data("edit-mode", true);
            button.removeClass("btn-succes");
            button.addClass("btn-warning");
            var btnSpan = button.find("span");
            btnSpan.removeClass("fa-check");
            btnSpan.addClass("fa-edit");
            editableContent.removeClass("editable-content");

            $(spanid).attr("contenteditable", true)

            var txt = $(spanid).text();

            $.ajax({
                method: "POST",
                url: "/Comment/Edit/",
                data: { "text": txt, "id": commentid }
            }).done(function (data) {
                if (data.result) {
                    $(CommentBodyId).load("/Comment/ShowProductComments?id=" + productid)
                }
                else {
                    alert("Yorum güncellenemedi...")
                }
            }).fail(function (e) {
                console.log(e);
                alert("Sunucu ile bağlantı kurulamadı")
            })
        }
    }
}