//DODAJ SALON
$(document).ready(function () {
    $("#dodajSalon").click(function () {
        //VALIDACIJA ADD
        $("#addFormSalon").validate({
            rules: {
                naziv: {
                    required: true,
                    minlength: 3
                },
                vlasnik: {
                    required: true,
                    minlength: 3
                },
                adresa: {
                    required: true,
                    minlength: 5
                },
                pib: {
                    required: true,
                    number: true,
                    minlength: 6,
                    maxlength: 9
                },
                brojZiroRacuna: {
                    required: true,
                    minlength: 10
                }
            },
            messages: {
                naziv: {
                    required: "Unesite validno ime salona",
                    minlength: "Mora imati bar 3 karaktera"
                },
                vlasnik: {
                    required: "Unesite validnog vlasnika",
                    minlength: "Mora imati bar 3 karaktera"
                },
                adresa: {
                    required: "Unesite validnu adresu",
                    minlength: "Mora imati bar 5 karaktera"
                },
                pib: {
                    required: "Unesite validan pib",
                    minlength: "Mora imati minimum 6",
                    maxlength: "Moze imati max 9 karaktera"
                },
                brojZiroRacuna: {
                    required: "Unesite validan broj ziro racuna",
                    minlength: "Mora imati bar 10 karaktera"
                },

            },
            //ako je u redu da doda
            submitHandler: function () {

                var object = {
                    "Naziv": $("#naziv").val(),
                    "Vlasnik": $("#vlasnik").val(),
                    "Adresa": $("#adresa").val(),
                    "Telefon": $("#telefon").val(),
                    "Email": $("#email").val(),
                    "WebStranica": $("#webStranica").val(),
                    "PIB": $("#pib").val(),
                    "BrojZiroRacuna": $("#brojZiroRacuna").val()
                };
                console.log(object.Naziv);
                console.log(object.Vlasnik);
                console.log(object.Adresa);
                console.log(object.Telefon);
                console.log(object.Email);
                console.log(object.WebStranica);
                console.log(object.PIB);
                console.log(object.BrojZiroRacuna);
                

                $.ajax({
                    type: "POST",
                    url: "api/Salons",
                    data: object,
                    success: function () {
                        alert("Uspesno dodat salon");
                        $('#modalSalon').modal('hide');
                        $("#naziv").val(""),
                        $("#vlasik").val(""),
                        $("#adresa").val(""),
                        $("#telefon").val(""),
                        $("#email").val(""),
                        $("#webStranica").val(""),
                        $("#pib").val(""),
                        $("#brojZiroRacuna").val("")

                    },
                    error: function () {
                        alert("Nije uspelo");
                    }
                });
            }
        });
    }
 );
    //var table = $('#tblSalon').DataTable();

    
    //na taj nacin mu blokiram koga da pretrazuje
    var table = $('#tblSalon').DataTable({
        "deferRender": true,

        "columnDefs": [
            //{ targets: [0,5], searchable: true },
            { targets: [0,1,2,6,7], searchable: true },
            { targets: '_all', searchable: false }
        ],
        "fnDestroy":true
    });
    //table.fnDestroy();
    //table.fnReloadAjax();
});
//KRAJ DODAJ SALON

$(document).ready(function () {
    var table = $('#tblUsers').DataTable({
        "deferRender": true,
        "columnDefs": [
            //{ targets: [0,5], searchable: true },
            { targets: [4], searchable: false },
            { targets: '_all', searchable: true }
        ]
    });
});

$(document).ready(function () {
    //var table = $('#tblRacun').DataTable({
    //    "deferRender": true,
    //    "columnDefs": [
    //        { targets: [2,6,7], searchable: true },
    //        { targets: '_all', searchable: false }
    //    ]
    //});


    var table = $('#tblRacun').DataTable({
        "deferRender": true,
            "columnDefs": [
                { targets: [2,6,7], searchable: true },
                { targets: '_all', searchable: false }
            ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                    i : 0;
            };

            // Total over all pages
            total = api
                .column(5, { 'search': 'applied' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(5, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(5).footer()).html(
                'Ukupno za uplatu:' + '<br>' + total.toFixed(2) + ' RSD'
            );
        }
    });




});

$(document).ready(function () {
    var table = $('#tblKategorijaNamestaja').DataTable({
        "deferRender": true,
        "columnDefs": [
            { targets: [0], searchable: true },
            { targets: '_all', searchable: false }
        ]
    });
});

$(document).ready(function () {
    var table = $('#tblKomadNamestaja').DataTable({
        "deferRender": true,
        "columnDefs": [
            { targets: [1,2,3,4], searchable: true },
            { targets: '_all', searchable: false }
        ]
    });
});
//DODAJ KATEGORIJU
$(document).ready(function () {
    $("#dodajKategorijuNamestaja").click(function () {
        //VALIDACIJA ADD
        $("#addFormKategorijaNamestaja").validate({
            rules: {
                naziv: {
                    required: true,
                    minlength: 3
                }
            },
            messages: {
                naziv: {
                    required: "Unesite validno ime kategorije",
                    minlength: "Mora imati bar 3 karaktera"
                }
            },
            //ako je u redu da doda
            submitHandler: function () {
                var object = {
                    "Naziv": $("#naziv").val(),
                    "Opis": $("#opis").val()
                };
                $.ajax({
                    type: "POST",
                    url: "/api/KategorijaNamestajas",
                    data: object,
                    success: function () {
                        //$("#tblKategorijaNamestaja").DataTable().destroy({
                        //$('#tblKategorijaNamestaja').DataTable().clear();
                        //});
                        var table = $("#tblKategorijaNamestaja").DataTable({
                            destroy: true
                        });
                        location.reload();

                        $('#modalKategorijaNamestaja').modal('hide');
                        $("#naziv").val(""),
                        $("#opis").val("")
                    }
                });
            }
        });
    }
        );
  
    var table = $('#tblKategorijaNamestaja').DataTable();
});
//KRAJ DODAJ KATEGORIJU

//DA UCITA SVE SALONE U DROPDOWN u SALONIMA ZA NEPRIJAVLJENE
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/tblSalons/getSalons",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Izaberite salon</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].SalonId + '">' + data[i].Naziv + '</option>';
            }
            $("#salonDropdown").html(s);
        }
    });
});


//sada da ispise tu vrednost sto je izabrao u salonu
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/tblSalons/getSalons",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Izaberite salon</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].SalonId + '">' + data[i].Naziv + '</option>';
            }
            $("#salonDropdown").html(s);
        }
    });
});

//uzima vrednost koju smo izabrali
function ispisiSalonSpisakNamestaja() {
    $.ajax({
        type: "GET",
        url: "/tblSalons/getNamestajPosleIzboraSalona",
        data: "{}",
        success: function (data) { //ispod treba da popuni tabelu u html-u
            $("#tabelaIspisNamestajaPosleBiranjaSalonaTBody").empty(); //da pri odabiru bude prazni svi childreni u tbody
   
            //if ('success' in data) {
            //    $('#tabelaIspisNamestajaPosleBiranjaSalona').DataTable().ajax.reload(null, false);
            //}
            var spisak = '<h3>Spisak Namestaja iz tog salona</h3>'; //string
            var artikal = "";
            for (var i = 0; i < data.length; i++) {
                if (data[i].SalonId == $("#salonDropdown").val()) { //ne valja
                    artikal = "<tr><td>" + data[i].Naziv + "</td><td>" + data[i].KolicinaUMagacinu + " </td><td>" + data[i].JedinicnaCena + "</td><td>" + data[i].Slika + "</td></tr>";
                    $("#tabelaIspisNamestajaPosleBiranjaSalonaTBody").append(artikal);
                }
            }
        }
    });
    $('#tabelaIspisNamestajaPosleBiranjaSalona').DataTable();
}

function ipisCeneBezPdvKodKreiranjaRacuna() { //treba da ispise cenu bez pdv-a u polje i ukupnu cenu
        $.ajax({
            type: "GET",
            url: "/tblRacuns/getJedinicnaCenaBezPdv",
            data: "{}",
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].KomadNamestajaId == $(".dropDownIzborNamestajaURacunu").val()) {
                        $(".txtCenaZaDobijanjeIzBaze").val(data[i].JedinicnaCena);
                        let zaZaokruzivanje = (data[i].JedinicnaCena * (($(".porez").val() / 100) + 1) * ($(".kolicinaURacunu").val())).toFixed(2);
                        $(".txtUkupnaCenaSaPdv").val(zaZaokruzivanje);
                        break;
                    }
                }
            }
        });
}

//Da doda novi artikal u bazu pre zavrsi racun
function dodajNoviArtikal() { //da ispise broj racuna iz baze
    //var uspesnoDodatArtikal = false;
    var komadNamestajaZaOduzimanjeIzMagacinaId;
    var komadNamestajaZaOduzimanjeIzMagacina;
    var objectZaUpdateKomadaNamestajaUMagacinu;
    var object = {
                "KomadNamestajaId": $(".dropDownIzborNamestajaURacunu").val(),
                "Kolicina": $(".kolicinaURacunu").val(),
                "Porez": $(".porez").val(),
                "CenaBezPdv": $(".txtCenaZaDobijanjeIzBaze").val(),
                "UkupnaCenaSaPorezom": $(".txtUkupnaCenaSaPdv").val(),
                "DatumIVremeKupovine": $(".datumIVreme").val(),
                "KupacKojiJeKupioRobu": $(".kupacKojiJeKupioRobu").val(),
                "SalonId": $(".spisakSalonaKodRacuna").val(),
                "BrojRacuna": $(".brojRacunaIzBaze").val(),
                "Uknjizeno": false
            };
    
    //$("#tblRacunTrenutniTBody").empty();
    $.ajax({
        type: "POST",
        url: "/api/Racuns",
        data: object,
        async: false,
        success: function (response) {

            //provera da li ima dovoljno komada u magacinu
            var brojKomadaUMagacinu = checkBrojKomadaUMagacinu($(".kolicinaURacunu").val(), $(".dropDownIzborNamestajaURacunu").val());
            if (brojKomadaUMagacinu >= $(".kolicinaURacunu").val()) {
                alert("Artikal dodat");
                //sada treba da doda taj artikal u tabelu u create
                var artikal = "<tr><td>" + $(".dropDownIzborNamestajaURacunu option:selected").text() + "</td><td>" + object.Kolicina + " </td><td>" + object.CenaBezPdv + " </td><td>" + object.Porez + "</td><td>" + object.UkupnaCenaSaPorezom + "</td></tr>";
                $("#tblRacunTrenutniTBody").append(artikal);
                racunajTrenutniRacunFooter();
                //uspesnoDodatArtikal = true;
            }
            else {
                response.status == "error";
                alert("***GRESKA*** nije uspelo jer ste izabrali vise komada nego sto ima u magacinu");
            }
        },
        error: function () {
            alert("Nije uspelo");
            //uspesnoDodatArtikal = false;
        }
    });
    //alert(uspesnoDodatArtikal + " uspesno dodat artikal true or false");
    
    //sada treba da oduzme jedan komad iz magacina ako je success
    //ali prvo da vidi koji je to id iz baze i vrednost koliko ima komada u magacinu
    $.ajax({
        type: "GET",
        url: "/tblRacuns/smanjiBrojKomadaIzMagacina",
        data: "{}",
        success: function (data) { //ispod treba da popuni tabelu u html-u
            for (var i = 0; i < data.length; i++) {
                if (data[i].KomadNamestajaId == $(".dropDownIzborNamestajaURacunu").val()) {
                    komadNamestajaZaOduzimanjeIzMagacinaId = data[i].KomadNamestajaId;
                    komadNamestajaZaOduzimanjeIzMagacina = data[i].KolicinaUMagacinu;

                    objectZaUpdateKomadaNamestajaUMagacinu = {
                        "KomadNamestajaId": $(".dropDownIzborNamestajaURacunu").val(),
                        "Sifra": data[i].Sifra,
                        "Naziv": data[i].Naziv,
                        "ZemljaProizvodnje": data[i].ZemljaProizvodnje,
                        "GodinaProizvodnje": data[i].GodinaProizvodnje,
                        "JedinicnaCena": data[i].JedinicnaCena,
                        "KolicinaUMagacinu": komadNamestajaZaOduzimanjeIzMagacina - $(".kolicinaURacunu").val(),
                        "SalonId": $(".spisakSalonaKodRacuna").val(),
                        "KategorijaNamestajaId": data[i].KategorijaNamestajaId,
                        "Slika": ""
                    };
                    updateKomadNamestajaIzMagacina(komadNamestajaZaOduzimanjeIzMagacinaId, objectZaUpdateKomadaNamestajaUMagacinu);
                }
            }
        }
    });
}


function checkBrojKomadaUMagacinu(broj, id) {
    var brojKomadaIzMagacina = 0;
    $.ajax({
        type: "GET",
        url: "/tblSalons/getNamestajPosleIzboraSalona",
        data: "{}",
        async:false,
        success: function (data) { //ispod treba da popuni tabelu u html-u
            for (var i = 0; i < data.length; i++) {
                if (data[i].KomadNamestajaId == id) {
                    brojKomadaIzMagacina = data[i].KolicinaUMagacinu;
                }
            }
        }
    });
    return brojKomadaIzMagacina;
}


function updateKomadNamestajaIzMagacina(id, objectZaUpdateKomadaNamestajaUMagacinu) {
    $.ajax({
        type: "PUT", 
        //on mora da doda tip tblKomadNamestaja ako hocu sa post da updatujem
        url: "../api/KomadNamestajas/" + id, //ne radi put
        data: objectZaUpdateKomadaNamestajaUMagacinu,
        success: function () {
            alert("Broj komada je promenjen u magacinu");
        },
        error: function () {
            alert("GRESKA, Broj komada nije promenjen u magacinu");
        }
    });
}


//za datatable trenutnog racuna i totala njegovog

function racunajTrenutniRacunFooter() {

        var table = $('#tblRacunTrenutni').DataTable({
            searching: false,
            paging: false,
            info: false,
            "footerCallback": function (row, data, start, end, display) {
                var api = this.api(), data;

                // Remove the formatting to get integer data for summation
                var intVal = function (i) {
                    return typeof i === 'string' ?
                        i.replace(/[\$,]/g, '') * 1 :
                        typeof i === 'number' ?
                        i : 0;
                };

                // Total over all pages
                total = api
                    .column(4)
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

                // Total over this page
                pageTotal = api
                    .column(4, { page: 'current' })
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

                // Update footer
                $(api.column(4).footer()).html(
                    'Ukupno za uplatu:' + '<br>' + total.toFixed(2) + ' RSD'
                );
            },
            
        });
        table.destroy();



    //$(document).ready(function () {
    //    var table = $('#tblRacunTrenutni').DataTable({
    //        searching: false,
    //        paging: false,
    //        info: false,
    //        "footerCallback": function (row, data, start, end, display) {
    //            var api = this.api(), data;

    //            // Remove the formatting to get integer data for summation
    //            var intVal = function (i) {
    //                return typeof i === 'string' ?
    //                    i.replace(/[\$,]/g, '') * 1 :
    //                    typeof i === 'number' ?
    //                    i : 0;
    //            };

    //            // Total over all pages
    //            total = api
    //                .column(3)
    //                .data()
    //                .reduce(function (a, b) {
    //                    return intVal(a) + intVal(b);
    //                }, 0);

    //            // Total over this page
    //            pageTotal = api
    //                .column(3, { page: 'current' })
    //                .data()
    //                .reduce(function (a, b) {
    //                    return intVal(a) + intVal(b);
    //                }, 0);

    //            // Update footer
    //            $(api.column(4).footer()).html(
    //                'Ukupno za uplatu:' + '<br>' + total + ' RSD'
    //            );
    //        }
    //    });
    //});
}
//zavrsi kupovinu
function zavrsiKupovinu() {
    var object = {
        "KomadNamestajaId": $(".dropDownIzborNamestajaURacunu").val(),
        "Porez": $(".porez").val(),
        "Kolicina": $(".kolicinaURacunu").val(),
        "CenaBezPdv": $(".txtCenaZaDobijanjeIzBaze").val(),
        "UkupnaCenaSaPorezom": $(".txtUkupnaCenaSaPdv").val(),
        "DatumIVremeKupovine": $(".datumIVreme").val(),
        "KupacKojiJeKupioRobu": $(".kupacKojiJeKupioRobu").val(),
        "SalonId": $(".spisakSalonaKodRacuna").val(),
        "BrojRacuna": $(".brojRacunaIzBaze").val(),
        "Uknjizeno": true
    };

    $.ajax({
        type: "POST",
        url: "/api/Racuns",
        data: object,
        success: function (response) {
            //prvo provera da li ima dovoljno komada u magacinu
            var brojKomadaUMagacinu = checkBrojKomadaUMagacinu($(".kolicinaURacunu").val(), $(".dropDownIzborNamestajaURacunu").val());
            if (brojKomadaUMagacinu >= $(".kolicinaURacunu").val()) {
                alert("Artikal dodat i zavrsena kupovina");
                var artikal = "<tr><td>" + $(".dropDownIzborNamestajaURacunu option:selected").text() + "</td><td>" + object.Kolicina + " </td><td>" + object.CenaBezPdv + " </td><td>" + object.Porez + "</td><td>" + object.UkupnaCenaSaPorezom + "</td></tr>";
                $("#tblRacunTrenutniTBody").append(artikal);
                racunajTrenutniRacunFooter();
                $("#stampajRacun").removeClass("disabled");
                $(".dodajNoviArtikal").attr("disabled", true);
                $(".dropDownIzborNamestajaURacunu").attr("disabled", true);
                $(".zavrsiKupovinu").attr("disabled", true);
                $(".kolicinaURacunu").attr("disabled", true);
                $(".spisakSalonaKodRacuna").attr("disabled", true);
                $(".stornirajRacun").attr("disabled", true);


            }
            else {
                response.status == "error";
                alert("***GRESKA*** nije uspelo jer ste izabrali vise komada nego sto ima u magacinu");
            }
        },
        error: function () {
            alert("Nije uspelo");
        }
    });


    //sada da smanji broj komada namestaja u komad namestaja
    $.ajax({
        type: "GET",
        url: "/tblRacuns/smanjiBrojKomadaIzMagacina",
        data: "{}",
        success: function (data) { //ispod treba da popuni tabelu u html-u
            for (var i = 0; i < data.length; i++) {
                if (data[i].KomadNamestajaId == $(".dropDownIzborNamestajaURacunu").val()) {
                    komadNamestajaZaOduzimanjeIzMagacinaId = data[i].KomadNamestajaId;
                    komadNamestajaZaOduzimanjeIzMagacina = data[i].KolicinaUMagacinu;

                    objectZaUpdateKomadaNamestajaUMagacinu = {
                        "KomadNamestajaId": $(".dropDownIzborNamestajaURacunu").val(),
                        "Sifra": data[i].Sifra,
                        "Naziv": data[i].Naziv,
                        "ZemljaProizvodnje": data[i].ZemljaProizvodnje,
                        "GodinaProizvodnje": data[i].GodinaProizvodnje,
                        "JedinicnaCena": data[i].JedinicnaCena,
                        "KolicinaUMagacinu": komadNamestajaZaOduzimanjeIzMagacina - $(".kolicinaURacunu").val(),
                        "SalonId": $(".spisakSalonaKodRacuna").val(),
                        "KategorijaNamestajaId": data[i].KategorijaNamestajaId,
                        "Slika": ""
                    };
                    updateKomadNamestajaIzMagacina(komadNamestajaZaOduzimanjeIzMagacinaId, objectZaUpdateKomadaNamestajaUMagacinu);
                }
            }
        }
    });
}


function updateKomadNamestajaIzMagacina(id, objectZaUpdateKomadaNamestajaUMagacinu) {
    $.ajax({
        type: "PUT",
        //on mora da doda tip tblKomadNamestaja ako hocu sa post da updatujem
        url: "../api/KomadNamestajas/" + id, //ne radi put
        data: objectZaUpdateKomadaNamestajaUMagacinu,
        success: function () {
            alert("Broj komada je smanjen u magacinu");
        },
        error: function () {
            alert("GRESKA, Broj komada nije smanjen u magacinu");
        }
    });
    
}


function ispisKomadaNamestajaPosleIzabranogSalonaKodRacuna() { //da u racunima posle izbora salona ispise izbor namestaja u njemu i kada zavrsi odma readonly
    $.ajax({
        type: "GET",
        url: "/tblRacuns/getNamestajPosleIzboraSalona",
        data: "{}",
        success: function (data) { 
            $(".dropDownIzborNamestajaURacunu").empty();
            var s;
            for (var i = 0; i < data.length; i++) {
                if (data[i].SalonId == $(".spisakSalonaKodRacuna").val()) {
                    s += '<option value="' + data[i].KomadNamestajaId + '">' + data[i].Naziv + '</option>';
                }
                $(".dropDownIzborNamestajaURacunu").html(s);
                $(".spisakSalonaKodRacuna").attr("disabled", true);
            }
        }
    });

    //sada da refresuje polja kod izbora salona
    $.ajax({
            type: "GET",
            url: "/tblRacuns/getJedinicnaCenaBezPdv",
            data: "{}",
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].KomadNamestajaId == $(".dropDownIzborNamestajaURacunu").val()) {
                        $(".txtCenaZaDobijanjeIzBaze").val(data[i].JedinicnaCena);

                        

                        let zaZaokruzivanje = (data[i].JedinicnaCena * (($(".porez").val() / 100) + 1) * $(".kolicinaURacunu").val()).toFixed(2);
                        $(".txtUkupnaCenaSaPdv").val(zaZaokruzivanje);
                        break;
                    }
                }
            }
        });

}



//da mi tbody kod kreitanja trenutnog racuna bude empty
$(document).ready(function () {
    $("#tblRacunTrenutniTBody").empty();
});

function zapocniRacun() {
    //da ispise broj racuna iz baze ako prosli nije zavrsen
    $(".zapocniRacun").attr("disabled", true);
    $.ajax({
        type: "GET",
        url: "/tblRacuns/deleteAkoNijeZavrsenRacun",
        data: "{}",
        success: function (data) { //ovo je samo da uzme broj racuna iz baze

            var object;
            var brojRacunaZaDelete;
            var zaBrisanje = false;
            for (var i = 0; i < data.length; i++) {
                if (i == data.length - 1 && data[i].Uknjizeno == 0) {
                    brojRacunaZaDelete = data[i].BrojRacuna;
                    zaBrisanje = true;
                    break;
                }
            }

            for (var i = 0; i < data.length; i++) { //ovo je da obrise zadnji racun koji nije zavrsen
                if (zaBrisanje == true && data[i].BrojRacuna == brojRacunaZaDelete ) {
                    $.ajax({
                        type: "DELETE",
                        url: "/api/Racuns/" + data[i].RacunId,
                        //data: object,
                        success: function () {
                            console.log("obrisano");
                        },
                        error: function () {
                            console.log("greska u brianju");
                        }

                    });
                }

            }
        }
    });
    
    $(".spisakSalonaKodRacuna").attr("disabled", false);
    //$(".dropDownIzborNamestajaURacunu").attr("disabled", false);
    //$(".dodajNoviArtikal").attr("disabled", false);
    //$(".zavrsiKupovinu").attr("disabled", false);
    //$(".stornirajRacun").attr("disabled", false);
    //$(".kolicinaURacunu").attr("disabled", false);
    //$(".kolicinaURacunu").val("1");
    


        $.ajax({
            type: "GET",
            url: "/tblRacuns/getBrojRacunaIzBaze",
            data: "{}",
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (i == data.length-1) {
                        //$(".brojRacunaIzBaze").val(data[i].BrojRacuna);
                        var brojRacuna = data[i].BrojRacuna + 1;
                        $(".brojRacunaIzBaze").val(brojRacuna);
                        //$(".brojRacunaIzBaze").val(data[i].brojRacuna);
                        break;
                    }

                }
                //sada da doda datum i vreme:
                var date;
                date = new Date();

                date = date.getUTCFullYear() + '-' +
                    ('00' + (date.getUTCMonth() + 1)).slice(-2) + '-' +
                    ('00' + date.getUTCDate()).slice(-2) + ' ' +
                    ('00' + date.getUTCHours()).slice(-2) + ':' +
                    ('00' + date.getUTCMinutes()).slice(-2) + ':' +
                    ('00' + date.getUTCSeconds()).slice(-2);
                $(".datumIVreme").val(date);
            }
        });

        $.ajax({
            type: "GET",
            url: "/tblRacuns/getJedinicnaCenaBezPdv",
            data: "{}",
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].KomadNamestajaId == $(".dropDownIzborNamestajaURacunu").val()) {
                        $(".txtCenaZaDobijanjeIzBaze").val(data[i].JedinicnaCena);
                        let zaZaokruzivanje = (data[i].JedinicnaCena * (($(".porez").val() / 100) + 1)*$(".kolicinaURacunu").val()).toFixed(2);
                        $(".txtUkupnaCenaSaPdv").val(zaZaokruzivanje);
                        break;
                    }
                }
            }
        });
}

//da mi ispise broj racuna i datum pri load stranice kod create racuna

function otkljucajPolja() {
    $(".dropDownIzborNamestajaURacunu").attr("disabled", false);
    $(".dodajNoviArtikal").attr("disabled", false);
    $(".zavrsiKupovinu").attr("disabled", false);
    $(".stornirajRacun").attr("disabled", false);
    $(".kolicinaURacunu").attr("disabled", false);
    $(".kolicinaURacunu").val("1");
}





//za storniranje racuna i brisanje svih recorda gde ima zadnji broj racuna
function stornirajRacun() {


    var potvrda = confirm("Da li ste sigurni da zelite da stornirate racun?");
    if (potvrda == true) {
        $(".spisakSalonaKodRacuna").attr("disabled", false);

        //prvo da vrati u magacin kolicinu

        //smanjiBrojKomadaIzMagacina
        var object = [];
        var brojRacuna;



        //sada da vrati nazad broj komada iz magacina jer je storniran racun
        //prvo moram da uradim get svih rekorda koji su u trenutnom racunu pa onda da cal function radim


        $.ajax({
            type: "GET",
            url: "/tblRacuns/getRacunZaAdmina",
            data: "{}",
            async: false,
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].BrojRacuna == $(".brojRacunaIzBaze").val()) {
                        salon = data[i].SalonId;
                        kolicina = data[i].Kolicina;
                        komadNamestaja = data[i].KomadNamestajaId;
                        updateNazadUMagacinKodStorniranja(salon, kolicina, komadNamestaja);
                    }
                }
            }
        });

        //prvo da uzme sve key value pair od racuna u bazi, pa onda da obrise rekorde u bazi u tblRacun
        $.ajax({
            type: "GET",
            url: "/tblRacuns/getBrojRacunaIzBaze",
            data: "{}",
            async: false,
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].BrojRacuna == $(".brojRacunaIzBaze").val()) {
                        object.push({ 'data[i].KomadNamestajaId': data[i].Kolicina });
                        $.ajax({
                            type: "DELETE",
                            url: "/api/Racuns/" + data[i].RacunId,
                            //data: object,
                            async: false,
                            success: function () {
                                //alert("Racun je uspesno storniran");
                                $("#tblRacunTrenutniTBody").empty();
                                //alert("Racun je uspesno storniran");
                                location.replace("/tblRacuns");
                            },
                            error: function () {
                                alert("Nije uspelo storniranje");
                            }
                        });
                    }
                }
                //updateKomadNamestajaIzMagacinaStorniranje(object);
            }
        });
    } else {
        
    }



    
}


function updateNazadUMagacinKodStorniranja(salon, kolicina, komadNamestaja) {
    //prvo moram da dobijem id rekorada za update nazad
    var id;
    var objectZaUpdateKomadaNamestajaUMagacinu;
    $.ajax({
        type: "GET",
        url: "/tblRacuns/smanjiBrojKomadaIzMagacina",
        data: "{}",
        async: false,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].SalonId == salon && data[i].KomadNamestajaId==komadNamestaja) {
                    id = data[i].KomadNamestajaId;
                    objectZaUpdateKomadaNamestajaUMagacinu = {
                        "KomadNamestajaId": data[i].KomadNamestajaId,
                        "Sifra": data[i].Sifra,
                        "Naziv": data[i].Naziv,
                        "ZemljaProizvodnje": data[i].ZemljaProizvodnje,
                        "GodinaProizvodnje": data[i].GodinaProizvodnje,
                        "JedinicnaCena": data[i].JedinicnaCena,
                        "KolicinaUMagacinu": data[i].KolicinaUMagacinu + kolicina,
                        "SalonId": data[i].SalonId,
                        "KategorijaNamestajaId": data[i].KategorijaNamestajaId,
                        "Slika": ""
                    }
                    break;
                }
            }
        }
    });

    //sada da uzme taj komplet objekat za update za storniranje



    $.ajax({
        type: "PUT",
        //on mora da doda tip tblKomadNamestaja ako hocu sa post da updatujem
        url: "../api/KomadNamestajas/" + id, //ne radi put
        data: objectZaUpdateKomadaNamestajaUMagacinu,
        async:false,
        success: function () {
            console.log("Broj komada uspesno azuriran u bazi");
        },
        error: function () {
            console.log("***GRESKA***, Broj komada nije azuriran u magacinu");
        }
    });
}



function updateKomadNamestajaIzMagacina(id, objectZaUpdateKomadaNamestajaUMagacinu) {
    $.ajax({
        type: "PUT", 
        //on mora da doda tip tblKomadNamestaja ako hocu sa post da updatujem
        url: "../api/KomadNamestajas/" + id, //ne radi put
        data: objectZaUpdateKomadaNamestajaUMagacinu,
        success: function () {
            console.log("Broj komada uspesno azuriran u bazi");
        },
        error: function () {
            console.log("***GRESKA***, Broj komada nije azuriran u magacinu");
        }
    });

}

//function updateKomadNamestajaIzMagacinaStorniranje(object) {
//    $.ajax({
//        type: "PUT",
//        //on mora da doda tip tblKomadNamestaja ako hocu sa post da updatujem
//        url: "../api/KomadNamestajas/" + object, //ne radi put
//        data: objectZaUpdateKomadaNamestajaUMagacinu,
//        success: function () {
//            alert("Broj komada je smanjen za jedan u magacinu");
//        },
//        error: function () {
//            alert("GRESKA, Broj komada nije smanjen u magacinu");
//        }
//    });

//}





//da se vrati na listu posle stampanja
function backToList() {
    location.replace("/Izvestaji");
}

function backToListRacun() {
    location.replace("/tblRacuns");
}


//da mi kod izvestaja ispise izabranu kategoriju namestaja
$(document).ready(function () {
   
    $.ajax({
        type: "GET",
        url: "/Izvestaji/ispisiKategorijaNamestajaIzvestajDropdown",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Izaberite kategoriju namestaja</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].KategroijaNamestajaId + '">' + data[i].Naziv + '</option>';
            }
            $("#kategorijaNamestajaIzvestajDropdown").html(s);
        }
    });
});

function ispisiKategorijaNamestajaIzvestajDropdown() { //treba ovo da radim
    //var table = $('#stampajIzvestaj').DataTable();
    
    $("#minUpis").text("");
    $("#maxUpis").text("");
    $("#stampajIzvestaj").removeClass("disabled");
    $("#tabelaIzvestajTBody").empty(); //da pri odabiru bude prazni svi childreni u tbody
    var komadNamestaja = [];
    var komadNamestajaId = [];
            $.ajax({
                type: "GET",
                url: "/Izvestaji/getKomadNamestajaPoKategoriji",
                data: "{}",
                success: function (dataKomadNamestaja) {
                    for (var i = 0; i < dataKomadNamestaja.length; i++) {
                        if (dataKomadNamestaja[i].KategorijaNamestajaId == $("#kategorijaNamestajaIzvestajDropdown").val()) {
                            komadNamestaja.push(dataKomadNamestaja[i].Naziv);
                            komadNamestajaId.push(dataKomadNamestaja[i].KomadNamestajaId);
                        }
                    }
                },
                async: false
            });

            $.ajax({
                type: "GET",
                async: false,
                url: "/Izvestaji/getRacunIzvestaj",
                data: "{}",
                success: function (dataRacun) {
                    //alert(salon[0]);
                    //getImeSalonaZaIzvestajZaAdmina();
                    //alert("drugo ovo treba");
                    var rekord = "";
                    for (var j = 0; j < dataRacun.length; j++) {
                        for (var k = 0; k < komadNamestajaId.length; k++) {
                            if (dataRacun[j].KomadNamestajaId == komadNamestajaId[k]) {
                                
                                var salon = getImeSalonaZaIzvestajZaAdmina(dataRacun[j].SalonId);
                                var kolicina = dataRacun[j].Kolicina;
                                var cenaBezPdv = dataRacun[j].CenaBezPdv;
                                var porez = dataRacun[j].Porez;
                                var ukupnaCenaSaPorezom = dataRacun[j].UkupnaCenaSaPorezom;
                                var datumIVremeKupovine = dataRacun[j].DatumIVremeKupovine;
                                rekord = "<tr><td>" + salon + "</td><td>" + komadNamestaja[k] + "</td><td>" + kolicina + " </td><td>" + cenaBezPdv.toFixed(2) + "</td><td>" + porez + "</td><td class='countable'>" + ukupnaCenaSaPorezom.toFixed(2) + " </td><td>" + datumIVremeKupovine + " </td></tr>";
                                $("#tabelaIzvestajTBody").append(rekord);
                                getSumIzvestajZaAdmina();
                            }
                        }

                    }
                    dataRangeTabele();

                    var oTable = $('#tabelaIzvestaj').dataTable();
                    
                    
                    //oTable.fnReloadAjax();
                    //oTable.fnDestroy();
                    $('#min').change(function () {
                        oTable.fnDraw();
                        getSumIzvestajZaAdmina();
                        
                    });
                    
                    $('#max').change(function () {
                        oTable.fnDraw();
                        getSumIzvestajZaAdmina();


                    });
                    //oTable.fnDestroy();
                    
                }
                
            });
        }
function getImeSalonaZaIzvestajZaAdmina(id) {
  naziv=""
    $.ajax({
        type: "GET",
        url: "/tblSalons/getSalons",
        data: "{}",
        async: false,
        success: function (dataSalon) {
            for (var i = 0; i < dataSalon.length; i++) {
                if (dataSalon[i].SalonId == id) {
                    naziv = dataSalon[i].Naziv;
                    break;
                }
            }
        },
        
    });
    return naziv;
}

function getSumIzvestajZaAdmina() {
    var cls = document.getElementById("tabelaIzvestaj").getElementsByTagName("td");
    var sum = 0;
    for (var i = 0; i < cls.length; i++) {
        if (cls[i].className == "countable") {
            sum += isNaN(cls[i].innerHTML) ? 0 : parseFloat(cls[i].innerHTML);
        }
    }
    //alert('sum is ' + sum);
    $("#sumIzvestajZaAdmina").val(sum.toFixed(2) + " RSD");
}




function dataRangeTabele() {
    $.fn.dataTableExt.afnFiltering.push(
        function (oSettings, aData, iDataIndex) {
            var iMin = document.getElementById('min').value;
            var iMax = document.getElementById('max').value;
            var iVersion = aData[6] == "-" ? 0 : aData[6];
            if (iMin == "" && iMax == "") {
                return true;
            }
            else if (iMin == "" && iVersion < iMax) {
                return true;
            }
            else if (iMin < iVersion && "" == iMax) {
                return true;
            }
            else if (iMin < iVersion && iVersion < iMax) {
                return true;
            }
            return false;
        }
    );
}

function updateSume() {
    table = $('#tabelaIzvestaj').DataTable({
        searching: false,
        paging: false,
        info: true,
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                    i : 0;
            };

            // Total over all pages
            total = api
                .column(5)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            total1 = api
                .column(2)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(5).footer()).html(
                'Ukupno zaradjeno sa pdv:' + '<br>' + total.toFixed(2) + ' RSD'
            );
            $(api.column(2).footer()).html(
                'Ukupno prodata komada namestaja:' + '<br>' + total1
            );
        }
    });
    table.destroy();
}


$(document).ready(function () {
        $.ajax({
            type: "GET",
            url: "/tblRacuns/getRacunZaAdmina",
            data: "{}",
            async: false,
            success: function (data) {
                var s = '<option value="-1">Izaberite racun</option>';
                for (var i = 0; i < data.length; i++) {
                    if (i>0) {
                        if (data[i].BrojRacuna == data[i-1].BrojRacuna) {
                            continue;
                        }
                    }
                    s += '<option value="' + data[i].BrojRacuna + '">' + data[i].BrojRacuna + '</option>';
                }
                $("#racunDropdownAdmin").html(s);
            }
        });
});


function uzmiBrojRacunaZaAdmina() {
    ispisiIzabraniRacunAdminDropdown($("#racunDropdownAdmin").val());
}

function ispisiIzabraniRacunAdminDropdown(id) {
    var rekord = "";
    var kupac = "";
    //var salon = "";
    var datum = "";
    
    $("#tabelaIspisRacunaPosleIzboraBrojaRacunaTBody").empty();
    $.ajax({
        type: "GET",
        url: "/tblRacuns/getRacunZaAdmina",
        data: "{}",
        async: false,
        success: function (data) {

            
            for (var i = 0; i < data.length; i++) {
                if (data[i].BrojRacuna == id) {

                    //prvo da umzem ime komada namestaja od id
                    var komadNamestaja = getImeKomadaNamestajaZaRacunZaAdmina(data[i].KomadNamestajaId);



                    rekord = "<tr><td>" + komadNamestaja + " </td><td>" + data[i].Kolicina + "</td><td>" + data[i].CenaBezPdv.toFixed(2) + "</td><td>" + data[i].Porez + " </td><td>" + data[i].UkupnaCenaSaPorezom.toFixed(2) + " </td></tr>";
                    $("#tabelaIspisRacunaPosleIzboraBrojaRacunaTBody").append(rekord);
                    getImeSalonaZaRacunZaAdmina(data[i].SalonId)
                    $(".kupacZaRacunZaAdmina").text(data[i].KupacKojiJeKupioRobu);
                    $(".datumZaRacunZaAdmina").text(data[i].DatumIVremeKupovine);
                    table = $('#tabelaIspisRacunaPosleIzboraBrojaRacuna').DataTable({
                        searching: false,
                        paging: false,
                        info: true,
                        "footerCallback": function (row, data, start, end, display) {
                            var api = this.api(), data;

                            // Remove the formatting to get integer data for summation
                            var intVal = function (i) {
                                return typeof i === 'string' ?
                                    i.replace(/[\$,]/g, '') * 1 :
                                    typeof i === 'number' ?
                                    i : 0;
                            };

                            // Total over all pages
                            total = api
                                .column(4)
                                .data()
                                .reduce(function (a, b) {
                                    return intVal(a) + intVal(b);
                                }, 0);

                            // Update footer
                            $(api.column(4).footer()).html(
                                'Ukupno sa pdv:' + '<br>' + total.toFixed(2) + ' RSD'
                            );
                        }
                    });
                    table.destroy();
                }
            }
         
            ;
            
            
        }
    });
    
}


function getImeKomadaNamestajaZaRacunZaAdmina(id) {
    naziv = ""
    $.ajax({
        type: "GET",
        url: "/tblSalons/getNamestajPosleIzboraSalona",
        data: "{}",
        async: false,
        success: function (dataKomadNamestaja) {
            for (var i = 0; i < dataKomadNamestaja.length; i++) {
                if (dataKomadNamestaja[i].KomadNamestajaId == id) {
                    naziv = dataKomadNamestaja[i].Naziv;
                    break;
                }
            }
        },

    });
    return naziv;
}




function getImeSalonaZaRacunZaAdmina(id) {
    $.ajax({
        type: "GET",
        url: "/tblSalons/getSalons",
        data: "{}",
        async: false,
        success: function (data) {
              for (var i = 0; i < data.length; i++) {
                  if (data[i].SalonId == id) {
                      $(".salonZaRacunZaAdmina").text(data[i].Naziv);
                      break;

                }
            }
        }
    });
}