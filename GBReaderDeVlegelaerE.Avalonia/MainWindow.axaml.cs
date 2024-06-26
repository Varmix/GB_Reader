using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderDeVlegelaerE.Domains;
using GBReaderDeVlegelaerE.Presenters;
using GBReaderDeVlegelaerE.Presenters.routes;
using Microsoft.CodeAnalysis.Operations;

namespace GBReaderDeVlegelaerE.Avalonia
{
    public partial class MainWindow : Window, IBrowseToViews
    {
        
        private readonly IDictionary<string, UserControl> _pages = new Dictionary<string, UserControl>();

        public MainWindow()
        {
            InitializeComponent();
        }


        internal void RegisterPage(string pageName, UserControl page)
        {
            _pages[pageName] = page;
            if(Content == null)
            {
                Content = page;
            }
        }

        public void GoTo(string pageName)
        {
            Content = _pages[pageName];
        }

        public void displayErrorMessage(string impossibleDÉtablirUneConnexionAvecLaBaseDeDonnées)
        {
            ErrorMessage.IsVisible = true;
            ErrorMessage.Text = impossibleDÉtablirUneConnexionAvecLaBaseDeDonnées;
        }
    }

    }