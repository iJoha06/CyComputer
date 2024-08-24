$(document).ready(function () {
  var listaProductos = [
      { id: 1, nombre: "Baldosas", precio: 10.0, imagen: "img/p2.png" },
      { id: 2, nombre: "Alambres", precio: 35.0, imagen: "img/p1.png" },
      { id: 3, nombre: "Madera", precio: 44.0, imagen: "img/p3.png" },
      { id: 4, nombre: "Tubos", precio: 110.0, imagen: "img/p4.png" },
      { id: 5, nombre: "Drywall", precio: 110.0, imagen: "img/p5.png" },
      { id: 6, nombre: "Varillas", precio: 110.0, imagen: "img/p6.png" },
      { id: 7, nombre: "Ladrillos", precio: 110.0, imagen: "img/p8.png" },
      { id: 8, nombre: "Varillas", precio: 110.0, imagen: "img/p7.png" }
  ];

  cargarProductos();

  $("#icono-carrito").click(function () {
      mostrarCarritoModal();
  });

  $(".cerrar-modal").click(function () {
      cerrarModal("#modal-carrito");
  });

  $("#cerrar-mensaje").click(function () {
      cerrarModal('#modal-pago');
  });

  $('#vaciar-carrito').click(function(){
      vaciarCarrito();
  });

  $('#pagar').click(function(){
    if (carrito.length === 0) {
      mostrarMensaje("Debe agregar un producto para pagar.");
  } else {
      procesarPago();
  }
});

  $("#enlace-carrito").click(function (event) {
    event.preventDefault();
    mostrarCarritoModal();
  });

  $("#lista-carrito").on("click", ".aumentar-cantidad", function () {
      var idProducto = $(this).data("id");
      aumentarCantidad(idProducto);
  });

  $("#lista-carrito").on("click", ".disminuir-cantidad", function () {
      var idProducto = $(this).data("id");
      disminuirCantidad(idProducto);
  });

  $("#lista-carrito").on("click", ".eliminar-producto", function () {
    var idProducto = $(this).data("id");
    eliminarProducto(idProducto);
});  

  $("#search-btn").click(function(event) {
    event.preventDefault();
    buscarProducto();
  });

  $("#enlace-ver-carrito").click(function(event) {
    event.preventDefault();
    mostrarCarritoModal();
  });

  function cargarProductos() {
      mostrarProductos(listaProductos);
  }

  function mostrarProductos(productos) {
      var listaProductosHTML = "";
      productos.forEach(function (producto) {
          listaProductosHTML += `
              <li class="producto">
                  <img src="${producto.imagen}" alt="${producto.nombre}">
                  <h3>${producto.nombre}</h3>
                  <p>Precio: $${producto.precio.toFixed(2)}</p>
                  <button class="agregar-carrito" data-id="${producto.id}">Agregar al carrito</button>
              </li>
          `;
      });
      $("#lista-productos").html(listaProductosHTML);

      $('.agregar-carrito').click(function () {
          var idProducto = $(this).data('id');
          var productoSeleccionado = listaProductos.find(function (producto) {
              return producto.id === idProducto;
          });

          agregarProductoCarrito(productoSeleccionado);
      });
  }

  var carrito = [];

  function agregarProductoCarrito(producto) {
      var productoExistente = carrito.find(function (item) {
          return item.id === producto.id;
      });
      if (productoExistente) {
          productoExistente.cantidad++;
      } else {
          productoExistente = {
              id: producto.id,
              nombre: producto.nombre,
              precio: producto.precio,
              cantidad: 1,
              imagen: producto.imagen,
          };
          carrito.push(productoExistente);
      }
      actualizarCarritoModal();
      mostrarMensajeAgregado();
  }

  function mostrarMensajeAgregado() {
    var mensaje = $("#mensaje-agregado");
    mensaje.fadeIn();

    setTimeout(function () {
      mensaje.fadeOut();
    }, 2000);     
  }

  function actualizarCarritoModal() {
      var listaCarrito = $("#lista-carrito");
      listaCarrito.empty();

      carrito.forEach(function (item) {
          var ProductoHTML = `
              <div class="producto-carrito">
                  <img src="${item.imagen}" alt="Imagen del producto">
                  <div class="producto-info">
                      <h3>${item.nombre}</h3>
                      <p>Precio unitario: $${item.precio.toFixed(2)}</p>
                      <p>Total: $${(item.precio * item.cantidad).toFixed(2)}</p>
                  </div>
                  <div class="producto-acciones">
                      <div class="cantidad">
                          <button class="disminuir-cantidad" data-id="${item.id}">-</button>
                          <button class="aumentar-cantidad" data-id="${item.id}">+</button>
                      </div>        
                      <button class="eliminar-producto" data-id="${item.id}">Eliminar</button>
                  </div>
              </div>
          `;
          listaCarrito.append(ProductoHTML);
      });

      var total = carrito.reduce(function (acc, item) {
          return acc + item.precio * item.cantidad;
      }, 0);
      $("#total").text(total.toFixed(2));
      $("#cantidad-carrito").text(carrito.length);
  }

  function aumentarCantidad(idProducto) {
      var producto = carrito.find(function (item) {
          return item.id === idProducto;
      });
      if (producto) {
          producto.cantidad++;
          actualizarCarritoModal();
      }
  }

  function disminuirCantidad(idProducto) {
      var producto = carrito.find(function (item) {
          return item.id === idProducto;
      });
      if (producto && producto.cantidad > 1) {
          producto.cantidad--;
          actualizarCarritoModal();
      }
  }

  function eliminarProducto(idProducto) {
    carrito = carrito.filter(function (item) {
        return item.id !== idProducto;
    });
    actualizarCarritoModal();
  }
  
  function vaciarCarrito() {
      carrito = [];
      actualizarCarritoModal();
  }

  function procesarPago() {
    if (carrito.length === 0) {
        mostrarMensaje("Debe agregar un producto para pagar.");
    } else {
        mostrarModal('#modal-pago');
        vaciarCarrito();
    }
  }

  function mostrarCarritoModal() {
      actualizarCarritoModal();
      mostrarModal("#modal-carrito");
  }

  function mostrarMensaje(mensaje) {
    $("#mensaje").text(mensaje);
    mostrarModal('#modal-mensaje');
  }

  function mostrarModal(idModal) {
      $(idModal).fadeIn();
  }

  function cerrarModal(idModal) {
      $(idModal).fadeOut();
  }

  function mostrarMensajeResultados() {
    var mensaje = $("#mensaje-resultados");
    mensaje.fadeIn();

    setTimeout(function () {
        mensaje.fadeOut();
    }, 2000);
}  

  function buscarProducto() {
    var nombreProducto = $('#inputBuscar').val().trim();
    if (nombreProducto === '') {
        alert('Por favor, ingrese el nombre del producto');
        return;
    }

    var resultados = listaProductos.filter(function (producto) {
        return producto.nombre.toLowerCase().includes(nombreProducto.toLowerCase());
    });
    mostrarProductos(resultados);
    mostrarMensajeResultados()
  }
});
