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

        public VentanaAdminImpl()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();
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
            text1.Focus();
        }


        private void Btn1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            TablaImplementos impl = TablaImplementos.GetInstance();
            impl.Show();
            this.Hide();
        }
    }
}
