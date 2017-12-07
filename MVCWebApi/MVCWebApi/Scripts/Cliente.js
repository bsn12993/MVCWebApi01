$(document).ready(function () {
    //Recuperar todas los clientes
    function recuperarClientes()
    {
        $.getJSON("/api/clientes/",
            function (data) {
                $("#clientes").empty();
                $.each(data, function (key, val) {
                    var str = '(' + val.Id + ')' + val.Nombre;
                    $("<li/>", {html: str}).appendTo($("#clientes"));
                });
            });
    }

    recuperarClientes();

    function buscarCliente(id, callback) {
        $.ajax({
            url: "/api/clientes",
            data: { id: id },
            type: "GET",
            contentType: "application/json",
            statusCode: {
                200: function (cliente) {
                    callback(cliente);
                },
                404: function () {
                    alert("Cliente no encontrado");
                }
            }
        });
    }

    $("#buscarCliente").click(function () {
        var id = $("#IdCliente").val();
        buscarCliente(id, function (cliente) {
            var str = '(' + cliente.Id + ')' + cliente.Nombre;
            $("#clientes").html(str);
        })
    });

    function crearCliente(nuevoCliente, callback) {
        $.ajax({
            url: "/api/clientes",
            data: JSON.stringify(nuevoCliente),
            type: "POST",
            contentType: "application/json",
            statusCode: {
                201: function (cliente) {
                    callback(cliente);
                },
            }
        });
    }

    $("#crearCliente").click(function () {
        var nuevoCliente = {
            Nombre: $("#NombreCliente").val(),
            User: $("#Usuario").val()
        };

        crearCliente(nuevoCliente, function (cliente) {
            console.log(cliente);
            recuperarClientes();
            alert("El nuevo cliente ha sido creado con el Id " + cliente.Id);
        });
    });

    function actualizarCliente(cliente, callback) {
        $.ajax({
            url: "/api/clientes",
            data: JSON.stringify(cliente),
            type: "PUT",
            contentType: "application/json",
            statusCode: {
                200: function () {
                    callback();
                },
                404: function () {
                    alert("Cliente no encontrado");
                }
            }
        });
    }

    $("#actualizarCliente").click(function () {
        var cliente = {
            Id: $("#Id").val(),
            Nombre: $("#Nombre").val(),
            User: $("#User").val()
        };

        actualizarCliente(cliente, function () {
            recuperarClientes();
            alert("El cliente se ha actualizado.");
        });
    });

    function eliminarCliente(id, callback) {
        $.ajax({
            url: "/api/clientes/"+id,
            data: JSON.stringify({  }),
            type: "DELETE",
            contentType: "application/json",
            statusCode: {
                204: function () {
                    callback();
                },
                404: function () {
                    alert("Cliente no encontrado");
                }
            }
        });
    }

    $("#eliminarCliente").click(function () {
        var id = $("#IdClienteEliminar").val();
        eliminarCliente(id, function () {
            recuperarClientes();
            alert("El cliente se ha eliminado");
        });
    })

});