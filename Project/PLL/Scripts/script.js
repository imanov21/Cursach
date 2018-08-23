function validReg(form) {

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

function validIn(form) {
    var log, pass;

    log = form.log.value;
    pass = form.pass.value;

    if (log == "" || log == " ") {
        alert("Вы не ввели логин!")
        return false;
    } else if (pass == "" || pass == " ") {
        alert("Вы не ввели пароль!")
        return false;
    } else
        window.location = "index.html";
}

function toReg() {
    window.location = "reg.html";
}

function forgotPass() {
    var PASSWORD = "PASSWORD";
    alert("Ваш пароль -" + PASSWORD);
    window.location = "auth.html";
}

function validVac(form) {
    var name, info, skill, city, cash, employment, work;
    name = form.input1.value;
    info = form.input2.value;
    skill = form.input3.value;
    city = form.input4.value;
    cash = form.input5.value;
    employment = form.typeОfEmployment.value;
    work = form.placeOfWork.value

    if (name == "" || name == " ") {
        alert("Вы не ввели имя!");
        return 0;
    } else if (info == "" || info == " ") {
        alert("Вы не ввели информацию!");
        return 0;
    } else if (skill == "" || skill == " ") {
        alert("Вы не ввели требования!");
        return 0;
    } else if (city == "" || city == " ") {
        alert("Вы не ввели город!");
        return 0;
    } else if (cash == "" || cash == " ") {
        alert("Вы не ввели заработную плату!");
        return 0;
    } else if (employment == "" || employment == " ") {
        alert("Вы не выбрали тип занятости!");
        return 0;
    } else if (work == "" || work == " ") {
        alert("Вы не выбрали место работы!");
        return 0;
    } else {
        alert("Вакансия добавленна!")
        window.location = "index.html";
    }
}