using SkateboardNeverDie.Models;
using SkateboardNeverDie.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkateboardNeverDie.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Skater Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}