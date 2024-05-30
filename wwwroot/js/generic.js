function mostrarModal(titulo = "Desea guardar los cambios?",
    texto = "Deseas registrar los cambios en la base de datos") {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si!'

    })
}

function correcto(title = "Se elimino correctamente") {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: title,
        showConfirmButton: false,
        timer: 1500
    })
}

function error(title = "Ocurrio un error") {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: title
    })
}

function pintar(url, campos, propiedadId, nombreController, popup = false, opciones = true, id = "tbDatos", idTable = "table", propiedadMostrar = "") {
    
    var contenido = "";
    if (id == null || id == undefined || id == "") {
        var tbody = document.getElementById("tbDatos");
    } else {
        var tbody = document.getElementById(id);
    }

    var nombreCampo;
    var objetoActual;

    $.get(url, function (data) {
        for (var i = 0; i < data.length; i++) {
            contenido += "<tr>";
            
            for (var j = 0; j < campos.length; j++) {
                nombreCampo = campos[j];
                objetoActual = data[i];
                contenido += "<td>" + objetoActual[nombreCampo] + "</td>"
            }
            if (opciones == true) {
                contenido += `
                                <td> `
                if (popup == false) {
                    contenido += `
                                        <a    
                                            href="${nombreController}/Editar/${objetoActual[propiedadId]}">                                           
                                            <i class="bi bi-pencil-square"></i>
                                        </a> 
                                                                        
                    `
                } else {
                    contenido += `
                                 <i class="bi bi-pencil-square btn btn-primary cursor-pointer" aria-hidden="true"
                                 data-toggle="modal" data-target="#exampleModal"
                                 onclick="Abrir(${objetoActual[propiedadId]})">Editar</i>
                            `;
                }
                contenido += `</td>`;
            } else {
                contenido += `
                        <td>                   
                                <i class="bi bi-check btn btn-success cursor-pointer" aria-hidden="true" 
                                    onclick="AsignarNombre(${objetoActual[propiedadId]},'${objetoActual[propiedadMostrar]}')">
                                </i>
                        </td>`;
            }
            contenido += "</tr>";
        }
        tbody.innerHTML = contenido;
    })
}

function LimpiarDatos() {
    var controles = document.getElementsByClassName("form-control");
    var ncontroles = controles.length;
    for (var i = 0; i < ncontroles; i++) {
        controles[i].value = "";
    }
}

function obj(id, valor) {
    document.getElementById(id).value = valor;
}
