﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace AppCatalogo
{
    public partial class frmArticulo : Form
    {
        private Articulo articulo = null;

        public frmArticulo()
        {
            InitializeComponent();
        }

        public frmArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar artículo";
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                if (articulo == null) articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.ImagenUrl = txtUrlImg.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Precio = int.Parse(txtPrecio.Text);

                if (articulo.Id != 0)
                {
                    articuloNegocio.modificarArticulo(articulo);
                    MessageBox.Show("Artículo modificado");
                }
                else
                {
                    articuloNegocio.agregarArticulo(articulo);
                    MessageBox.Show("Artículo agregado");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Close();
            }
        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {

                cboMarca.DataSource = articuloNegocio.listarArticulos();
                cboMarca.ValueMember = "Id"; 
                cboMarca.DisplayMember = "Descripcion"; 
                cboCategoria.DataSource = articuloNegocio.listarArticulos();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)  
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtUrlImg.Text = articulo.ImagenUrl;
                    cboMarca.SelectedItem = articulo.Marca.Id;
                    cboCategoria.SelectedItem = articulo.Categoria.Id;
                    txtPrecio.Text = articulo.Precio.ToString();

                    cargarImagen(articulo.ImagenUrl);

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://www.blackwallst.directory/images/NoImageAvailable.png");
            }
        }

        private void txtUrlImg_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImg.Text);
        }
    }
}
