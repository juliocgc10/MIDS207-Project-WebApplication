MIDS207_Project = window.MIDS207_Project || {};

MIDS207_Project.Student = (function ($, window, document, navigator, localStorage, sessionStorage, undefined) {

    var defaults = null;
    var iLoading = 0;


    const swalBootstrap = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    var npMainFunction = function (args) {

        defaults = args;

        Initialize_Application(defaults);
        Initialization_Events(defaults);

    }

    function LoadingShow(psNameForm) {

        if (iLoading < 1) {
            $('#' + psNameForm).showLoader();
        }

        iLoading = iLoading + 1;
    }

    function LoadingHider(psNameForm) {
        iLoading = iLoading - 1;

        if (iLoading == 0) {
            $('#' + psNameForm).hideLoader();
        }

        if (iLoading < 0)
            iLoading = 0;
    }

    function Initialize_Application(defaults) {
        validateStudent();
        $("#hdnStudentId").val('');
        loadStudents();
    }

    function Initialization_Events(defaults) {

        try {
            $("#btnSearchStudents").click(function () {
                loadStudents();
            });
        } catch (e) {
            alert("Element:btnNumberAreas Event:click \nException: " + e.message);
        }

        try {
            $('#studentNav').click(function () {
                $("#hdnStudentId").val(null);
                $("#studentForm").trigger("reset");
                $(".fileDownload").hide();
                $("#titleStudent").text("Agregar alumno")

            });
        } catch (e) {
            alert("Element:btnNumberAreasEmployee Event:click \nException: " + e.message);
        }

        try {
            $("#btnSaveStudent").click(function () {
                save();
            });
        } catch (e) {
            alert("Element:btnSaveStudent Event:click \nException: " + e.message);
        }






    }

    //#region Functions Initialization_Events

    function loadStudents() {
        try {
            LoadingShow('searchForm');
            var settings = {
                url: defaults.UrlWepApi + "api/Student/GetSearch?searchStudent=" + $('#input-name-search').val(),
                async: true,
                type: 'GET',
                dataType: 'json'
            };

            $.ajax(settings).done(function (response) {

                if (response.IsSuccess) {
                    $('#tableStudents').DataTable({
                        data: response.Data,
                        dom: 'Bfrtip',
                        bAutoWidth: false,
                        responsive: true,
                        paging: true,
                        searching: false,
                        destroy: true,
                        columnDefs: [
                            {
                                "className": "text-center",
                                "targets": "_all"
                            }],
                        columns:
                            [
                                {
                                    "data": "FirstName", "render": function (data, type, row) {
                                        return data + ' ' + (row.MiddleName != null ? row.MiddleName : '');
                                    }
                                }, {
                                    "data": "LastName"
                                }, {
                                    "data": "BirthDate", "render": function (data, type, row) {
                                        return new Date(data).toLocaleDateString("es-MX");
                                    }
                                }, {
                                    "data": "Gender"
                                }, {
                                    "data": "IsActive", "render": function (data, type, row) {
                                        return data ? '<span class="label label-success">Activo</span >' : '<span class="label label-danger">Inactivo</span >';
                                    }
                                }, {
                                    "data": "Email"
                                }, {
                                    "data": "PhoneNumber"
                                }, {
                                    "data": "RFC", "render": function (data, type, row) {
                                        return data != null ? data : "";
                                    }
                                }, {
                                    "data": "CURP", "render": function (data, type, row) {
                                        return data != null ? data : "";
                                    }
                                }, {
                                    "data": "State"
                                }, {
                                    "data": "PostalCode"
                                }, {
                                    "data": "StudentID", "render": function (data, type, row) {
                                        var filesHtml = '';
                                        $.each(row.Files, function (index, value) {
                                            filesHtml += '<a href = "' + defaults.UrlWepApi + "Files/" + data + "/" + value.FileName + '" title="' + value.FileName + '" target = "_blank" type = "button" class="btn btn-default" > <i class="glyphicon glyphicon-eye-open" aria-hidden="true"></i></a>&nbsp;';
                                        });

                                        return filesHtml;
                                    }
                                }, {
                                    "data": "StudentID", "render": function (data, type, row) {
                                        return '<button type="button" class="btn btn-danger btn-sm" title="Eliminar" onclick=MIDS207_Project.Student.inactiveStudent(' + data + ')><span class="glyphicon glyphicon-trash"></span></button> ' +
                                            '<button type="button" class="btn btn-info btn-sm" title="Editar" onclick=MIDS207_Project.Student.edit(' + data + ')><span class="glyphicon glyphicon-edit"></span></button> ';
                                    }
                                }

                            ]
                    });

                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: response.Message
                    });

                }

                LoadingHider('searchForm');
            }).fail(function (jqXHR, textStatus, err) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...' + err,
                    text: textStatus
                });
                LoadingHider('searchForm');
            });

        } catch (e) {
            alert("Method: loadStudents \nException: " + e.message);
        }
    }

    function inactiveStudent(id) {
        try {

            swalBootstrap.fire({
                title: '¿Está usted seguro que desea Eliminar (Inactivar) el usuario?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí',
                cancelButtonText: 'No',

            }).then((result) => {
                if (result.isConfirmed) {

                    LoadingShow('searchForm');

                    var settings = {
                        url: defaults.UrlWepApi + "api/Student/InactiveStudent?studentId=" + id,
                        async: true,
                        type: 'GET',
                        dataType: 'json'
                    };

                    $.ajax(settings).done(function (response) {
                        if (response.IsSuccess) {

                            Swal.fire({
                                title: '¡Se ha inactivado correctamente el alumno',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500
                            });
                            loadStudents();
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: response.Message
                            });

                        }
                        LoadingHider('searchForm');
                    }).fail(function (jqXHR, textStatus, err) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...' + err,
                            text: textStatus
                        });
                        LoadingHider('searchForm');
                    });

                }
            })




        } catch (e) {
            alert("Method: update \nException: " + e.message);
        }
    }

    function edit(id) {
        try {
            LoadingShow('searchForm');

            var settings = {
                url: defaults.UrlWepApi + "api/Student/GetStudentById?studentId=" + id,
                async: true,
                type: 'GET',
                dataType: 'json'
            };

            $.ajax(settings).done(function (response) {

                console.log(response);

                if (response.IsSuccess) {
                    var student = response.Data;
                    $('.nav-tabs a[href="#studentTab"]').tab('show');
                    $("#hdnStudentId").val(student.StudentID);


                    $('#inputFirstName').val(student.FirstName);
                    $('#inputMiddleName').val(student.MiddleName);
                    $('#inputLastName').val(student.LastName);

                    document.getElementById("inputBirthDate").valueAsDate = new Date(student.BirthDate);
                    $('#inputRFC').val(student.RFC);
                    $('#inputCURP').val(student.CURP);
                    $('#inputState').val(student.State);
                    $('#inputPostalCode').val(student.PostalCode);
                    $('#inputGender').val(student.Gender);
                    $('#inputPhoneNumber').val(student.PhoneNumber);
                    $('#inputEmail').val(student.Email);
                    $('#inputPassword').val(student.Password);
                    $('#inputRepeatPassword').val(student.Password);

                    $("#checkActive").prop('checked', student.IsActive);


                    $(".fileDownload").show();
                    $("#titleStudent").text("Editar alumno")

                    $("input[type=file]").val('');

                    $.each(student.Files, function (index, value) {
                        $('#fileDownload' + (index + 1)).attr('title', value.FileName);
                        /*$('#fileDownload' + (index + 1)).attr('href', defaults.UrlWepApi + "Files/" + student.StudentID + "/" + value.FileName);*/
                        $("#myModalLabel").text(value.FileName);
                        $('#fileDownload' + (index + 1)).click(function () {
                            $("#frameFile").attr('src', defaults.UrlWepApi + "Files/" + student.StudentID + "/" + value.FileName);
                        });
                    });



                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: response.Message
                    });

                }

                LoadingHider('searchForm');
            }).fail(function (jqXHR, textStatus, err) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...' + err,
                    text: textStatus
                });
                LoadingHider('searchForm');
            });



        } catch (e) {
            alert("Method: update \nException: " + e.message);
        }
    }

    function save() {
        if ($("#studentForm").valid()) {
            var student = {
                StudentID: $("#hdnStudentId").val() != '' ? $("#hdnStudentId").val() : 0,
                FirstName: $('#inputFirstName').val(),
                MiddleName: $('#inputMiddleName').val(),
                LastName: $('#inputLastName').val(),
                RFC: $('#inputRFC').val(),
                CURP: $('#inputCURP').val(),
                BirthDate: $('#inputBirthDate').val(),
                State: $('#inputState').val(),
                PostalCode: $('#inputPostalCode').val(),
                Gender: $('#inputGender').val(),
                PhoneNumber: $('#inputPhoneNumber').val(),
                Email: $('#inputEmail').val(),
                Password: $('#inputPassword').val(),
                IsActive: $("#checkActive").prop('checked'),
                Files: [
                    { FileDataURI: $("#inputFile1").attr("data-fileDataURI"), FileName: $("#inputFile1").attr("data-fileName") },
                    { FileDataURI: $("#inputFile2").attr("data-fileDataURI"), FileName: $("#inputFile2").attr("data-fileName") },
                    { FileDataURI: $("#inputFile3").attr("data-fileDataURI"), FileName: $("#inputFile3").attr("data-fileName") },
                ]
            };

            var settings = {
                url: defaults.UrlWepApi + "api/Student/CreateUpdateStudent",
                async: true,
                data: JSON.stringify(student),
                type: 'POST',
                contentType: "application/json",
                dataType: 'json'
            };

            LoadingShow('studentForm');

            $.ajax(settings).done(function (response) {
                LoadingHider('studentForm');

                if (response.IsSuccess) {
                    $('.nav-tabs a[href="#searchTab"]').tab('show');
                    $("#hdnStudentId").val(student.StudentID);
                    loadStudents();

                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: response.Message
                    });

                }

            }).fail(function (jqXHR, textStatus, err) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...' + err,
                    text: textStatus
                });
                LoadingHider('studentForm');
            });
        }
    }

    function validateStudent() {
        $("#studentForm").validate({
            rules: {
                MiddleName: {                    
                    maxlength: 50
                },
                FirstName: {
                    required: true,
                    maxlength: 50
                },
                LastName: {
                    required: true,
                    maxlength: 50
                },
                PostalCode: {                    
                    maxlength: 5
                },
                State: {
                    required: true
                },
                Gender: {
                    required: true
                },                
                Password: {
                    required: true
                },
                RepeatPassword: {
                    required: true,
                    equalTo: '#inputPassword'
                }
            }
        });
    }

    $.extend($.validator.messages, {
        required: "Este campo es obligatorio.",
        remote: "Por favor, rellena este campo.",
        email: "Por favor, escribe una dirección de correo válida.",
        url: "Por favor, escribe una URL válida.",
        date: "Por favor, escribe una fecha válida.",
        dateISO: "Por favor, escribe una fecha (ISO) válida.",
        number: "Por favor, escribe un número válido.",
        digits: "Por favor, escribe sólo dígitos.",
        creditcard: "Por favor, escribe un número de tarjeta válido.",
        equalTo: "Por favor, escribe el mismo valor de nuevo.",
        extension: "Por favor, escribe un valor con una extensión aceptada.",
        maxlength: $.validator.format("Por favor, no escribas más de {0} caracteres."),
        minlength: $.validator.format("Por favor, no escribas menos de {0} caracteres."),
        rangelength: $.validator.format("Por favor, escribe un valor entre {0} y {1} caracteres."),
        range: $.validator.format("Por favor, escribe un valor entre {0} y {1}."),
        max: $.validator.format("Por favor, escribe un valor menor o igual a {0}."),
        min: $.validator.format("Por favor, escribe un valor mayor o igual a {0}."),
        nifES: "Por favor, escribe un NIF válido.",
        nieES: "Por favor, escribe un NIE válido.",
        cifES: "Por favor, escribe un CIF válido."
    });


    $("input[type=file]").change(function () {

        var $this = $(this);
        var file = this.files[0];
        if (file != null) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $this.attr("data-fileDataURI", e.target.result);
                $this.attr("data-fileName", file.name);
            };

            reader.onerror = function (e) {
                alert("Ocurrio un error en la lectura:" + event.type);
            };

            reader.readAsDataURL(file);
        }
        else {
            $this.attr("data-fileDataURI", null);
            $this.attr("data-fileName", null);
        }

    });
    //#endregion



    return {
        Config: defaults,
        npMainFunction: npMainFunction,
        inactiveStudent: inactiveStudent,
        edit: edit
    }
}(jQuery, window, document, navigator, localStorage, sessionStorage, undefined));



