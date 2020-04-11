using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCWA.Templates.ParticipantModule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParticipantsTemplate : ContentView
    {
        public Action<int, int> RequestParticipants;
        public int numberToSkipParticipants = 0;
        public const int numberOfParticipants = 50;
        private bool isAllowLoadParticipants = true;
        private StackLayout mainStackLayout = new StackLayout();
        
        public void SetParticipants(string participantsJSON)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<ParticipantsTable> participantsTable =
                JsonSerializer.Deserialize<List<ParticipantsTable>>(participantsJSON);

            foreach (var participantTable in participantsTable)
            {
                ParticipantTemplate participant = new ParticipantTemplate();
                participant.ParticipantsTable = participantTable;
                mainStackLayout.Children.Add(participant);
            }

            //Lock loading if there are no more participants 
            if (participantsTable.Count != 0)
            {
                isAllowLoadParticipants = true;
            }
            sw.Stop();
            Console.WriteLine("SetParticipants = " + sw.ElapsedTicks);
        }        

        public void ScrolledListener(object sender, EventArgs args)
        {
            if (!isAllowLoadParticipants)
            {
                return;
            }
            var scrollView = (ScrollView)sender;

            bool isNeededMoreParticipants =
                scrollView.ScrollY >
                (int)(scrollView.ContentSize.Height - scrollView.ContentSize.Height / 2);

            if (isNeededMoreParticipants)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                RequestParticipants(numberToSkipParticipants, numberOfParticipants);
                sw.Stop();
                Console.WriteLine("RequestParticipants = " + sw.ElapsedTicks);
                numberToSkipParticipants += numberOfParticipants;
                isAllowLoadParticipants = false;
            }
        }

        public ParticipantsTemplate()
        {
            InitializeComponent();
            MainScrollView.Content = mainStackLayout;
            MainScrollView.Scrolled += ScrolledListener;
        }

        public void Start()
        {
            RequestParticipants(0, numberOfParticipants);
        }
    }
}