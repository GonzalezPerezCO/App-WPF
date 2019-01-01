﻿using Deportes_WPF.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para ventanaPrestamosCas.xaml
    /// </summary>
    public partial class ventanaPrestamosCas : Window
    {

        private Entorno entorno;
        private static ventanaPrestamosCas instance = null;
        private static List<string> lista = new List<string>();

        private ventanaPrestamosCas()
        {
            InitializeComponent();
            entorno = Entorno.GetInstance();

            codigo.Focus();

            lista = separarIds(entorno.disponiblesCasilleros());
            actualizarListaDisp();
        }        

        public static ventanaPrestamosCas GetInstance()
        {
            if (instance == null)
                instance = new ventanaPrestamosCas();

            return instance;
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private List<string> separarIds(List<string> lista)
        {
            // a,b,c,...,x

            List<string> result = new List<string>();

            string[] separadas;
            separadas = lista[0].Split(',');

            foreach (var item in separadas)
            {
                result.Add(item);
                Debug.WriteLine("<< id a lista: " + item);
            }

            return result;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            if (codigo.Text == "")
            {
                MessageBox.Show("Ingrese un número de carnet!");
            }
            else
            {
                // Lista: nombre, codigo, casillero, disponible{0:no, 1:si}, entrada, salida
                int codigoEs = Convert.ToInt32(codigo.Text);
                int codigoCas = Convert.ToInt32(cmbox.SelectedValue);
                List<string> busCod = entorno.buscarCasilleroEstu(codigoEs);
                bool estudiante = entorno.buscarEstudiante( Convert.ToUInt32(codigoEs), ""); // false: no existe en testudiantes

                if (busCod.Count != 0)
                {
                    if (cmbox.SelectedItem != null)
                    {
                        MessageBox.Show("El estudiante ya tiene un casillero asignado!");
                    }
                    else
                    {
                        MessageBox.Show("Estudiante encontrado! Casillero liberado.");
                        Debug.WriteLine("<<< Prestamo liberado: codigoEst = " + codigoEs + ".");
                        entorno.quitarEstudianteCasillero(codigoEs);
                        actualizarListaDisp();
                    }


                }
                else if (!estudiante)
                {
                    MessageBox.Show("Estudiante no encontrado!");
                }
                else if (estudiante)
                {
                    if (cmbox.Text == "")
                    {
                        MessageBox.Show("Agregue un casillero primero.");
                    }
                    else
                    {
                        entorno.agregarEstudianteCasillero(codigoCas, codigoEs);
                        Debug.WriteLine("<<< Prestamo: id_c = " + codigoCas + ", codigoEst = " + codigoEs + ".");
                        MessageBox.Show("Casillero asignado!");
                        actualizarListaDisp();
                    }
                }
            }

            limpiar();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            ocultar();
        }

        private void limpiar() {
            codigo.Text = "";
            cmbox.SelectedValue = null;
            //this.Hide();
        }

        private void ocultar() {
            Casilleros casilleros = Casilleros.GetInstance();
            casilleros.Show();
            this.Hide();
        }

        private void actualizarListaDisp()
        {
            lista = separarIds(entorno.disponiblesCasilleros());
            cmbox.ItemsSource = lista;
            ocultar();
        }        
    }
}
