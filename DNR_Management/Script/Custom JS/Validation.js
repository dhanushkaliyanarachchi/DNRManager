
   //function numbersonly()
   //{
   //    var data = document.getElementById('<%=TextBoxAccountNo.ClientID %>').value;
   //    var filter=/^[a-zA-Z0-9]+$/;
   //    if(!filter.test(data))
   //    {
   //        alert("Please enter alphanumeric only");
   //        document.getElementById('<%=TextBoxAccountNo.ClientID %>').value = "";
   //        document.getElementById('<%=TextBoxAccountNo.ClientID %>').focus();
   //        return false;
   //    }
   //    else if(data.Length!=10)
   //    {
   //        alert("Please enter 10 digits");
   //        return false;
   //    }
   //    else
   //        return true;
   //}
function validate(key) {
    //getting key code of pressed key
    var keycode = (key.which) ? key.which : key.keyCode;
    var phn = document.getElementById('ctl00$ContentPlaceHolder1$TextBoxAccountNo');
    var phnName = document.getElementsByName("");
    "use strict";
   
    //comparing pressed keycodes
    if (!(keycode == 8 ) && (keycode < 48 || keycode > 57)) {
        return false;
    }
    else if ((keycode == 9)) {
        return false;
    }
    else {
        return true;
    }
}

function tabKeyDown(key,obj) {
    var keycode = (key.which) ? key.which : key.keyCode;
    var txt = obj.value.length;
    if (event.keyCode == 9) {
        if (txt != 10) {
            alert('insert 10 digit acc No');
            return false;
        }

    }
}
