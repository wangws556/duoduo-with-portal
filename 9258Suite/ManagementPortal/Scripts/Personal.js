var maleManager;
var femaleManager;


function submitForm() {
    var name = $('#Name').val();
    var nickName = $('#NickName').val();
    var age = $('#Age').val();
    var email = $('#Email').val();
    var country = $('#Country').val();
    var state = $('#State').val();
    var city = $('#City').val();
    var gender = false;
    if (maleManager.getValue()) {
        gender = true;
    }
    $.ajax({
        url: 'Home/SavePersonal',
        type: 'POST',
        data: {name:name,nickName:nickName,age:age,email:email,country:country,state:state,city:city,gender:gender},
        success: function (msg) {
            if (msg.Success) {
                $.ligerDialog.success("保存成功！");
            }
            else {
                alert(msg.Message);
            }
            isSaving = false;
        },
        error: function (error) {
            $.ligerDialog.error("保存失败！");
            isSaving = false;
        }
    });
}

function InitFormValidation(formTagId)
{
    $.metadata.setType("attr", "validate");
    var v = $(formTagId).validate({
        //调试状态，不会提交数据的
        debug: false,
        errorPlacement: function (lable, element) {

            if (element.hasClass("l-textarea")) {
                element.addClass("l-textarea-invalid");
            }
            else if (element.hasClass("l-text-field")) {
                element.parent().addClass("l-text-invalid");
            }

            var nextCell = element.parents("td:first").next("td");
            if (nextCell.find("div.l-exclamation").length == 0) {
                $('<div class="l-exclamation" title="' + lable.html() + '"></div>').appendTo(nextCell).ligerTip();
            }
        },
        invalidHandler: function (form, validator) {
            var errors = validator.numberOfInvalids();
            if (errors) {
                var message = errors == 1
                  ? 'You missed 1 field. It has been highlighted'
                  : 'You missed ' + errors + ' fields. They have been highlighted';
                $("#errorLabelContainer").html(message).show();
            }
        },
        success: function (lable) {
            var element = $("#" + lable.attr("for"));
            var nextCell = element.parents("td:first").next("td");
            if (element.hasClass("l-textarea")) {
                element.removeClass("l-textarea-invalid");
            }
            else if (element.hasClass("l-text-field")) {
                element.parent().removeClass("l-text-invalid");
            }
            nextCell.find("div.l-exclamation").remove();
        },
        submitHandler: function () {
            $("#errorLabelContainer").html("").hide();
        }
    });
}

function InitForm(maleTagId, femaleTagId, formTagId) {
    InitFormValidation(formTagId);
    maleManager = $(maleTagId).ligerRadio();
    femaleManager = $(femaleTagId).ligerRadio();
    $(formTagId).ligerForm();
}

function changePassword() {
    var oldPwd = $('#OldPwd').val();
    var newPwd = $('#NewPwd').val();
    var cNewPwd = $('#ConfirmNewPwd').val();
    if (newPwd != cNewPwd)
    {
        alert("密码不一致");
        return;
    }
    $.ajax({
        url: 'Home/ChangePassword',
        type: 'POST',
        async:false,
        data: { oldPwd: oldPwd, newPwd: newPwd},
        success: function (msg) {
            if (msg.Success) {
                $.ligerDialog.success("修改密码成功！");
            }
            else {
                $.ligerDialog.error("修改密码失败！");
            }
        },
        error: function (error) {
            $.ligerDialog.error("修改密码失败！");
        }
    });
}
