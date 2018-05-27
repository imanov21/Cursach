function valid(form) {

    var log, pass1, pass2, controlWord;
    log = form.name.value;
    pass1 = form.password1.value;
    pass2 = form.password2.value;
    controlWord = form.controlWord.value;

    if (log == "" || log == " ") {
        alert("Вы не ввели логин!")
        return false;
    } else if (pass1 == "" || pass1 == " ") {
        alert("Вы не ввели пароль!")
        return false;
    } else if (pass2 == "" || pass2 == " ") {
        alert("Вы не повторили пароль!")
        return false;
    } else if (pass1 != pass2) {
        alert("Пароли не совпадают")
        return false;
    } else if (controlWord == "" || controlWord == " ") {
        alert("Вы не ввели проверочное слово!")
        return false;
    } else 
    window.location = "auth.html";
}