﻿using System;
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

namespace SisEstoque
{
    /// <summary>
    /// Lógica interna para InfoProduto.xaml
    /// </summary>
    public partial class InfoProduto : Window
    {
        public InfoProduto()
        {
            InitializeComponent();
        }
        public void bnt_Salvar(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
