using System;
using System.Diagnostics;
using TCWA.Core;
using TCWA.Core.Defines;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCWA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Header : ContentView
    {
        Image arrowLeft;
        public Header()
        {
            InitializeComponent();

            arrowLeft = this.FindByName<Image>("ArrowLeft");

            arrowLeft.Source = ImageSource.FromResource(
            "TCWA.Resources.Arrow.png"
            );
        }

        void OnImageClicked_GoBack(object sender, EventArgs args)
        {
            UIManagement.singleton.GoBack();
        }        
    }
}