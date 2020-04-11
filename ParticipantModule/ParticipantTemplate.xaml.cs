using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCWA.Templates.ParticipantModule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParticipantTemplate : ContentView
    {
        public class OnParticipantChecked
        {
            public OnParticipantChecked(ParticipantsTable participantsTable)
            {
                this.participantsTable = participantsTable;
            }
            public ParticipantsTable participantsTable;
        }

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        public void OnCheckedChanged_MainCheckBox(object sender, CheckedChangedEventArgs e)
        => CheckedChanged.Invoke(sender, e);

        public static readonly BindableProperty ParticipantsTableBindableProperty =
            BindableProperty.Create(
                "ParticipantsTable",
                typeof(ParticipantsTable),
                typeof(ParticipantTemplate),
                null);

        public ParticipantsTable ParticipantsTable
        {
            get { return (ParticipantsTable)GetValue(ParticipantsTableBindableProperty); }
            set { SetValue(ParticipantsTableBindableProperty, value); }
        }

        public class toParticipantsTable : IValueConverter
        {
            public object Convert(
                object value, 
                Type targetType, 
                object parameter,
                CultureInfo culture)
            {
                return
                    value?.
                    GetType()?.
                    GetProperty(parameter?.ToString())?.
                    GetValue(value)?.
                    ToString();
            }

            public object ConvertBack(
               object value, 
               Type targetType, 
               object parameter,
               CultureInfo culture)
            {
                throw new NotImplementedException();
            }

        }

        public ParticipantTemplate()
        {
            InitializeComponent();

            LabelFirstName.SetBinding(
                Label.TextProperty,
                new Binding(
                    "ParticipantsTable", 
                    converter: new toParticipantsTable(), 
                    converterParameter: "FirstName_",
                    source: this));
            LabelLastName.SetBinding(
                Label.TextProperty,
                new Binding(
                    "ParticipantsTable",
                    converter: new toParticipantsTable(),
                    converterParameter: "LastName_",
                    source: this));
            LabelPhoneNumber.SetBinding(
                Label.TextProperty,
                new Binding(
                    "ParticipantsTable",
                    converter: new toParticipantsTable(),
                    converterParameter: "PhoneNumber_",
                    source: this));
        }
    }
}