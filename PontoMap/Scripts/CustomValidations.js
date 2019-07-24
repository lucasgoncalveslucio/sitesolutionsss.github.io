
function validaCPF(s) {
    var i;
    var l = '';
    for (i = 0; i < s.length; i++) if (!isNaN(s.charAt(i))) l += s.charAt(i);
    s = l;
    if (s.length != 11) return false;
    var c = s.substr(0, 9);
    var dv = s.substr(9, 2);
    var d1 = 0;
    for (i = 0; i < 9; i++) d1 += c.charAt(i) * (10 - i);
    if (d1 == 0) return false;
    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(0) != d1) return false;
    d1 *= 2;
    for (i = 0; i < 9; i++) d1 += c.charAt(i) * (11 - i)
    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(1) != d1) return false;
    return true;
}

function validaCNPJ(c) {
    var b = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    if ((c = c.replace(/[^\d]/g, "")).length != 14)
        return false;

    if (/0{14}/.test(c))
        return false;

    for (var i = 0, n = 0; i < 12; n += c[i] * b[++i]);
    if (c[12] != (((n %= 11) < 2) ? 0 : 11 - n))
        return false;

    for (var i = 0, n = 0; i <= 12; n += c[i] * b[i++]);
    if (c[13] != (((n %= 11) < 2) ? 0 : 11 - n))
        return false;

    return true;

}


function validaDATA(field) {
    var checkstr = "0123456789";
    var DateField = field;
    var Datevalue = "";
    var DateTemp = "";
    var seperator = "/";
    var day;
    var month;
    var year;
    var leap = 0;
    var err = 0;
    var i;
    err = 0;
    DateValue = DateField.value;
    /* Deleta todos os caracteres exceto de 0 a 9. */
    for (i = 0; i < DateValue.length; i++) {
        if (checkstr.indexOf(DateValue.substr(i, 1)) >= 0) {
            DateTemp = DateTemp + DateValue.substr(i, 1);
        }
    }
    DateValue = DateTemp;

    if (DateValue.length == 6) {
        DateValue = DateValue.substr(0, 4) + '20' + DateValue.substr(4, 2);
    }
    if (DateValue.length != 8) {
        err = 19;
    }

    year = DateValue.substr(4, 4);
    if ((year == 0) || (year < 1800) || (year > 2099)) {
        err = 20;
    }

    month = DateValue.substr(2, 2);
    if ((month < 1) || (month > 12)) {
        err = 21;
    }

    day = DateValue.substr(0, 2);
    if (day < 1) {
        err = 22;
    }

    if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
        leap = 1;
    }
    if ((month == 2) && (leap == 1) && (day > 29)) {
        err = 23;
    }
    if ((month == 2) && (leap != 1) && (day > 28)) {
        err = 24;
    }

    if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
        err = 25;
    }
    if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
        err = 26;
    }

    if ((day == 0) && (month == 0) && (year == 00)) {
        err = 0; day = ""; month = ""; year = ""; seperator = "";
    }

    if (err == 0) {
        DateField.value = day + seperator + month + seperator + year;


        // Checando se o id  Existe
        if ($('#' + $(field).attr('id') + 'ValidationMessage').length == 0) {
            $('#' + $(field).attr('id') + 'ValidationMessage').html("");
        }

    }

    else {

        // Checando se o id não Existe
        if ($('#' + $(field).attr('id') + 'ValidationMessage').length == 0) {
            alert("Insira uma data valida.");
        } else {
            $('#' + $(field).attr('id') + 'ValidationMessage').html("Data invalida");
        }

        DateField.value = ""
        DateField.focus();
    }
}



function validaInicialFinal(dtInicial, dtFinal) {
    //Necessário importar biblioteca moment.js
    //dtHora --> "04/09/2013 15:00:00" ==========
    var ms = moment(dtFinal, "DD/MM/YYYY").diff(moment(dtInicial, "DD/MM/YYYY"));
    
    if (ms < 0) {

        return false;
    }
    return true;
}