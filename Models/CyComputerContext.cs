using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CyComputer.Models;

public partial class CyComputerContext : DbContext
{
    public CyComputerContext()
    {
    }

    public CyComputerContext(DbContextOptions<CyComputerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Almacen> Almacens { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetalleCarrito> DetalleCarritos { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleEntradum> DetalleEntrada { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<DetalleSalidum> DetalleSalida { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<GuiaEntradum> GuiaEntrada { get; set; }

    public virtual DbSet<GuiaSalidum> GuiaSalida { get; set; }

    public virtual DbSet<MovimientosAlmacen> MovimientosAlmacens { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Persist Security Info=False;Integrated Security=true; Initial Catalog= CyComputer; Server=DESKTOP-P3DJB58\\SQLEXPRESS;Encrypt=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Almacen>(entity =>
        {
            entity.HasKey(e => e.IdAlmacen).HasName("PK__Almacen__EBC82D6D2BB2A2C2");

            entity.ToTable("Almacen");

            entity.Property(e => e.IdAlmacen).HasColumnName("Id_almacen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ubicacion");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PK__Carrito__83A2AD9CEA36AC02");

            entity.ToTable("Carrito");

            entity.Property(e => e.IdCarrito).HasColumnName("id_carrito");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Carrito__id_clie__5535A963");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__4A033A939ACA2C33");

            entity.Property(e => e.IdCategoria).HasColumnName("Id_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__FCE03992FD1C28DA");

            entity.HasIndex(e => e.Correo, "UQ__Clientes__2A586E0B53B5C3F8").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("Id_cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compras__C4BAA604FBB67EE5");

            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Compras__id_prov__656C112C");
        });

        modelBuilder.Entity<DetalleCarrito>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCarrito).HasName("PK__DetalleC__23531572EC409EF9");

            entity.ToTable("DetalleCarrito");

            entity.Property(e => e.IdDetalleCarrito).HasColumnName("id_detalleCarrito");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdCarrito).HasColumnName("id_carrito");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.DetalleCarritos)
                .HasForeignKey(d => d.IdCarrito)
                .HasConstraintName("FK__DetalleCa__id_ca__5812160E");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCarritos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleCa__id_pr__59063A47");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("PK__DetalleC__DBCC3B51906C189F");

            entity.ToTable("DetalleCompra");

            entity.Property(e => e.IdDetalleCompra).HasColumnName("id_detalleCompra");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.CostoUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("costo_unitario");
            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK__DetalleCo__id_co__68487DD7");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleCo__id_pr__693CA210");
        });

        modelBuilder.Entity<DetalleEntradum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleEntrada).HasName("PK__DetalleE__2958C2F5EDB6E8CB");

            entity.Property(e => e.IdDetalleEntrada).HasColumnName("id_detalleEntrada");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdGuiaEntrada).HasColumnName("id_guiaEntrada");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdGuiaEntradaNavigation).WithMany(p => p.DetalleEntrada)
                .HasForeignKey(d => d.IdGuiaEntrada)
                .HasConstraintName("FK__DetalleEn__id_gu__7E37BEF6");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleEntrada)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleEn__id_pr__7F2BE32F");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.IdDetallePedido).HasName("PK__DetalleP__3CFD0D21565E4C76");

            entity.ToTable("DetallePedido");

            entity.Property(e => e.IdDetallePedido).HasColumnName("id_detallePedido");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__DetallePe__id_pe__60A75C0F");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetallePe__id_pr__619B8048");
        });

        modelBuilder.Entity<DetalleSalidum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleSalida).HasName("PK__DetalleS__5D145AE11B3CAD51");

            entity.Property(e => e.IdDetalleSalida).HasColumnName("id_detalleSalida");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdGuiaSalida).HasColumnName("id_guiaSalida");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdGuiaSalidaNavigation).WithMany(p => p.DetalleSalida)
                .HasForeignKey(d => d.IdGuiaSalida)
                .HasConstraintName("FK__DetalleSa__id_gu__75A278F5");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleSalida)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleSa__id_pr__76969D2E");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__01AC2829F8BEDF9F");

            entity.ToTable("Empleado");

            entity.HasIndex(e => e.Email, "UQ__Empleado__AB6E6164F751B96F").IsUnique();

            entity.Property(e => e.IdEmpleado).HasColumnName("Id_empleado");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Empleado__Id_usu__3F466844");
        });

        modelBuilder.Entity<GuiaEntradum>(entity =>
        {
            entity.HasKey(e => e.IdGuiaEntrada).HasName("PK__guia_Ent__FC3A1F60E5C8AAAF");

            entity.ToTable("guia_Entrada");

            entity.Property(e => e.IdGuiaEntrada).HasColumnName("id_guiaEntrada");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdAlmacen).HasColumnName("id_almacen");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

            entity.HasOne(d => d.IdAlmacenNavigation).WithMany(p => p.GuiaEntrada)
                .HasForeignKey(d => d.IdAlmacen)
                .HasConstraintName("FK__guia_Entr__id_al__7B5B524B");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.GuiaEntrada)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__guia_Entr__id_pr__7A672E12");
        });

        modelBuilder.Entity<GuiaSalidum>(entity =>
        {
            entity.HasKey(e => e.IdGuiaSalida).HasName("PK__guia_Sal__11E8E0955C2CF2A6");

            entity.ToTable("guia_Salida");

            entity.Property(e => e.IdGuiaSalida).HasColumnName("id_guiaSalida");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdAlmacen).HasColumnName("id_almacen");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

            entity.HasOne(d => d.IdAlmacenNavigation).WithMany(p => p.GuiaSalida)
                .HasForeignKey(d => d.IdAlmacen)
                .HasConstraintName("FK__guia_Sali__id_al__71D1E811");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.GuiaSalida)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__guia_Sali__id_cl__72C60C4A");
        });

        modelBuilder.Entity<MovimientosAlmacen>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__2A071C24FD76E666");

            entity.ToTable("MovimientosAlmacen");

            entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdAlmacen).HasColumnName("id_almacen");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdAlmacenNavigation).WithMany(p => p.MovimientosAlmacens)
                .HasForeignKey(d => d.IdAlmacen)
                .HasConstraintName("FK__Movimient__id_al__6D0D32F4");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.MovimientosAlmacens)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Movimient__id_pr__6E01572D");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedidos__6FF01489C2536FB3");

            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Pedidos__id_clie__5CD6CB2B");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Pedidos__id_empl__5DCAEF64");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__1D8EFF0152BC3E5B");

            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdCategoria).HasColumnName("Id_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Productos__Id_ca__46E78A0C");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__6704E5A85331327A");

            entity.HasIndex(e => e.Correo, "UQ__Proveedo__2A586E0B66025015").IsUnique();

            entity.Property(e => e.IdProveedor).HasColumnName("Id_proveedor");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__EF59F76258FC8F31");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
