﻿using AlphaSport.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AlphaSport.Vista
{
    /// <summary>
    /// Lógica de interacción para ventanaAdminImpl.xaml
    /// </summary>
    public partial class VentanaAdminImpl : Window
    {
        private Entorno entorno;
        private static VentanaAdminImpl instance = null;

        private bool nuevoImpl; // si es o no un nuevo prestamo
        private UInt64 codigo;

        public VentanaAdminImpl()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();

            ActualizarCmbx();

            Limpiar();
        }

        public static VentanaAdminImpl GetInstance()
        {
            if (instance == null)
                instance = new VentanaAdminImpl();

            return instance;
        }

        private void Limpiar()
        {
            text1.Text = "";

            nuevoImpl = false;
            codigo = 0;

            cmbox1.SelectedValue = null;

            text1.Focus();
        }

        private void ActualizarCmbx()
        {
            List<string> lista = entorno.Implementos_disponiblesSigla();

            if (lista.Count != 0 && (lista[0] == entorno.INFOSQL || lista[0] == entorno.INFOSQL)) // cuando si esta pero no tiene casillero
            {
                MessageBox.Show(lista[1]);
            }
            else
            {
                cmbox1.ItemsSource = lista;
            }
        }

        private bool CapturarDatos()
        {
            bool result = false;

            if (text1.Text == "" || !UInt64.TryParse(text1.Text, out UInt64 abc))
            {
                MessageBox.Show("Ingrese un número de carnet valido!");
            }
            else
            {
                ActualizarCmbx(); 

                // Lista: nombre, codigo, casillero, disponible{0:no, 1:si}, entrada, salida
                codigo = Convert.ToUInt64(text1.Text);
                result = true;
            }

            return result;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            bool result = CapturarDatos();
          

            if (result)
            {
                MessageBox.Show("ok");
            }
            else
            {
                MessageBox.Show("error");
            }
            //Limpiar();
            //Ocultar();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            Ocultar();
        }

        private void Ocultar()
        {
            TablaImplementos impl = TablaImplementos.GetInstance();
            impl.Show();
            this.Hide();
        }
    }
}
