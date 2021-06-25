﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Domain;
using System.Diagnostics;

namespace GKCOMSYSTEMCHAMIBEN
{
    public partial class Inicio : Form
    {


        // [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        /*private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );*/




        //string connectionString = @"Server=localhost;Database=chamiben;Uid=root;";
        // string connectionString2 = @"Server=greasesoftwaresolutions.mysql.database.azure.com;Port=3306;Database=chamiben;Uid=nilver@greasesoftwaresolutions;Pwd=Kimberly16;CheckParameters=False;";



        public int order;
        
        public bool state;
        string mozosname = "";
        public decimal subtotal = 0;

        int subtotalprod = 0;
        int currentmesa = 0;
        decimal[] mesatotal = new decimal[35];
        public string printer1;
        public string printer2;
        public string printer3;
        bool helado = true;
        
        bool custom;
        string tipoplato = "";



        public Inicio()
        {

            InitializeComponent();
            MysqldStateResolve();

           
            //this.FormBorderStyle = FormBorderStyle.None;
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            addcolcomanda();

            GridFillBebidas();
            GridFillEntradas();
            GridFillPlatoFondo();
            GridFillPostres();


            unabledeletebtn();
            deletebuttonstart();



            combosstart();
            activealltables();
            UpdateFontListaTotal();



           


            ingresarbtn.BackColor = Color.Transparent;





            SelectLastNOrder();
            DisableSafeMode();

        }
        private void DisableSafeMode() {

            ProductModel prod = new ProductModel();
            prod.DisableSafeMode();


        }

        private void MysqldStateResolve() {

            
            Process[] _proceses = null;
            _proceses = Process.GetProcessesByName("mysqld");

            if (_proceses.Length != 0)
            {
                    
            }
            else
            {
                try
                { 

                    Process exeProcess = Process.Start(@"C:\xampp\mysql\bin\mysqld.exe");
                   
                    


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
               

            }

        }


        public string GetMozosName() {



            if (albradio.Checked == true)
            {

                 mozosname = "ALBERTO";

            }
            if (jenkradio.Checked == true)
            {

                mozosname = "JENKER";
                
            }
            if (pacradio.Checked == true)
            {

                mozosname = "ORLANDO";
                

            }
            if (renanradio.Checked == true)
            {

               mozosname = "RENAN";
              

            }
            if (roxradio.Checked == true)
            {

                mozosname = "ROXANA";
              

            }
            if (elmradio.Checked == true)
            {

                mozosname = "ELMER";
              

            }
            if (inv1radio.Checked == true)
            {

                mozosname = "INVITADO 1";
              

            }
            if (inv2radio.Checked == true)
            {

                mozosname = "INVITADO 2";
                

            }

            return mozosname;
        }

        public bool IfCheckedMozo() {


            if (albradio.Checked == true || jenkradio.Checked == true
                || pacradio.Checked == true || renanradio.Checked == true || roxradio.Checked == true || elmradio.Checked == true || inv1radio.Checked == true
                || inv2radio.Checked == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private void Entradasbtn1_Click(object sender, EventArgs e)
        {


            if (IfCheckedMozo())
            {
                ChamiTab.SelectedTab = EntradasTab;

                UpdateFontListaEntrada();

                deletebtn2.Enabled = false;

                mozolbl2.Text = GetMozosName();


            }
            else
            {
                MessageBox.Show("Seleccionar mozo para continuar", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        private void Bebidabtn1_Click(object sender, EventArgs e)
        {
            if (IfCheckedMozo())
            {
                ChamiTab.SelectedTab = BebidasTab;
                UpdateFontListaBebida();
                deletebtn.Enabled = false;


                mozolbl.Text = GetMozosName();
            }
            else
            {
                MessageBox.Show("Seleccionar mozo para continuar", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }
        
        private void Platofondobtn1_Click_1(object sender, EventArgs e)
        {

            if (IfCheckedMozo())
            {
                ChamiTab.SelectedTab = PlatoFondoTab;
                GridFillPlatoFondo();

                UpdateFontListaPlatoFondo();
                deletebtn3.Enabled = false;



                mozolbl3.Text = GetMozosName() ;
            }
            else
            {
                MessageBox.Show("Seleccionar mozo para continuar", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }
        
        private void Postres1_Click(object sender, EventArgs e)
        {


            if (IfCheckedMozo())
            {
                ChamiTab.SelectedTab = PostresTab;

                UpdateFontListaPostre();
                deletebtn4.Enabled = false;

                
                mozolbl4.Text = GetMozosName();
            }
            else
            {
                MessageBox.Show("Seleccionar mozo para continuar", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }



        private void albradio_CheckedChanged(object sender, EventArgs e)
        {
            mozosnametxt1.Text = "ALBERTO";
        }

        private void jenkradio_CheckedChanged(object sender, EventArgs e)
        {
            mozosnametxt1.Text = "JENKER";
        }

        private void pacradio_CheckedChanged(object sender, EventArgs e)
        {
            mozosnametxt1.Text = "ORLANDO";
        }

        private void renanradio_CheckedChanged(object sender, EventArgs e)
        {
            mozosnametxt1.Text = "RENAN";
        }

        private void roxradio_CheckedChanged(object sender, EventArgs e)
        {
            mozosnametxt1.Text = "ROXANA";
        }
        private void inv1radio_CheckedChanged(object sender, EventArgs e)
        {
            mozosnametxt1.Text = "INVITADO 1";
        }

        private void inv2radio_CheckedChanged(object sender, EventArgs e)
        {
            mozosnametxt1.Text = "INVITADO 2";
        }


        private void Final1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estas seguro de terminar la Mesa?", "Observación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (dgvlistatotal.RowCount != 0)
                {



                    enableradio1(true);

                    mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);


                    guardarbtn1.Enabled = true;

                    mozosname = GetMozosName();


                    foreach (DataGridViewRow row in dgvlistatotal.Rows)
                    {
                        try
                        {

                            if (row.IsNewRow) continue;

                            ProductModel prod = new ProductModel();
                            prod.SaveSell(mozosname, currentmesa,Convert.ToString(row.Cells[0].Value),
                                Convert.ToDecimal(row.Cells[1].Value), Convert.ToInt16(row.Cells[2].Value),
                                Convert.ToDecimal(row.Cells[3].Value), Convert.ToString(row.Cells[5].Value));


                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("heres ht eerrror");
                            MessageBox.Show(ex.Message);
                          
                        }



                    }

                    MakeTicket(mozosname, dgvlistatotal,Convert.ToDecimal(totaltxtmesa.Text),printer3);
                    
                    dgvlistatotal.Rows.Clear();


                    totaltxtmesa.Text = "";

                    mesa1btn.Text = "";
                    mesatotal[1] -= mesatotal[1];
                    ResetRadioButtons();
                    mozosnametxt1.Text = "Seleccione Mozo";
                    actualplatotxt1.Text = "";
                    ChamiTab.SelectedTab = Mesastab;



                }
                else
                {
                    MessageBox.Show("Debe agregar productos a la orden total", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }



        }

        private void MakeTicket(string mozosname, DataGridView dgv,decimal subtotal,string printer) {

            CrearTicket ticket = new CrearTicket();



            //ticket.AbreCajon();//Para abrir el cajon de dinero.

            //De aqui en adelante pueden formar su ticket a su gusto... Les muestro un ejemplo

            //Datos de la cabecera del Ticket.
            ticket.TextoCentro("CHAMI - BENAVIDES");
            //ticket.TextoIzquierda("EXPEDIDO EN: LOCAL PRINCIPAL");
            //ticket.TextoIzquierda("DIREC: DIRECCION DE LA EMPRESA");
            //ticket.TextoIzquierda("TELEF: 4530000");
            //ticket.TextoIzquierda("R.F.C: XXXXXXXXX-XX");
            //ticket.TextoIzquierda("EMAIL: cmcmarce14@gmail.com");//Es el mio por si me quieren contactar ...
            ticket.TextoIzquierda("");
            //ticket.TextoExtremos("Caja # 1", "Ticket # 002-0000006");
            ticket.lineasAsteriscos();

            //Sub cabecera.
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("MOZO: " + mozosname);
            ticket.TextoIzquierda("MESA: " + "1");
            ticket.TextoIzquierda("SECCION: " + "CAJA");
            //ticket.TextoIzquierda("CLIENTE: PUBLICO EN GENERAL");
            ticket.TextoIzquierda("");
            ticket.TextoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToLongTimeString());
            ticket.lineasAsteriscos();

            //Articulos a vender.
            ticket.EncabezadoVenta();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
            ticket.lineasAsteriscos();
            //Si tiene una DataGridView donde estan sus articulos a vender pueden usar esta manera para agregarlos al ticket.

            foreach (DataGridViewRow fila in dgv.Rows)//dgvLista es el nombre del datagridview
            {
                if (fila.IsNewRow) continue;

                ticket.AgregaArticulo("-" + fila.Cells[0].Value.ToString(), int.Parse(fila.Cells[2].Value.ToString()),
                decimal.Parse(fila.Cells[1].Value.ToString()), decimal.Parse(fila.Cells[3].Value.ToString()));
            }/*
                    ticket.AgregaArticulo("Articulo A", 2, 20, 40);
                    ticket.AgregaArticulo("Articulo B", 1, 10, 10);
                    ticket.AgregaArticulo("Este es un nombre largo del articulo, para mostrar como se bajan las lineas", 1, 30, 30);*/
            ticket.lineasIgual();

            //Resumen de la venta. Sólo son ejemplos
            //ticket.AgregarTotales("         SUBTOTAL......$", 100);
            //ticket.AgregarTotales("         IVA...........$", 10.04M);//La M indica que es un decimal en C#
            ticket.AgregarTotales("SUBTOTAL.........S/", subtotal);
            ticket.TextoIzquierda("");
            //ticket.AgregarTotales("         EFECTIVO......$", 200);
            // ticket.AgregarTotales("         CAMBIO........$", 0);

            //Texto final del Ticket.
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            //ticket.TextoIzquierda("ARTÍCULOS VENDIDOS: 3");
            // ticket.TextoIzquierda("");
            //ticket.TextoCentro("¡GRACIAS POR SU COMPRA!");
            ticket.CortaTicket();
            ticket.ImprimirTicket(printer3);//Nombre de la impresora ticketera




        }

        private void deletebtntotal1_Click(object sender, EventArgs e)
        {
            if (dgvlistatotal.Rows.Count > 0)
            {


                DataGridViewRow row1 = dgvlistatotal.CurrentRow;

                if (row1.Cells[0].Value.ToString().Contains("-- ELIMINADO"))
                {

                    MessageBox.Show("No puedes eliminar dos veces un producto", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

                else
                {

                    


                        string message = "¿Deseas anular el producto seleccionado?";
                        string caption = "Irreversible";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                        DialogResult result;

                        // Displays the MessageBox.
                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {


                            int selectedrowindex = dgvlistatotal.SelectedCells[3].RowIndex;

                            DataGridViewRow selectedRow = dgvlistatotal.Rows[selectedrowindex];

                            string a = Convert.ToString(selectedRow.Cells[3].Value);


                            mesatotal[1] -= Convert.ToInt16(a);
                            totaltxtmesa.Text = mesatotal[1].ToString();

                            dgvlistatotal.Rows.Add(row1.Cells[0].Value.ToString() + " -- ELIMINADO", row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), row1.Cells[3].Value.ToString(), row1.Cells[4].Value.ToString(), row1.Cells[5].Value.ToString());

                            dgvlistatotal.Rows.RemoveAt(dgvlistatotal.CurrentRow.Index);



                            if (tipoplato == "cocina")

                            {
                                CrearTicket ticket = new CrearTicket();



                                //ticket.AbreCajon();//Para abrir el cajon de dinero.

                                //De aqui en adelante pueden formar su ticket a su gusto... Les muestro un ejemplo

                                //Datos de la cabecera del Ticket.
                                //ticket.TextoCentro("CHAMI - BENAVIDES");
                                //ticket.TextoIzquierda("EXPEDIDO EN: LOCAL PRINCIPAL");
                                //ticket.TextoIzquierda("DIREC: DIRECCION DE LA EMPRESA");
                                //ticket.TextoIzquierda("TELEF: 4530000");
                                //ticket.TextoIzquierda("R.F.C: XXXXXXXXX-XX");
                                //ticket.TextoIzquierda("EMAIL: cmcmarce14@gmail.com");//Es el mio por si me quieren contactar ...
                                ticket.TextoIzquierda("");
                                //ticket.TextoExtremos("Caja # 1", "Ticket # 002-0000006");
                                //ticket.lineasAsteriscos();

                                //Sub cabecera.
                                ticket.TextoIzquierda("");
                                ticket.TextoIzquierda("MOZO: " + mozosnametxt1.Text);
                                ticket.TextoIzquierda("MESA: " + "1");
                                ticket.TextoIzquierda("PLATOS ANULADOS DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("PLATOS ANULADOS DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("PLATOS ANULADOS DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("PLATOS ANULADOS DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("PLATOS ANULADOS DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                //ticket.TextoIzquierda("CLIENTE: PUBLICO EN GENERAL");
                                ticket.TextoIzquierda("");
                                ticket.TextoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToLongTimeString());
                                ticket.lineasAsteriscos();

                                //Articulos a vender.
                                //ticket.EncabezadoVenta();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
                                //ticket.lineasAsteriscos();
                                //Si tiene una DataGridView donde estan sus articulos a vender pueden usar esta manera para agregarlos al ticket.
                                //dgvLista es el nombre del datagridview



                                ticket.AgregaArticulo("-" + row1.Cells[0].Value.ToString(), int.Parse(row1.Cells[2].Value.ToString()),
                                decimal.Parse(row1.Cells[1].Value.ToString()), decimal.Parse(row1.Cells[3].Value.ToString()));
                                /*
                                ticket.AgregaArticulo("Articulo A", 2, 20, 40);
                                ticket.AgregaArticulo("Articulo B", 1, 10, 10);
                                ticket.AgregaArticulo("Este es un nombre largo del articulo, para mostrar como se bajan las lineas", 1, 30, 30);*/
                                ticket.lineasIgual();

                                //Resumen de la venta. Sólo son ejemplos
                                //ticket.AgregarTotales("         SUBTOTAL......$", 100);
                                //ticket.AgregarTotales("         IVA...........$", 10.04M);//La M indica que es un decimal en C#
                                //ticket.AgregarTotales("TOTAL.S/", Convert.ToDecimal(mesatotal[1]));
                                //ticket.TextoIzquierda("");
                                // ticket.AgregarTotales("         EFECTIVO......$", 200);
                                // ticket.AgregarTotales("         CAMBIO........$", 0);

                                //Texto final del Ticket.
                                ticket.TextoIzquierda("");
                                ticket.TextoIzquierda("PLATOS ANULADOS DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("PLATOS ANULADOS DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("PLATOS ANULADOS DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("PLATOS ANULADOS DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                //ticket.TextoIzquierda("ARTÍCULOS VENDIDOS: 3");
                                // ticket.TextoIzquierda("");
                                //ticket.TextoCentro("¡GRACIAS POR SU COMPRA!");
                                ticket.CortaTicket();
                                ticket.ImprimirTicket(printer2);//Nombre de la impresora ticketera


                            }

                            else
                            {
                                CrearTicket ticket = new CrearTicket();



                                //ticket.AbreCajon();//Para abrir el cajon de dinero.

                                //De aqui en adelante pueden formar su ticket a su gusto... Les muestro un ejemplo

                                //Datos de la cabecera del Ticket.
                                //ticket.TextoCentro("CHAMI - BENAVIDES");
                                //ticket.TextoIzquierda("EXPEDIDO EN: LOCAL PRINCIPAL");
                                //ticket.TextoIzquierda("DIREC: DIRECCION DE LA EMPRESA");
                                //ticket.TextoIzquierda("TELEF: 4530000");
                                //ticket.TextoIzquierda("R.F.C: XXXXXXXXX-XX");
                                //ticket.TextoIzquierda("EMAIL: cmcmarce14@gmail.com");//Es el mio por si me quieren contactar ...
                                //ticket.TextoIzquierda("");
                                //ticket.TextoExtremos("Caja # 1", "Ticket # 002-0000006");
                                //ticket.lineasAsteriscos();

                                //Sub cabecera.
                                ticket.TextoIzquierda("");
                                ticket.TextoIzquierda("MOZO: " + mozosnametxt1.Text);
                                ticket.TextoIzquierda("MESA: " + "1");
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                //ticket.TextoIzquierda("CLIENTE: PUBLICO EN GENERAL");
                                ticket.TextoIzquierda("");
                                ticket.TextoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToLongTimeString());
                                ticket.lineasAsteriscos();

                                //Articulos a vender.
                                //ticket.EncabezadoVenta();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
                                // ticket.lineasAsteriscos();
                                //Si tiene una DataGridView donde estan sus articulos a vender pueden usar esta manera para agregarlos al ticket.
                                //dgvLista es el nombre del datagridview



                                ticket.AgregaArticulo("-" + row1.Cells[0].Value.ToString(), int.Parse(row1.Cells[2].Value.ToString()),
                                decimal.Parse(row1.Cells[1].Value.ToString()), decimal.Parse(row1.Cells[3].Value.ToString()));
                                /*
                                ticket.AgregaArticulo("Articulo A", 2, 20, 40);
                                ticket.AgregaArticulo("Articulo B", 1, 10, 10);
                                ticket.AgregaArticulo("Este es un nombre largo del articulo, para mostrar como se bajan las lineas", 1, 30, 30);*/
                                ticket.lineasIgual();

                                //Resumen de la venta. Sólo son ejemplos
                                //ticket.AgregarTotales("         SUBTOTAL......$", 100);
                                //ticket.AgregarTotales("         IVA...........$", 10.04M);//La M indica que es un decimal en C#
                                //ticket.AgregarTotales("TOTAL.S/", Convert.ToDecimal(mesatotal[1]));
                                //ticket.TextoIzquierda("");
                                // ticket.AgregarTotales("         EFECTIVO......$", 200);
                                // ticket.AgregarTotales("         CAMBIO........$", 0);

                                //Texto final del Ticket.
                                ticket.TextoIzquierda("");
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                ticket.TextoIzquierda("BEBIDAS O POSTRE ANULADA DE LA ORDEN Nº " + row1.Cells[4].Value.ToString());
                                //ticket.TextoIzquierda("ARTÍCULOS VENDIDOS: 3");
                                // ticket.TextoIzquierda("");
                                //ticket.TextoCentro("¡GRACIAS POR SU COMPRA!");
                                ticket.CortaTicket();
                                ticket.ImprimirTicket(printer1);//Nombre de la impresora ticketera

                            }



                        }















                }




            }
            else
            {
                string message = "No existen productos a eliminar";
                string caption = "Obs.";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;


                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            }

        }


        private void guardarbtn1_Click(object sender, EventArgs e)
        {
            string message = "¿Deseas guardar la mesa?";
            string caption = "Guardar";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;


            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {


                if (dgvlistatotal.RowCount > 0)
                {
                    ChamiTab.SelectedTab = Mesastab;
                    enableradio1(false);
                    //actualplatotxt1.Text = "";


                }
                else
                {
                    MessageBox.Show("Debe realizar algun pedido", "Datos no validos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                /*

                MySqlConnection mysqlCon = new MySqlConnection(connectionString);

                if (albradio.Checked == true)
                {

                    mozosname = "Alberto";

                }
                if (henkradio.Checked == true)
                {

                    mozosname = "Jenker";

                }
                if (pacradio.Checked == true)
                {

                    mozosname = "ORLANDO";

                }
                if (renanradio.Checked == true)
                {

                    mozosname = "Renan";

                }
                if (roxradio.Checked == true)
                {

                    mozosname = "Roxana";

                }*/
                /*
               
                    foreach (DataGridViewRow row in dgvlistatotal.Rows)
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand();
                            cmd = mysqlCon.CreateCommand();

                            if (row.IsNewRow) continue;
                            cmd.Parameters.AddWithValue("@Mozo", mozosname);
                            cmd.Parameters.AddWithValue("@Mesa", mesatxt.Text);
                            cmd.Parameters.AddWithValue("@Producto", row.Cells[0].Value);
                            cmd.Parameters.AddWithValue("@Precio", row.Cells[1].Value);
                            cmd.Parameters.AddWithValue("@Unidades", row.Cells[2].Value);
                            cmd.Parameters.AddWithValue("@Subtotal", row.Cells[3].Value);
                            
                            cmd.Parameters.AddWithValue("@Estado", mesastate);


                            cmd.CommandText = "INSERT INTO logsp1ben(Mozo,Mesa,Producto,Precio,Unidades,Subtotal,FechaInc,Estado) VALUES(@Mozo,@Mesa,@Producto,@Precio,@Unidades,@Subtotal,now(),@Estado)";

                            mysqlCon.Open();
                            cmd.ExecuteNonQuery();
                            mysqlCon.Close();


                        

                    }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }




                    }*/

                //dgvlistatotal.Columns.Clear();


            }



            /* if (mesanum == 1)
             {
                 mesas[0] -=mesas[0];

             }
             */

        }


        private void deleteordentotalbtn1_Click(object sender, EventArgs e)
        {


            string message = "¿Deseas elimnar la orden?";
            string caption = "Observación";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {



                if (dgvlistatotal.RowCount > 0)

                {


                    if (albradio.Checked == true)
                    {

                        Verif ver = new Verif(this);
                        ver.mozoveriflbl.Text = "ALBERTO";


                        ver.ShowDialog();


                        if (state == true)
                        {
                            ChamiTab.SelectedTab = Mesastab;

                            guardarbtn1.Enabled = true;

                            dgvlistatotal.Rows.Clear();


                            mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);

                            totaltxtmesa.Text = "";
                            mesatotal[1] -= mesatotal[1];
                            mesa1btn.Text = "";

                            enableradio1(true);
                            actualplatotxt1.Text = "";



                            ResetRadioButtons();

                            mozosnametxt1.Text = "Seleccione Mozo";

                        }

                        state = false;




                    }
                    if (jenkradio.Checked == true)
                    {
                        Verif ver = new Verif(this);

                        ver.mozoveriflbl.Text = "JENKER";


                        ver.ShowDialog();

                        if (state == true)
                        {
                            ChamiTab.SelectedTab = Mesastab;

                            guardarbtn1.Enabled = true;

                            dgvlistatotal.Rows.Clear();


                            mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);

                            totaltxtmesa.Text = "";
                            mesatotal[1] -= mesatotal[1];
                            mesa1btn.Text = "";

                            enableradio1(true);
                            actualplatotxt1.Text = "";



                            ResetRadioButtons();

                            mozosnametxt1.Text = "Seleccione Mozo";

                        }

                        state = false;

                    }
                    if (pacradio.Checked == true)
                    {
                        Verif ver = new Verif(this);
                        ver.mozoveriflbl.Text = "ORLANDO";


                        ver.ShowDialog();
                        if (state == true)
                        {
                            ChamiTab.SelectedTab = Mesastab;

                            guardarbtn1.Enabled = true;

                            dgvlistatotal.Rows.Clear();


                            mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);

                            totaltxtmesa.Text = "";
                            mesatotal[1] -= mesatotal[1];
                            mesa1btn.Text = "";

                            enableradio1(true);
                            actualplatotxt1.Text = "";



                            ResetRadioButtons();

                            mozosnametxt1.Text = "Seleccione Mozo";

                        }

                        state = false;


                    }
                    if (renanradio.Checked == true)
                    {
                        Verif ver = new Verif(this);
                        ver.mozoveriflbl.Text = "RENAN";


                        ver.ShowDialog();
                        if (state == true)
                        {
                            ChamiTab.SelectedTab = Mesastab;

                            guardarbtn1.Enabled = true;

                            dgvlistatotal.Rows.Clear();


                            mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);

                            totaltxtmesa.Text = "";
                            mesatotal[1] -= mesatotal[1];
                            mesa1btn.Text = "";

                            enableradio1(true);
                            actualplatotxt1.Text = "";



                            ResetRadioButtons();

                            mozosnametxt1.Text = "Seleccione Mozo";

                        }

                        state = false;

                    }
                    if (roxradio.Checked == true)
                    {
                        Verif ver = new Verif(this);
                        ver.mozoveriflbl.Text = "ROXANA";


                        ver.ShowDialog();
                        if (state == true)
                        {
                            ChamiTab.SelectedTab = Mesastab;

                            guardarbtn1.Enabled = true;

                            dgvlistatotal.Rows.Clear();


                            mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);

                            totaltxtmesa.Text = "";
                            mesatotal[1] -= mesatotal[1];
                            mesa1btn.Text = "";

                            enableradio1(true);
                            actualplatotxt1.Text = "";



                            ResetRadioButtons();

                            mozosnametxt1.Text = "Seleccione Mozo";

                        }

                        state = false;

                    }
                    if (elmradio.Checked == true)
                    {
                        Verif ver = new Verif(this);
                        ver.mozoveriflbl.Text = "ELMER";


                        ver.ShowDialog();
                        if (state == true)
                        {
                            ChamiTab.SelectedTab = Mesastab;

                            guardarbtn1.Enabled = true;

                            dgvlistatotal.Rows.Clear();


                            mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);

                            mozosnametxt1.Text = "";
                            totaltxtmesa.Text = "";
                            mesatotal[1] -= mesatotal[1]; mesa1btn.Text = "";

                            enableradio1(true); actualplatotxt1.Text = "";
                            ResetRadioButtons();
                            mozosnametxt1.Text = "Selecione Mozo";

                        }

                        state = false;

                    }
                    if (inv1radio.Checked == true)
                    {
                        Verif ver = new Verif(this);
                        ver.mozoveriflbl.Text = "INVITADO 1";


                        ver.ShowDialog();
                        if (state == true)
                        {
                            ChamiTab.SelectedTab = Mesastab;

                            guardarbtn1.Enabled = true;

                            dgvlistatotal.Rows.Clear();


                            mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);

                            mozosnametxt1.Text = "";
                            totaltxtmesa.Text = "";
                            mesatotal[1] -= mesatotal[1]; mesa1btn.Text = "";

                            enableradio1(true); actualplatotxt1.Text = "";
                            ResetRadioButtons();
                            mozosnametxt1.Text = "Selecione Mozo";

                        }

                        state = false;

                    }
                    if (inv2radio.Checked == true)
                    {
                        Verif ver = new Verif(this);
                        ver.mozoveriflbl.Text = "INVITADO 2";


                        ver.ShowDialog();
                        if (state == true)
                        {
                            ChamiTab.SelectedTab = Mesastab;

                            guardarbtn1.Enabled = true;

                            dgvlistatotal.Rows.Clear();


                            mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);

                            mozosnametxt1.Text = "";
                            totaltxtmesa.Text = "";
                            mesatotal[1] -= mesatotal[1]; mesa1btn.Text = "";

                            enableradio1(true); actualplatotxt1.Text = "";
                            ResetRadioButtons();
                            mozosnametxt1.Text = "Selecione Mozo";

                        }

                        state = false;

                    }





                }
                else
                {

                    ChamiTab.SelectedTab = Mesastab;

                    guardarbtn1.Enabled = true;

                    dgvlistatotal.Rows.Clear();


                    mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);

                    totaltxtmesa.Text = "";
                    mesatotal[1] -= mesatotal[1];
                    mesa1btn.Text = "";

                    enableradio1(true);
                    actualplatotxt1.Text = "";



                    ResetRadioButtons();

                    mozosnametxt1.Text = "Seleccione Mozo";



                }

            }

        }



        private void deletebuttonstart()
        {
            deletebtn.Enabled = false;
            deletebtn2.Enabled = false;
            deletebtn3.Enabled = false;
            deletebtn4.Enabled = false;
        }

        private void ResetRadioButtons()
        {
            albradio.Checked = false;
            jenkradio.Checked = false;
            pacradio.Checked = false;
            renanradio.Checked = false;
            roxradio.Checked = false;
            elmradio.Checked = false;
            inv1radio.Checked = false;
            inv2radio.Checked = false;
           

        }
        /*
        public void SelectLastPrinter1()
        {
            MySqlConnection mysqlCon = new MySqlConnection(connectionString);
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2 = mysqlCon.CreateCommand();


            string queryStr = "select Name from printer1 ORDER BY id DESC LIMIT 1;;";


            cmd2 = new MySql.Data.MySqlClient.MySqlCommand(queryStr, mysqlCon);

            mysqlCon.Open();

            var queryResult = cmd2.ExecuteScalar();//Return an object so first check for null

            if (queryResult != null)
                // If we have result, then convert it from object to string.
                printer1 = Convert.ToString(queryResult);
            else
                // Else make id = "" so you can later check it.
                printer1 = "";


            //select* from getLastRecord ORDER BY id DESC LIMIT 1;
            mysqlCon.Close();


        }
        
        public void SelectLastPrinter2()
        {
            MySqlConnection mysqlCon = new MySqlConnection(connectionString);
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2 = mysqlCon.CreateCommand();


            string queryStr = "select Name from printer2 ORDER BY id DESC LIMIT 1;";


            cmd2 = new MySql.Data.MySqlClient.MySqlCommand(queryStr, mysqlCon);

            mysqlCon.Open();

            var queryResult = cmd2.ExecuteScalar();//Return an object so first check for null

            if (queryResult != null)
                // If we have result, then convert it from object to string.
                printer2 = Convert.ToString(queryResult);
            else
                // Else make id = "" so you can later check it.
                printer2 = "";


            //select* from getLastRecord ORDER BY id DESC LIMIT 1;
            mysqlCon.Close();


        }
        public void SelectLastPrinter3()
        {
            MySqlConnection mysqlCon = new MySqlConnection(connectionString);
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2 = mysqlCon.CreateCommand();


            string queryStr = "select Name from printer3 ORDER BY id DESC LIMIT 1;";


            cmd2 = new MySql.Data.MySqlClient.MySqlCommand(queryStr, mysqlCon);

            mysqlCon.Open();

            var queryResult = cmd2.ExecuteScalar();//Return an object so first check for null

            if (queryResult != null)
                // If we have result, then convert it from object to string.
                printer3 = Convert.ToString(queryResult);
            else
                // Else make id = "" so you can later check it.
                printer3 = "";


            //select* from getLastRecord ORDER BY id DESC LIMIT 1;
            mysqlCon.Close();


        }*/
        public void SelectLastNOrder()
        {
            try
            {


                ProductModel prod = new ProductModel();

                order = prod.GetLastNO();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            



        }
        public void SaveLastOrder()
        {
            try
            {

               ProductModel prod = new ProductModel();
               prod.SaveLastNO(order);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private void addcolcomanda()
        {

            dgvorden.Columns.Add("0", "Prod.");
            dgvorden.Columns.Add("1", "Prec.");
            dgvorden.Columns.Add("2", "Uds.");
            dgvorden.Columns.Add("3", "Sub./Pr.");


            dgvorden2.Columns.Add("0", "Prod.");
            dgvorden2.Columns.Add("1", "Prec.");
            dgvorden2.Columns.Add("2", "Uds.");
            dgvorden2.Columns.Add("3", "Sub./Pr.");

            dgvorden3.Columns.Add("0", "Prod.");
            dgvorden3.Columns.Add("1", "Prec.");
            dgvorden3.Columns.Add("2", "Uds.");
            dgvorden3.Columns.Add("3", "Sub./Pr.");

            dgvorden4.Columns.Add("0", "Prod.");
            dgvorden4.Columns.Add("1", "Prec.");
            dgvorden4.Columns.Add("2", "Uds.");
            dgvorden4.Columns.Add("3", "Sub./Pr.");



            dgvlistatotal.Columns.Add("0", "Prod.");
            dgvlistatotal.Columns.Add("1", "Prec.");
            dgvlistatotal.Columns.Add("2", "Uds.");
            dgvlistatotal.Columns.Add("3", "Sub./Pr.");
            dgvlistatotal.Columns.Add("4", "Ord");
            dgvlistatotal.Columns.Add("5", "Tipo");
            dgvlistatotal.Columns.Add("6", "Raz.Elim.");
            dgvlistatotal.Columns[4].Visible = false;
            dgvlistatotal.Columns[5].Visible = false;
            dgvlistatotal.Columns[6].Visible = false;
            

        }

        private void ActiveIcons(Boolean active)
        {
            bebidabtn1.IconVisible = active;
            entradabtn1.IconVisible = active;
            platofondobtn1.IconVisible = active;
            postrebtn1.IconVisible = active;

            printbtn.IconVisible = active;
            printbtn2.IconVisible = active;
            printbtn3.IconVisible = active;
            printbtn4.IconVisible = active;


            criollobtn.IconVisible = active;
            marinobtn.IconVisible = active;
            allbtn.IconVisible = active;
            pcombbtn.IconVisible = active;

            mesa1btn.IconVisible = active;
            mesa2btn.IconVisible = active;
            mesa3btn.IconVisible = active;
            mesa4btn.IconVisible = active;
            mesa5btn.IconVisible = active;
            mesa6btn.IconVisible = active;
            mesa7btn.IconVisible = active;
            mesa8btn.IconVisible = active;
            mesa9btn.IconVisible = active;
            mesa10btn.IconVisible = active;
            mesa11btn.IconVisible = active;
            mesa12btn.IconVisible = active;
            mesa13btn.IconVisible = active;
            mesa14btn.IconVisible = active;
            mesa15btn.IconVisible = active;
            mesa16btn.IconVisible = active;
            mesa17btn.IconVisible = active;
            mesa18btn.IconVisible = active;
            mesa19btn.IconVisible = active;
            mesa20btn.IconVisible = active;
            mesa21btn.IconVisible = active;
            mesa22btn.IconVisible = active;
            mesa23btn.IconVisible = active;
            mesa24btn.IconVisible = active;
            mesa25btn.IconVisible = active;
            mesa26btn.IconVisible = active;
            mesa27btn.IconVisible = active;
            mesa28btn.IconVisible = active;
            mesa29btn.IconVisible = active;
            mesa30btn.IconVisible = active;
            mesa31btn.IconVisible = active;
            mesa32btn.IconVisible = active;
            mesa33btn.IconVisible = active;
           

            cancelbtn.IconVisible = active;
            cancelbtn2.IconVisible = active;
            cancelbtn3.IconVisible = active;
            cancelbtn4.IconVisible = active;

            final1.IconVisible = active;
           
        }
        public void GridFillBebidas()
        {

            ProductModel prod = new ProductModel();

            bebidasdgv.DataSource = prod.ListBebidas();
            bebidasdgv.Columns[0].Visible = false;

        }
        public void GridFillEntradas()
        {
            ProductModel prod = new ProductModel();

            entradasdgv.DataSource = prod.ListEntradas();
            entradasdgv.Columns[0].Visible = false;
        }
        public void GridFillPlatoFondo()
        {
            ProductModel prod = new ProductModel();

            platofondodgv.DataSource = prod.ListPlatoFondo();
            platofondodgv.Columns[0].Visible = false;

        }
        public void GridFillCriollo()
        {
            ProductModel prod = new ProductModel();

            platofondodgv.DataSource = prod.ListPCriollo();
            platofondodgv.Columns[0].Visible = false;
        }
        public void GridFillMarino()
        {
            ProductModel prod = new ProductModel();

            platofondodgv.DataSource = prod.ListPMarino();
            platofondodgv.Columns[0].Visible = false;
        }
        public void GridFillComb()
        {
            ProductModel prod = new ProductModel();

            platofondodgv.DataSource = prod.ListPComb();
            platofondodgv.Columns[0].Visible = false;
        }

        void GridFillPostres()
        {
            ProductModel prod = new ProductModel();

            postredgv.DataSource = prod.ListPostres();
            postredgv.Columns[0].Visible = false;
        }
       
        private void activealltables()
        {

            mesa1btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa2btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa3btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa4btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa5btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa6btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa7btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa8btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa9btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa10btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa11btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa12btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa13btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa14btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa15btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa16btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa17btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa18btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa19btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa20btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa21btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa22btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa23btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa24btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa25btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa26btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa27btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa28btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa29btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa30btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa31btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa32btn.Normalcolor = Color.FromArgb(46, 139, 87);
            mesa33btn.Normalcolor = Color.FromArgb(46, 139, 87);
            


        }

        private void combosstart()
        {

            unidadescmb.SelectedIndex = 0;
            unidadescmb2.SelectedIndex = 0;
            unidadescmb3.SelectedIndex = 0;
            unidadescmb4.SelectedIndex = 0;
            azucarcmb.SelectedIndex = 0;
            svcmb1.SelectedIndex = 0;
            svcmb2.SelectedIndex = 0;
            svcmb3.SelectedIndex = 0;
            svcmb4.SelectedIndex = 0;
        }



        private void UpdateFontListaTotal()
        {
            DataGridViewColumn columnlistatotal1 = dgvlistatotal.Columns[0];
            columnlistatotal1.Width = 360;

        }
        private void UpdateFontListaBebida()
        {
            DataGridViewColumn columnlista1 = bebidasdgv.Columns[1];
            columnlista1.Width = 335;

            DataGridViewColumn orden1 = dgvorden.Columns[0];
            orden1.Width = 250;



        }
        private void UpdateFontListaEntrada()
        {
            DataGridViewColumn columnlista2 = entradasdgv.Columns[1];
            columnlista2.Width = 335;

            DataGridViewColumn orden2 = dgvorden2.Columns[0];
            orden2.Width = 250;


        }
        private void UpdateFontListaPlatoFondo()
        {
            DataGridViewColumn columnlista3 = platofondodgv.Columns[1];
            columnlista3.Width = 335;
            DataGridViewColumn orden3 = dgvorden3.Columns[0];
            orden3.Width = 250;

        }
        private void UpdateFontListaPostre()
        {
            DataGridViewColumn columnlista4 = postredgv.Columns[1];
            columnlista4.Width = 345;
            DataGridViewColumn orden4 = dgvorden4.Columns[0];
            orden4.Width = 250;

        }

        private void enableradio1(Boolean active)
        {

            albradio.Enabled = active;
            jenkradio.Enabled = active;
            pacradio.Enabled = active;
            renanradio.Enabled = active;
            roxradio.Enabled = active;
            elmradio.Enabled = active;
            inv1radio.Enabled = active;
            inv2radio.Enabled = active;
        }
       
        private void unabledeletebtn()
        {

            if (dgvorden.Rows.Count == 0)
            {

                deletebtn.Enabled = false;


            }
            if (dgvorden2.Rows.Count == 0)
            {

                deletebtn2.Enabled = false;


            }
            if (dgvorden3.Rows.Count == 0)
            {

                deletebtn3.Enabled = false;


            }
            if (dgvorden4.Rows.Count == 0)
            {

                deletebtn4.Enabled = false;


            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChamiTab.SelectedTab = Mesastab;
        }





        private void dgvlistatotal1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvlistatotal.RowCount == 0)
            {
                actualplatotxt1.Text = "";






            }
            else
            {
                DataGridViewRow row1 = dgvlistatotal.CurrentRow;


                actualplatotxt1.Text = row1.Cells[0].Value.ToString();


                if (row1.Cells[0].Value.ToString().Contains("&"))
                {

                    tipoplato = "cocina";

                }
                else

                    tipoplato = "bar";






            }




        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvorden.CurrentRow;
            dgvorden.Rows.RemoveAt(dgvorden.CurrentRow.Index);

            string precioag = row.Cells[3].Value.ToString();



            subtotal -= Convert.ToInt16(precioag);

            subtotaltxt.Text = subtotal.ToString();

            unabledeletebtn();


        }

        private void rightbtn_Click_2(object sender, EventArgs e)
        {



            deletebtn.Enabled = true;

            DataGridViewRow row1 = bebidasdgv.CurrentRow;
            string sv = svcmb2.Text;

            //CAPTURA DATOS DE LA TABLA DE LISTA DE PRODUCTOS
            string precio = row1.Cells[2].Value.ToString();
            string unidades = unidadescmb.Text;


            int precioprod = Convert.ToInt16(precio);
            int unidadesprod = Convert.ToInt16(unidades);
            subtotalprod = precioprod * unidadesprod;








            ////////////////////////////

            if (helado == true)
            {

                string message = "¿Desea la bebida Helada?";
                string caption = "Adicional";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);


                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    /////AGREGA DATOS A LA TABLA DE ORDENES




                    if (row1.Cells[1].Value.ToString().Contains("#"))

                    {

                        dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "_HELAD@" + "_" + azucarcmb.Text, row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                        subtotal += subtotalprod;

                        subtotaltxt.Text = subtotal.ToString();
                        svcmb2.SelectedIndex = 0;

                    }
                    else
                    {

                        if (row1.Cells[1].Value.ToString().Contains("&"))
                        {
                            string messageg = "¿Desea el agua con gas?";
                            string captiong = "Adicional";
                            MessageBoxButtons buttonsg = MessageBoxButtons.YesNo;
                            DialogResult resultg;

                            // Displays the MessageBox.
                            resultg = MessageBox.Show(messageg, captiong, buttonsg, MessageBoxIcon.Question);


                            if (resultg == System.Windows.Forms.DialogResult.Yes)
                            {

                                dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "_HELAD@_ConGAS" + "_", row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                                subtotal += subtotalprod;

                                subtotaltxt.Text = subtotal.ToString();
                                svcmb2.SelectedIndex = 0;


                            }
                            else
                            {

                                dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "_HELAD@_SinGAS" + "_", row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                                subtotal += subtotalprod;

                                subtotaltxt.Text = subtotal.ToString();
                                svcmb2.SelectedIndex = 0;
                            }



                        }

                        else
                        {


                            dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "_HELAD@", row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                            subtotal += subtotalprod;

                            subtotaltxt.Text = subtotal.ToString();
                            svcmb2.SelectedIndex = 0;
                        }





                    }

                    ////////////////////////////

                }
                else
                {
                    /////AGREGA DATOS A LA TABLA DE ORDENES



                    if (row1.Cells[1].Value.ToString().Contains("#"))

                    {

                        dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "_SIN HELAR" + "_" + azucarcmb.Text, row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                        subtotal += subtotalprod;

                        subtotaltxt.Text = subtotal.ToString();
                        svcmb2.SelectedIndex = 0;

                    }
                    else
                    {

                        if (row1.Cells[1].Value.ToString().Contains("&"))
                        {
                            string messageg = "¿Desea el agua con gas?";
                            string captiong = "Adicional";
                            MessageBoxButtons buttonsg = MessageBoxButtons.YesNo;
                            DialogResult resultg;

                            // Displays the MessageBox.
                            resultg = MessageBox.Show(messageg, captiong, buttonsg, MessageBoxIcon.Question);


                            if (resultg == System.Windows.Forms.DialogResult.Yes)
                            {

                                dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "_SIN HELAR_ConGAS" + "_", row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                                subtotal += subtotalprod;

                                subtotaltxt.Text = subtotal.ToString();
                                svcmb2.SelectedIndex = 0;


                            }
                            else
                            {

                                dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "_SIN HELAR_SinGAS" + "_", row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                                subtotal += subtotalprod;

                                subtotaltxt.Text = subtotal.ToString();
                                svcmb2.SelectedIndex = 0;
                            }



                        }

                        else
                        {
                            dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "_SIN HELAR", row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                            subtotal += subtotalprod;

                            subtotaltxt.Text = subtotal.ToString();
                            svcmb2.SelectedIndex = 0;
                        }






                    }

                }




            }
            else
            {





                /////AGREGA DATOS A LA TABLA DE ORDENES
                if (row1.Cells[1].Value.ToString().Contains("#"))

                {

                    dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + /*"_SIN HELAR" + "_" + */azucarcmb.Text, row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                    subtotal += subtotalprod;

                    subtotaltxt.Text = subtotal.ToString();
                    svcmb2.SelectedIndex = 0;

                }
                else
                {




                    dgvorden.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() /*+ "_SIN HELAR"*/, row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                    subtotal += subtotalprod;

                    subtotaltxt.Text = subtotal.ToString();
                    svcmb2.SelectedIndex = 0;






                }



                /*
                dgvorden.Rows.Add(row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb.SelectedItem, subtotalprod.ToString());

                subtotal += subtotalprod;

                subtotaltxt.Text = subtotal.ToString();*/

            }

            combosstart();





        }

        private void printbtn_Click(object sender, EventArgs e)
        {

            DataGridViewRow row1 = bebidasdgv.CurrentRow;
            DataGridViewRow row2 = dgvorden.CurrentRow;



            //MySqlConnection mysqlCon = new MySqlConnection(connectionString);


            string message = "¿Deseas imprimir la orden?";
            string caption = "Solicitud de impresion";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                if (dgvorden.RowCount != 0)
                {





                    order++;

                    SaveLastOrder();




                    if (currentmesa == 1){
                        mesa1btn.Normalcolor = Color.FromArgb(192, 32, 22);
                        mozosnametxt1.Text = mozosname; mesa1btn.Text = mozosname;
                        enableradio1(false);

                        foreach (DataGridViewRow row3 in dgvorden.Rows){
                            if (row3.IsNewRow) continue;
                            dgvlistatotal.Rows.Add(row3.Cells[0].Value.ToString(), row3.Cells[1].Value.ToString(), row3.Cells[2].Value.ToString(), row3.Cells[3].Value.ToString(), order.ToString(), "BEBIDAS"); } }

                  

                    PrintOrder(dgvorden, coment1txt.Text, printer1);

                    

                    //////////////////////////////////
                    /*//*/
                    dgvorden.Rows.Clear();//////
                                          //////////////////////////////////




                    if (currentmesa == 1)
                    { mesatotal[1] += Convert.ToDecimal(subtotaltxt.Text);
                        totaltxtmesa.Text = Convert.ToString(mesatotal[1]);
                        ChamiTab.SelectedTab = mesacom;
                        /*if (dgvlistatotal.SelectedCells.Count != 0)
                        { deletebtntotal.Enabled = true; }*/

                    }




                    //limpiar cuadro subtotal, subtotalproductocadauno 0 y subtotalcuadro 0
                    subtotal -= subtotal;
                    subtotalprod -= subtotalprod;
                    subtotaltxt.Text = "";
                    coment1txt.Text = "";


                    svcmb2.SelectedIndex = 0;
                    actualordertxt1.Text = "";

                }


                else
                {
                    MessageBox.Show("Debe agregar productos a la orden", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
        }
        public void PrintOrder(DataGridView dgv, string comment, string printer) {

            CrearTicket ticket = new CrearTicket();



            //ticket.AbreCajon();//Para abrir el cajon de dinero.

            //De aqui en adelante pueden formar su ticket a su gusto... Les muestro un ejemplo

            //Datos de la cabecera del Ticket.
            ticket.TextoCentro("CHAMI - BENAVIDES");
            //ticket.TextoIzquierda("EXPEDIDO EN: LOCAL PRINCIPAL");
            //ticket.TextoIzquierda("DIREC: DIRECCION DE LA EMPRESA");
            //ticket.TextoIzquierda("TELEF: 4530000");
            //ticket.TextoIzquierda("R.F.C: XXXXXXXXX-XX");
            //ticket.TextoIzquierda("EMAIL: cmcmarce14@gmail.com");//Es el mio por si me quieren contactar ...
            ticket.TextoIzquierda("");
            //ticket.TextoExtremos("Caja # 1", "Ticket # 002-0000006");
            ticket.lineasAsteriscos();

            //Sub cabecera.
            ticket.TextoIzquierda("");

            ticket.TextoIzquierda("Nº ORDEN: " + order);
            ticket.TextoIzquierda("MOZO: " + mozosname);
            ticket.TextoIzquierda("MESA: " + currentmesa);
            ticket.TextoIzquierda("SECCION: " + "BAR");
            //ticket.TextoIzquierda("CLIENTE: PUBLICO EN GENERAL");
            ticket.TextoIzquierda("");
            ticket.TextoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToLongTimeString());
            ticket.lineasAsteriscos();

            //Articulos a vender.
            ticket.EncabezadoVenta();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
            ticket.lineasAsteriscos();
            //Si tiene una DataGridView donde estan sus articulos a vender pueden usar esta manera para agregarlos al ticket.
            foreach (DataGridViewRow fila in dgv.Rows)//dgvLista es el nombre del datagridview
            {
                if (fila.IsNewRow) continue;

                ticket.AgregaArticulo("-" + fila.Cells[0].Value.ToString(), int.Parse(fila.Cells[2].Value.ToString()),
                decimal.Parse(fila.Cells[1].Value.ToString()), decimal.Parse(fila.Cells[3].Value.ToString()));
            }/*
                    ticket.AgregaArticulo("Articulo A", 2, 20, 40);
                    ticket.AgregaArticulo("Articulo B", 1, 10, 10);
                    ticket.AgregaArticulo("Este es un nombre largo del articulo, para mostrar como se bajan las lineas", 1, 30, 30);*/
            ticket.lineasIgual();

            //Resumen de la venta. Sólo son ejemplos
            //ticket.AgregarTotales("         SUBTOTAL......$", 100);
            //ticket.AgregarTotales("         IVA...........$", 10.04M);//La M indica que es un decimal en C#
            //ticket.AgregarTotales("TOTAL.........S/", Convert.ToDecimal(subtotal));
            ticket.TextoIzquierda("Comentarios: " + comment);
            // ticket.AgregarTotales("         EFECTIVO......$", 200);
            // ticket.AgregarTotales("         CAMBIO........$", 0);

            //Texto final del Ticket.
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            //ticket.TextoIzquierda("ARTÍCULOS VENDIDOS: 3");
            // ticket.TextoIzquierda("");
            //ticket.TextoCentro("¡GRACIAS POR SU COMPRA!");
            ticket.CortaTicket();
            ticket.ImprimirTicket(printer);//Nombre de la impresora ticketera



        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            string message = "¿Deseas cancelar el pedido?";
            string caption = "Observación";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                subtotal -= subtotal;
                subtotaltxt.Text = "";

                //ChamiTab.SelectedTab = mesastab;
                dgvorden.Rows.Clear();



                if (currentmesa == 1) { ChamiTab.SelectedTab = mesacom; }
               

                coment1txt.Text = "";
                svcmb2.SelectedIndex = 0;
                actualordertxt1.Text = "";

            }
        }

        private void dgvorden_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvorden.Rows.Count == 0)
            {

                deletebtn.Enabled = false;
                actualordertxt1.Text = "";

            }
            else
            {
                DataGridViewRow row1 = dgvorden.CurrentRow;


                actualordertxt1.Text = row1.Cells[0].Value.ToString();
            }

        }

        private void dgvlista2_SelectionChanged_1(object sender, EventArgs e)
        {
            DataGridViewRow row1 = entradasdgv.CurrentRow;


            //dgvorden3.Rows.Add(row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb3.SelectedItem, subtotalprod.ToString());

            try
            {
                if (row1.Cells[1].Value.ToString() == "LECHE DE TIGRE &" || row1.Cells[1].Value.ToString() == "ENSALADA CHAMI &")
                {
                    custom = true;
                    custombtn2.Enabled = true;
                    //unidadescmb2.Enabled = false;
                    //rightbtn2.Enabled = false;
                }

                else
                {
                    custom = false;
                    custombtn2.Enabled = false;
                    //unidadescmb2.Enabled = true;
                    //rightbtn2.Enabled = true;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "Elije", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void rightbtn2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row1 = entradasdgv.CurrentRow;
            string sv = svcmb1.Text;

            if (custom == true)
            {
                string message = "¿Deseas personalizar el plato?";
                string caption = "Añadir";


                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    if (Convert.ToString( row1.Cells[1].Value) == "LECHE DE TIGRE &")
                    {

                        LecheTigreCustom lechetigreform = new LecheTigreCustom(this);

                        lechetigreform.platotxt.Text = "(" + sv + ")" + this.entradasdgv.CurrentRow.Cells[1].Value.ToString();
                        lechetigreform.preciolbl.Text = this.entradasdgv.CurrentRow.Cells[2].Value.ToString();
                        lechetigreform.lbl1.Text = "(" + sv + ")" + this.entradasdgv.CurrentRow.Cells[1].Value.ToString() + "";
                        lechetigreform.unidadescmb.SelectedIndex = 0;
                        lechetigreform.ajicmb.SelectedIndex = 1;
                        lechetigreform.cebollacmb.SelectedIndex = 0;

                        lechetigreform.ShowDialog();

                    }
                    if (Convert.ToString(row1.Cells[1].Value) == "ENSALADA CHAMI &")
                    {

                        ChamiEnsaladaCustom ensaladacustom = new ChamiEnsaladaCustom(this);

                        ensaladacustom.unidadescmb.SelectedIndex = 0;

                        ensaladacustom.platolbl.Text = "(" + sv + ")" + this.entradasdgv.CurrentRow.Cells[1].Value.ToString();
                        ensaladacustom.lbl1.Text = "(" + sv + ")" + this.entradasdgv.CurrentRow.Cells[1].Value.ToString() + "";



                        ensaladacustom.ShowDialog();

                    }
                }
                else
                {
                    deletebtn2.Enabled = true;


                    //CAPTURA DATOS DE LA TABLA DE LISTA DE PRODUCTOS
                    string precio = row1.Cells[2].Value.ToString();
                    string unidades = unidadescmb2.Text;


                    int precioprod = Convert.ToInt16(precio);
                    int unidadesprod = Convert.ToInt16(unidades);
                    subtotalprod = precioprod * unidadesprod;



                    /////AGREGA DATOS A LA TABLA DE ORDENES

                    dgvorden2.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb2.SelectedItem, subtotalprod.ToString());

                    subtotal += subtotalprod;

                    subtotaltxt2.Text = subtotal.ToString();



                    combosstart();

                }






            }
            else
            {
                deletebtn2.Enabled = true;




                //CAPTURA DATOS DE LA TABLA DE LISTA DE PRODUCTOS
                string precio = row1.Cells[2].Value.ToString();
                string unidades = unidadescmb2.Text;


                int precioprod = Convert.ToInt16(precio);
                int unidadesprod = Convert.ToInt16(unidades);
                subtotalprod = precioprod * unidadesprod;



                /////AGREGA DATOS A LA TABLA DE ORDENES

                dgvorden2.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb2.SelectedItem, subtotalprod.ToString());

                subtotal += subtotalprod;

                subtotaltxt2.Text = subtotal.ToString();



                combosstart();

            }
            svcmb1.SelectedIndex = 0;

        }

        private void deletebtn2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvorden2.CurrentRow;
            dgvorden2.Rows.RemoveAt(dgvorden2.CurrentRow.Index);

            string precioag = row.Cells[3].Value.ToString();



            subtotal -= Convert.ToInt16(precioag);

            subtotaltxt2.Text = subtotal.ToString();
            if (dgvorden2.Rows.Count == 0)
            {

                deletebtn2.Enabled = false;
                actualordertxt2.Text = "";

            }

        }

        private void custombtn2_Click_1(object sender, EventArgs e)
        {
            DataGridViewRow row1 = entradasdgv.CurrentRow;
            string sv = svcmb1.Text;

            //dgvorden3.Rows.Add(row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb3.SelectedItem, subtotalprod.ToString());

            if (row1.Cells[1].Value.ToString() == "LECHE DE TIGRE &")
            {

                LecheTigreCustom lechetigreform = new LecheTigreCustom(this);

                lechetigreform.platotxt.Text = "(" + sv + ")" + this.entradasdgv.CurrentRow.Cells[1].Value.ToString();
                lechetigreform.preciolbl.Text = this.entradasdgv.CurrentRow.Cells[2].Value.ToString();
                lechetigreform.lbl1.Text = "(" + sv + ")" + this.entradasdgv.CurrentRow.Cells[1].Value.ToString() + "";
                lechetigreform.unidadescmb.SelectedIndex = 0;
                lechetigreform.ajicmb.SelectedIndex = 1;
                lechetigreform.cebollacmb.SelectedIndex = 0;

                lechetigreform.ShowDialog();

            }
            if (row1.Cells[1].Value.ToString() == "ENSALADA CHAMI &")
            {

                ChamiEnsaladaCustom ensaladacustom = new ChamiEnsaladaCustom(this);

                ensaladacustom.unidadescmb.SelectedIndex = 0;

                ensaladacustom.platolbl.Text = "(" + sv + ")" + this.entradasdgv.CurrentRow.Cells[1].Value.ToString();
                ensaladacustom.lbl1.Text = "(" + sv + ")" + this.entradasdgv.CurrentRow.Cells[1].Value.ToString() + "";



                ensaladacustom.ShowDialog();

            }

        }

        private void printbtn2_Click(object sender, EventArgs e)
        {


            DataGridViewRow row1 = entradasdgv.CurrentRow;
            DataGridViewRow row2 = dgvorden2.CurrentRow;

            string precio = row1.Cells[2].Value.ToString();
            string unidades = unidadescmb.Text;
            int precioprod = Convert.ToInt16(precio);
            int unidadesprod = Convert.ToInt16(unidades);
            int subtotalprod = precioprod * unidadesprod;

            


            string message = "¿Deseas imprimir la orden?";
            string caption = "Solicitud de impresion";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                if (dgvorden2.RowCount != 0)
                {

                   

                    order++;

                    SaveLastOrder();

                    PrintOrder(dgvorden2,coment2txt.Text,printer2);


                    if (currentmesa == 1) { mesa1btn.Normalcolor = Color.FromArgb(192, 32, 22); mozosnametxt1.Text = mozosname; mesa1btn.Text = mozosname; enableradio1(false); foreach (DataGridViewRow row3 in dgvorden2.Rows) { if (row3.IsNewRow) continue; dgvlistatotal.Rows.Add(row3.Cells[0].Value.ToString(), row3.Cells[1].Value.ToString(), row3.Cells[2].Value.ToString(), row3.Cells[3].Value.ToString(), order.ToString(), "ENTRADAS"); } }
                    //////////////////////////////////
                    /*//*/
                    dgvorden2.Rows.Clear();//////
                    //////////////////////////////////



                    if (currentmesa == 1) { mesatotal[1] += Convert.ToInt16(subtotaltxt2.Text); totaltxtmesa.Text = mesatotal[1].ToString(); ChamiTab.SelectedTab = mesacom; if (dgvlistatotal.SelectedCells.Count != 0) { deletebtntotal.Enabled = true; } }
                   
                    //limpiar cuadro subtotal, subtotalproductocadauno 0 y subtotalcuadro 0
                    subtotal -= subtotal;
                    subtotalprod -= subtotalprod;
                    subtotaltxt2.Text = "";
                    coment2txt.Text = "";
                    actualordertxt2.Text = "";
                    coment2txt.Text = "";
                    svcmb1.SelectedIndex = 0;
                    actualordertxt2.Text = "";
                }


                else
                {
                    MessageBox.Show("Debe agregar productos a la orden", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void cancelbtn2_Click(object sender, EventArgs e)
        {

            string message = "¿Deseas cancelar el pedido?";
            string caption = "Observación";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                subtotal -= subtotal;
                subtotaltxt2.Text = "";


                dgvorden2.Rows.Clear();

                if (currentmesa == 1) { ChamiTab.SelectedTab = mesacom; }
                
                coment2txt.Text = "";
                svcmb1.SelectedIndex = 0;
                actualordertxt2.Text = "";
            }

        }

        private void dgvorden2_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvorden2.Rows.Count == 0)
            {

                deletebtn2.Enabled = false;
                actualordertxt2.Text = "";

            }
            else
            {
                DataGridViewRow row1 = dgvorden2.CurrentRow;


                actualordertxt2.Text = row1.Cells[0].Value.ToString();
            }


        }

        private void dgvlista3_SelectionChanged_1(object sender, EventArgs e)
        {
            DataGridViewRow row1 = platofondodgv.CurrentRow;


            //dgvorden3.Rows.Add(row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb3.SelectedItem, subtotalprod.ToString());

            try
            {
                if (Convert.ToString( row1.Cells[1].Value )== "CEBICHE DE CONCHAS NEGRAS(SAB&DOM) &" ||
                Convert.ToString(row1.Cells[1].Value) == "CEBICHE DE LA PESCA DEL DIA &" ||
                Convert.ToString(row1.Cells[1].Value) == "CEBICHE DE LENGUADO &" ||
                Convert.ToString(row1.Cells[1].Value) == "CEBICHE DE MARISCOS &" ||
                Convert.ToString(row1.Cells[1].Value) == "CEBICHE MIXTO &" ||
                //row1.Cells[1].Value.ToString() == "DUO" ||
                //row1.Cells[1].Value.ToString() == "TRIO" ||
                Convert.ToString(row1.Cells[1].Value) == "RONDA CRIOLLISIMA &"
                )
                {
                    custom = true;
                    custombtn3.Enabled = true;
                    //unidadescmb3.Enabled = false;
                    //rightbtn3.Enabled = false;
                }


                else
                {
                    if (row1.Cells[1].Value.ToString() != "DUO &" ||
                row1.Cells[1].Value.ToString() != "TRIO &")
                    {
                        custom = false;
                        custombtn3.Enabled = false;
                        // unidadescmb3.Enabled = true;
                        //rightbtn3.Enabled = true;
                    }


                    if (row1.Cells[1].Value.ToString() == "DUO &" ||
                row1.Cells[1].Value.ToString() == "TRIO &")
                    {
                        custombtn3.Enabled = true;
                    }
                }




                if (platofondodgv.Rows.Count == 0)
                {


                    actuallistatxt3.Text = "";

                }
                else
                {



                    actuallistatxt3.Text = row1.Cells[1].Value.ToString();
                }







            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "Elije", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }






        }

        private void rightbtn3_Click_1(object sender, EventArgs e)
        {

            DataGridViewRow row1 = platofondodgv.CurrentRow;
            string sv = svcmb3.Text;

            if (custom == true)
            {

                string message = "¿Deseas personalizar el plato?";
                string caption = "Añadir";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {


                    //dgvorden3.Rows.Add(row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb3.SelectedItem, subtotalprod.ToString());

                    if (row1.Cells[1].Value.ToString() == "CEBICHE DE CONCHAS NEGRAS(SAB&DOM) &" ||
                        row1.Cells[1].Value.ToString() == "CEBICHE DE LA PESCA DEL DIA &" ||
                        row1.Cells[1].Value.ToString() == "CEBICHE DE LENGUADO &" ||
                        row1.Cells[1].Value.ToString() == "CEBICHE DE MARISCOS &" ||
                        row1.Cells[1].Value.ToString() == "CEBICHE MIXTO &"
                        )
                    {

                        CebicheCustom cebform = new CebicheCustom(this);

                        cebform.custompreciolblceb1.Text = this.platofondodgv.CurrentRow.Cells[2].Value.ToString();
                        cebform.namecebichelbl1.Text = "(" + sv + ")" + this.platofondodgv.CurrentRow.Cells[1].Value.ToString();
                        cebform.lbl1.Text = "(" + sv + ")" + this.platofondodgv.CurrentRow.Cells[1].Value.ToString() + "";
                        cebform.customcmbceb1.SelectedIndex = 0;

                        cebform.ShowDialog();

                    }/*
                    if (row1.Cells[1].Value.ToString() == "DUO")
                    {

                        Duo duocustom = new Duo(this);

                        duocustom.preciolbl.Text = this.dgvlista3.CurrentRow.Cells[2].Value.ToString();
                        duocustom.platonombrelbl.Text = this.dgvlista3.CurrentRow.Cells[1].Value.ToString();
                        duocustom.lbl1.Text = this.dgvlista3.CurrentRow.Cells[1].Value.ToString() + "";
                        duocustom.unidadescmb.SelectedIndex = 0;
                        duocustom.ajicmb.SelectedIndex = 0;
                        duocustom.platoscmb.SelectedIndex = 0;

                        duocustom.ShowDialog();

                    }*/
                    /*if (row1.Cells[1].Value.ToString() == "TRIO")
                    {

                        Trio triocustom = new Trio(this);

                        triocustom.preciolbl.Text = this.dgvlista3.CurrentRow.Cells[2].Value.ToString();
                        triocustom.platonombrelbl.Text = this.dgvlista3.CurrentRow.Cells[1].Value.ToString();
                        triocustom.lbl1.Text = this.dgvlista3.CurrentRow.Cells[1].Value.ToString() + "";
                        triocustom.unidadescmb.SelectedIndex = 0;

                        triocustom.ShowDialog();

                    }*/
                    if (row1.Cells[1].Value.ToString() == "RONDA CRIOLLISIMA &")
                    {

                        Ronda Rondacustom = new Ronda(this);

                        Rondacustom.preciolbl.Text = this.platofondodgv.CurrentRow.Cells[2].Value.ToString();
                        Rondacustom.platolbl.Text = "(" + sv + ")" + this.platofondodgv.CurrentRow.Cells[1].Value.ToString();
                        Rondacustom.lbl1.Text = "(" + sv + ")" + this.platofondodgv.CurrentRow.Cells[1].Value.ToString() + "";
                        Rondacustom.unidadescmb.SelectedIndex = 0;
                        Rondacustom.rondacmb.SelectedIndex = 0;

                        Rondacustom.ShowDialog();

                    }


                }

                else
                {
                    deletebtn3.Enabled = true;




                    //CAPTURA DATOS DE LA TABLA DE LISTA DE PRODUCTOS
                    string precio = row1.Cells[2].Value.ToString();
                    string unidades = unidadescmb3.Text;


                    int precioprod = Convert.ToInt16(precio);
                    int unidadesprod = Convert.ToInt16(unidades);
                    subtotalprod = precioprod * unidadesprod;



                    /////AGREGA DATOS A LA TABLA DE ORDENES

                    dgvorden3.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb3.SelectedItem, subtotalprod.ToString());

                    subtotal += subtotalprod;

                    subtotaltxt3.Text = subtotal.ToString();

                    combosstart();
                }

            }
            else
            {
                if (row1.Cells[1].Value.ToString() != "DUO &" && row1.Cells[1].Value.ToString() != "TRIO &")
                {
                    deletebtn3.Enabled = true;




                    //CAPTURA DATOS DE LA TABLA DE LISTA DE PRODUCTOS
                    string precio = row1.Cells[2].Value.ToString();
                    string unidades = unidadescmb3.Text;


                    int precioprod = Convert.ToInt16(precio);
                    int unidadesprod = Convert.ToInt16(unidades);
                    subtotalprod = precioprod * unidadesprod;



                    /////AGREGA DATOS A LA TABLA DE ORDENES

                    dgvorden3.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb3.SelectedItem, subtotalprod.ToString());

                    subtotal += subtotalprod;

                    subtotaltxt3.Text = subtotal.ToString();

                    combosstart();
                }



                if (row1.Cells[1].Value.ToString() == "DUO &")
                {

                    Duo duocustom = new Duo(this);

                    duocustom.preciolbl.Text = this.platofondodgv.CurrentRow.Cells[2].Value.ToString();
                    duocustom.platonombrelbl.Text = "(" + sv + ")" + this.platofondodgv.CurrentRow.Cells[1].Value.ToString();
                    duocustom.lbl1.Text = "(" + sv + ")" + this.platofondodgv.CurrentRow.Cells[1].Value.ToString() + "";
                    duocustom.unidadescmb.SelectedIndex = 0;
                    duocustom.ajicmb.SelectedIndex = 0;
                    duocustom.platoscmb.SelectedIndex = 0;


                    duocustom.ShowDialog();

                }
                if (row1.Cells[1].Value.ToString() == "TRIO &")
                {

                    Trio triocustom = new Trio(this);

                    triocustom.preciolbl.Text = this.platofondodgv.CurrentRow.Cells[2].Value.ToString();
                    triocustom.platonombrelbl.Text = "(" + sv + ")" + this.platofondodgv.CurrentRow.Cells[1].Value.ToString();
                    triocustom.lbl1.Text = "(" + sv + ")" + this.platofondodgv.CurrentRow.Cells[1].Value.ToString() + "";
                    triocustom.unidadescmb.SelectedIndex = 0;
                    triocustom.platoscmb.SelectedIndex = 0;
                    triocustom.ajicmb.SelectedIndex = 0;

                    triocustom.ShowDialog();

                }


            }
        }

        private void deletebtn3_Click_1(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvorden3.CurrentRow;
            dgvorden3.Rows.RemoveAt(dgvorden3.CurrentRow.Index);

            string precioag = row.Cells[3].Value.ToString();



            subtotal -= Convert.ToInt16(precioag);

            subtotaltxt3.Text = subtotal.ToString();
            if (dgvorden3.Rows.Count == 0)
            {

                deletebtn3.Enabled = false;
                actualordertxt3.Text = "";

            }
        }

        private void custombtn3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row1 = platofondodgv.CurrentRow;


            //dgvorden3.Rows.Add(row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb3.SelectedItem, subtotalprod.ToString());

            if (row1.Cells[1].Value.ToString() == "CEBICHE DE CONCHAS NEGRAS(SAB&DOM) &" ||
                row1.Cells[1].Value.ToString() == "CEBICHE DE LA PESCA DEL DIA &" ||
                row1.Cells[1].Value.ToString() == "CEBICHE DE LENGUADO &" ||
                row1.Cells[1].Value.ToString() == "CEBICHE DE MARISCOS &" ||
                row1.Cells[1].Value.ToString() == "CEBICHE MIXTO &"
                )
            {

                CebicheCustom cebform = new CebicheCustom(this);

                cebform.custompreciolblceb1.Text = this.platofondodgv.CurrentRow.Cells[2].Value.ToString();
                cebform.namecebichelbl1.Text = this.platofondodgv.CurrentRow.Cells[1].Value.ToString();
                cebform.lbl1.Text = this.platofondodgv.CurrentRow.Cells[1].Value.ToString() + "";
                cebform.customcmbceb1.SelectedIndex = 0;

                cebform.ShowDialog();

            }
            if (row1.Cells[1].Value.ToString() == "DUO &")
            {

                Duo duocustom = new Duo(this);

                duocustom.preciolbl.Text = this.platofondodgv.CurrentRow.Cells[2].Value.ToString();
                duocustom.platonombrelbl.Text = this.platofondodgv.CurrentRow.Cells[1].Value.ToString();
                duocustom.lbl1.Text = this.platofondodgv.CurrentRow.Cells[1].Value.ToString() + "";
                duocustom.unidadescmb.SelectedIndex = 0;
                duocustom.ajicmb.SelectedIndex = 0;
                duocustom.platoscmb.SelectedIndex = 0;

                duocustom.ShowDialog();

            }
            if (row1.Cells[1].Value.ToString() == "TRIO &")
            {

                Trio triocustom = new Trio(this);

                triocustom.preciolbl.Text = this.platofondodgv.CurrentRow.Cells[2].Value.ToString();
                triocustom.platonombrelbl.Text = this.platofondodgv.CurrentRow.Cells[1].Value.ToString();
                triocustom.lbl1.Text = this.platofondodgv.CurrentRow.Cells[1].Value.ToString() + "";
                triocustom.unidadescmb.SelectedIndex = 0;

                triocustom.ShowDialog();

            }
            if (row1.Cells[1].Value.ToString() == "RONDA CRIOLLISIMA &")
            {

                Ronda Rondacustom = new Ronda(this);

                Rondacustom.preciolbl.Text = this.platofondodgv.CurrentRow.Cells[2].Value.ToString();
                Rondacustom.platolbl.Text = this.platofondodgv.CurrentRow.Cells[1].Value.ToString();
                Rondacustom.lbl1.Text = this.platofondodgv.CurrentRow.Cells[1].Value.ToString() + "";
                Rondacustom.unidadescmb.SelectedIndex = 0;
                Rondacustom.rondacmb.SelectedIndex = 0;

                Rondacustom.ShowDialog();

            }



        }

        private void dgvorden3_SelectionChanged_1(object sender, EventArgs e)
        {

            if (dgvorden3.Rows.Count == 0)
            {

                deletebtn3.Enabled = false;
                actualordertxt3.Text = "";

            }
            else
            {
                DataGridViewRow row1 = dgvorden3.CurrentRow;


                actualordertxt3.Text = row1.Cells[0].Value.ToString();
            }

        }

        private void printbtn3_Click_1(object sender, EventArgs e)
        {

            DataGridViewRow row1 = platofondodgv.CurrentRow;
            DataGridViewRow row2 = dgvorden3.CurrentRow;

            string precio = row1.Cells[2].Value.ToString();
            string unidades = unidadescmb.Text;
            int precioprod = Convert.ToInt16(precio);
            int unidadesprod = Convert.ToInt16(unidades);
            int subtotalprod = precioprod * unidadesprod;

           


            string message = "¿Deseas imprimir la orden?";
            string caption = "Solicitud de impresion";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                if (dgvorden3.RowCount != 0)
                {

                   
                    order++;

                    SaveLastOrder();


                    PrintOrder(dgvorden3, coment3txt.Text, printer2);
                    if (currentmesa == 1) { mesa1btn.Normalcolor = Color.FromArgb(192, 32, 22); mozosnametxt1.Text = mozosname; mesa1btn.Text = mozosname; enableradio1(false); foreach (DataGridViewRow row3 in dgvorden3.Rows) { if (row3.IsNewRow) continue; dgvlistatotal.Rows.Add(row3.Cells[0].Value.ToString(), row3.Cells[1].Value.ToString(), row3.Cells[2].Value.ToString(), row3.Cells[3].Value.ToString(), order.ToString(), "PLATOS FONDO"); } }
                    //////////////////////////////////
                    /*//*/
                    dgvorden3.Rows.Clear();//////
                    //////////////////////////////////



                    if (currentmesa == 1) { mesatotal[1] += Convert.ToInt16(subtotaltxt3.Text); totaltxtmesa.Text = mesatotal[1].ToString(); ChamiTab.SelectedTab = mesacom; if (dgvlistatotal.SelectedCells.Count != 0) { deletebtntotal.Enabled = true; } }
                   

                    //limpiar cuadro subtotal, subtotalproductocadauno 0 y subtotalcuadro 0
                    subtotal -= subtotal;
                    subtotalprod -= subtotalprod;
                    subtotaltxt3.Text = "";
                    coment3txt.Text = "";
                    svcmb3.SelectedIndex = 0;
                    actualordertxt3.Text = "";
                }


                else
                {
                    MessageBox.Show("Debe agregar productos a la orden", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void cancelbtn3_Click_1(object sender, EventArgs e)
        {
            string message = "¿Deseas cancelar el pedido?";
            string caption = "Observación";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                subtotal -= subtotal;
                subtotaltxt3.Text = "";


                dgvorden3.Rows.Clear();
                if (currentmesa == 1) { ChamiTab.SelectedTab = mesacom; }
                
                coment3txt.Text = "";
                svcmb3.SelectedIndex = 0;


                actualordertxt3.Text = "";
            }
        }

        private void rightbtn4_Click_1(object sender, EventArgs e)
        {
            deletebtn4.Enabled = true;

            DataGridViewRow row1 = postredgv.CurrentRow;
            string sv = svcmb4.Text;

            //CAPTURA DATOS DE LA TABLA DE LISTA DE PRODUCTOS
            string precio = row1.Cells[2].Value.ToString();
            string unidades = unidadescmb4.Text;


            int precioprod = Convert.ToInt16(precio);
            int unidadesprod = Convert.ToInt16(unidades);

            subtotalprod = precioprod * unidadesprod;




            if (Convert.ToString(row1.Cells[1].Value) .Contains("&"))
            {
                string message = "¿Desea el postre calentado?";
                string caption = "Adicional";

 
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    /////AGREGA DATOS A LA TABLA DE ORDENES

                    dgvorden4.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "CALENTADO", row1.Cells[2].Value.ToString(), unidadescmb4.SelectedItem, subtotalprod.ToString());

                    subtotal += subtotalprod;

                    subtotaltxt4.Text = subtotal.ToString();

                    combosstart();




                }


                else
                {
                    dgvorden4.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString() + "FRIO", row1.Cells[2].Value.ToString(), unidadescmb4.SelectedItem, subtotalprod.ToString());

                    subtotal += subtotalprod;

                    subtotaltxt4.Text = subtotal.ToString();

                    combosstart();

                }


            }


            else
            {
                dgvorden4.Rows.Add("(" + sv + ")" + row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb4.SelectedItem, subtotalprod.ToString());

                subtotal += subtotalprod;

                subtotaltxt4.Text = subtotal.ToString();

                combosstart();
            }





        }

        private void deletebtn4_Click_1(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvorden4.CurrentRow;
            dgvorden4.Rows.RemoveAt(dgvorden4.CurrentRow.Index);

            string precioag = row.Cells[3].Value.ToString();



            subtotal -= Convert.ToInt16(precioag);

            subtotaltxt4.Text = subtotal.ToString();
            if (dgvorden4.Rows.Count == 0)
            {

                deletebtn4.Enabled = false;

            }
        }

        private void unidadescmb4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvorden4_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvorden4.Rows.Count == 0)
            {

                deletebtn4.Enabled = false;
                actualordertxt4.Text = "";

            }
            else
            {
                DataGridViewRow row1 = dgvorden4.CurrentRow;


                actualordertxt4.Text = row1.Cells[0].Value.ToString();
            }
        }

        private void printbtn4_Click_1(object sender, EventArgs e)
        {
            DataGridViewRow row1 = postredgv.CurrentRow;
            DataGridViewRow row2 = dgvorden4.CurrentRow;

            string precio = row1.Cells[2].Value.ToString();
            string unidades = unidadescmb.Text;
            int precioprod = Convert.ToInt16(precio);
            int unidadesprod = Convert.ToInt16(unidades);
            int subtotalprod = precioprod * unidadesprod;

           // MySqlConnection mysqlCon = new MySqlConnection(connectionString);


            string message = "¿Deseas imprimir la orden?";
            string caption = "Solicitud de impresion";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                if (dgvorden4.RowCount != 0)
                {


                    

                    order++;
                    SaveLastOrder();
                    PrintOrder(dgvorden4, coment4txt.Text, printer1);


                    if (currentmesa == 1) { mesa1btn.Normalcolor = Color.FromArgb(192, 32, 22); mozosnametxt1.Text = mozosname; mesa1btn.Text = mozosname; enableradio1(false); foreach (DataGridViewRow row3 in dgvorden4.Rows) { if (row3.IsNewRow) continue; dgvlistatotal.Rows.Add(row3.Cells[0].Value.ToString(), row3.Cells[1].Value.ToString(), row3.Cells[2].Value.ToString(), row3.Cells[3].Value.ToString(), order.ToString(), "POSTRES"); } }
                   
                    //////////////////////////////////
                    /*//*/
                    dgvorden4.Rows.Clear();//////
                    //////////////////////////////////



                    if (currentmesa == 1) { mesatotal[1] += Convert.ToInt16(subtotaltxt4.Text); totaltxtmesa.Text = mesatotal[1].ToString(); ChamiTab.SelectedTab = mesacom; if (dgvlistatotal.SelectedCells.Count != 0) { deletebtntotal.Enabled = true; } }
               


                    //limpiar cuadro subtotal, subtotalproductocadauno 0 y subtotalcuadro 0
                    subtotal -= subtotal;
                    subtotalprod -= subtotalprod;
                    subtotaltxt4.Text = "";
                    coment4txt.Text = "";
                    svcmb4.SelectedIndex = 0;
                    actualordertxt4.Text = "";

                }


                else
                {
                    MessageBox.Show("Debe agregar productos a la orden", "Observación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void cancelbtn4_Click_1(object sender, EventArgs e)
        {
            string message = "¿Deseas cancelar el pedido?";
            string caption = "Observación";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                subtotal -= subtotal;
                subtotaltxt4.Text = "";


                dgvorden4.Rows.Clear();
                if (currentmesa == 1) { ChamiTab.SelectedTab = mesacom; }
               

                coment4txt.Text = "";
                svcmb4.SelectedIndex = 0;
                actualordertxt4.Text = "";
            }
        }


        private void tabPage4_Click(object sender, EventArgs e){}
        private void tabPage3_Click(object sender, EventArgs e) { }
        
        private void Form1_Load(object sender, EventArgs e) { }



        private void mesa1btn_Click(object sender, EventArgs e)
        {
            currentmesa = 1;
            namemesatxt.Text = "Mesa            1";
            ChamiTab.SelectedTab = mesacom;
            





        }

        private void mesa2btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 2;
            namemesatxt.Text = "Mesa            2";

            ChamiTab.SelectedTab = mesacom;
           


        }

        private void mesa3btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 3;
            namemesatxt.Text = "Mesa            3";
            ChamiTab.SelectedTab = mesacom;
            

           
        }

        private void mesa4btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 4;
            namemesatxt.Text = "Mesa            4";
            ChamiTab.SelectedTab = mesacom;
            
          
        }

        private void mesa5btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 5;
            namemesatxt.Text = "Mesa            5";
            ChamiTab.SelectedTab = mesacom;
            
          
        }

        private void mesa6btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 6;
            namemesatxt.Text = "Mesa            6";
            ChamiTab.SelectedTab = mesacom;
            
           
        }

        private void mesa7btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 7;
            namemesatxt.Text = "Mesa            7";
            ChamiTab.SelectedTab = mesacom;
            
          
        }

        private void mesa8btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 8;
            namemesatxt.Text = "Mesa            8";
            ChamiTab.SelectedTab = mesacom;
            
           
        }

        private void mesa9btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 9;
            namemesatxt.Text = "Mesa            9";
            ChamiTab.SelectedTab = mesacom;
            
        }

        private void mesa10btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 10;
            namemesatxt.Text = "Mesa            10";
            ChamiTab.SelectedTab = mesacom;
            
          
        }

        private void mesa11btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 11;
            namemesatxt.Text = "Mesa            11";
            ChamiTab.SelectedTab = mesacom;
            
           
        }

        private void mesa12btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 12;
            namemesatxt.Text = "Mesa            12";
            ChamiTab.SelectedTab = mesacom;
            
           
        }

        private void mesa13btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 13;
            namemesatxt.Text = "Mesa            13";
            ChamiTab.SelectedTab = mesacom;
            
            
        }

        private void mesa14btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 14;
            namemesatxt.Text = "Mesa            14";
            ChamiTab.SelectedTab = mesacom;
           
            
        }

        private void mesa15btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 15;
            namemesatxt.Text = "Mesa            15";
            ChamiTab.SelectedTab = mesacom;
            
           
        }

        private void mesa16btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 16;
            namemesatxt.Text = "Mesa            16";
            ChamiTab.SelectedTab = mesacom;
            

        }

        private void mesa17btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 17;
            namemesatxt.Text = "Mesa            17";
            ChamiTab.SelectedTab = mesacom;
            
           
        }

        private void mesa18btn_Click_1(object sender, EventArgs e)
        {
            currentmesa = 18;
            namemesatxt.Text = "Mesa            18";
            ChamiTab.SelectedTab = mesacom;
           
            
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            ChamiTab.SelectedTab = InicioTab;
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();




        }


        private void ingresarbtn_Click(object sender, EventArgs e)
        {
            ActiveIcons(true);

            ChamiTab.SelectedTab = Mesastab;

            printer1 = new ProductModel().GetLastPrinter1();
            printer2 = new ProductModel().GetLastPrinter2();
            printer3 = new ProductModel().GetLastPrinter3();





            

        }
        
        private void dgvlista1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow row1 = bebidasdgv.CurrentRow;


            //dgvorden3.Rows.Add(row1.Cells[1].Value.ToString(), row1.Cells[2].Value.ToString(), unidadescmb3.SelectedItem, subtotalprod.ToString())

            try
            {

                if (row1.Cells[1].Value.ToString().Contains("#"))


                    azucarcmb.Enabled = true;

                else

                    azucarcmb.Enabled = false;




                if (row1.Cells[1].Value.ToString().Contains("@"))


                    helado = true;



                else
                    helado = false;




            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "Obs.", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void configbtn_Click(object sender, EventArgs e)
        {
            Configuracion config = new Configuracion(this);

            config.ShowDialog();

        }

        private void icebtn_Click(object sender, EventArgs e)
        {

            string message = "¿VASO GRANDE DE HIELO?";
            string caption = "Pregunta";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {


                dgvorden.Rows.Add("VASO GRANDE DE HIELO", "0", "1", "0");
                deletebtn.Enabled = true;

            }
            else
            {
                dgvorden.Rows.Add("VASO NORMAL DE HIELO", "0", "1", "0");
                deletebtn.Enabled = true;
            }




        }

        private void coment4btn_Click(object sender, EventArgs e)
        {
            coment3 coment3 = new coment3(this);
            string c3;

            c3 = coment3txt.Text;

            coment3.c3txt.Text = c3;


            coment3.ShowDialog();

        }

        private void coment1btn_Click(object sender, EventArgs e)
        {
            coment1 coment1 = new coment1(this);
            string c1;

            c1 = coment1txt.Text;

            coment1.c1txt.Text = c1;


            coment1.ShowDialog();
        }

        private void coment2btn_Click(object sender, EventArgs e)
        {
            coment2 coment2 = new coment2(this);
            string c2;

            c2 = coment2txt.Text;

            coment2.c2txt.Text = c2;


            coment2.ShowDialog();
        }

        private void coment4btn_Click_1(object sender, EventArgs e)
        {
            coment4 coment4 = new coment4(this);
            string c4;

            c4 = coment4txt.Text;

            coment4.c4txt.Text = c4;


            coment4.ShowDialog();
        }

        private void reportbtn_Click(object sender, EventArgs e)
        {
            Report rep = new Report(this);

            rep.ShowDialog();
        }

        private void dgvorden_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!((DataGridViewRow)(dgvorden.Rows[e.RowIndex])).Selected)
                {
                    dgvorden.ClearSelection();
                    ((DataGridViewRow)dgvorden.Rows[e.RowIndex]).Selected = true;
                    if (dgvorden.SelectedRows.Count > 0)
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvorden.Rows[e.RowIndex];

                    }
                }
            }
        }

        private void criollobtn_Click(object sender, EventArgs e)
        {
            GridFillCriollo();
        }

        private void marinobtn_Click(object sender, EventArgs e)
        {
            GridFillMarino();
        }

        private void allbtn_Click(object sender, EventArgs e)
        {
            GridFillPlatoFondo();
        }

        private void Pcombbtn_Click(object sender, EventArgs e)
        {
            GridFillComb();

        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Inicio_FormClosed(object sender, FormClosedEventArgs e)
        {
            

        }

        private void mesa19btn_Click(object sender, EventArgs e)
        {
            currentmesa = 19;
            namemesatxt.Text = "Mesa            1(2P)";
            ChamiTab.SelectedTab = mesacom;

         


        }

        private void mesa20btn_Click(object sender, EventArgs e)
        {
            currentmesa = 20;
            namemesatxt.Text = "Mesa            2(2P)";
            ChamiTab.SelectedTab = mesacom;




        }

        private void mesa21btn_Click(object sender, EventArgs e)
        {
            currentmesa = 21;
            namemesatxt.Text = "Mesa            3(2P)";
            ChamiTab.SelectedTab = mesacom;




        }

        private void mesa22btn_Click(object sender, EventArgs e)
        {
            currentmesa = 22;
            namemesatxt.Text = "Mesa            4(2P)";
            ChamiTab.SelectedTab = mesacom;




        }

        private void mesa23btn_Click(object sender, EventArgs e)
        {
            currentmesa = 23;
            namemesatxt.Text = "Mesa            5(2P)";
            ChamiTab.SelectedTab = mesacom;


        }

        private void mesa24btn_Click(object sender, EventArgs e)
        {
            currentmesa = 24;
            namemesatxt.Text = "Mesa            6(2P)";
            ChamiTab.SelectedTab = mesacom;




        }

        private void mesa25btn_Click(object sender, EventArgs e)
        {
            currentmesa = 25;
            namemesatxt.Text = "Mesa            7(2P)";
            ChamiTab.SelectedTab = mesacom;



        }

        private void mesa26btn_Click(object sender, EventArgs e)
        {
            currentmesa = 26;
            namemesatxt.Text = "Mesa            8(2P)";
            ChamiTab.SelectedTab = mesacom;



        }

        private void mesa27btn_Click(object sender, EventArgs e)
        {
            currentmesa = 27;
            namemesatxt.Text = "Mesa            9(2P)";
            ChamiTab.SelectedTab = mesacom;


        }

        private void mesa28btn_Click(object sender, EventArgs e)
        {
            currentmesa = 28;
            namemesatxt.Text = "Mesa            10(2P)";
            ChamiTab.SelectedTab = mesacom;


        }

        private void mesa29btn_Click(object sender, EventArgs e)
        {
            currentmesa = 29;
            namemesatxt.Text = "Mesa            11(2P)";
            ChamiTab.SelectedTab = mesacom;




        }

        private void mesa30btn_Click(object sender, EventArgs e)
        {
            currentmesa = 30;
            namemesatxt.Text = "Mesa            12(2P)";
            ChamiTab.SelectedTab = mesacom;



        }

        private void mesa31btn_Click(object sender, EventArgs e)
        {
            currentmesa = 31;
            namemesatxt.Text = "Mesa            13(2P)";
            ChamiTab.SelectedTab = mesacom;




        }

        private void mesa32btn_Click(object sender, EventArgs e)
        {
            currentmesa = 32;
            namemesatxt.Text = "Mesa            14(2P)";
            ChamiTab.SelectedTab = mesacom;




        }

        private void mesa33btn_Click(object sender, EventArgs e)
        {
            currentmesa = 33;
            namemesatxt.Text = "Mesa            15(2P)";
            ChamiTab.SelectedTab = mesacom;




        }

        private void secfloorbtn_Click(object sender, EventArgs e)
        {
            ChamiTab.SelectedTab = Mesas2tab;
        }

        private void firstfloorbtn_Click(object sender, EventArgs e)
        {
            ChamiTab.SelectedTab = Mesastab;
        }



        private void elmradio_CheckedChanged(object sender, EventArgs e)
        {
            mozosnametxt1.Text = "ELMER";
        }

        
    }
}

